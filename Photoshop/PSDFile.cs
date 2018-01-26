﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Photoshop
{
    public class PsdFile : IDisposable
    {
        #region Fields

        private bool disposed;

        private bool m_absoluteAlpha;

        /// <summary>If ColorMode is ColorModes.Indexed, the following 768 bytes will contain 
        /// a 256 color palette. If the ColorMode is ColorModes.Duotone, the data following 
        /// presumably consists of screen parameters and other related information.</summary>
        public byte[] ColorModeData = new byte[0];

        /// <summary>Masking data for the PSD</summary>
        byte[] GlobalLayerMaskData = new byte[0];

        /// <summary>The raw image data from the file, 
        /// seperated by the channels.</summary>
        public byte[][] m_imageData;

        private ColorModes m_colorMode;

        private Image compositImage;

        private ImageCompression m_imageCompression;

        private int m_columns;

        private int m_depth;

        private int m_rows;

        private List<ImageResource> m_imageResources = new List<ImageResource>();
        List<Layer> m_layers = new List<Layer>();

        private short m_channels;

        /// <summary>Always equal to 1.</summary>
        private short m_version = 1;

        #endregion Fields

        #region Enums

        public enum ColorModes
        {
            Bitmap = 0, Grayscale = 1, Indexed = 2, RGB = 3, CMYK = 4, Multichannel = 7, Duotone = 8, Lab = 9
        };

        #endregion Enums

        #region Constructors

        public PsdFile(string filename)
        {
            Load(filename);
            compositImage = ImageDecoder.DecodeImage(this);
        }

        public PsdFile(Stream stream)
        {
            Load(stream);
            compositImage = ImageDecoder.DecodeImage(this);
        }

        ~PsdFile()
        {
            Dispose(false);
        }

        public PsdFile() { }

        #endregion Constructors

        #region Properties

        public bool AbsoluteAlpha
        {
            get { return m_absoluteAlpha; }
            set { m_absoluteAlpha = value; }
        }

        public byte[][] ImageData
        {
            get { return m_imageData; }
            set { m_imageData = value; }
        }

        /// <summary>The color mode of the file.</summary>
        public ColorModes ColorMode
        {
            get { return m_colorMode; }
            set { m_colorMode = value; }
        }

        public Image CompositImage
        {
            get { return compositImage; }
            set { compositImage = value; }
        }

        public ImageCompression ImageCompression
        {
            get { return m_imageCompression; }
            set { m_imageCompression = value; }
        }

        /// <summary>The width of the image in pixels.</summary>
        public int Columns
        {
            get { return m_columns; }
            set
            {
                if (value < 0 || value > 30000)
                    throw new ArgumentException("Supported range is 1 to 30000.");
                m_columns = value;
            }
        }

        /// <summary>The number of bits per channel. 
        /// Supported values are 1, 8, and 16.</summary>
        public int Depth
        {
            get { return m_depth; }
            set
            {
                if (value == 1 || value == 8 || value == 16)
                    m_depth = value;
                else
                    throw new ArgumentException("Supported values are 1, 8, and 16.");
            }
        }

        /// <summary>The height of the image in pixels.</summary>
        public int Rows
        {
            get { return m_rows; }
            set
            {
                if (value < 0 || value > 30000)
                    throw new ArgumentException("Supported range is 1 to 30000.");
                m_rows = value;
            }
        }

        /// <summary>The Image resource blocks for the file.</summary>
        public List<ImageResource> ImageResources
        {
            get { return m_imageResources; }
        }

        public List<Layer> Layers
        {
            get { return m_layers; }
        }

        public ResolutionInfo Resolution
        {
            get
            {
                return (ResolutionInfo)m_imageResources.Find((ImageResource x) => x.ID == (int)ResourceIDs.ResolutionInfo);
            }

            set
            {
                ImageResource oldValue = m_imageResources.Find((ImageResource x) => x.ID == (int)ResourceIDs.ResolutionInfo);
                if (oldValue != null)
                    m_imageResources.Remove(oldValue);

                m_imageResources.Add(value);
            }
        }

        /// <summary>The number of channels in the image, including 
        /// any alpha channels. Supported range is 1 to 24.</summary>
        public short Channels
        {
            get { return m_channels; }
            set
            {
                if (value < 1 || value > 24)
                    throw new ArgumentException("Supported range is 1 to 24");
                m_channels = value;
            }
        }

        public short Version
        {
            get { return m_version; }
        }

        #endregion Properties

        #region Methods

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Load(string filename)
        {
            using (var stream = new MemoryStream(File.ReadAllBytes(filename)))
            {
                Load(stream);
            }
        }

        public void Load(Stream stream)
        {
            //binary reverse reader reads data types in big-endian format.
            BinaryReverseReader reader = new BinaryReverseReader(stream);

            //The headers area is used to check for a valid PSD file
            Debug.WriteLine("LoadHeader started at " + reader.BaseStream.Position.ToString());

            string signature = new string(reader.ReadChars(4));
            if (signature != "8BPS")
                throw new IOException("Bad or invalid file stream supplied");

            //get the version number, should be 1 always
            if ((m_version = reader.ReadInt16()) != 1)
                throw new IOException("Invalid version number supplied");

            //get rid of the 6 bytes reserverd in PSD format
            reader.BaseStream.Position += 6;

            //get the rest of the information from the PSD file.
            //Everytime ReadInt16() is called, it reads 2 bytes.
            //Everytime ReadInt32() is called, it reads 4 bytes.
            m_channels = reader.ReadInt16();
            m_rows = reader.ReadInt32();
            m_columns = reader.ReadInt32();
            m_depth = reader.ReadInt16();
            m_colorMode = (ColorModes)reader.ReadInt16();

            //by end of headers, the reader has read 26 bytes into the file.

            /// <summary>
            /// If ColorMode is ColorModes.Indexed, the following 768 bytes will contain
            /// a 256-color palette. If the ColorMode is ColorModes.Duotone, the data
            /// following presumably consists of screen parameters and other related information.
            /// Unfortunately, it is intentionally not documented by Adobe, and non-Photoshop
            /// readers are advised to treat duotone images as gray-scale images.
            /// </summary>
            Debug.WriteLine("LoadColorModeData started at " + reader.BaseStream.Position.ToString());

            uint paletteLength = reader.ReadUInt32(); //readUint32() advances the reader 4 bytes.
            if (paletteLength > 0)
            {
                ColorModeData = reader.ReadBytes((int)paletteLength);
            }

            //This part takes extensive use of classes that I didn't write therefore
            //I can't document much on what they do.

            Debug.WriteLine("LoadingImageResources started at " + reader.BaseStream.Position.ToString());

            m_imageResources.Clear();

            uint imgResLength = reader.ReadUInt32();
            if (imgResLength > 0)
            {
                long startPosition = reader.BaseStream.Position;

                while ((reader.BaseStream.Position - startPosition) < imgResLength)
                {
                    ImageResource imgRes = new ImageResource(reader);

                    ResourceIDs resID = (ResourceIDs)imgRes.ID;
                    switch (resID)
                    {
                        case ResourceIDs.ResolutionInfo:
                            imgRes = new ResolutionInfo(imgRes);
                            break;
                        case ResourceIDs.Thumbnail1:
                        case ResourceIDs.Thumbnail2:
                            imgRes = new Thumbnail(imgRes);
                            break;
                        case ResourceIDs.AlphaChannelNames:
                            imgRes = new AlphaChannels(imgRes);
                            break;
                    }

                    m_imageResources.Add(imgRes);
                }
                // make sure we are not on a wrong offset, so set the stream position
                // manually
                reader.BaseStream.Position = startPosition + imgResLength;

                //We are gonna load up all the layers and masking of the PSD now.
                Debug.WriteLine("LoadLayerAndMaskInfo - Part1 started at " + reader.BaseStream.Position.ToString());
                uint layersAndMaskLength = reader.ReadUInt32();

                if (layersAndMaskLength > 0)
                {
                    //new start position
                    startPosition = reader.BaseStream.Position;

                    //Lets start by loading up all the layers
                    LoadLayers(reader);
                    //we are done the layers, load up the masks
                    LoadGlobalLayerMask(reader);

                    // make sure we are not on a wrong offset, so set the stream position
                    // manually
                    reader.BaseStream.Position = startPosition + layersAndMaskLength;

                    //we have loaded up all the information from the PSD file
                    //into variables we can use later on.

                    //lets finish loading the raw data that defines the image
                    //in the picture.

                    Debug.WriteLine("LoadImage started at " + reader.BaseStream.Position.ToString());

                    m_imageCompression = (ImageCompression)reader.ReadInt16();

                    m_imageData = new byte[m_channels][];

                    //---------------------------------------------------------------

                    if (m_imageCompression == ImageCompression.Rle)
                    {
                        // The RLE-compressed data is proceeded by a 2-byte data count for each row in the data,
                        // which we're going to just skip.
                        reader.BaseStream.Position += m_rows * m_channels * 2;
                    }

                    //---------------------------------------------------------------

                    int bytesPerRow = 0;

                    switch (m_depth)
                    {
                        case 1:
                            bytesPerRow = m_columns;//NOT Sure
                            break;
                        case 8:
                            bytesPerRow = m_columns;
                            break;
                        case 16:
                            bytesPerRow = m_columns * 2;
                            break;
                    }

                    //---------------------------------------------------------------

                    for (int ch = 0; ch < m_channels; ch++)
                    {
                        m_imageData[ch] = new byte[m_rows * bytesPerRow];

                        switch (m_imageCompression)
                        {
                            case ImageCompression.Raw:
                                reader.Read(m_imageData[ch], 0, m_imageData[ch].Length);
                                break;
                            case ImageCompression.Rle:
                                {
                                    for (int i = 0; i < m_rows; i++)
                                    {
                                        int rowIndex = i * m_columns;
                                        RleHelper.DecodedRow(reader.BaseStream, m_imageData[ch], rowIndex, bytesPerRow);
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (compositImage != null)
                    {
                        compositImage.Dispose();
                        compositImage = null;
                    }
                }

                disposed = true;
            }
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>Load up the masking information of the supplied PSD.</summary>        
        private void LoadGlobalLayerMask(BinaryReverseReader reader)
        {
            Debug.WriteLine("LoadGlobalLayerMask started at " + reader.BaseStream.Position.ToString());

            uint maskLength = reader.ReadUInt32();

            if (maskLength <= 0)
                return;

            GlobalLayerMaskData = reader.ReadBytes((int)maskLength);
        }

        /// <summary>Loads up the Layers of the supplied PSD file.</summary>      
        private void LoadLayers(BinaryReverseReader reader)
        {
            Debug.WriteLine("LoadLayers started at " + reader.BaseStream.Position.ToString());

            uint layersInfoSectionLength = reader.ReadUInt32();

            if (layersInfoSectionLength <= 0)
                return;

            long startPosition = reader.BaseStream.Position;

            short numberOfLayers = reader.ReadInt16();

            // If <0, then number of layers is absolute value,
            // and the first alpha channel contains the transparency data for
            // the merged result.
            if (numberOfLayers < 0)
            {
                AbsoluteAlpha = true;
                numberOfLayers = Math.Abs(numberOfLayers);
            }

            m_layers.Clear();

            if (numberOfLayers == 0)
                return;

            for (int i = 0; i < numberOfLayers; i++)
            {
                m_layers.Add(new Layer(reader, this));
            }

            foreach (Layer layer in m_layers)
            {
                foreach (Layer.Channel channel in layer.Channels)
                {
                    if (channel.ID != -2)
                        channel.LoadPixelData(reader);
                }
                layer.MaskData.LoadPixelData(reader);
            }

            if (reader.BaseStream.Position % 2 == 1)
                reader.ReadByte();

            // make sure we are not on a wrong offset, so set the stream position
            // manually
            reader.BaseStream.Position = startPosition + layersInfoSectionLength;
        }

        #endregion Private Methods

        #endregion Methods
    }

    public enum ImageCompression
    {
        /// <summary>Raw data</summary>
        Raw = 0,

        /// <summary>RLE compressed</summary>
        Rle = 1,

        /// <summary>ZIP without prediction.</summary>
        /// <remarks>
        /// Not supported. Loading will result in an image 
        /// where all channels are set to zero.</remarks>
        Zip = 2,

        /// <summary>ZIP with prediction.</summary>
        /// <remarks>
        /// Not supported. Loading will result in an image 
        /// where all channels are set to zero.</remarks>
        ZipPrediction = 3
    }

    class RleHelper
    {
        #region Methods

        #region Public Methods

        public static int EncodedRow(Stream stream, byte[] imgData, int startIdx, int columns)
        {
            long startPosition = stream.Position;

            RlePacketStateMachine machine = new RlePacketStateMachine(stream);

            for (int x = 0; x < columns; ++x)
                machine.Push(imgData[x + startIdx]);

            machine.Flush();

            return (int)(stream.Position - startPosition);
        }

        public static void DecodedRow(Stream stream, byte[] imgData, int startIdx, int columns)
        {
            int count = 0;
            while (count < columns)
            {
                byte byteValue = (byte)stream.ReadByte();

                int len = (int)byteValue;
                if (len < 128)
                {
                    len++;
                    while (len != 0 && (startIdx + count) < imgData.Length)
                    {
                        byteValue = (byte)stream.ReadByte();

                        imgData[startIdx + count] = byteValue;
                        count++;
                        len--;
                    }
                }
                else if (len > 128)
                {
                    // Next -len+1 bytes in the dest are replicated from next source byte.
                    // (Interpret len as a negative 8-bit int.)
                    len ^= 0x0FF;
                    len += 2;
                    byteValue = (byte)stream.ReadByte();

                    while (len != 0 && (startIdx + count) < imgData.Length)
                    {
                        imgData[startIdx + count] = byteValue;
                        count++;
                        len--;
                    }
                }
                //else if (128 == len)
                //{
                //    // Do nothing
                //}
            }
        }

        #endregion Public Methods

        #endregion Methods

        #region Nested Classes

        private class RlePacketStateMachine
        {
            #region Fields

            private bool m_rlePacket = false;

            private byte[] m_packetValues = new byte[128];

            private int packetLength;

            private Stream m_stream;

            #endregion Fields

            #region Constructors

            internal RlePacketStateMachine(Stream stream)
            {
                m_stream = stream;
            }

            #endregion Constructors

            #region Methods

            #region Internal Methods

            internal void Flush()
            {
                byte header;
                if (m_rlePacket)
                {
                    header = (byte)(-(packetLength - 1));
                }
                else
                {
                    header = (byte)(packetLength - 1);
                }

                m_stream.WriteByte(header);

                int length = (m_rlePacket ? 1 : packetLength);

                m_stream.Write(m_packetValues, 0, length);

                packetLength = 0;
            }

            internal void Push(byte color)
            {
                if (packetLength == 0)
                {
                    // Starting a fresh packet.
                    m_rlePacket = false;
                    m_packetValues[0] = color;
                    packetLength = 1;
                }
                else if (packetLength == 1)
                {
                    // 2nd byte of this packet... decide RLE or non-RLE.
                    m_rlePacket = (color == m_packetValues[0]);
                    m_packetValues[1] = color;
                    packetLength = 2;
                }
                else if (packetLength == m_packetValues.Length)
                {
                    // Packet is full. Start a new one.
                    Flush();
                    Push(color);
                }
                else if (packetLength >= 2 && m_rlePacket && color != m_packetValues[packetLength - 1])
                {
                    // We were filling in an RLE packet, and we got a non-repeated color.
                    // Emit the current packet and start a new one.
                    Flush();
                    Push(color);
                }
                else if (packetLength >= 2 && m_rlePacket && color == m_packetValues[packetLength - 1])
                {
                    // We are filling in an RLE packet, and we got another repeated color.
                    // Add the new color to the current packet.
                    ++packetLength;
                    m_packetValues[packetLength - 1] = color;
                }
                else if (packetLength >= 2 && !m_rlePacket && color != m_packetValues[packetLength - 1])
                {
                    // We are filling in a raw packet, and we got another random color.
                    // Add the new color to the current packet.
                    ++packetLength;
                    m_packetValues[packetLength - 1] = color;
                }
                else if (packetLength >= 2 && !m_rlePacket && color == m_packetValues[packetLength - 1])
                {
                    // We were filling in a raw packet, but we got a repeated color.
                    // Emit the current packet without its last color, and start a
                    // new RLE packet that starts with a length of 2.
                    --packetLength;
                    Flush();
                    Push(color);
                    Push(color);
                }
            }

            #endregion Internal Methods

            #endregion Methods
        }

        #endregion Nested Classes
    }
}


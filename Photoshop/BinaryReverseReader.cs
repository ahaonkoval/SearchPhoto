﻿/////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006, Frank Blumenberg
//
// See License.txt for complete licensing and attribution information.
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

/////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////
//
// This code is adapted from code in the Endogine sprite engine by Jonas Beckeman.
// http://www.endogine.com/CS/
//

/////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;

namespace Photoshop
{
    /// <summary>Reads primitive data types as binary values in in big-endian format</summary>
    public class BinaryReverseReader : BinaryReader
    {
        #region Constructors

        public BinaryReverseReader(Stream a_stream)
            : base(a_stream)
        { }

        #endregion Constructors

        #region Methods

        #region Public Methods

        public override int ReadInt32()
        {
            int val = base.ReadInt32();
            unsafe
            {
                SwapBytes((byte*)&val, 4);
            }
            return val;
        }

        public override long ReadInt64()
        {
            long val = base.ReadInt64();
            unsafe
            {
                SwapBytes((byte*)&val, 8);
            }
            return val;
        }

        public override short ReadInt16()
        {
            short val = base.ReadInt16();
            unsafe
            {
                SwapBytes((byte*)&val, 2);
            }
            return val;
        }

        public string ReadPascalString()
        {
            byte stringLength = base.ReadByte();

            char[] c = base.ReadChars(stringLength);

            if ((stringLength % 2) == 0)
                base.ReadByte();

            return new string(c);
        }

        [CLSCompliant(false)]
        public override uint ReadUInt32()
        {
            uint val = base.ReadUInt32();
            unsafe
            {
                SwapBytes((byte*)&val, 4);
            }
            return val;
        }

        [CLSCompliant(false)]
        public override ulong ReadUInt64()
        {
            ulong val = base.ReadUInt64();
            unsafe
            {
                SwapBytes((byte*)&val, 8);
            }
            return val;
        }

        [CLSCompliant(false)]
        public override ushort ReadUInt16()
        {
            ushort val = base.ReadUInt16();
            unsafe
            {
                SwapBytes((byte*)&val, 2);
            }
            return val;
        }

        #endregion Public Methods

        #endregion Methods

        [CLSCompliant(false)]
        unsafe static protected void SwapBytes(byte* ptr, int nLength)
        {
            for (long i = 0; i < nLength / 2; ++i)
            {
                byte t = *(ptr + i);
                *(ptr + i) = *(ptr + nLength - i - 1);
                *(ptr + nLength - i - 1) = t;
            }
        }
    }

    /// <summary>Writes primitive data types as binary values in in big-endian format</summary>
    public class BinaryReverseWriter : BinaryWriter
    {
        #region Fields

        public bool AutoFlush { get; set; }

        #endregion Fields

        #region Constructors

        public BinaryReverseWriter(Stream a_stream)
            : base(a_stream)
        { }

        #endregion Constructors

        #region Methods

        #region Public Methods

        public override void Write(short val)
        {
            unsafe
            {
                SwapBytes((byte*)&val, 2);
            }
            base.Write(val);

            if (AutoFlush)
                Flush();
        }

        public override void Write(int val)
        {
            unsafe
            {
                SwapBytes((byte*)&val, 4);
            }
            base.Write(val);

            if (AutoFlush)
                Flush();
        }

        public override void Write(long val)
        {
            unsafe
            {
                SwapBytes((byte*)&val, 8);
            }
            base.Write(val);

            if (AutoFlush)
                Flush();
        }

        [CLSCompliant(false)]
        public override void Write(ushort val)
        {
            unsafe
            {
                SwapBytes((byte*)&val, 2);
            }
            base.Write(val);

            if (AutoFlush)
                Flush();
        }

        [CLSCompliant(false)]
        public override void Write(uint val)
        {
            unsafe
            {
                SwapBytes((byte*)&val, 4);
            }
            base.Write(val);

            if (AutoFlush)
                Flush();
        }

        [CLSCompliant(false)]
        public override void Write(ulong val)
        {
            unsafe
            {
                SwapBytes((byte*)&val, 8);
            }
            base.Write(val);

            if (AutoFlush)
                Flush();
        }

        public void WritePascalString(string s)
        {
            char[] c;
            if (s.Length > 255)
                c = s.Substring(0, 255).ToCharArray();
            else
                c = s.ToCharArray();

            base.Write((byte)c.Length);
            base.Write(c);

            int realLength = c.Length + 1;

            if ((realLength % 2) == 0)
                return;

            for (int i = 0; i < (2 - (realLength % 2)); i++)
                base.Write((byte)0);

            if (AutoFlush)
                Flush();
        }

        #endregion Public Methods

        #endregion Methods

        [CLSCompliant(false)]
        unsafe static protected void SwapBytes(byte* ptr, int nLength)
        {
            for (long i = 0; i < nLength / 2; ++i)
            {
                byte t = *(ptr + i);
                *(ptr + i) = *(ptr + nLength - i - 1);
                *(ptr + nLength - i - 1) = t;
            }
        }
    }

    class LengthWriter : IDisposable
    {
        #region Fields

        BinaryReverseWriter m_writer;
        long m_lengthPosition = long.MinValue;
        long m_startPosition;

        #endregion Fields

        #region Constructors

        public LengthWriter(BinaryReverseWriter writer)
        {
            m_writer = writer;

            // we will write the correct length later, so remember
            // the position
            m_lengthPosition = m_writer.BaseStream.Position;
            m_writer.Write((uint)0xFEEDFEED);

            // remember the start  position for calculation Image
            // resources length
            m_startPosition = m_writer.BaseStream.Position;
        }

        #endregion Constructors

        #region Methods

        #region Public Methods

        public void Dispose()
        {
            Write();
        }

        public void Write()
        {
            if (m_lengthPosition != long.MinValue)
            {
                long endPosition = m_writer.BaseStream.Position;

                m_writer.BaseStream.Position = m_lengthPosition;
                long length = endPosition - m_startPosition;
                m_writer.Write((uint)length);
                m_writer.BaseStream.Position = endPosition;

                m_lengthPosition = long.MinValue;
            }
        }

        #endregion Public Methods

        #endregion Methods
    }
}

// Decompiled with JetBrains decompiler
// Type: FotoS.NumericTextBox
// Assembly: FotoS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 470692A6-11C2-4A54-ACAA-7A0C32039435
// Assembly location: D:\develops\Resharp\FotoS.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace FotoS
{
    public class NumericTextBox : TextBox
    {
        private operation_text op_text = new operation_text();
        private bool allowSpace = false;
        private bool begincursor = false;
        private bool m_bEventMode = false;
        private bool m_bHideInput = false;
        private bool m_bColorFocus = false;
        private string m_sScannedQty = "";
        private IContainer components = (IContainer)null;
        private bool m_bDelimiterScanned;
        private string m_sScannedBarcode;
        private bool m_bScanningStarted;
        private DateTime m_dtScanningStarted;

        public event BarcodeScannedEventHandler SpecialBarcodeScanned;

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode != Keys.V)
                return;
            this.Paste();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (this.m_bScanningStarted && !char.IsDigit(e.KeyChar) && this.m_sScannedBarcode != null && this.m_sScannedBarcode.Length > 0)
            {
                this.m_bDelimiterScanned = true;
                if (this.m_bEventMode)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (this.m_bScanningStarted && char.IsDigit(e.KeyChar))
            {
                if (!this.m_bDelimiterScanned)
                {
                    this.m_sScannedBarcode = this.m_sScannedBarcode + e.KeyChar.ToString();
                }
                else
                {
                    this.m_sScannedQty = this.m_sScannedQty + e.KeyChar.ToString();
                    e.Handled = true;
                    return;
                }
            }
            NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormat.NumberDecimalSeparator;
            string numberGroupSeparator = numberFormat.NumberGroupSeparator;
            string negativeSign = numberFormat.NegativeSign;
            string str = e.KeyChar.ToString();
            if (char.IsDigit(e.KeyChar) || (str.Equals(decimalSeparator) || str.Equals(numberGroupSeparator) || str.Equals(negativeSign) || (int)e.KeyChar == 8) || this.allowSpace && (int)e.KeyChar == 32)
                return;
            e.Handled = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyValue == 71 || e.KeyValue == 72 || e.KeyValue == 74)
            {
                this.m_bScanningStarted = true;
                this.m_bDelimiterScanned = false;
                this.m_dtScanningStarted = DateTime.Now;
                this.m_sScannedQty = "";
                this.m_sScannedBarcode = "";
            }
            if (e.KeyValue == 13 && this.m_bScanningStarted && this.m_dtScanningStarted > DateTime.Now.AddMilliseconds(-2500.0))
            {
                this.m_bScanningStarted = false;
                this.m_bDelimiterScanned = false;
                SpecialBarcodeScannedEventArgs e1 = new SpecialBarcodeScannedEventArgs();
                e1.Barcode = this.m_sScannedBarcode;
                e1.Qty = this.m_sScannedQty;
                if (this.m_bEventMode)
                    this.SpecialBarcodeScanned((object)this, e1);
                if (this.m_sScannedQty.Length > 0)
                    return;
            }
            base.OnKeyDown(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            this.Text = this.op_text.RemoveNonDigitsAndWhiteSpacesFromString(this.Text);
            if (!this.begincursor)
                this.SelectionStart = this.Text.Length;
            else
                this.SelectionStart = 0;
        }

        protected override void OnEnter(EventArgs e)
        {
            if (!this.m_bColorFocus)
                return;
            this.BackColor = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, 192);
        }

        protected override void OnLeave(EventArgs e)
        {
            if (!this.m_bColorFocus)
                return;
            this.BackColor = SystemColors.Window;
        }

        public int IntValue
        {
            get
            {
                return int.Parse(this.Text);
            }
        }

        public Decimal DecimalValue
        {
            get
            {
                return Decimal.Parse(this.Text);
            }
        }

        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }
            get
            {
                return this.allowSpace;
            }
        }

        public bool EventFireMode
        {
            set
            {
                this.m_bEventMode = value;
            }
            get
            {
                return this.m_bEventMode;
            }
        }

        public bool ColorFocus
        {
            set
            {
                this.m_bColorFocus = value;
            }
            get
            {
                return this.m_bColorFocus;
            }
        }

        public bool HideInput
        {
            set
            {
                this.m_bHideInput = value;
            }
            get
            {
                return this.m_bHideInput;
            }
        }

        public bool IsSelectionCursorInBegin
        {
            set
            {
                this.begincursor = value;
            }
            get
            {
                return this.begincursor;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new Container();
        }
    }
}

// Decompiled with JetBrains decompiler
// Type: FotoS.SpecialBarcodeScannedEventArgs
// Assembly: FotoS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 470692A6-11C2-4A54-ACAA-7A0C32039435
// Assembly location: D:\develops\Resharp\FotoS.exe

using System;

namespace FotoS
{
  public class SpecialBarcodeScannedEventArgs : EventArgs
  {
    private string m_sBarcode;
    private string m_sQty;

    public string Qty
    {
      get
      {
        return this.m_sQty;
      }
      set
      {
        this.m_sQty = value;
      }
    }

    public string Barcode
    {
      get
      {
        return this.m_sBarcode;
      }
      set
      {
        this.m_sBarcode = value;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: FotoS.operation_text
// Assembly: FotoS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 470692A6-11C2-4A54-ACAA-7A0C32039435
// Assembly location: D:\develops\Resharp\FotoS.exe

namespace FotoS
{
  internal class operation_text
  {
    public string Remove_NonDigits_And_WhiteSpaces_From_String_Allow_znak(string s)
    {
      string str = "";
      try
      {
        foreach (char c in s)
        {
          if (char.IsDigit(c) && !char.IsWhiteSpace(c) || char.ToString(c) == ";")
            str += c.ToString();
        }
      }
      catch
      {
      }
      return str;
    }

    public string Remove_NonDigits_And_WhiteSpaces_From_String_Allow_some(string s)
    {
      string str = "";
      try
      {
        foreach (char c in s)
        {
          if (char.IsDigit(c) && !char.IsWhiteSpace(c) || (char.ToString(c) == ";" || char.ToString(c) == ",") || char.ToString(c) == "." || char.ToString(c) == "&")
            str += c.ToString();
        }
      }
      catch
      {
      }
      return str;
    }

    public string Remove_NonDigits_From_String(string s)
    {
      string str = "";
      try
      {
        foreach (char c in s)
        {
          if (char.IsDigit(c))
            str += c.ToString();
        }
      }
      catch
      {
      }
      return str;
    }

    public string RemoveNonDigitsAndWhiteSpacesFromString(string s)
    {
      int num = 0;
      string str = "";
      try
      {
        s = s.Replace(",", ".");
        foreach (char c in s)
        {
          if (char.IsDigit(c) && !char.IsWhiteSpace(c) || char.ToString(c) == ".")
          {
            if (char.ToString(c) == ".")
              ++num;
            str = num <= 1 ? str + c.ToString() : str + c.ToString().Replace(".", "");
          }
        }
      }
      catch
      {
      }
      return str;
    }

    public string Remove_WhiteSpaces_From_String(string s)
    {
      string str = "";
      try
      {
        foreach (char c in s)
        {
          if (!char.IsWhiteSpace(c))
            str += c.ToString();
        }
      }
      catch
      {
      }
      return str;
    }
  }
}

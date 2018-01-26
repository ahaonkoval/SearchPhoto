// Decompiled with JetBrains decompiler
// Type: FotoS.Program
// Assembly: FotoS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 470692A6-11C2-4A54-ACAA-7A0C32039435
// Assembly location: D:\develops\Resharp\FotoS.exe

using System;
using System.Windows.Forms;

namespace PhotoSearch
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run((Form)new frmFindFiles());
            //Application.Run((Form)new FotoS.Form1()); //frmFindFiles
        }
    }
}

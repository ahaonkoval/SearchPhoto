// Decompiled with JetBrains decompiler
// Type: FotoS.Properties.Settings
// Assembly: FotoS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 470692A6-11C2-4A54-ACAA-7A0C32039435
// Assembly location: D:\develops\Resharp\FotoS.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace FotoS.Properties
{
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
  [CompilerGenerated]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }
  }
}

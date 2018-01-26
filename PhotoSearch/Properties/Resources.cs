// Decompiled with JetBrains decompiler
// Type: FotoS.Properties.Resources
// Assembly: FotoS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 470692A6-11C2-4A54-ACAA-7A0C32039435
// Assembly location: D:\develops\Resharp\FotoS.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FotoS.Properties
{
  [DebuggerNonUserCode]
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) FotoS.Properties.Resources.resourceMan, (object) null))
          FotoS.Properties.Resources.resourceMan = new ResourceManager("FotoS.Properties.Resources", typeof (FotoS.Properties.Resources).Assembly);
        return FotoS.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return FotoS.Properties.Resources.resourceCulture;
      }
      set
      {
        FotoS.Properties.Resources.resourceCulture = value;
      }
    }

    internal static Bitmap delete_16
    {
      get
      {
        return (Bitmap) FotoS.Properties.Resources.ResourceManager.GetObject(nameof (delete_16), FotoS.Properties.Resources.resourceCulture);
      }
    }
  }
}

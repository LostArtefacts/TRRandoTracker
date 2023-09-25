using System;
using System.Reflection;
using System.Windows;
using TRRandoTracker.Utils;

namespace TRRandoTracker;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public string Version { get; private set; }
    public string TaggedVersion { get; private set; }

    public App()
    {
        WindowUtils.SetMenuAlignment();

        Assembly assembly = Assembly.GetExecutingAssembly();
        Version v = assembly.GetName().Version;
        Version = string.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Build);
        TaggedVersion = "v" + Version;
    }
}
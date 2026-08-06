using Main.Common;

namespace Main.WebAppCore.DependentServices;

public static class AppSettings
{
    public static ConfigurationSettings Current
    {
        get; set;
    } = new ();
}

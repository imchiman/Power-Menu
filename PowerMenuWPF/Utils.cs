using PowerMenuWPF.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace PowerMenuWPF
{
    class Utils
    {
        private static Hashtable osInfo;

        public static void initOSInfoHashTable()
        {
            if (osInfo == null)
                osInfo = new Hashtable();
        }

        public static void resetOSInfoHashTable()
        {
            initOSInfoHashTable();
            if (osInfo.Count > 0)
                osInfo.Clear();
        }

        public static void addToOSInfoHashTable(string key, object o)
        {
            osInfo.Add(key, o);
        }

        public static long getOSInfoHashTableCount()
        {
            return osInfo.Count;
        }

        public static bool isOSInfoContainsKey(string key)
        {
            return osInfo[key] != null;
        }

        public static string getOSInfoByKey(string key)
        {
            return osInfo[key].ToString();
        }

        public static bool isNT6OrAbove(string versionCode)
        {
            if (Int32.Parse(versionCode.Substring(0, 1)) >= WindowsVersion.WINDOWSVISTA)
                return true;
            return false;
        }

        public static bool IsWinVistaOrHigher()
        {
            OperatingSystem OS = Environment.OSVersion;
            return (OS.Platform == PlatformID.Win32NT) && (OS.Version.Major >= 6);
        }

        public static object getSettingValue(string key)
        {
            return Properties.Settings.Default[key];
        }

        public static void restoreDefaultSetting()
        {
            Properties.Settings.Default["waitTime"] = 0;
            Properties.Settings.Default["showConfirm"] = true;
            Properties.Settings.Default["lang"] = "en-US";
        }

        public static void updateSetting()
        {
            Properties.Settings.Default.Save();
        }

        public static void updateSetting(string key, object value)
        {
            Properties.Settings.Default[key] = value;
            Properties.Settings.Default.Save();
        }

        public static void saveAllSetting(string[] keys, object[] values)
        {
            for (int i = 0; i < keys.Length && i < values.Length; i++)
            {
                Properties.Settings.Default[keys[i]] = values[i];
            }
            Properties.Settings.Default.Save();
        }

        public static void Restart()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        public static MessageBoxResult getConfirmMsgResult(PowerOptions mode)
        {
            string msg = String.Empty;
            string title = Resources.strWarningTitle;
            if (mode == PowerOptions.SHUTDOWN)
            {
                msg = Resources.strConfirmShutdown;
            }
            else if (mode == PowerOptions.REBOOT)
            {
                msg = Resources.strConfirmReboot;
            }
            else if (mode == PowerOptions.LOGOFF)
            {
                msg = Resources.strConfirmLogoff;
            }
            else if (mode == PowerOptions.LOCK)
            {
                msg = Resources.strConfirmLock;
            }
            else if (mode == PowerOptions.SLEEP)
            {
                msg = Resources.strConfirmSleep;
            }
            else if (mode == PowerOptions.HIBERNATE)
            {
                msg = Resources.strConfirmHibernate;
            }
            return MessageBox.Show(msg, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }

    public static class WindowsVersion
    {
        public static double WINDOWS2000 = 5.0;
        public static double WINDOWSXP = 5.1;
        public static double WINDOWSXP64BIT = 5.2;
        public static double WINDOWSSERVER2003 = 5.2;
        public static double WINDOWSSERVER2003R2 = 5.2;
        public static double WINDOWSVISTA = 6.0;
        public static double WINDOWSSERVER2008 = 6.0;
        public static double WINDOWS7 = 6.1;
        public static double WINDOWSSERVER2008R2 = 6.1;
        public static double WINDOWS8 = 6.2;
        public static double WINDOWSSERVER2012 = 6.2;
        public static double WINDOWS81 = 6.3;
        public static double WINDOWSSERVER2012R2 = 6.3;
        public static double WINDOWS10 = 10;
        public static double WINDOWSSERVER2016 = 10;
    }

    public static class ShutdownClassPath
    {
        public static string NT5 = "Win32Shutdown";
        public static string NT6 = "Win32ShutdownTracker";
    }

    public static class OSInfo
    {
        public static string Version = "Version";
        public static string Caption = "Caption";
    }

    public enum PowerOptions
    {
        // See: http://msdn.microsoft.com/en-us/library/windows/desktop/aa394058(v=vs.85).aspx
        LOGOFF = 0,
        FORCELOGOFF = 4,
        SHUTDOWN = 1,
        FORCESHUTDOWN = 5,
        REBOOT = 2,
        FORCEREBOOT = 6,
        POWEROFF = 8,
        FORCEPOWEROFF = 12,
        LOCK = 99,
        SLEEP = 88,
        HIBERNATE = 77
    }

    public static class RuntimeLocalizer
    {
        private static CultureInfo culture;

        public static void ChangeCulture(Window window, string cultureCode)
        {
            culture = CultureInfo.GetCultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = culture;

            SetLanguageDictionary(window, cultureCode);
        }

        public static void SetLanguageDictionary(Window window)
        {
            SetLanguageDictionary(window, Thread.CurrentThread.CurrentCulture.ToString());
        }

        public static void SetLanguageDictionary(Window window, string cultureCode)
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (cultureCode)
            {
                case "zh-HK":
                    dict.Source = new Uri(@"..\Resources\String.zh-HK.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri(@"..\Resources\String.xaml", UriKind.Relative);
                    break;
            }
            window.Resources.MergedDictionaries.Add(dict);
        }
    }
}

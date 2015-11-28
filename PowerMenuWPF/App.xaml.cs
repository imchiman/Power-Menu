using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PowerMenuWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private CultureInfo cultureOverride = new CultureInfo("qps-PLOC");

        public App()
        {
            if (Debugger.IsAttached == true && cultureOverride != null)
            {
                Thread.CurrentThread.CurrentUICulture = cultureOverride;
                Thread.CurrentThread.CurrentCulture = cultureOverride;
            }
            else
            {
                string lang = (string)Utils.getSettingValue("lang");
                if (lang.Contains("zh"))
                    lang = "zh-Hant";

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            }
        }
    }
}

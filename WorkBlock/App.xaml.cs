using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Windows.AppNotifications;

namespace WorkBlock
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppNotificationManager.Default.Register();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            AppNotificationManager.Default.Unregister();
            base.OnExit(e);
        }
    }

}

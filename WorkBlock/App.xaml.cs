using System.IO;
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
        private bool notificationsRegistered;

        protected override void OnStartup(StartupEventArgs e)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            base.OnStartup(e);

            AppNotificationManager.Default.Register();
            notificationsRegistered = true;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (notificationsRegistered)
            {
                AppNotificationManager.Default.Unregister();
            }

            base.OnExit(e);
        }
    }

}

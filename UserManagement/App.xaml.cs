using DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using UserManagement.Common.Utilities;
using UserManagement.Core.Views;
using UserManagement.Manager;
using UserManagement.Manager.Mappers;
using UserManagement.UI;
using UserManagement.WebServices;

namespace UserManagement.Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IServiceEntityMapper, ServiceEntityMapper>();
            containerRegistry.RegisterSingleton<IConnectivity, Connectivity>();

            containerRegistry.Register<IWindowsWebService, WindowsWebService>();
            containerRegistry.Register<IWindowsManager, WindowsManager>();
            containerRegistry.Register<ILocationManager, LocationManager>();

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(typeof(UIModule));
        }

        protected async override void OnExit(ExitEventArgs e)
        {
            await Pushers.Client.Dispose();
            base.OnExit(e);
        }
    }
}

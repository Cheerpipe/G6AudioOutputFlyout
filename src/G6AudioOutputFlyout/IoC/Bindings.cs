using G6AudioOutputFlyout.Services.FlyoutServices;
using G6AudioOutputFlyout.Services.ISBControllerService;
using G6AudioOutputFlyout.Services.TrayIconServices;
using Ninject.Modules;

namespace G6AudioOutputFlyout.IoC
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ITrayIconService>().To<WindowsTrayIconService>().InSingletonScope();
            Bind<IFlyoutService>().To<WindowsFlyoutService>().InSingletonScope();
            Bind<ISBControllerService>().To<SBControllerService>().InSingletonScope();
        }
    }
}
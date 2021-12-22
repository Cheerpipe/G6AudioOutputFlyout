using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using G6AudioOutputFlyout.IoC;
using G6AudioOutputFlyout.SBOutputController;
using G6AudioOutputFlyout.Screens.FlyoutContainer;
using G6AudioOutputFlyout.Services.FlyoutServices;
using G6AudioOutputFlyout.Services.ISBControllerService;
using G6AudioOutputFlyout.Services.TrayIconServices;
using Application = Avalonia.Application;

namespace G6AudioOutputFlyout
{
 
    public class Program
    {
        public static CancellationTokenSource RunCancellationTokenSource { get; } = new();
        private static readonly CancellationToken RunCancellationToken = RunCancellationTokenSource.Token;

        // This method is needed for IDE previewer infrastructure
        public static AppBuilder BuildAvaloniaApp()
        {
            var builder = AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .UseSkia()
                .With(new Win32PlatformOptions()
                {
                    UseWindowsUIComposition = true,
                    CompositionBackdropCornerRadius = 10f,
                });
            return builder;
        }

        // The entry point. Things aren't ready yet, so at this point
        // you shouldn't use any Avalonia types or anything that expects
        // a SynchronizationContext to be ready
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().Start(AppMain, args);
        }

        // Application entry point. Avalonia is completely initialized.
        static void AppMain(Application app, string[] args)
        {
            // Do you startup code here
            Kernel.Initialize(new Bindings());

            IFlyoutService flyoutService = Kernel.Get<IFlyoutService>();
            flyoutService.SetPopulateViewModelFunc(() =>
            {
                return Kernel.Get<FlyoutContainerViewModel>();
            });

            ITrayIconService trayIconService = Kernel.Get<ITrayIconService>();
            trayIconService.Show();

            ISBControllerService sbControllerService = Kernel.Get<SBControllerService>();
            
            sbControllerService.OutputChanged += (_, e) =>
            {
                trayIconService.UpdateIcon(e.OutputMode == DeviceOutputModes.Speakers ? "Speakers.ico" : "Headphones.ico");
            };


            // Start the main loop
            app.Run(RunCancellationToken);

            // Stop things
            trayIconService.Hide();

        }
    }
}

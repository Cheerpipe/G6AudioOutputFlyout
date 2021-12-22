using System;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Threading;
using G6AudioOutputFlyout.SBOutputController;
using G6AudioOutputFlyout.Services.FlyoutServices;
using AvaloniaTrayIcon = Avalonia.Controls.TrayIcon;

namespace G6AudioOutputFlyout.Services.TrayIconServices
{
    public class TrayIconService : ITrayIconService, IDisposable
    {
        private readonly IFlyoutService _flyoutService;
        private readonly AvaloniaTrayIcon _trayIcon;
        private readonly ISBControllerService.ISBControllerService _sbControllerService;
        private readonly Timer _alwaysVisibletimer;

        public TrayIconService(IFlyoutService flyoutService, ISBControllerService.ISBControllerService sbControllerService)
        {
            _flyoutService = flyoutService;
            _flyoutService.PreLoad();
            _sbControllerService = sbControllerService;
            _flyoutService.PreLoad();
            _trayIcon = new AvaloniaTrayIcon();
            _trayIcon.Clicked += TrayIcon_Clicked;
            _alwaysVisibletimer = new Timer(1000);
            _alwaysVisibletimer.Elapsed += _alwaysVisibletimer_Elapsed;
        }

        public void Refresh()
        {
            _trayIcon.IsVisible = false;
            _trayIcon.IsVisible = true;
        }

        private void _alwaysVisibletimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_trayIcon.IsVisible)
                _trayIcon.IsVisible = true;
        }

        public void Show()
        {
            _trayIcon.Menu = new NativeMenu();
            UpdateIcon(_sbControllerService.GetCurrentOutput() == DeviceOutputModes.Speakers ? "Speakers.ico" : "Headphones.ico");
            NativeMenuItem exitMenu = new("Exit SoundblasterX audio output Flyout");
            exitMenu.Click += ExitMenu_Click;
            _trayIcon.Menu.Items.Add(exitMenu);
            _trayIcon.IsVisible = true;
        }
        public void Hide()
        {
            _trayIcon.IsVisible = false;
        }

        public void UpdateIcon(string iconName)
        {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            var icon = new WindowIcon(assets.Open(new Uri(@$"resm:G6AudioOutputFlyout.Assets.{iconName}")));
            Dispatcher.UIThread.Post(() => { _trayIcon.Icon = icon; });
        }

        public void UpdateTooltip(string tooltipText)
        {
            Dispatcher.UIThread.Post(() => { _trayIcon.ToolTipText = tooltipText; });
        }

        private void ExitMenu_Click(object sender, EventArgs e)
        {
            Program.RunCancellationTokenSource.Cancel();
        }

        private void TrayIcon_Clicked(object sender, EventArgs e)
        {
            _flyoutService.Toggle();
        }

        public void Dispose()
        {
            _trayIcon?.Dispose();
        }
    }
}

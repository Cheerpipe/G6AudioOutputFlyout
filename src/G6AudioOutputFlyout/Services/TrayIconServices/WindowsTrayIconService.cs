﻿using System;
using System.Windows.Forms;
using G6AudioOutputFlyout.Platform.Windows;
using G6AudioOutputFlyout.Services.FlyoutServices;

namespace G6AudioOutputFlyout.Services.TrayIconServices
{
    internal class WindowsTrayIconService : TrayIconService, IDisposable
    {
        private static uint WM_TASKBARCREATED = Win32MessageGrabber.RegisterWindowMessage("TaskbarCreated");
        private readonly Win32MessageGrabber _win32MessageGrabber = new Win32MessageGrabber();

        public WindowsTrayIconService(IFlyoutService flyoutService, ISBControllerService.ISBControllerService sbControllerService) : base(flyoutService, sbControllerService)
        {
            _win32MessageGrabber.MessageReceived += _win32MessageGrabber_MessageReceived;
        }


        private void _win32MessageGrabber_MessageReceived(object sender, Message e)
        {
            if (e.Msg == WM_TASKBARCREATED)
                Refresh();
        }

        public new void Dispose()
        {
            base.Dispose();
            _win32MessageGrabber?.Dispose();
        }
    }
}

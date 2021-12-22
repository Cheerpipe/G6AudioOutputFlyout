using System;
using System.IO;
using System.Linq;
using G6AudioOutputFlyout.IoC;
using G6AudioOutputFlyout.SBOutputController;
using G6AudioOutputFlyout.Services.TrayIconServices;

namespace G6AudioOutputFlyout.Services.ISBControllerService
{
    public class SBControllerService : ISBControllerService
    {
        private SBController _sbController;

        public SBControllerService()
        {
            Directory.SetCurrentDirectory(@"C:\Program Files (x86)\Creative\Sound Blaster Command");
            _sbController = new SBController(@"C:\Program Files (x86)\Creative\Sound Blaster Command");
            _sbController.OutputModeChangedEvent += _sbController_OutputModeChangedEvent;
        }

        private void _sbController_OutputModeChangedEvent(object sender, OutputModeChangedEventArgs e)
        {
            OutputChanged?.Invoke(this, e);

            // Workaround Event is not working because OutputChanged is always null. Injection problems
            ITrayIconService trayIconService = Kernel.Get<ITrayIconService>();
            trayIconService.UpdateIcon(e.OutputMode == DeviceOutputModes.Speakers ? "Speakers.ico" : "Headphones.ico");
        }

        public void SetSpeakers()
        {
            _sbController.SwitchToOutputMode(_sbController.GetDevices().FirstOrDefault(), DeviceOutputModes.Speakers);
        }

        public void SetHeadphones()
        {
            _sbController.SwitchToOutputMode(_sbController.GetDevices().FirstOrDefault(), DeviceOutputModes.Headphones);
        }

        public void EnableDirect()
        {
            _sbController.SwitchDirectMode(_sbController.GetDevices().FirstOrDefault(), DirectModeStates.On);
        }

        public void DisableDirect()
        {
            _sbController.SwitchDirectMode(_sbController.GetDevices().FirstOrDefault(), DirectModeStates.Off);
        }

        public DeviceOutputModes GetCurrentOutput()
        {
            return _sbController.GetOutputModeForDevice(_sbController.GetDevices().FirstOrDefault());
        }

        public DirectModeStates GetCurrentDirectModeState()
        {
            return _sbController.GetDirectModeStateForDevice(_sbController.GetDevices().FirstOrDefault());
        }

        public event EventHandler<OutputModeChangedEventArgs> OutputChanged;
        public int GetGameLevel()
        {
            return (int)_sbController.GetGameLevel((_sbController.GetDevices().FirstOrDefault()));
        }

        public bool GetSbxState()
        {
            return _sbController.GetSbxState(_sbController.GetDevices().FirstOrDefault());
        }

        public void SetSbxState(bool enabled)
        {
            _sbController.SetSbxState(_sbController.GetDevices().FirstOrDefault(),enabled);
        }

        public bool GetScoutState()
        {
            return _sbController.GetScoutState((_sbController.GetDevices().FirstOrDefault()));
        }

        public void SetScoutState(bool enabled)
        {
            _sbController.SetScoutState(_sbController.GetDevices().FirstOrDefault(), enabled);
        }

        public bool GetEqState()
        {
            return _sbController.GetEqState((_sbController.GetDevices().FirstOrDefault()));
        }

        public void SetEqState(bool enabled)
        {
            _sbController.SetEqState(_sbController.GetDevices().FirstOrDefault(), enabled);
        }
    }
}
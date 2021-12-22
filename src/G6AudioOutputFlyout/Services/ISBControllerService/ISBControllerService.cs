using System;
using G6AudioOutputFlyout.SBOutputController;

namespace G6AudioOutputFlyout.Services.ISBControllerService
{
    public interface ISBControllerService
    {
        public void SetSpeakers();
        public void SetHeadphones();
        public void EnableDirect();
        public void DisableDirect();
        public DeviceOutputModes GetCurrentOutput();
        public DirectModeStates GetCurrentDirectModeState();
        public event EventHandler<OutputModeChangedEventArgs> OutputChanged;
        public int GetGameLevel();
        bool GetSbxState();
        void SetSbxState(bool enabled);
        bool GetScoutState();
        void SetScoutState(bool enabled);
        bool GetEqState();
        void SetEqState(bool enabled);
    }
}
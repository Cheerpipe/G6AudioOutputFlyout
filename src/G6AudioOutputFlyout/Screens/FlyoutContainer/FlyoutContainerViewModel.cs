using System.Diagnostics;
using System.Reactive.Disposables;
using Avalonia.Media;
using G6AudioOutputFlyout.SBOutputController;
using G6AudioOutputFlyout.Services.FlyoutServices;
using G6AudioOutputFlyout.Services.ISBControllerService;
using G6AudioOutputFlyout.ViewModels;
using ReactiveUI;

namespace G6AudioOutputFlyout.Screens.FlyoutContainer
{
    public class FlyoutContainerViewModel : ViewModelBase
    {
        private readonly IFlyoutService _flyoutService;
        private readonly ISBControllerService _sbControllerService;

        private const int MainPageWidth = 290;

        public FlyoutContainerViewModel(
            IFlyoutService flyoutService,
            ISBControllerService sbControllerService)
        {
            _flyoutService = flyoutService;
            _sbControllerService = sbControllerService;

            this.WhenActivated(disposables =>
            {
                /* Handle activation */

                Disposable
                    .Create(() =>
                    {
                        /* Handle deactivation */


                    })
                    .DisposeWith(disposables);
            });

            FlyoutWindowWidth = MainPageWidth;
            FlyoutWindowHeight = 320;
        }


        public void GoMoreOptions()
        {
            ActivePageindex = 1;
        }

        public bool DirectMode
        {
            get => _sbControllerService.GetCurrentDirectModeState() == DirectModeStates.On;
            set
            {
                if (value)
                    _sbControllerService.EnableDirect();
                else
                    _sbControllerService.DisableDirect();
                this.RaisePropertyChanged(nameof(DirectMode));
            }
        }

        public bool ScoutMode
        {
            get => _sbControllerService.GetScoutState();
            set
            {
                _sbControllerService.SetScoutState(value);
                this.RaisePropertyChanged(nameof(ScoutMode));
            }
        }

        public bool GraphicEQ
        {
            get => _sbControllerService.GetEqState();
            set
            {
                _sbControllerService.SetEqState(value);
                this.RaisePropertyChanged(nameof(ScoutMode));
            }
        }

        public bool AcousticEngine
        {
            get => _sbControllerService.GetSbxState();
            set
            {
                _sbControllerService.SetSbxState(value);
                this.RaisePropertyChanged(nameof(AcousticEngine));
            }
        }

        public void GoMainPage()
        {
            ActivePageindex = 0;
        }

        public void SetHeadphones()
        {
            _sbControllerService.SetHeadphones();
            this.RaisePropertyChanged(nameof(UsingHeadphones));
            this.RaisePropertyChanged(nameof(UsingSpeakers));
        }

        public void SetSpeakers()
        {
            _sbControllerService.SetSpeakers();
            this.RaisePropertyChanged(nameof(UsingHeadphones));
            this.RaisePropertyChanged(nameof(UsingSpeakers));
        }

        public bool DirectModeState
        {
            get => _sbControllerService.GetCurrentDirectModeState() == DirectModeStates.On;
            set
            {
                if (value)
                    _sbControllerService.EnableDirect();
                else
                    _sbControllerService.DisableDirect();
            }
        }

        private int _activePageIndex;
        public int ActivePageindex
        {
            get => _activePageIndex;
            set => this.RaiseAndSetIfChanged(ref _activePageIndex, value);
        }
        
        public bool UsingHeadphones => _sbControllerService.GetCurrentOutput() == DeviceOutputModes.Headphones;
        public bool UsingSpeakers => _sbControllerService.GetCurrentOutput() == DeviceOutputModes.Speakers;

        private double _flyoutWindowWidth;
        public double FlyoutWindowWidth
        {
            get => _flyoutWindowWidth;
            set
            {
                _flyoutService.SetWidth(value);
                _flyoutWindowWidth = value;
                FlyoutWidth = _flyoutWindowWidth;
            }
        }

        private LinearGradientBrush _backgroundBrush;
        public LinearGradientBrush BackgroundBrush
        {
            get => _backgroundBrush;
            set
            {
                _backgroundBrush = value;
                this.RaisePropertyChanged(nameof(BackgroundBrush));
            }
        }

        private double _flyoutWidth;
        public double FlyoutWidth
        {
            get => _flyoutWidth;
            set => this.RaiseAndSetIfChanged(ref _flyoutWidth, value);
        }


        private double _flyoutWindowHeight;
        public double FlyoutWindowHeight
        {
            get => _flyoutWindowHeight;
            set
            {
                _flyoutService.SetHeight(value);
                _flyoutWindowHeight = value;
                FlyoutHeight = _flyoutWindowHeight;
            }
        }

        private double _flyoutHeight;
        public double FlyoutHeight
        {
            get => _flyoutHeight;
            set => this.RaiseAndSetIfChanged(ref _flyoutHeight, value);
        }


        // ReSharper disable once UnusedMember.Local
        private void GoSettingsCommand()
        {
            Windows.Run(@"C:\Program Files (x86)\Creative\Sound Blaster Command\Creative.SBCommand.exe");
        }
    }
    public class Windows
    {
        public static void Run(string commandLine, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = commandLine;
            startInfo.Arguments = arguments;
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}

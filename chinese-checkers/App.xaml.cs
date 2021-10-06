using System;

using chinese_checkers.Services;

using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace chinese_checkers
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;

            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.MainMenu));
        }

        private readonly double minW = 800, minH = 600;

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            SetWindowMinSize(new Size(args.Window.Bounds.Width, args.Window.Bounds.Height));
            args.Window.CoreWindow.SizeChanged += CoreWindow_SizeChanged;
            base.OnWindowCreated(args);
        }

        private void CoreWindow_SizeChanged(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.WindowSizeChangedEventArgs args)
        {
            if (SetWindowMinSize(args.Size)) sender.ReleasePointerCapture();
        }

        private bool SetWindowMinSize(Size size)
        {
            if (size.Width < minW || size.Height < minH)
            {
                if (size.Width < minW) size.Width = minW;
                if (size.Height < minH) size.Height = minH;
                return ApplicationView.GetForCurrentView().TryResizeView(size);
            }
            return false;
        }
    }
}

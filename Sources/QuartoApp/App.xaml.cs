using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Storage;
using Plugin.Maui.Audio;

namespace QuartoApp
{
    public partial class App : Application
    {

        public static readonly BindableProperty GlobalBackgroundImageProperty =
             BindableProperty.Create(nameof(GlobalBackgroundImage), typeof(ImageSource), typeof(App), default(ImageSource));

        public readonly IAudioManager _audioManager;

        public IAudioPlayer? BackgroundPlayer => _backgroundPlayer;
        private IAudioPlayer? _backgroundPlayer;

        public ImageSource GlobalBackgroundImage
        {
            get => (ImageSource)GetValue(GlobalBackgroundImageProperty);
            set
            {
                SetValue(GlobalBackgroundImageProperty, value);
                OnPropertyChanged();
            }
        }

        public GameManager? GameManager { get; set; }

        public App(IAudioManager audioManager)
        {
            InitializeComponent();
            GlobalBackgroundImage = "simple.png";
            _audioManager = audioManager;
            PlayBackgroundMusic();
        }

        private async void PlayBackgroundMusic()
        {
            if (_backgroundPlayer == null)
            {
                var stream = await FileSystem.OpenAppPackageFileAsync("backsound.mp3");
                _backgroundPlayer = _audioManager.CreatePlayer(stream);
                _backgroundPlayer.Loop = true; // Active la boucles
                _backgroundPlayer.Play();
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
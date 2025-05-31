using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuartoApp
{
    public partial class App : Application
    {
        public static readonly BindableProperty GlobalBackgroundImageProperty =
             BindableProperty.Create(nameof(GlobalBackgroundImage), typeof(ImageSource), typeof(App), default(ImageSource));

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

        public App()
        {
            InitializeComponent();
            GlobalBackgroundImage = "simple.png";
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
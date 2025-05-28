using Model;

namespace QuartoApp
{
    public partial class App : Application
    {
        public GameManager? GameManager { get; set; }

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
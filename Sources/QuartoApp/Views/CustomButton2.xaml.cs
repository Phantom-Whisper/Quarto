namespace QuartoApp.Views;

public partial class CustomButton2 : ContentView
{
	public CustomButton2()
	{
		InitializeComponent();
		BindingContext = this;
	}

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomButton2), default(string));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}
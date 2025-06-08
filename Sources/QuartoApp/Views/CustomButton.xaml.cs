namespace QuartoApp.Views;

public partial class CustomButton : ContentView
{
	public CustomButton()
	{
		InitializeComponent();
		BindingContext = this;
	}

	public static readonly BindableProperty TextProperty =
		BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomButton), default(string));

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}
}
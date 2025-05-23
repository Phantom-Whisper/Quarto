namespace QuartoApp.Views;

public partial class CarteMembre : ContentView
{
	public CarteMembre()
	{
		InitializeComponent();
		BindingContext = this;
	}

	public static readonly BindableProperty NameProperty =
		BindableProperty.Create(nameof(Name), typeof(string), typeof(CarteMembre), default(string));

    public static readonly BindableProperty RoleProperty =
        BindableProperty.Create(nameof(Role), typeof(string), typeof(CustomButton), default(string));

    public string Name
	{
		get => (string)GetValue(NameProperty);
		set => SetValue(NameProperty, value);
	}

    public string Role
    {
        get => (string)GetValue(RoleProperty);
        set => SetValue(RoleProperty, value);
    }
}
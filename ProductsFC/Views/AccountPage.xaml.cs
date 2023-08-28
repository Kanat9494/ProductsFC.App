using Microsoft.Maui.Controls.Shapes;

namespace ProductsFC.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
		InitializeComponent();

		BuildUI();

		BindingContext = new AccountViewModel();
	}

	private void BuildUI()
	{
		versionBorder.Content = new StackLayout
		{
			Orientation = StackOrientation.Horizontal,

			Children =
			{
				new Label
				{
					VerticalOptions = LayoutOptions.Center,
				}.Text("Версия 1.4").Font(size: 20, bold: true, family: "RegularFont"),

				new Border
				{
					HorizontalOptions = LayoutOptions.EndAndExpand,
					HeightRequest = 40,
					WidthRequest = 40,
					StrokeThickness = 0,
					BackgroundColor = Color.FromArgb("#f2f2f2"),
					StrokeShape = new RoundRectangle
					{
						CornerRadius = new CornerRadius(40, 40, 40, 40)
					},

					Content = new Image()
					{
						HeightRequest = 30,
						WidthRequest = 30,
					}.Source("next_icon.png"),
				}.Paddings(10, 10, 10, 10)
			}
		};
	}
}
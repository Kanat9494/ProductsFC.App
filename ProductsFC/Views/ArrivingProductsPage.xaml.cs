namespace ProductsFC.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ArrivingProductsPage : ContentPage
{
	public ArrivingProductsPage()
	{
		InitializeComponent();

		BindingContext = new ArrivingProductsViewModel();
	}
}
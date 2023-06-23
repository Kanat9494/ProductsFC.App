namespace ProductsFC.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class HomePage : ContentPage
{
	public HomePage(GoodsDBService goodsService)
	{
		InitializeComponent();

		BindingContext = new MainViewModel(goodsService);
	}
}
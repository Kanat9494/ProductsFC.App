namespace ProductsFC.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ArrivingProductsPage : ContentPage
{
	public ArrivingProductsPage(GoodsDBService goodsDbService)
	{
		InitializeComponent();

		BindingContext = new ArrivingProductsViewModel(goodsDbService);
	}
}
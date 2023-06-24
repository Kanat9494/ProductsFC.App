namespace ProductsFC.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ProductDetailsPage : ContentPage
{
	public ProductDetailsPage(GoodsDBService goodsDBService, int productId)
	{
		InitializeComponent();

		BindingContext = new ProductDetailsViewModel(goodsDBService, productId);
	}
}
namespace ProductsFC.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class NewProductPage : ContentPage
{
	public NewProductPage(GoodsDBService goodsService)
	{
		InitializeComponent();

		this.BindingContext = new NewProductViewModel(goodsService);
	}
}
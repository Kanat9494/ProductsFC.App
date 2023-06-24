namespace ProductsFC.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ReportsPage : ContentPage
{
	public ReportsPage(GoodsDBService goodsDbService)
	{
		InitializeComponent();

		BindingContext = new ReportsViewModel(goodsDbService);
	}
}
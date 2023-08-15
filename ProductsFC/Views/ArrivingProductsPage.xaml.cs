namespace ProductsFC.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ArrivingProductsPage : ContentPage
{
	public ArrivingProductsPage(GoodsDBService goodsDbService)
	{
		InitializeComponent();

		BindingContext = _viewModel = new ArrivingProductsViewModel(goodsDbService);
	}

	ArrivingProductsViewModel _viewModel;

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.OnTextChanged();
    }
}
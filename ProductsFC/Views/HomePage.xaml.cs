namespace ProductsFC.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class HomePage : ContentPage
{
	public HomePage(GoodsDBService goodsService)
	{
		InitializeComponent();

		BindingContext = _viewModel = new MainViewModel(goodsService);
	}

	MainViewModel _viewModel;

	private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
	{
		_viewModel.OnTextChanged();
	}
}
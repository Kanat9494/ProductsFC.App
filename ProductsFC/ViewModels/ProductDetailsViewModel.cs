namespace ProductsFC.ViewModels;

public class ProductDetailsViewModel : BaseViewModel
{
    public ProductDetailsViewModel(GoodsDBService goodsDbService, int productId)
    {
        _goodsDbService = goodsDbService;
        _productId = productId;

        SaveProductCommand = new Command(async () => await OnSaveProduct());
        DeleteProductCommand = new Command(async () => await OnDeleteProduct());

        Task.Run(async () =>
        {
            await InitializeProduct();
        });
    }

    private readonly GoodsDBService _goodsDbService;

    private int _productId;

    public ICommand SaveProductCommand { get; }
    public ICommand DeleteProductCommand { get; }


    private Product _currentProduct;
    public Product CurrentProduct
    {
        get => _currentProduct;
        set => SetProperty(ref _currentProduct, value);
    }

    async Task InitializeProduct()
    {
        CurrentProduct = await _goodsDbService.GetItemAsync(_productId);
    }

    private async Task OnSaveProduct()
    {
        await _goodsDbService.UpdateItemAsync(this.CurrentProduct);
        await App.Current.MainPage.Navigation.PopModalAsync();
    }

    private async Task OnDeleteProduct()
    {
        await _goodsDbService.DeleteItemAsync(this.CurrentProduct);
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}

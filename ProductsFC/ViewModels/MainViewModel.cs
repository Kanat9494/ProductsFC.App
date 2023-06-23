namespace ProductsFC.ViewModels;

public class MainViewModel : BaseViewModel
{
    public MainViewModel(GoodsDBService goodsDbService)
    {
        IsBusy = true;
        _goodsDbService = goodsDbService;
        Products = new ObservableCollection<Product>();

        Task.Run(async () =>
        {
            await Task.Delay(1500);
            await LoadProducts();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsBusy = false;
        });
    }

    private readonly GoodsDBService _goodsDbService;

    public ObservableCollection<Product> Products { get; set; }

    private async Task LoadProducts()
    {
        var products = await _goodsDbService.GetItemsNotDelivered();
        if (products != null)
        {
            foreach (var item in products)
                Products.Add(item);
        }
    }
}

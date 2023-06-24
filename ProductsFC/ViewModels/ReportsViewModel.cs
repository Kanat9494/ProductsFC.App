namespace ProductsFC.ViewModels;

internal class ReportsViewModel : BaseViewModel
{
    public ReportsViewModel(GoodsDBService goodsDbService)
    {
        IsBusy = true;
        _goodsDbService = goodsDbService;
        ProductsCount = 0;
        ProductsWeight = 0;
        ProductsPrice = 0;

        Task.Run(async () =>
        {
            await Task.Delay(500);
            await GetReports();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsBusy = false;
        });
    }

    private readonly GoodsDBService _goodsDbService;

    private int _productsCount;
    public int ProductsCount
    {
        get => _productsCount;
        set => SetProperty(ref _productsCount, value);
    }
    private double _productsWeight;
    public double ProductsWeight
    {
        get => _productsWeight;
        set => SetProperty(ref _productsWeight, value);
    }
    private double _productsPrice;
    public double ProductsPrice
    {
        get => _productsPrice;
        set => SetProperty(ref _productsPrice, value);
    }

    private async Task GetReports()
    {
        var products = await _goodsDbService.GetItemsNotDelivered();
        if (products != null)
        {
            if (products.Count > 0)
            {
                ProductsCount = products.Count;

                for (int i = 0; i < products.Count; i++)
                {
                    ProductsWeight = ProductsWeight + (double)products[i].Weight;
                    ProductsPrice = ProductsPrice + (double)products[i].Price;
                }
            }
        }

    }
}

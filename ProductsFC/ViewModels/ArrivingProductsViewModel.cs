namespace ProductsFC.ViewModels;

internal class ArrivingProductsViewModel : BaseViewModel
{
    internal ArrivingProductsViewModel(GoodsDBService goodsDbService)
    {
        IsBusy = true;
        _goodsDbService = goodsDbService;
        ArrivingProducts = new ObservableCollection<Product>();

        ProductDetailsCommand = new Command<int>(async (productId) => await OnProductDetails(productId));


        RefreshProductsCommand = new Command(() =>
        {
            Task.Run(async () =>
            {
                await LoadArrivingProductsAsync();
            }).GetAwaiter().OnCompleted(() =>
            {
                IsRefreshing = false;
            });
        });

        Task.Run(async () =>
        {
            await LoadArrivingProductsAsync();
        }).GetAwaiter().OnCompleted(() =>
        {
            IsBusy = false;
        });
    }

    private readonly GoodsDBService _goodsDbService;

    public ObservableCollection<Product> ArrivingProducts { get; set; }
    public ICommand RefreshProductsCommand { get; }
    public ICommand ProductDetailsCommand { get; }



    private bool _isRefreshing;
    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }


    private async Task LoadArrivingProductsAsync()
    {
        await Task.Delay(1000);
        var response = await _goodsDbService.GetArrivingItemsAsync();

        if (response != null)
        {
            for (int i = 0; i < response.Count; i++)
                ArrivingProducts.Add(response[i]);
        }
    }

    

    private async Task OnProductDetails(int productId)
    {
        await App.Current.MainPage.Navigation.PushModalAsync(new ProductDetailsPage(_goodsDbService, productId));
    }
}

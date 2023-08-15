namespace ProductsFC.ViewModels;

internal class ArrivingProductsViewModel : BaseViewModel
{
    internal ArrivingProductsViewModel(GoodsDBService goodsDbService)
    {
        IsBusy = true;
        _goodsDbService = goodsDbService;
        ArrivingProducts = new ObservableCollection<Product>();

        ProductDetailsCommand = new Command<int>(async (productId) => await OnProductDetails(productId));

        DeleteItemCommand = new Command<Product>(async (product) =>
        {
            await OnDeleteItem(product);
        });


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
    public ICommand DeleteItemCommand { get; }




    private bool _isRefreshing;
    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }
    private string _searchText;
    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }
    private int _productsCount;
    public int ProductsCount
    {
        get => _productsCount;
        set => SetProperty(ref _productsCount, value);
    }



    private async Task LoadArrivingProductsAsync()
    {
        await Task.Delay(1000);
        var response = await _goodsDbService.GetArrivingItemsAsync();

        if (response != null)
        {
            for (int i = 0; i < response.Count; i++)
                ArrivingProducts.Add(response[i]);

            ProductsCount = ArrivingProducts.Count;
        }
    }




    

    private async Task OnProductDetails(int productId)
    {
        await App.Current.MainPage.Navigation.PushModalAsync(new ProductDetailsPage(_goodsDbService, productId));
    }



    private async Task OnDeleteItem(Product product)
    {
        var result = await _goodsDbService.DeleteItemAsync(product);

        if (result == 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Произошла непредвиденная ошибка, обновите экран и попробуйте удалить ещё раз", "Ок");
            return;
        }

        ArrivingProducts.Remove(product);
    }



    public void OnTextChanged()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                await LoadArrivingProductsAsync();
            }).GetAwaiter().OnCompleted(() =>
            {
                IsBusy = false;
            });
        }
        else
        {
            var products = ArrivingProducts.Where(p => p.TrackCode.Contains(SearchText)).ToList<Product>();
            ArrivingProducts.Clear();
            for (int i = 0; i < products.Count; i++)
                ArrivingProducts.Add(products[i]);
        }
    }
}

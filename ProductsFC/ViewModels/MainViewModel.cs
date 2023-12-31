﻿namespace ProductsFC.ViewModels;

public class MainViewModel : BaseViewModel
{
    public MainViewModel(GoodsDBService goodsDbService)
    {
        IsBusy = true;
        _goodsDbService = goodsDbService;
        Products = new ObservableCollection<Product>();
        NewProductCommand = new Command(async () => await OnNewProduct());
        ProductDetailsCommand = new Command<int>(async (productId) => await OnProductDetails(productId));
        DeleteItemCommand = new Command<Product>(async (product) =>
        {
            await OnDeleteItem(product);
        });
        ItemDeliveredCommand = new Command<Product>(async (product) =>
        {
            await OnItemDelivered(product);
        });
        ProductsListToSearch = new List<Product>();
        RefreshProductsCommand = new Command(() =>
        {
            Task.Run(async () =>
            {
                await LoadProducts();
            }).GetAwaiter().OnCompleted(() =>
            {
                IsRefreshing = false;
            });
        });

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

    private int _productId;
    public ObservableCollection<Product> Products { get; set; }
    public static List<Product> ProductsListToSearch { get; set; }
    public ICommand NewProductCommand { get; }
    public ICommand ProductDetailsCommand { get; }
    public ICommand RefreshProductsCommand { get; }
    public ICommand DeleteItemCommand { get; }
    public ICommand ItemDeliveredCommand { get; }



    #region Properties
    private string _searchText;
    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }
    private bool _isRefreshing;
    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }
    private int _productsCount;
    public int ProductsCount
    {
        get => _productsCount;
        set => SetProperty(ref _productsCount, value);
    }



    #endregion

    private async Task LoadProducts()
    {
        Products.Clear();
        var products = await _goodsDbService.GetItemsNotDelivered();
        if (products != null)
        {
            foreach (var item in products)
                Products.Add(item);

            ProductsListToSearch = products;
            ProductsCount = Products.Count;
        }
    }

    private async Task OnNewProduct()
        //=> await App.Current.MainPage.Navigation.PushModalAsync(new NewProductPage(_goodsDbService));
        => await Shell.Current.GoToAsync("NewProductPage");

    private async Task OnProductDetails(int productId)
    {
        await App.Current.MainPage.Navigation.PushModalAsync(new ProductDetailsPage(_goodsDbService, productId));
    }

    public void OnTextChanged()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                await LoadProducts();
            }).GetAwaiter().OnCompleted(() =>
            {
                IsBusy = false;
            });
        }
        else
        {
            var products = Products.Where(p => p.TrackCode.Contains(SearchText)).ToList<Product>();
            Products.Clear();
            for (int i = 0; i < products.Count; i++)
                Products.Add(products[i]);
        }
    }

    private async Task OnDeleteItem(Product product)
    {
        var result = await _goodsDbService.DeleteItemAsync(product);

        if (result == 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Произошла непредвиденная ошибка, обновите экран и попробуйте удалить ещё раз", "Ок");
            return;
        }

        Products.Remove(product);
    }



    private async Task OnItemDelivered(Product product)
    {
        product.IsDelivered = 1;
        var result = await _goodsDbService.UpdateItemAsync(product);

        if (result == 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Произошла непредвиденная ошибка, обновите экран и попробуйте сделать доставлено ещё раз", "Ок");
            return;
        }

        Products.Remove(product);
    }
}

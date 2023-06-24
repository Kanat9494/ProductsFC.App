namespace ProductsFC.ViewModels;

public class NewProductViewModel : BaseViewModel
{
    public NewProductViewModel(GoodsDBService goodsDbService)
    {
        SaveProductCommand = new Command(async () => await OnSaveProduct());
        _goodsDbService = goodsDbService;
        MinDate = DateTime.Today;
    }

    private readonly GoodsDBService _goodsDbService;

    public ICommand SaveProductCommand { get; }

    #region Properties
    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    private string _trackCode;
    public string TrackCode
    {
        get => _trackCode;
        set => SetProperty(ref _trackCode, value);
    }
    private double _price;
    public double Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }
    private double _weight;
    public double Weight
    {
        get => _weight;
        set => SetProperty(ref _weight, value);
    }
    private string _orderDate;
    public string OrderDate
    {
        get => _orderDate;
        set => SetProperty(ref _orderDate, value);
    }
    private byte _isDelivered;
    public byte IsDelivered
    {
        get => _isDelivered;
        set => SetProperty(ref _isDelivered, value);
    }
    private DateTime _minDate;
    public DateTime MinDate
    {
        get => _minDate;
        set => SetProperty(ref _minDate, value);
    }
    #endregion

    private async Task OnSaveProduct()
    {
        await _goodsDbService.SaveItemAsync(new Product
        {
            //ProductId = _productId,
            Name = this.Name,
            TrackCode = this.TrackCode,
            Price = this.Price,
            Weight = this.Weight,
            OrderDate = OrderDate,
            IsDelivered = this.IsDelivered,
        });
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}

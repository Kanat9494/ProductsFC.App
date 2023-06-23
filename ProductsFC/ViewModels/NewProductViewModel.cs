namespace ProductsFC.ViewModels;

public class NewProductViewModel : BaseViewModel
{
    public NewProductViewModel(GoodsDBService goodsDbService)
    {
        SaveProductCommand = new Command(async () => await OnSaveProduct());
        _goodsDbService = goodsDbService;
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
    private string _price;
    public string Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }
    private string _weight;
    public string Weight
    {
        get => _weight;
        set => SetProperty(ref _weight, value);
    }
    private DateTime _orderDate;
    public DateTime OrderDate
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
        var product = new Product
        {
            Name = this.Name,
            TrackCode = this.TrackCode,
            Price = this.Price,
            Weight = this.Weight,
            OrderDate = this.OrderDate,
            IsDelivered = this.IsDelivered,
        };

        await _goodsDbService.SaveItemAsync(product);
    }
}

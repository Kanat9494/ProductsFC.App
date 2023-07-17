namespace ProductsFC.ViewModels;

public class NewProductViewModel : BaseViewModel
{
    public NewProductViewModel(GoodsDBService goodsDbService)
    {
        SaveProductCommand = new Command(async () => await OnSaveProduct());
        UploadPhotoCommand = new Command(async () => await OnUploadPhoto());
        _goodsDbService = goodsDbService;
        MinDate = DateTime.Today;
    }

    private readonly GoodsDBService _goodsDbService;

    public ICommand SaveProductCommand { get; }
    public ICommand UploadPhotoCommand { get; }

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
    private double? _price;
    public double? Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }
    private double? _weight;
    public double? Weight
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
    private string _imageUrl;
    public string ImageUrl
    {
        get => _imageUrl;
        set => SetProperty(ref _imageUrl, value);
    }
    #endregion

    private async Task OnSaveProduct()
    {
        await _goodsDbService.SaveItemAsync(new Product
        {
            //ProductId = _productId,
            Name = this.Name,
            TrackCode = this.TrackCode,
            Price = this.Price ?? 0,
            Weight = this.Weight ?? 0,
            OrderDate = OrderDate,
            IsDelivered = this.IsDelivered,
            ImageUrl = this.ImageUrl,
        });
        await App.Current.MainPage.Navigation.PopModalAsync();
    }

    private async Task OnUploadPhoto()
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Выберите изображение",
            FileTypes = FilePickerFileType.Images,
        });

        if (result == null)
            return;

        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string imageFolderPath = Path.Combine(folderPath, "Images");
        string imageName = "uploaded_image_" + DateTime.UtcNow.ToString("ddMMM_hhmmss") + ".jpg";

        if (!Directory.Exists(imageFolderPath))
            Directory.CreateDirectory(imageFolderPath);

        var imagePath = Path.Combine(imageFolderPath, imageName);

        using (var fileStream = await result.OpenReadAsync())
        using (var destinationStream = File.Create(imagePath))
        {
            await fileStream.CopyToAsync(destinationStream);
        }

        ImageUrl = imagePath;
    }
}

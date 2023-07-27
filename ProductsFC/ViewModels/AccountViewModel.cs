namespace ProductsFC.ViewModels;

internal class AccountViewModel : BaseViewModel
{
    public AccountViewModel()
    {
        IsBusy = true;
        ArrivingProductsCommand = new Command(async () => await OnArrivingProducts());


        RefreshAccountInfo = new Command(() =>
        {
            Task.Run(async () =>
            {
                await InitializeUser();
            }).GetAwaiter().OnCompleted(() =>
            {
                IsRefreshing = false;
            });
        });
        ExitCommand = new Command(OnExit);
        ReportsCommand = new Command(async () => await OnReports());

        Task.Run(async () =>
        {
            await Task.Delay(500);
            await InitializeUser();
        });
    }

    public ICommand RefreshAccountInfo { get; set; }
    public ICommand ExitCommand { get; set; }
    public ICommand ReportsCommand { get; }
    public ICommand ArrivingProductsCommand { get; }


    private bool _isRefreshing;
    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }
    private string _userName;
    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }
    private string _profileImg;
    public string ProfileImg
    {
        get => _profileImg;
        set => SetProperty(ref _profileImg, value);
    }
    private double _userBalance;
    public double UserBalance
    {
        get => _userBalance;
        set => SetProperty(ref _userBalance, value);
    }
    private int _userId;
    public int UserId
    {
        get => _userId;
        set => SetProperty(ref _userId, value);
    }

    public async Task InitializeUser()
    {
        try
        {
            UserName = "Aika_pm";
            ProfileImg = "https://picsum.photos/id/1/200/300";
            UserBalance = 5000;
            UserId = 55;
        }
        catch
        {
            IsBusy = false;
        }
    }

    void OnExit()
        => System.Diagnostics.Process.GetCurrentProcess().Kill();

    private async Task OnReports()
        => await Shell.Current.GoToAsync("ReportsPage");

    async Task OnArrivingProducts()
        => await Shell.Current.GoToAsync("ArrivingProductsPage");
}

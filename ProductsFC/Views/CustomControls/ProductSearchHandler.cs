using ProductsFC.ViewModels;

namespace ProductsFC.Views.CustomControls;

internal class ProductSearchHandler : SearchHandler
{
    public IList<Product> Products { get; set; }
    public Type SelectedItemNavigationTarget { get; set; }

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);
        Products = MainViewModel.ProductsListToSearch;

        if (string.IsNullOrEmpty(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            ItemsSource = Products.Where(p => p.TrackCode.Contains(newValue)).ToList<Product>();
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        //await Task.Delay(1000);
        ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
        //await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}?{ProductDetailViewModel.ProductId}={((Product)item).ProductId}");
        await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}");

    }
}

    //string GetNavigationTarget()
    //{
    //    return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
    //}

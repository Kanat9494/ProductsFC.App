namespace ProductsFC;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        RegisterRoutingPages();
	}

    private void RegisterRoutingPages()
    {
        Routing.RegisterRoute("NewProductPage", typeof(NewProductPage));
        Routing.RegisterRoute("NewProductPage", typeof(ProductDetailPage));
    }
}

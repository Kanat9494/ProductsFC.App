using Microsoft.Extensions.Logging;
namespace ProductsFC;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseDevExpress()
			.UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<NewProductPage>();
        builder.Services.AddTransient<ReportsPage>();
        builder.Services.AddSingleton<GoodsDBService>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        Microsoft.Maui.Handlers.SearchBarHandler.Mapper.AppendToMapping("MyCustomizationSearchBar", (handler, view) =>
        {
#if ANDROID

            Android.Widget.LinearLayout linearLayout = handler.PlatformView.GetChildAt(0) as Android.Widget.LinearLayout;
            linearLayout = linearLayout.GetChildAt(2) as Android.Widget.LinearLayout;
            linearLayout = linearLayout.GetChildAt(1) as Android.Widget.LinearLayout;
            linearLayout.Background = null;
#endif
        });

        return builder.Build();
	}
}

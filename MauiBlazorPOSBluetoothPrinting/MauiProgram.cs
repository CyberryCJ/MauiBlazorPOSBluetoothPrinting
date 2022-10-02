using MauiBlazorPOSBluetoothPrinting.Class;
using MauiBlazorPOSBluetoothPrinting.Data;
using MauiBlazorPOSBluetoothPrinting.Platforms.Android;
using MudBlazor.Services;

namespace MauiBlazorPOSBluetoothPrinting;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		builder.Services.AddMudServices();
		builder.Services.AddSingleton<INativePages, NativePages>();
		builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}

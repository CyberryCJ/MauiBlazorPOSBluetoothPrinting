namespace MauiBlazorPOSBluetoothPrinting.Class
{
    public interface INativePages
    {
        Task<bool> StartActivityInPrinting(string printername, string datatoprint);

    }
}

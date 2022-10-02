using Android.Bluetooth;
using MauiBlazorPOSBluetoothPrinting.Class;

namespace MauiBlazorPOSBluetoothPrinting.Platforms.Android
{
    public class NativePages : INativePages
    {
        [Obsolete]
        public async Task<bool> StartActivityInPrinting(string printername, string datatoprint)
        {
            Print print = new Print();

            bool ss = false;

            if (BluetoothAdapter.DefaultAdapter != null && BluetoothAdapter.DefaultAdapter.IsEnabled)
            {
                foreach (var pairedDevice in BluetoothAdapter.DefaultAdapter.BondedDevices)
                {
                    // add validation to select InnerPrinter
                    if (pairedDevice.Address == "00:11:22:33:44:55")
                        printername = pairedDevice.Name;
                }

                if (printername != null)
                {
                    ss = await print.BluetoothPrinting(printername, datatoprint);

                }

            }
            return ss;
        }
    }
}

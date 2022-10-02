using Android.Bluetooth;
using Java.Util;
using System.Text;

namespace MauiBlazorPOSBluetoothPrinting.Platforms.Android
{
    public class Print
    {
        [Obsolete]
        public async Task<bool> BluetoothPrinting(string printername, string datatoprint)
        {
            bool done = false;
            var stream = new MemoryStream();


            using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
            {
                if (bluetoothAdapter == null)
                {

                    throw new Exception("No default adapter");
                    //return;
                }

                if (!bluetoothAdapter.IsEnabled)
                {

                    throw new Exception("Bluetooth not enabled");
                }

                BluetoothDevice device = (from bd in bluetoothAdapter.BondedDevices
                                          where bd.Name == printername
                                          select bd).FirstOrDefault();

                if (device == null) { throw new Exception($"{printername}" + " device not found."); }

                try
                {
                    using (BluetoothSocket _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                    {

                        await _socket.ConnectAsync();

                        string ESC = Convert.ToString((char)27);
                        string GS = Convert.ToString((char)29);



                        string center = ESC + "a" + (char)1; //align center
                        string left = ESC + "a" + (char)0; //align left
                        string right = ESC + "a" + (char)2; //align left
                        string bold_on = ESC + "E" + (char)1; //turn on bold mode
                        string bold_off = ESC + "E" + (char)0; //turn off bold mode
                        string bigSize = ESC + "!" + (char)255;
                        string smallSize = ESC + "!" + (char)0;
                        string cut = ESC + "d" + (char)1 + GS + "V" + (char)66; //add 1 extra line before partial cut


                        string initp = ESC + (char)64; //initialize printer
                        string buffer = ""; //store all the data that want to be printed



                        buffer += "-------------------------------\n";
                        buffer += center;
                        buffer += bigSize;
                        buffer += bold_on;
                        buffer += $"HELLO WORLD!\n";
                        buffer += $"{datatoprint}\n";
                        buffer += bold_off;
                        buffer += smallSize;
                        buffer += left;
                        buffer += "********************************\n";



                        //--------------------------------



                        buffer += smallSize;
                        buffer += "Jerry O. Mates Jr.";
                        buffer += smallSize;
                        buffer += "\n";
                        buffer += "\n";
                        buffer += "\n";
                        buffer += "\n";



                        byte[] message = Encoding.ASCII.GetBytes(buffer);
                        await _socket.OutputStream.WriteAsync(message, 0, message.Length);
                        _socket.OutputStream.WriteByte(0x0A);



                        Java.Lang.Thread.Sleep(2000);




                        _socket.Close();
                        done = true;
                    }
                }
                catch (Exception exp)
                {
                    done = false;
                    throw exp;
                }



            }




            return done;
        }
    }
}

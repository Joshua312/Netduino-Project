using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication6
{
    using System.Text.RegularExpressions;

    using Microsoft.SPOT.Net.NetworkInformation;

    using NetMf.CommonExtensions;

    public class Program
    {
        public static void Main()
        {
            // write your code here
            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);

            int port = 8080;

            Thread.Sleep(5000);

            NetworkInterface networkInterface = NetworkInterface.GetAllNetworkInterfaces()[0];

            Debug.Print("my ip address: " + networkInterface.IPAddress.ToString());

            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint listenerEndPoint = new IPEndPoint(IPAddress.Any, port);

            listenerSocket.Bind(listenerEndPoint);
            listenerSocket.Listen(1);

            while (true)
            {
                Socket clientSocket = listenerSocket.Accept();

                bool dataReady = clientSocket.Poll(5000000, SelectMode.SelectRead);


                if (dataReady && clientSocket.Available > 0)
                {
                    byte[] buffer = new byte[clientSocket.Available];
                    int bytesRead = clientSocket.Receive(buffer);

                    string request = new string(System.Text.Encoding.UTF8.GetChars(buffer));

                    var regex = new Regex(@"\b\s/(\w*)\b");
                    var match = regex.Match(request);

                    if (match.Groups[0].Value.Trim() == "/ON")
                    {
                        led.Write(true);
                    }
                    else if (match.Groups[0].Value.Trim() == "/OFF")
                    {
                        led.Write(false);
                    }

                    //if (request.IndexOf("ON") >= 0)
                    //{
                    //    led.Write(true);
                    //}
                    //else if (request.IndexOf("OFF") >= 0)
                    //{
                    //    led.Write(false);
                    //}

                    string statusText = (led.Read() ? "ON" : "OFF");

                    string response = "HTTP/1.1 200 OK\r\n" + "Content-Type: text/html; charset=utf-8\r\n\r\n"
                                      + StringUtility.Format(GetHtml(), statusText);

                    clientSocket.Send(System.Text.Encoding.UTF8.GetBytes(response));
                }

                clientSocket.Close();
            }
        }

        private static string GetHtml()
        {
            return Markup.GetString(Markup.StringResources.Html);
        }
    }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication8
{
    public class BlinkingLed
    {
        public static void Main()
        {
            var ledPort = new OutputPort(Pins.ONBOARD_LED, false);

            while (true)
            {
                ledPort.Write(true);
                Thread.Sleep(500);

                ledPort.Write(false);
                Thread.Sleep(500);
            }


        }

    }
}

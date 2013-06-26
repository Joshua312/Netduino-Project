using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication9
{
    public class LightSwitch
    {
        public static void Main()
        {
            var switchPort = new InputPort(Pins.ONBOARD_SW1, false, Port.ResistorMode.Disabled);
            var ledPort = new OutputPort(Pins.ONBOARD_LED, false);

            while (true)
            {
                bool isClosed = switchPort.Read();
                if (isClosed)
                {
                    ledPort.Write(true);
                }
                else
                {
                    ledPort.Write(false);
                }
                Thread.Sleep(100);
            }
          }

    }
 }

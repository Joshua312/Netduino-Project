using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Voltage_Reader
{
    public class VoltageReader
    {
        public static void Main()
        {

            const double maxVoltage = 3.3;
            const int maxAdcValue = 1023;

            var voltagePort = new AnalogInput(Pins.GPIO_PIN_A1);
            var lowPort = new OutputPort(Pins.GPIO_PIN_A0, false);
            var highPort = new OutputPort(Pins.GPIO_PIN_A2, true);

            while (true)
            {
                int rawValue = voltagePort.Read();
                double value = (rawValue * maxVoltage) / maxAdcValue;
                Debug.Print(rawValue + " " + value.ToString("f"));
            }
        }

    }
}

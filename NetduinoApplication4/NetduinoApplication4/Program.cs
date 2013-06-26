using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication4
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            PWM led = new PWM(Pins.GPIO_PIN_D5);
            AnalogInput pot = new AnalogInput(Pins.GPIO_PIN_A0);
            pot.SetRange(0, 100);
            int potValue = 0;

            while (true)
            {
                potValue = pot.Read();
                led.SetDutyCycle((unit)potValue);
            }
        }

    }
}

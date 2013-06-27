namespace HelloPachube
{
    using System;
    using System.Threading;

    using Endjin.XivleyClient;

    using Microsoft.SPOT;
    using Microsoft.SPOT.Hardware;
    using SecretLabs.NETMF.Hardware.Netduino;

    public class HelloPachube
    {
        public static void Main()
        {
            const string ApiKey = "FHowwfdHmgYTCvyfYMilfzIN52OwdkGc1ZmcUPfyfKjbMElQ";
            const string FeedId = "620930332";
            const int SamplingPeriod = 20000;

            const double MaxVoltage = 3.3;
            const int MaxAdcValue = 1023;

            var voltagePort = new AnalogInput(new Cpu.AnalogChannel());

            try
            {
                var lowPort = new OutputPort(Pins.GPIO_PIN_A0, false);
                var highPort = new OutputPort(Pins.GPIO_PIN_A2, true);
            }
            catch (Exception exception)
            {
                Debug.Print(exception.ToString());    
            }

            while (true)
            {
                WaitUntilNextPeriod(SamplingPeriod);
                double rawValue = voltagePort.Read();
                double value = (rawValue * MaxVoltage) / MaxAdcValue;
                string sample = "{ \"voltage\":\"" + value.ToString("f") + "\"}";
                Debug.Print("new message:  " + sample);
                XivleyClient.Send("FHowwfdHmgYTCvyfYMilfzIN52OwdkGc1ZmcUPfyfKjbMElQ", "620930332", sample);
            }
        }

        private static void WaitUntilNextPeriod(int period)
        {
            long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var offset = (int)(now % period);
            int delay = period - offset;
            Debug.Print("sleep for " + delay + "ms\r\n");
            Thread.Sleep(delay);
        }
    }
}

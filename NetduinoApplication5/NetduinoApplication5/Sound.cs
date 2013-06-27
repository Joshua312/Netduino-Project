using System.Threading;

using Microsoft.SPOT.Hardware;

using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication5
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            var scale = new System.Collections.Hashtable
            {
                { "c", 1915u },
                { "d", 1700u },
                { "e", 1519u },
                { "f", 1432u },
                { "g", 1275u },
                { "a", 1136u },
                { "b", 1014u },
                { "C", 956u },
                { "D", 851u },
                { "E", 758u },
                { "h", 0u }
            };

            int beatsPerMinute = 90;
            int beatTimeInMilliseconds = 6000 / beatsPerMinute;
            int pauseTimeInMillisenconds = (int)(beatTimeInMilliseconds * 0.1);

            string song = "C1C1C1g1a1a1g2E1E1D1D1C2";

            PWM speaker = new PWM(PWMChannels.PWM_PIN_D5, 100, .5, false);
            speaker.Start();

            for (int i = 0; i < song.Length; i += 2)
            {
                string note = song.Substring(i, 1);
                int beatCount = int.Parse(song.Substring(i + 1, 1));

                uint noteDuration = (uint)scale[note];
                speaker.Duration = noteDuration;
                speaker.Period = (noteDuration * 2);
                
                Thread.Sleep(beatTimeInMilliseconds * beatCount - pauseTimeInMillisenconds);
                speaker.DutyCycle = 0;
                Thread.Sleep(pauseTimeInMillisenconds);
            }

            speaker.Stop();
        
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
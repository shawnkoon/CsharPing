using System;
using System.Threading;
using System.Net.NetworkInformation;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            for(int i = 1; i <= 20; i++)
            {
                Ping ping = new Ping();

                string webToPing = "tistory.co.kr";

                string msg = "Pinging attempt " + i + " ["+webToPing+"]";

                try
                {
                    long rtt = ping.SendPingAsync(webToPing).Result.RoundtripTime;
                    if(rtt > 80)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(msg + " RTT = " + rtt.ToString() + "ms");
                        Console.ResetColor();
                    }
                    else if(rtt > 50 && rtt <= 80)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(msg + " RTT = " + rtt.ToString() + "ms");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(msg + " RTT = " + rtt.ToString() + "ms");
                        Console.ResetColor();
                    }
                }
                catch
                {
                    Console.WriteLine(msg + "* * * * 0ms");
                }

                Thread.Sleep(1000);
            }

        }
    }
}

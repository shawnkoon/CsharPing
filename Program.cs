using System;
using System.Threading;
using System.Net.NetworkInformation;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int maxPing = -1;
            int minPing = -1;
            int totalPing = 0;
            int numTimesPinged = 0;

            for(int i = 1; i <= 40; i++)
            {
                Ping ping = new Ping();

                string webToPing = "tistory.co.kr";

                string msg = "Pinging attempt " + i + " ["+webToPing+"]";

                numTimesPinged = i;

                try
                {
                    int rtt = (Int32)ping.SendPingAsync(webToPing).Result.RoundtripTime;

                    if(maxPing == -1 || minPing == -1)
                    {
                        maxPing = rtt;
                        minPing = rtt;
                    }

                    totalPing +=  rtt;

                    maxPing = checkforMax(rtt,maxPing);
                    minPing = checkforMin(rtt,minPing);

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


                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Max Ping: "+maxPing+"\tMin Ping: "+minPing+"\tCur Ping: "+rtt+"\tAvg Ping: "+(totalPing/numTimesPinged));
                    Console.ResetColor();


                }
                catch
                {
                    Console.WriteLine(msg + "* * * * 0ms");
                }

                


                Thread.Sleep(1000);
            }

        }// End of Main.

        public static int checkforMax(int rtt, int oldMax)
        {
            int newMax;

            if(rtt > oldMax)
            {
                newMax = rtt;
            }
            else
            {
                newMax = oldMax;
            }

            return newMax;
        }// End of checkforMax

        public static int checkforMin(int rtt, int oldMin)
        {
            int newMin;

            if(rtt < oldMin)
            {
                newMin = rtt;
            }
            else
            {
                newMin = oldMin;
            }

            return newMin;
        }// End of checkforMin


    }
}

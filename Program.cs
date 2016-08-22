using System;
using System.Threading;
using System.Net.NetworkInformation;

namespace ConsoleApplication
{
    public class Program
    {
        private static string webToPing = "google.com";

        public static void Main(string[] args)
        {
            int maxPing = -1;
            int minPing = -1;
            int totalPing = 0;
            int numTimesPinged = 0;

            // Can be changed to whilte(true) for infinit iteration.o
            for(int i = 1; i <= 40; i++)
            {
                Ping ping = new Ping();

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

                    updateView(maxPing, minPing, (totalPing/numTimesPinged), rtt);

                    // if(rtt > 80)
                    // {
                    //     Console.ForegroundColor = ConsoleColor.Red;
                    //     Console.WriteLine(msg + " RTT = " + rtt.ToString() + "ms");
                    //     Console.ResetColor();
                    // }
                    // else if(rtt > 50 && rtt <= 80)
                    // {
                    //     Console.ForegroundColor = ConsoleColor.Yellow;
                    //     Console.WriteLine(msg + " RTT = " + rtt.ToString() + "ms");
                    //     Console.ResetColor();
                    // }
                    // else
                    // {
                    //     Console.ForegroundColor = ConsoleColor.Green;
                    //     Console.WriteLine(msg + " RTT = " + rtt.ToString() + "ms");
                    //     Console.ResetColor();
                    // }


                    // Console.ForegroundColor = ConsoleColor.Blue;
                    // Console.WriteLine("Max Ping: "+maxPing+"\tMin Ping: "+minPing+"\tCur Ping: "+rtt+"\tAvg Ping: "+(totalPing/numTimesPinged));
                    // Console.ResetColor();


                }
                catch
                {
                    Console.WriteLine(msg + "* * * * 0ms");
                }




                Thread.Sleep(1500);
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

        public static void updateView(int maxPing, int minPing, int avgPing, int curPing)
        {
            for(int i = 0; i < 39; i++)
                Console.WriteLine("");
            
            Console.WriteLine("\t\t\t     Pinging attempt [ "+webToPing+" ]");
            Console.Write("\t\t\tMax : ");
            writeColoredNumber(maxPing);
            Console.Write(" | Min : ");
            writeColoredNumber(minPing);
            Console.Write(" | Avg : ");
            writeColoredNumber(avgPing);
            Console.Write(" | Cur : ");
            writeColoredNumber(curPing);

            Console.WriteLine("");

            for(int i = 0; i < (49 +44); i++)
                Console.Write("-");

            Console.WriteLine();

            for(int row = 0; row < 25; row++)
            {
                Console.Write("| ");
                //printGraphLine(row);
                Console.Write("# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #");
                Console.Write(" |\n");
            }

            for(int i = 0; i < (49 +44); i++)
                Console.Write("-");
            
            Console.WriteLine("");

        }// End of updateView

        //Helper method that will write a string to a console in diff color based on the time.
        public static void writeColoredNumber(int pingRTT)
        {
            if(pingRTT > 80)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(pingRTT+"");
                Console.ResetColor();
            }
            else if(pingRTT > 50 && pingRTT <= 80)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(pingRTT+"");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(pingRTT+"");
                Console.ResetColor();
            }
        }
    }
}

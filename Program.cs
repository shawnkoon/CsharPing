using System;
using System.Threading;
using System.Net.NetworkInformation;

using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        private static string UNIT = "#", BLANK = " ";
        private static List<List<string>> graphLines;
        private static int height = 25;
        private static int width = 45;
        private static string webToPing = "tistory.co.kr";


        public static void Main(string[] args)
        {
            int maxPing = -1;
            int minPing = -1;
            int totalPing = 0;
            int numTimesPinged = 0;

	        InitializeList();


            // Can be changed to whilte(true) for infinit iteration.o
            for(int i = 1; i <= 100; i++)
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

		            BuildGraph(maxPing, minPing, (totalPing/numTimesPinged), rtt);

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
                catch(Exception exp)
                {
                    Console.WriteLine(msg + "* * * * 0ms");
		    Console.WriteLine(exp);
                }



                Thread.Sleep(1500);
            }

        }// End of Main.

	public static void BuildGraph(int maxPing, int minPing, int avgPing, int curPing)
	{
		int num = curPing/5 + 1;
		for(int i = 0; i < graphLines.Count; i++)
		{
			if(i >= graphLines.Count - num)
				graphLines[i].Insert(0, UNIT);
			else
				graphLines[i].Insert(0, BLANK);
            graphLines[i].RemoveAt(graphLines[i].Count-1);
			
		}
	}//End BuildGraph

	public static string PrintGraphLine(List<string> row)
	{
		string word = "";
		foreach(string unit in row)
		{
			word+=unit + " ";
		}
		return word;
	}//End PrintGraphLine


	//Initializes graphLines to be full of blank spaces
	public static void InitializeList()
	{
		graphLines = new List<List<string>>();
		for(int i = 0; i < height; i++)
		{
			graphLines.Add(new List<string>());

			for(int j = 0; j < width; j++)
				graphLines[i].Add(" ");
		}
	}// End of FillList

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
                if(row < 8)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(PrintGraphLine(graphLines[row]));
                    Console.ResetColor();
                }
                else if(row >= 8 && row < 16)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(PrintGraphLine(graphLines[row]));
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(PrintGraphLine(graphLines[row]));
                    Console.ResetColor();
                }
                Console.Write("|\n");
            }

            for(int i = 0; i < (49 +44); i++)
                Console.Write("-");
            
            Console.WriteLine("");

        }// End of updateView

        //Helper method that will write a string to a console in diff color based on the time.
        public static void writeColoredNumber(int pingRTT)
        {
            if(pingRTT > 84)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(pingRTT+"");
                Console.ResetColor();
            }
            else if(pingRTT > 44 && pingRTT <= 84)
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

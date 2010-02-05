using System;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace NetTunnel
{
    class Client
    {
        private static ASCIIEncoding encoding = new ASCIIEncoding();

        private static AutoResetEvent inputRead = new AutoResetEvent(false);
        private static AutoResetEvent readyForInput = new AutoResetEvent(false);
        private static volatile string inputLine;
        private static volatile bool exit = false;

        static void readInput()
        {
            while (!exit)
            {                
                inputLine = Console.ReadLine().Trim();
                inputRead.Set();
                readyForInput.WaitOne();
            }
        }

        static void Main(string[] args)
        {
            // Create thread to read from input
            var input_thread = new Thread(readInput);
            input_thread.Start();

            using (var client = new TcpClient( "localhost", Protocol.PORT ) )
            using (var n = client.GetStream())
            {                
                var buffer = new byte[64];

                while (!exit)
                {
                    if (n.DataAvailable)
                    {
                        n.Read(buffer, 0, buffer.Length);
                        Console.WriteLine(encoding.GetString(buffer));
                    }

                    if (inputRead.WaitOne(100)) // Wait 100 ms for input to be read
                    {
                        // There's something waiting for us, process it...
                        string op;
                        int space_index = inputLine.IndexOf(' ');
                        if (space_index >= 0) // Was found
                        {
                            op = inputLine.Substring(0, space_index);
                            inputLine = inputLine.Substring(space_index + 1);
                        }
                        else
                        {
                            op = inputLine; // Just a straight copy
                            inputLine = "";
                        }

                        switch (op)
                        {
                            case "c":
                                n.Write(encoding.GetBytes(inputLine), 0, inputLine.Length);
                                n.Flush();
                                break;

                            case "exit":
                            case "quit":
                                exit = true;                                
                                break;

                            default:
                                Console.WriteLine("Unrecognized input");
                                break;
                        }

                        readyForInput.Set();
                    }
                }
            }
        }

    }
}

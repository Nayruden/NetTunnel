using System;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization;

namespace NetTunnel
{
    class Client
    {
        private static ASCIIEncoding encoding = new ASCIIEncoding();

        private static AutoResetEvent inputRead = new AutoResetEvent(false);
        private static AutoResetEvent readyForInput = new AutoResetEvent(false);

        private static AutoResetEvent streamRead = new AutoResetEvent(false);
        private static AutoResetEvent readyForStream = new AutoResetEvent(false);

        private static volatile string inputLine;
        private static volatile byte[] buffer = new byte[Protocol.MAX_MESSAGE_SIZE];
        private static volatile int buffer_length;
        private static volatile NetworkStream stream;
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

        static void readStream()
        {
            using (var client = new TcpClient("localhost", Protocol.PORT))
            using (stream = client.GetStream())            
            {
                try
                {
                    while (!exit)
                    {
                        Array.Clear(buffer, 0, buffer.Length);
                        buffer_length = stream.Read(buffer, 0, buffer.Length);
                        if (buffer_length > 0) // Data available
                        {
                            var ds = new DataContractSerializer(typeof(Message));
                            Message m;
                            using (var memory_stream = new MemoryStream(buffer, 0, buffer_length))
                                m = (Message)ds.ReadObject(memory_stream);

                            m.executeLogic();
                        }
                        else // Disconnect
                        {
                            Console.WriteLine("Server left");
                            exit = true;
                        }
                        streamRead.Set();
                        readyForStream.WaitOne();
                    }
                }
                catch (IOException exception) // This is thrown when we get rid of the stream object so this thread can exit
                    // Also thrown when server leaves
                {
                    exit = true;
                }
            }

        }

        static void Main(string[] args)
        {
            // Create thread to read from input
            var input_thread = new Thread(readInput);
            input_thread.Start();

            // Create thread to read from stream
            var stream_thread = new Thread(readStream);
            stream_thread.Start();

            while (!exit)
            {
                if (streamRead.WaitOne(100)) // Wait 100 ms for stream to be read
                {
                    string str = encoding.GetString(buffer, 0, buffer_length);
                    Console.WriteLine(str);
                    readyForStream.Set();
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
                            stream.Write(encoding.GetBytes(inputLine), 0, inputLine.Length);
                            stream.Flush();
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

            stream.Dispose(); // So the stream thread can stop blocking!
        }

    }
}

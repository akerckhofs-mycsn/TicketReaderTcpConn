using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TicketReaderTcpConn {
    class Program {
        static void Main(string[] args) {
            using var client = new TcpClient();
            client.Connect("127.0.0.1", 2005);

            using var stream = client.GetStream();

            while (true) {
                if (stream.CanRead) {
                    var buffer = new byte[64];
                    var barcode = new StringBuilder();

                    // Incoming message may be larger than the buffer size.
                    do {
                        var read = stream.Read(buffer, 0, buffer.Length);
                        barcode.AppendFormat("{0}",
                            Encoding.ASCII.GetString(buffer, 0, read));
                    } while (stream.DataAvailable);
                    
                    Console.WriteLine($"Barcode: {barcode}");
                }
                else {
                    Console.WriteLine("Stream unreadable.");
                }

                // For obvious reasons, don't use Thread.Sleep() in async code but instead use Task.Delay()
                Thread.Sleep(10);
            }
        }
    }
}
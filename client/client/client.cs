using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client {
    class client {
        private Socket sender;
        private byte[] bytes;
        
        public client() {
            bytes = new byte[1024];
        }

        public void send(byte[] msg) {
            int bytesSent = sender.Send(msg);
        }

        public void closeConnection() {
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        public string recive() {
            int bytesRec = sender.Receive(bytes);
            return Encoding.ASCII.GetString(bytes, 0, bytesRec);
        }

        public void StartClient(string newHost, int port) {
            IPHostEntry host = Dns.GetHostEntry(newHost);
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
            try {
                sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(remoteEP);

            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

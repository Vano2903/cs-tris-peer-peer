using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client {
    class server {
        private Socket listener;
        private Socket handler;

        public void send(byte[] msg) {
            handler.Send(msg);
        }

        public string recive() {
            byte[] bytes = null;

            string data = null;
            while (true) {
                bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1) {
                    break;
                }
            }
            return data;
        }

        public void closeServer() {
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }

        public void StartServer(string newHost, int port) {
            IPHostEntry host = Dns.GetHostEntry(newHost);
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            try {
                listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");
                handler = listener.Accept();
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

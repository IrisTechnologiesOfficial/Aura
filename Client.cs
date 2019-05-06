//Copyright 2019 Iris Technologies, All Rights Reserved
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace AuraEngine
{
    internal static class Client
    {
        private enum NetworkResult : byte
        {
            ClientSuccessfullyExited,
            ClientCannotSendResponse,
            ClientCannotReceiveResponse,
            ClientCannotSendNumberOfPackages,
            ClientCannotReceiveNumberOfPackages,
            ClientCannotSendNumberOfBytesOfLastPackage,
            ClientCannotReceiveNumberOfBytesOfLastPackage,
            ClientCannotSendPackage,
            ClientCannotReceivePackage,
            ClientCannotSendLastPackage,
            ClientCannotReceiveLastPackage
        }

        internal static bool CheckConnection()
        {
            Ping check = new Ping();
            PingReply pr = check.Send("31.13.72.36");
            if (pr != null)
                return true;
            else
            {
                pr = check.Send("104.244.42.65");
                if (pr != null)
                    return true;
                else
                {
                    pr = check.Send("216.58.207.238");
                    if (pr != null)
                        return true;
                    return false;
                }
            }
        }

        private static void _Client(ref Socket client, ref string text, out string result, out NetworkResult nr)
        {
            result = "";
            nr = NetworkResult.ClientCannotSendNumberOfPackages;
            try
            {
                int NumberOfPackages = text.Length / 1024;
                if (client.Send(Utilities.IntToBytes(NumberOfPackages)) == 4)
                {
                    byte[] Auxiliar = new byte[1];
                    nr = NetworkResult.ClientCannotReceiveResponse;
                    if (client.Receive(Auxiliar) == 1)
                    {
                        int NumberOfBytesOfLastPackage = text.Length - (NumberOfPackages * 1024);
                        nr = NetworkResult.ClientCannotSendNumberOfBytesOfLastPackage;
                        if (client.Send(Utilities.IntToBytes(NumberOfBytesOfLastPackage)) == 4)
                        {
                            nr = NetworkResult.ClientCannotReceiveResponse;
                            if (client.Receive(Auxiliar) == 1)
                            {
                                int PackageOffset = 0;
                                byte[] Package = new byte[1024];
                                nr = NetworkResult.ClientCannotSendPackage;
                                for (int PackageID = 0; PackageID < NumberOfPackages; PackageID++)
                                {
                                    if (client.Send(Encoding.ASCII.GetBytes(text.Substring(PackageOffset, 1024))) == 1024)
                                    {
                                        nr = NetworkResult.ClientCannotReceiveResponse;
                                        if (client.Receive(Auxiliar) == 1)
                                        {
                                            nr = NetworkResult.ClientCannotSendPackage;
                                            PackageOffset += 1024;
                                        }
                                        else return;
                                    }
                                    else return;
                                }
                                nr = NetworkResult.ClientCannotSendLastPackage;
                                if (client.Send(Encoding.ASCII.GetBytes(text.Substring(PackageOffset))) == NumberOfBytesOfLastPackage)
                                {
                                    nr = NetworkResult.ClientCannotReceiveResponse;
                                    if (client.Receive(Auxiliar) == 1)
                                    {
                                        nr = NetworkResult.ClientCannotSendResponse;
                                        if (client.Send(Auxiliar) == 1)
                                        {
                                            nr = NetworkResult.ClientCannotReceiveNumberOfPackages;
                                            byte[] NumberOfPackets = new byte[4];
                                            if (client.Receive(NumberOfPackets) == 4)
                                            {
                                                nr = NetworkResult.ClientCannotSendResponse;
                                                if (client.Send(Auxiliar) == 1)
                                                {
                                                    nr = NetworkResult.ClientCannotReceiveNumberOfBytesOfLastPackage;
                                                    NumberOfPackages = Utilities.BytesToInt(NumberOfPackets);
                                                    if (client.Receive(NumberOfPackets) == 4)
                                                    {
                                                        nr = NetworkResult.ClientCannotSendResponse;
                                                        if (client.Send(Auxiliar) == 1)
                                                        {
                                                            nr = NetworkResult.ClientCannotReceivePackage;
                                                            Database.NetworkExpectedSize = NumberOfPackages + 1;
                                                            for (int PackageID = 0; PackageID < NumberOfPackages; PackageID++)
                                                            {
                                                                if (client.Receive(Package) == 1024)
                                                                {
                                                                    nr = NetworkResult.ClientCannotSendResponse;
                                                                    if (client.Send(Auxiliar) == 1)
                                                                    {
                                                                        nr = NetworkResult.ClientCannotReceivePackage;
                                                                        result += Encoding.ASCII.GetString(Package);
                                                                        Database.NetworkSize++;
                                                                    }
                                                                    else return;
                                                                }
                                                                else return;
                                                            }
                                                            nr = NetworkResult.ClientCannotReceiveLastPackage;
                                                            int BytesOfLastPackage = Utilities.BytesToInt(NumberOfPackets);
                                                            byte[] LastPackage = new byte[BytesOfLastPackage];
                                                            if (client.Receive(LastPackage) == BytesOfLastPackage)
                                                            {
                                                                result += Encoding.ASCII.GetString(LastPackage);
                                                                nr = NetworkResult.ClientSuccessfullyExited;
                                                                Database.NetworkSize++;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SocketException) { }
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        private static string NetworkLog(NetworkResult nr, ref string result)
        {
            switch (nr)
            {
                case NetworkResult.ClientSuccessfullyExited:
                    Database.NetworkResult = result;
                    Database.NetworkDone = true;
                    return "Done!!!";
                case NetworkResult.ClientCannotReceiveLastPackage:
                    return "Client did not receive last package!";
                case NetworkResult.ClientCannotReceiveNumberOfBytesOfLastPackage:
                    return "Client did not receive the number of bytes of the last package!";
                case NetworkResult.ClientCannotReceiveNumberOfPackages:
                    return "Client did not receive the number of packages!";
                case NetworkResult.ClientCannotReceivePackage:
                    return "Client did not receive a package!";
                case NetworkResult.ClientCannotReceiveResponse:
                    return "Client did not receive a response from server!";
                case NetworkResult.ClientCannotSendLastPackage:
                    return "Client did not send the last package!";
                case NetworkResult.ClientCannotSendNumberOfBytesOfLastPackage:
                    return "Client did not send the number of bytes of the last package!";
                case NetworkResult.ClientCannotSendNumberOfPackages:
                    return "Client did not send the number of packages!";
                case NetworkResult.ClientCannotSendPackage:
                    return "Client did not send a package!";
                case NetworkResult.ClientCannotSendResponse:
                    return "Client did not send a response to the server!";
            }
            return "";
        }

        internal static void Send(string text)
        {
            if (Database.ClientDone)
            {
                Database.ClientDone = false;
                Thread t = new Thread(() =>
                {
                    Socket client = null;
                    try
                    {
                        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        client.NoDelay = true;
                        client.Connect(new IPEndPoint(IPAddress.Parse("??"), ??));
                        string Result;
                        NetworkResult nr;
                        _Client(ref client, ref text, out Result, out nr);
                        NetworkLog(nr, ref Result);
                    }
                    catch (SocketException) { }
                });
                t.IsBackground = true;
                t.Start();
            }
        }

        internal static void Flush()
        {
            Database.NetworkResult = "";
            Database.NetworkDone = false;
            Database.NetworkExpectedSize = 0;
            Database.NetworkSize = 0;
            Database.ClientDone = true;
        }
    }
}

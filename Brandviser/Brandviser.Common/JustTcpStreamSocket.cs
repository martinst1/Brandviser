using System.Net.Sockets;
using Brandviser.Common.Contracts;

namespace Brandviser.Common
{
    public class JustTcpStreamSocket : Socket, ISocket
    {
        public JustTcpStreamSocket() : base(SocketType.Stream, ProtocolType.Tcp)
        {

        }
    }
}

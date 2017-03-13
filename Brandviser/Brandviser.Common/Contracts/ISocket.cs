using System;

namespace Brandviser.Common.Contracts
{
    public interface ISocket : IDisposable
    {
        void Connect(string host, int port);

        int Send(byte[] bytes);

        int Receive(byte[] bytes);
    }
}

using System.Text;
using Brandviser.Common.Contracts;

namespace Brandviser.Tests.Utils.WhoisTests
{
    internal class FakeSocket : ISocket
    {
        private string expectedMessage;

        public FakeSocket(string expectedMessage)
        {
            this.expectedMessage = expectedMessage;
        }
        public void Connect(string host, int port)
        {
            
        }

        public void Dispose()
        {
            
        }

        public int Receive(byte[] bytes)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(expectedMessage);
            for (int i = 0; i < expectedBytes.Length; i++)
            {
                bytes[i] = expectedBytes[i];
            }
            return 0;
        }

        public int Send(byte[] bytes)
        {
            return 0;
        }
    }
}

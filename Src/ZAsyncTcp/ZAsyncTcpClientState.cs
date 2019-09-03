using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ZAsyncTcp
{
    public class ZAsyncTcpClientState
    {
        /// <summary>
        /// 与客户端相关的TcpClient
        /// </summary>
        public TcpClient Client { get; private set; }

        /// <summary>
        /// 获取缓冲区
        /// </summary>
        public byte[] Buffer { get; private set; }

        /// <summary>
        /// 获取网络流
        /// </summary>
        public NetworkStream Stream
        {
            get { return Client.GetStream(); }
        }

        public ZAsyncTcpClientState(TcpClient client, byte[] buffer)
        {
            if (client == null)
                throw new ArgumentNullException("tcpClient");
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            Client = client;
            Buffer = buffer;
        }
        
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            //关闭数据的接受和发送
            Client.Close();
            Buffer = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ZAsyncTcp
{
    public class ZAsyncTcpServer
    {
        /// <summary>
        /// 服务器使用的异步TcpListener
        /// </summary>
        TcpListener _tcpListener;

        /// <summary>
        /// 客户端会话列表
        /// </summary>
        List<object> _tcpClientStateList;

        /// <summary>
        /// 服务器是否运行中
        /// </summary>
        public bool IsRunning { get; private set; }
        /// <summary>
        /// 监听的IP地址
        /// </summary>
        public IPAddress Address { get; private set; }
        /// <summary>
        /// 监听的端口
        /// </summary>
        public int Port { get; private set; }

        private bool disposed = false;

        #region 构造函数

        /// <summary>
        /// 异步 TCP 服务器
        /// </summary>
        /// <param name="port">监听端口</param>
        public ZAsyncTcpServer(int port) 
            : this(IPAddress.Any, port)
        {
            
        }

        /// <summary>
        /// 异步 TCP 服务器
        /// </summary>
        /// <param name="ip">监听 IP</param>
        /// <param name="port">监听端口</param>
        public ZAsyncTcpServer(IPAddress ip, int port)
            : this(new IPEndPoint(ip, port))
        {

        }

        /// <summary>
        /// 异步 TCP 服务器
        /// </summary>
        /// <param name="endPoint">监听终结点</param>
        public ZAsyncTcpServer(IPEndPoint endPoint)
        {
            _tcpClientStateList = new List<object>();

            Address = endPoint.Address;
            Port = endPoint.Port;

            _tcpListener = new TcpListener(endPoint);
            _tcpListener.AllowNatTraversal(true);
        }

        #endregion

        /// <summary>
        /// 启动服务器
        /// </summary>
        public void Start()
        {
            if (!IsRunning)
            {
                _tcpListener.Start();
                IsRunning = true;
                _tcpListener.BeginAcceptTcpClient(BeginAcceptTcpClientHandler, _tcpListener);
            }
        }

        /// <summary>
        /// 处理客户端连接的函数
        /// </summary>
        /// <param name="ar"></param>
        private void BeginAcceptTcpClientHandler(IAsyncResult ar)
        {
            TcpListener tcpListener = (TcpListener)ar.AsyncState;
            TcpClient tcpClient = tcpListener.EndAcceptTcpClient(ar);

            byte[] buffer = new byte[tcpClient.ReceiveBufferSize];

            ZAsyncTcpClientState clientState = new ZAsyncTcpClientState(tcpClient, buffer);
            _tcpClientStateList.Add(clientState);
            RaiseClientConnected(clientState);

            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.BeginRead(clientState.Buffer, 0, clientState.Buffer.Length, BeginReadHandler, clientState);

            tcpListener.BeginAcceptTcpClient(BeginAcceptTcpClientHandler, tcpListener);
        }

        /// <summary>
        /// 数据接受回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void BeginReadHandler(IAsyncResult ar)
        {
            int numberOfReadBytes = 0;
            ZAsyncTcpClientState clientState = (ZAsyncTcpClientState)ar.AsyncState;
            NetworkStream networkStream = clientState.Client.GetStream();
            numberOfReadBytes = networkStream.EndRead(ar);
            if (numberOfReadBytes > 0)
            {
                //byte[] buffer = new byte[numberOfReadBytes];
                //Buffer.BlockCopy(clientState.Buffer, 0, buffer, 0, numberOfReadBytes);
                RaiseDataReceived(clientState);
            }

            networkStream.BeginRead(clientState.Buffer, 0, clientState.Buffer.Length, BeginReadHandler, clientState);
        }

        #region 发送

        /// <summary>
        /// 异步发送数据至指定的客户端
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="data">报文</param>
        public void Send(TcpClient client, string data)
        {
            Send(client, Encoding.Default.GetBytes(data));
        }

        /// <summary>
        /// 异步发送数据至指定的客户端
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="data">报文</param>
        public void Send(TcpClient client, byte[] data)
        {
            if (!IsRunning)
                throw new InvalidProgramException("This TCP Scoket server has not been started.");

            if (client == null)
                throw new ArgumentNullException("client");

            if (data == null)
                throw new ArgumentNullException("data");
            client.GetStream().BeginWrite(data, 0, data.Length, SendDataEnd, client);
        }

        /// <summary>
        /// 广播，向所有客户端发送数据
        /// </summary>
        /// <param name="data">数据</param>
        public void Broadcast(string data)
        {
            Broadcast(Encoding.Default.GetBytes(data));
        }

        /// <summary>
        /// 广播，向所有客户端发送数据
        /// </summary>
        /// <param name="data">数据</param>
        public void Broadcast(byte[] data)
        {
            if (!IsRunning)
            {
                throw new InvalidProgramException("This TCP server has not been started.");
            }

            foreach (ZAsyncTcpClientState state in _tcpClientStateList)
            {
                Send(state.Client, data);
            }
        }

        /// <summary>
        /// 发送数据完成处理函数
        /// </summary>
        /// <param name="ar">目标客户端Socket</param>
        private void SendDataEnd(IAsyncResult ar)
        {
            ((TcpClient)ar.AsyncState).GetStream().EndWrite(ar);
            RaiseCompletedSend(null);
        }

        #endregion

        #region 关闭

        /// <summary>
        /// 停止服务器
        /// </summary>
        public void Stop()
        {
            if (IsRunning)
            {
                _tcpListener.Stop();
                IsRunning = false;

                lock (_tcpClientStateList)
                {
                    //关闭所有客户端连接
                    CloseAllClient();
                }
            }
        }

        /// <summary>
        /// 关闭一个与客户端之间的会话
        /// </summary>
        /// <param name="state">需要关闭的客户端会话对象</param>
        public void Close(ZAsyncTcpClientState state)
        {
            if (state != null)
            {
                state.Close();
                _tcpClientStateList.Remove(state);
                //TODO 触发关闭事件
            }
        }
        /// <summary>
        /// 关闭所有的客户端会话,与所有的客户端连接会断开
        /// </summary>
        public void CloseAllClient()
        {
            foreach (ZAsyncTcpClientState state in _tcpClientStateList)
            {
                Close(state);
            }
            _tcpClientStateList.Clear();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 与客户端的连接已建立事件
        /// </summary>
        public event EventHandler<ZAsyncTcpEventArgs> ClientConnected;
        /// <summary>
        /// 与客户端的连接已断开事件
        /// </summary>
        public event EventHandler<ZAsyncTcpEventArgs> ClientDisconnected;
        
        /// <summary>
        /// 触发客户端连接事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseClientConnected(ZAsyncTcpClientState state)
        {
            if (ClientConnected != null)
            {
                ClientConnected(this, new ZAsyncTcpEventArgs(state));
            }
        }
        /// <summary>
        /// 触发客户端连接断开事件
        /// </summary>
        /// <param name="client"></param>
        private void RaiseClientDisconnected(ZAsyncTcpClientState state)
        {
            if (ClientDisconnected != null)
            {
                ClientDisconnected(this, new ZAsyncTcpEventArgs("连接断开"));
            }
        }

        /// <summary>
        /// 接收到数据事件
        /// </summary>
        public event EventHandler<ZAsyncTcpEventArgs> DataReceived;

        private void RaiseDataReceived(ZAsyncTcpClientState state)
        {
            if (DataReceived != null)
            {
                DataReceived(this, new ZAsyncTcpEventArgs(state));
            }
        }

        /// <summary>
        /// 发送数据前的事件
        /// </summary>
        public event EventHandler<ZAsyncTcpEventArgs> PrepareSend;

        /// <summary>
        /// 触发发送数据前的事件
        /// </summary>
        /// <param name="state"></param>
        private void RaisePrepareSend(ZAsyncTcpClientState state)
        {
            if (PrepareSend != null)
            {
                PrepareSend(this, new ZAsyncTcpEventArgs(state));
            }
        }

        /// <summary>
        /// 数据发送完毕事件
        /// </summary>
        public event EventHandler<ZAsyncTcpEventArgs> CompletedSend;

        /// <summary>
        /// 触发数据发送完毕的事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseCompletedSend(ZAsyncTcpClientState state)
        {
            if (CompletedSend != null)
            {
                CompletedSend(this, new ZAsyncTcpEventArgs(state));
            }
        }

        /// <summary>
        /// 网络错误事件
        /// </summary>
        public event EventHandler<ZAsyncTcpEventArgs> NetError;
        /// <summary>
        /// 触发网络错误事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseNetError(ZAsyncTcpClientState state)
        {
            if (NetError != null)
            {
                NetError(this, new ZAsyncTcpEventArgs(state));
            }
        }

        /// <summary>
        /// 异常事件
        /// </summary>
        public event EventHandler<ZAsyncTcpEventArgs> OtherException;
        /// <summary>
        /// 触发异常事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseOtherException(ZAsyncTcpClientState state, string descrip)
        {
            if (OtherException != null)
            {
                OtherException(this, new ZAsyncTcpEventArgs(descrip, state));
            }
        }
        private void RaiseOtherException(ZAsyncTcpClientState state)
        {
            RaiseOtherException(state, "");
        }

        #endregion

        #region 释放

        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release 
        /// both managed and unmanaged resources; <c>false</c> 
        /// to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        Stop();
                        if (_tcpListener != null)
                        {
                            _tcpListener = null;
                        }
                    }
                    catch (SocketException)
                    {
                        //TODO
                        RaiseOtherException(null);
                    }
                }
                disposed = true;
            }
        }

        #endregion
    }
}

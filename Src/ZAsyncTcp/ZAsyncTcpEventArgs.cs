using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAsyncTcp
{
    /// <summary>
    /// 异步TcpListener TCP服务器事件参数类 
    /// </summary>
    public class ZAsyncTcpEventArgs : EventArgs
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        public string _msg;

        /// <summary>
        /// 客户端状态封装类
        /// </summary>
        public ZAsyncTcpClientState _state;

        /// <summary>
        /// 是否已经处理过了
        /// </summary>
        public bool IsHandled { get; set; }

        public ZAsyncTcpEventArgs(string msg)
        {
            this._msg = msg;
            IsHandled = false;
        }
        public ZAsyncTcpEventArgs(ZAsyncTcpClientState state)
        {
            this._state = state;
            IsHandled = false;
        }
        public ZAsyncTcpEventArgs(string msg, ZAsyncTcpClientState state)
        {
            this._msg = msg;
            this._state = state;
            IsHandled = false;
        }
    }
}

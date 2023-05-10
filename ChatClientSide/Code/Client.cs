using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientSide.Code
{
    internal class Client
    {
        ///<summary>用户ID</summary>
        internal int UserID { get; private set; }

        ///<summary>用户名</summary>
        internal string UserName { get; private set; }

        internal Client( string username )
        {
            UserName = username;
        }

        /// <summary>
        /// 更新用户名
        /// </summary>
        /// <param name="userName">用户名</param>
        internal void UpdateUserName( string userName )
        {
            UserName = userName;
        }

    }
}

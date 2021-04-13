using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject1.Data
{
    public interface INetworkConnection
    {
        bool IsConnected { get; }
        void CheckNetworkConnection();
    }
}

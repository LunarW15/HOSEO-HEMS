using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOSEO_HEMS.Data
{
    public class NetworkCheck
    {
        public bool CheckConnectivity()
        {
            var isConnected = CrossConnectivity.Current.IsConnected;

            if (isConnected == true)
            {
                return true; //연결됨
            }
            else //isConnected == false
            {
                return false; //연결안됨
            }
        }
    }
}

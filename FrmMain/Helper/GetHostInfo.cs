using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Global.Helper
{
    //获取本机信息
    class GetHostInfo
    {
        public static string strHostName = Dns.GetHostName();
        public static string GetIPAddress()
        {
            /// <summary>
            /// 获取本机物理网卡的ip
            /// </summary>
            /// <returns></returns>
            string userIP = "";
            System.Net.NetworkInformation.NetworkInterface[] fNetworkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

            foreach (System.Net.NetworkInformation.NetworkInterface adapter in fNetworkInterfaces)
            {
                string fRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + adapter.Id + "\\Connection";

                Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(fRegistryKey, false);

                if (rk != null)
                {
                    // 区分 PnpInstanceID      
                    // 如果前面有 PCI 就是本机的真实网卡          
                    string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                    int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
                    if (fPnpInstanceID.Length > 3 &&
                    fPnpInstanceID.Substring(0, 3) == "PCI")
                    {
                        //string fCardType = "物理网卡";
                        System.Net.NetworkInformation.IPInterfaceProperties fIPInterfaceProperties = adapter.GetIPProperties();
                        System.Net.NetworkInformation.UnicastIPAddressInformationCollection UnicastIPAddressInformationCollection = fIPInterfaceProperties.UnicastAddresses;
                        foreach (System.Net.NetworkInformation.UnicastIPAddressInformation UnicastIPAddressInformation in UnicastIPAddressInformationCollection)
                        {
                            if (UnicastIPAddressInformation.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                userIP = UnicastIPAddressInformation.Address.ToString(); // Ip 地址     
                        }
                        break;
                    }

                }
            }
            return userIP;

        }

    }
}

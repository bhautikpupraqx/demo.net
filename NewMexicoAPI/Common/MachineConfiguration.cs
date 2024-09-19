using Microsoft.Win32;
using System.Diagnostics;
using System.Management;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Security.Cryptography;

namespace NewMexicoAPI.Common
{
    public class MachineConfiguration
    {
        #region Machine Configuration
        #region GetCurrentPCName
        public string GetCurrentPCName()
        {
            return System.Environment.MachineName;
        }
        #endregion End

        #region GetCurrentPCIP
        public string GetCurrentPCIP()
        {
            string hostName = Dns.GetHostName();
            return Dns.GetHostByName(hostName).AddressList[1].ToString();
        }
        #endregion End GetCurrentPCIP

        #region GetProcessorName
        public string GetProcessorName()
        {
            string processorName = string.Empty;

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
            {
                ManagementObjectCollection collection = searcher.Get();
                foreach (ManagementObject obj in collection)
                {
                    processorName = obj["Name"].ToString();
                    break;
                }
            }


            return processorName;
        }
        #endregion End GetProcessorName

        #region GetInstalledRam
        public string GetInstalledRam()
        {
            string installedRam = string.Empty;

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
            {
                ManagementObjectCollection collection = searcher.Get();
                foreach (ManagementObject obj in collection)
                {
                    ulong ramBytes = Convert.ToUInt64(obj["TotalPhysicalMemory"]);
                    installedRam = FormatBytes(ramBytes);
                    break; // Assuming there's only one system information object, you can break out of the loop.
                }
            }

            return installedRam;
        }
        #endregion End GetInstalledRam

        #region FormatBytes
        public string FormatBytes(ulong bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int suffixIndex = 0;
            double size = bytes;

            while (size >= 1024 && suffixIndex < suffixes.Length - 1)
            {
                size /= 1024;
                suffixIndex++;
            }

            return $"{size:N1} {suffixes[suffixIndex]}";
        }
        #endregion End FormatBytes

        #region GenerateDeviceID
        public string GenerateDeviceID(string ipAddress, string userAgent)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string dataToHash = $"{ipAddress}-{userAgent}";
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(dataToHash));
                StringBuilder stringBuilder = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
        #endregion End GenerateDeviceID

        #region GetWindowsProductID
        public string GetWindowsProductID()
        {
            string productID = string.Empty;


            using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                if (registryKey != null)
                {
                    productID = registryKey.GetValue("ProductId")?.ToString();
                }
            }

            return productID;
        }
        #endregion End GetWindowsProductID

        #region GetWindowsEdition
        public string GetWindowsEdition()
        {
            string windowsEdition = "Unknown";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection collection = searcher.Get();
                foreach (ManagementObject obj in collection)
                {
                    windowsEdition = obj["Caption"].ToString();
                    break;
                }
            }


            return windowsEdition;
        }
        #endregion End GetWindowsEdition

        #region GetWindowsVersion
        public string GetWindowsVersion()
        {
            string windowsVersion = "Unknown";


            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection collection = searcher.Get();
                foreach (ManagementObject obj in collection)
                {
                    windowsVersion = obj["Version"].ToString();
                    break;
                }
            }

            return windowsVersion;
        }
        #endregion End GetWindowsVersion

        #region GetWindowsExperienceScore
        public string GetWindowsExperienceScore()
        {
            string experienceScore = "Unknown";


            Process process = new Process();
            process.StartInfo.FileName = "winsat";
            process.StartInfo.Arguments = "formal";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            foreach (var line in output.Split('\n'))
            {
                if (line.Contains("Total assessed score"))
                {
                    experienceScore = line.Split(':')[1].Trim();
                    break;
                }
            }

            return experienceScore;
        }
        #endregion End GetWindowsExperienceScore

        #region GetSubnetMask
        public string GetSubnetMask(string ipAddress)
        {

            IPAddress ip = IPAddress.Parse(ipAddress);
            IPAddress subnetMask = GetSubnetMask(ip);
            return subnetMask.ToString();

        }
        #endregion End GetSubnetMask

        #region GetSubnetMask
        public IPAddress GetSubnetMask(IPAddress ipAddress)
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.Equals(ipAddress))
                    {
                        return ip.IPv4Mask;
                    }
                }
            }
            return null;
        }
        #endregion End GetSubnetMask

        #region GetCurrentPCIPv6
        public string GetCurrentPCIPv6()
        {
            string hostName = Dns.GetHostName();
            return Dns.GetHostAddresses(hostName)
                .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)?.ToString();
        }
        #endregion End GetCurrentPCIPv6

        #region GetFeatureExperiencePack
        public string GetFeatureExperiencePack()
        {
            string featureExperiencePack = "Not available";


            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection collection = searcher.Get();
                foreach (ManagementObject obj in collection)
                {
                    PropertyData featureExperiencePackProperty = obj.Properties["FeatureExperiencePack"];
                    if (featureExperiencePackProperty != null && featureExperiencePackProperty.Value != null)
                    {
                        featureExperiencePack = featureExperiencePackProperty.Value.ToString();
                        break;
                    }
                }
            }

            return featureExperiencePack;
        }
        #endregion End GetFeatureExperiencePack

        #endregion
    }
}

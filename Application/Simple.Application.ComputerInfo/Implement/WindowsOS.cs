using Simple.Application.ComputerInfo.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Application.ComputerInfo.Implement
{
    public class WindowsOS
    {
        private readonly string unknow = "unknow";
        private readonly double convert = Math.Pow(1024, 3);
        public CPUInfo GetCPUInfo()
        {
            var cpuInfo = new CPUInfo();
            var mos = new ManagementObjectSearcher("Select * from Win32_Processor").Get();

            foreach (var item in mos)
            {
                cpuInfo.Name = item["Name"].ToString() ?? unknow;
                cpuInfo.Cores = item["NumberOfCores"].ToString() ?? unknow;
                cpuInfo.LogicCores = item["NumberOfLogicalProcessors"].ToString() ?? unknow;
            }
            var perCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuInfo.UsedRate = Math.Round((decimal)perCounter.NextValue(), 2, MidpointRounding.AwayFromZero).ToString();

            return cpuInfo;
        }

        public RAMInfo GetRAMInfo()
        {
            var mc = new ManagementClass("Win32_ComputerSystem");
            var moc = mc.GetInstances();
            var ramInfo = new RAMInfo();

            foreach (var item in moc)
            {
                ramInfo.TotalSpace = (int)Math.Round(Convert.ToInt64(item["TotalPhysicalMemory"]) / convert, 0);
            }
            var mos = new ManagementObjectSearcher("Select * from Win32_OperatingSystem").Get();
            foreach (var item in mos)
            {
                ramInfo.FreeSpace = Math.Round(Convert.ToInt64(item["FreePhysicalMemory"]) / convert, 1);
            }
            return ramInfo;
        }

        public List<DiskInfo> GetDiskInfo()
        {
            var moc = new ManagementObjectSearcher("Select * from win32_logicaldisk").Get();
            var diskInfoList = new List<DiskInfo>();

            foreach (var item in moc)
            {
                diskInfoList.Add(new DiskInfo
                {
                    Name = item["Name"].ToString() ?? unknow,
                    TotalSpace = (int)Math.Round(Convert.ToInt64(item["Size"]) / convert, 0),
                    FreeSpace = Math.Round(Convert.ToInt64(item["FreeSpace"]) / convert, 1)
                });
            }
            return diskInfoList;
        }
        public OSInfo GetOSName()
        {
            var mos = new ManagementObjectSearcher("Select * from Win32_OperatingSystem").Get();
            var osInfo = new OSInfo();
            foreach (var item in mos)
            {
                osInfo.Name = item["Name"].ToString() ?? unknow;
            }
            osInfo.RunSeconds = (DateTimeOffset.Now - Process.GetCurrentProcess().StartTime).TotalMilliseconds;
            return osInfo;
        }
    }
}

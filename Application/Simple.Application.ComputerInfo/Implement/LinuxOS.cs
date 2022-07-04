using Simple.Application.ComputerInfo.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Application.ComputerInfo.Implement
{
    public class LinuxOS
    {
        public CPUInfo GetCPUInfo()
        {
            var cpuInfo = new CPUInfo();
            cpuInfo.LogicCores = Environment.ProcessorCount.ToString();

            var cpuInfoList = (File.ReadAllText(@"/proc/cpuinfo")).Split(' ').Where(o => o != string.Empty).ToList();
            cpuInfo.Name = string.Format("{0} {1} {2}", cpuInfoList[7], cpuInfoList[8], cpuInfoList[9]);
            var psi = new ProcessStartInfo("top", " -b -n 1") { RedirectStandardOutput = true };
            var proc = Process.Start(psi);
            if (proc == null)
            {
                return cpuInfo;
            }
            else
            {
                using (var sr = proc.StandardOutput)
                {
                    var index = 0;
                    while (!sr.EndOfStream)
                    {
                        if (index == 2)
                        {
                            GetCpuUsed(sr.ReadLine(), cpuInfo);
                            break;
                        }
                        sr.ReadLine();
                        index++;

                    }
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
            }

            return cpuInfo;
        }

        private static void GetCpuUsed(string cpuInfo, CPUInfo cpuOutput)
        {
            try
            {
                var str = cpuInfo.Replace("%Cpu(s):", "").Trim();
                var list = str.Split(",").ToList();
                var dic = new Dictionary<string, string>();
                foreach (var item in list)
                {
                    var key = item.Substring(item.Length - 2, 2);
                    var value = item.Replace(key, "");
                    dic[key] = value;
                }
                cpuOutput.UsedRate = dic["us"];
            }
            catch (Exception)
            {

            }
        }
    }
}

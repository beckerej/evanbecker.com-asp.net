using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EvanBecker.Models
{
    public class MachineStatsViewModel
    {
        private readonly IList<MachineStats> _machineStats;
        public MachineStatsViewModel(IList<MachineStats> machineStats)
        {
            _machineStats = machineStats;
        }

        public IList<int> Id => _machineStats.Select(i => i.Id).ToList();
        
        public IList<string> MachineNames => _machineStats.Select(i => i.MachineName).ToList();
        
        public IList<int> TotalMemories => _machineStats.Select(i => i.TotalMemory).ToList();
        
        public IList<int> UsedMemories => _machineStats.Select(i => i.UsedMemory).ToList();
        
        public IList<int> FreeMemories => _machineStats.Select(i => i.FreeMemory).ToList();
        
        public IList<int> SharedMemories => _machineStats.Select(i => i.SharedMemory).ToList();
        
        public IList<int> CacheMemories => _machineStats.Select(i => i.CacheMemory).ToList();
        
        public IList<int> AvailableMemories => _machineStats.Select(i => i.AvailableMemory).ToList();
        
        public IList<int> DiskUsages => _machineStats.Select(i => i.DiskUsage).ToList();
        
        public IList<float> CpuUsages => _machineStats.Select(i => i.CpuUsage).ToList();
        
        public IList<float> CpuIdles => _machineStats.Select(i => i.CpuIdle).ToList();
        
        public IList<string> DateTimes => _machineStats.Select(i => i.DateTime.ToString("hh:mm tt")).ToList();

        public bool IsOnline { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(_machineStats);
        }
    }
}

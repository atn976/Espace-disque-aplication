using System;
using System.IO;
using System.Threading;


public class DiskSpaceMonitor
{
    private readonly Timer _timer

  
    public DiskSpaceMonitor(int intervalInSeconds)
    {
        
        _timer = new Time(CheckDiskSpace, null, TimeSpan.Zero, TimeSpan.FromSeconds(intervalInSeconds));
    }

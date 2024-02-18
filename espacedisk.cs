using System;
using System.IO;
using System.Threading;


public class DiskSpaceMonitor
{
    private readonly Timer _timer; 

   
    public DiskSpaceMonitor(int intervalInSeconds)
    {
        
        _timer = new Timer(CheckDiskSpace, null, TimeSpan.Zero, TimeSpan.FromSeconds(intervalInSeconds));
    }

    

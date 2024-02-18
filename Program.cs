using System;

class Program
{
    static void Main(string[] args)
    {
        int interval = 10; 
        if (args.Length > 0 && int.TryParse(args[0], out int argInterval))
        {
            interval = argInterval;
        }

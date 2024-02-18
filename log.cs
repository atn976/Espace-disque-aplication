using System;
using System.IO;


public class LogManager
{
   
    private static LogManager _instance;

    
    private static readonly object _lock = new object();

   
    private readonly string _logFilePath;

  private LogManager()
    {
        // Formatage du nom de fichier pour inclure la date et l'heure actuelles, rendant chaque fichier log unique.
        string dateTimeFormat = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        _logFilePath = $"disk_space_log_{dateTimeFormat}.txt";
    }

    public static LogManager Instance
    {
        get
        {
            lock (_lock)
            {
                // Si _instance est null, une nouvelle instance de LogManager est créée.
                if (_instance == null)
                {
                    _instance = new LogManager();
                }
                // Retourne l'instance unique existante ou nouvellement créée.
                return _instance;
            }
        }
    }

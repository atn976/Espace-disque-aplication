using System;
using System.IO;

// La classe LogManager implémente le design pattern Singleton pour assurer une unique instance de gestion des logs.
// Ce pattern est particulièrement utile pour centraliser la gestion des logs dans une application,
// permettant ainsi une écriture cohérente et synchronisée dans le fichier de log.
public class LogManager
{
    // Instance unique de LogManager, initialisée à null au départ.
    private static LogManager _instance;

    // Objet de verrouillage utilisé pour synchroniser l'accès à l'instance Singleton dans un environnement multithread.
    private static readonly object _lock = new object();

    // Chemin du fichier de log, incluant la date et l'heure de création de l'instance LogManager pour un suivi unique.
    private readonly string _logFilePath;

    // Constructeur privé pour empêcher l'instanciation directe et assurer le contrôle via la propriété Instance.
    private LogManager()
    {
        // Formatage du nom de fichier pour inclure la date et l'heure actuelles, rendant chaque fichier log unique.
        string dateTimeFormat = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        _logFilePath = $"disk_space_log_{dateTimeFormat}.txt";
    }

    // Propriété publique statique pour accéder à l'instance unique de LogManager.
    // L'utilisation de 'lock' assure que l'instance est créée de manière thread-safe.
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

    // Méthode pour écrire un message dans le fichier log, incluant un horodatage.
    public void Log(string message)
    {
        // Ajout de l'horodatage au message avant de l'écrire dans le fichier log.
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logMessage = $"{timestamp} - {message}";

        // Écriture du message formaté dans le fichier log, avec un saut de ligne à la fin.
        File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
    }
}

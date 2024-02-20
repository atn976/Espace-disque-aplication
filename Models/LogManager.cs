using System;
using System.IO;

namespace ratrapage_version_2._0.Models
{
    // La classe LogManager utilise le design pattern Singleton pour s'assurer qu'une seule instance de cette classe est utilisée dans toute l'application.
    public class LogManager
    {
        // Variable statique pour l'instance du singleton, initialement nulle.
        private static LogManager? _instance;

        // Objet de verrouillage utilisé pour synchroniser l'accès à l'instance du singleton.
        private static readonly object _lock = new object();

        // Chemin du fichier où les logs seront écrits.
        private readonly string _logFilePath;

        // Constructeur privé pour empêcher l'instanciation externe.
        private LogManager()
        {
            // Formatage de la date et de l'heure actuelles pour créer un nom de fichier de log unique.
            string dateTimeFormat = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            // Combinaison du chemin de base de l'application avec le nom de fichier pour créer le chemin complet du fichier de log.
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"disk_space_log_{dateTimeFormat}.txt");
        }

        // Propriété statique pour accéder à l'instance du LogManager. Utilise 'lock' pour être thread-safe.
        public static LogManager Instance
        {
            get
            {
                // Synchronisation pour éviter que plusieurs threads créent des instances séparées.
                lock (_lock)
                {
                    // Si l'instance est nulle, utilise l'opérateur ??= pour l'assigner.
                    _instance ??= new LogManager();
                    // Retourne l'instance existante ou celle nouvellement créée.
                    return _instance;
                }
            }
        }

        // Méthode pour écrire un message dans le fichier log.
        public void Log(string message)
        {
            // Obtention de l'horodatage actuel.
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // Formatage du message de log avec l'horodatage.
            string logMessage = $"{timestamp} - {message}{Environment.NewLine}";
            // Synchronisation pour éviter les conflits d'écriture en multithreading.
            lock (_lock)
            {
                // Écriture du message formaté dans le fichier log.
                File.AppendAllText(_logFilePath, logMessage);
            }
        }
    }
}

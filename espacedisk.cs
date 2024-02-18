using System;
using System.IO;
using System.Threading;

// La classe DiskSpaceMonitor utilise le pattern Observer de manière implicite grâce à l'utilisation d'un Timer.
// Ce Timer agit comme un sujet qui notifie périodiquement l'observateur (la méthode CheckDiskSpace) pour exécuter une action.
public class DiskSpaceMonitor
{
    private readonly Timer _timer; // Déclaration d'un Timer pour planifier les vérifications de l'espace disque.

    // Constructeur de la classe DiskSpaceMonitor qui initialise le Timer.
    // intervalInSeconds définit la périodicité des vérifications de l'espace disque.
    public DiskSpaceMonitor(int intervalInSeconds)
    {
        // Initialisation du Timer. La méthode CheckDiskSpace est appelée à chaque échéance de l'intervalle défini.
        // La première échéance est immédiate (TimeSpan.Zero), et les suivantes sont espacées par l'intervalle spécifié.
        _timer = new Timer(CheckDiskSpace, null, TimeSpan.Zero, TimeSpan.FromSeconds(intervalInSeconds));
    }

    // Méthode privée appelée par le Timer à chaque intervalle.
    // Elle vérifie l'espace disque disponible sur le lecteur C et enregistre cette information.
    private void CheckDiskSpace(object state)
    {
        DriveInfo driveInfo = new DriveInfo("C"); // Accède aux informations du lecteur C:.
        long availableSpace = driveInfo.AvailableFreeSpace; // Espace disponible en octets.

        // Convertit l'espace libre en gigaoctets pour un affichage plus lisible.
        double availableSpaceGB = availableSpace / (1024.0 * 1024.0 * 1024.0);

        // Construit le message pour inclure l'espace disponible en GB et en bytes.
        string message = $"Available disk space on C: {availableSpaceGB:F2} GB ({availableSpace} bytes)";

        Console.WriteLine(message); // Affiche le message dans la console.
        LogManager.Instance.Log(message); // Log le message via l'instance de LogManager.
    }

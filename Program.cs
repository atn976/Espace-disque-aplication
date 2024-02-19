using System;

class Program
{
    static void Main(string[] args)
    {
        int interval = 10; // Valeur par défaut de 10 secondes
        if (args.Length > 0 && int.TryParse(args[0], out int argInterval))
        {
            interval = argInterval;
        }
        else
        {
            // Demande à l'utilisateur de saisir l'intervalle en secondes s'il n'a pas été fourni en argument.
            Console.WriteLine("Entrez l'intervalle en secondes pour la surveillance de l'espace disque:");
            while (!int.TryParse(Console.ReadLine(), out interval) || interval <= 0)
            {
                Console.WriteLine("Entrée invalide. Veuillez saisir un entier positif :");
            }
        }

        DiskSpaceMonitor monitor = new DiskSpaceMonitor(interval);
        Console.WriteLine($"Surveillance de l'espace disque toutes les {interval} secondes. Appuyez sur une touche pour quitter...");

        Console.ReadKey();
    }
}

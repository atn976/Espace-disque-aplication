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
        else
        {
            
            Console.WriteLine("Entrez l'intervalle en secondes pour la surveillance de l'espace disque:");
            while (!int.TryParse(Console.ReadLine(), out interval) || interval <= 0)
            {
                Console.WriteLine("Entrée invalide. Veuillez saisir un entier positif :");
            }
        }

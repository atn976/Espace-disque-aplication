using System;
using System.Windows.Input;

namespace ratrapage_version_2._0.ViewModels
{
    // Utilisation du design pattern Command
    // ICommand est une interface couramment utilisée pour encapsuler la logique d'une commande exécutable par l'interface utilisateur
    // RelayCommand est une implémentation de ICommand qui permet de définir des actions exécutables par l'interface utilisateur
    public class RelayCommand : ICommand
    {
        private readonly Action _execute; // Action à exécuter lorsque la commande est appelée
        private readonly Func<bool>? _canExecute; // Fonction déterminant si la commande peut être exécutée, nullable

        // Événement qui est déclenché lorsque l'état de l'exécution de la commande peut être modifié
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; } // Ajoute un gestionnaire d'événements à l'événement RequerySuggested du CommandManager
            remove { CommandManager.RequerySuggested -= value; } // Supprime un gestionnaire d'événements de l'événement RequerySuggested du CommandManager
        }

        // Constructeur de la classe RelayCommand
        public RelayCommand(Action execute, Func<bool>? canExecute = null) // Le paramètre canExecute est nullable
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute)); // Vérifie que l'action à exécuter n'est pas nulle
            _canExecute = canExecute; // Définit la fonction de vérification de l'exécution de la commande
        }

        // Méthode permettant de déterminer si la commande peut être exécutée
        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke() ?? true; // Si _canExecute n'est pas nul, invoque la fonction de vérification, sinon retourne true
        }

        // Méthode qui effectue l'exécution de la commande
        public void Execute(object? parameter)
        {
            _execute(); // Exécute l'action associée à la commande
        }
    }
}

using ratrapage_version_2._0.Models; // Importation de l'espace de noms pour accéder à d'autres classes du projet
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Threading;

namespace ratrapage_version_2._0.ViewModels // Déclaration de l'espace de noms du ViewModel
{
    public class DiskSpaceViewModel : INotifyPropertyChanged // Définition de la classe DiskSpaceViewModel qui implémente l'interface INotifyPropertyChanged pour notifier les changements de propriété à la vue
    {
        private readonly DispatcherTimer _timer; // Utilisation d'un DispatcherTimer pour déclencher des événements à intervalles réguliers sur le thread de l'interface utilisateur
        private int _updateInterval; // Stockage de l'intervalle de mise à jour de l'espace disque

        // Propriété contenant les entrées de journal avec une notification de changement de propriété
        public ObservableCollection<string> LogEntries { get; private set; }
        public event PropertyChangedEventHandler? PropertyChanged; // Événement indiquant qu'une propriété a changé

        // Propriété permettant de définir l'intervalle de mise à jour de l'espace disque avec notification de changement de propriété
        public int UpdateInterval
        {
            get => _updateInterval;
            set
            {
                if (_updateInterval != value)
                {
                    _updateInterval = value;
                    OnPropertyChanged(nameof(UpdateInterval)); // Appel de la méthode OnPropertyChanged pour notifier la vue du changement de propriété
                    UpdateTimer(); // Mise à jour du minuteur en conséquence
                }
            }
        }

        // Commande pour définir l'intervalle de mise à jour
        public ICommand SetIntervalCommand { get; }

        // Constructeur de la classe DiskSpaceViewModel
        public DiskSpaceViewModel(int intervalInSeconds)
        {
            LogEntries = new ObservableCollection<string>(); // Initialisation de la collection des entrées de journal
            _updateInterval = intervalInSeconds; // Initialisation de l'intervalle de mise à jour
            _timer = new DispatcherTimer(); // Création d'une nouvelle instance de DispatcherTimer
            _timer.Tick += (sender, e) => CheckDiskSpace(); // Définition de l'événement déclenché à chaque tic du minuteur
            _timer.Interval = TimeSpan.FromSeconds(_updateInterval); // Définition de l'intervalle du minuteur
            _timer.Start(); // Démarrage du minuteur pour déclencher les vérifications de l'espace disque

            SetIntervalCommand = new RelayCommand(UpdateTimer); // Création de la commande pour définir l'intervalle de mise à jour
        }

        // Méthode pour mettre à jour le minuteur
        private void UpdateTimer()
        {
            _timer.Stop(); // Arrêt du minuteur
            _timer.Interval = TimeSpan.FromSeconds(_updateInterval); // Mise à jour de l'intervalle du minuteur
            _timer.Start(); // Redémarrage du minuteur avec le nouvel intervalle
        }

        // Méthode pour vérifier l'espace disque disponible
        private void CheckDiskSpace()
        {
            DriveInfo driveInfo = new DriveInfo("C"); // Création d'une instance DriveInfo pour le lecteur C
            long availableSpace = driveInfo.AvailableFreeSpace; // Récupération de l'espace disque disponible
            double availableSpaceGB = availableSpace / (1024.0 * 1024.0 * 1024.0); // Conversion de l'espace disponible en gigaoctets
            string message = $"Available disk space on C: {availableSpaceGB:F2} GB ({availableSpace} bytes)"; // Création du message à afficher
            LogManager.Instance.Log(message); // Enregistrement du message dans le journal via le gestionnaire de logs
            Dispatcher.CurrentDispatcher.Invoke(() => { LogEntries.Add(message); }); // Ajout du message dans la collection des entrées de journal avec prise en compte de l'interface utilisateur
            OnPropertyChanged(nameof(LogEntries)); // Notification de changement de propriété pour mettre à jour la vue avec les nouvelles entrées de journal
        }

        // Méthode pour notifier les changements de propriété
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Appel de l'événement PropertyChanged pour notifier les écouteurs de changements de propriété
        }
    }
}

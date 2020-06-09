using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Dev.Dominion.Common;
using Dev.Dominion.Scraper.Models;
using Newtonsoft.Json;

namespace Dev.Dominion.CardUpdater
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Set> Sets { get; set; }

        public Card SelectedCard
        {
            get { return _selectedCard; }
            set {
                _selectedCard = value;
                OnPropertyChanged("SelectedCard");
                OnPropertyChanged("SelectedImage");
            }
        }

        public string SelectedImage
        {
            get
            {
                if (SelectedCard == null) return null;
                return _rootPath + SelectedCard.LocalImageFileName;
            }
        }

        private readonly string _rootPath;
        private Card _selectedCard;
        

        public MainViewModel()
        {
            _rootPath = @"C:\Me\dev\develorem\MyDominion\Cards";

            Sets = new ObservableCollection<Set>();
            ReloadSets();

            _saveCommand = new CommandHandler(Save, true);
            _reloadCommand = new CommandHandler(ReloadSets, true);
        }

        private void ReloadSets()
        {
            var lst = new List<Set>();

            var childDirectories = Directory.GetDirectories(_rootPath);
            foreach (var child in childDirectories)
            {
                var file = Directory.GetFiles(child).FirstOrDefault(x => x.EndsWith(".dom"));
                if (file != null)
                {
                    var json = File.ReadAllText(file);
                    var set = JsonConvert.DeserializeObject<Set>(json);
                    lst.Add(set);
                }
            }

            if (Sets.Any())
            {
                Sets.Clear();
            }
            foreach (var set in lst)
            {
                Sets.Add(set);
            }

            // TODO Remove later
            foreach (var card in Sets.SelectMany(x => x.Cards))
            {
                if (!string.IsNullOrEmpty(card.SpecialText))
                {
                    // card.HasSpecialEffect = true;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand; }
        }
        private void Save()
        {
            foreach (var set in Sets)
            {
                var json = JsonConvert.SerializeObject(set);
                var setNameSafe = set.Name.ToLower().Replace(" ", "").Trim();
                var path = _rootPath + @"\" + setNameSafe;
                if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
                var filePath = path + "\\cards.dom";
                if (File.Exists(filePath)) File.Delete(filePath);
                File.AppendAllText(filePath, json);
            }
        }

        private ICommand _reloadCommand;
        public ICommand ReloadCommand
        {
            get { return _reloadCommand; }
        }
        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
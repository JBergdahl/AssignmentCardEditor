using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data;
using Microsoft.Win32;

namespace AssignmentCardEditor.ViewModels
{
    public class BrowserViewModel : ObservableRecipient
    {
        private readonly IDbMethods _dbMethods;
        private int _attack;
        private string _cardType;
        private int _defense;
        private string _imagePath;
        private int _mana;
        private string _name;
        private string _selectedCard;
        private int _speed;

        public BrowserViewModel(IDbMethods dbMethods)
        {
            _dbMethods = dbMethods;
            DeleteCommand = new RelayCommand(OnDeleteCommandExecuted);
            ExportCommand = new RelayCommand(OnExportCommandExecuted);
            UpdateCommand = new RelayCommand(OnUpdateCommandExecuted);
        }

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }

        public ObservableCollection<string> CardNameCollection { get; set; } = new();

        public string SelectedCard
        {
            get => _selectedCard;
            set
            {
                if (SetProperty(ref _selectedCard, value))
                {
                    var card = _dbMethods.GetCardByName(_selectedCard);
                    if (card != null)
                    {
                        Name = card.Name;
                        Attack = card.Attack;
                        CardType = card.CardType;
                        Defense = card.Defense;
                        Speed = card.Speed;
                        Mana = card.Mana;
                        ImagePath = card.ImagePath;
                    }
                }
            }
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string CardType
        {
            get => _cardType;
            set => SetProperty(ref _cardType, value);
        }

        public int Attack
        {
            get => _attack;
            set => SetProperty(ref _attack, value);
        }

        public int Defense
        {
            get => _defense;
            set => SetProperty(ref _defense, value);
        }

        public int Speed
        {
            get => _speed;
            set => SetProperty(ref _speed, value);
        }

        public int Mana
        {
            get => _mana;
            set => SetProperty(ref _mana, value);
        }

        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        public void OnDeleteCommandExecuted()
        {
            _dbMethods.DeleteOneCard(_selectedCard);
        }

        public void OnUpdateCommandExecuted()
        {
            CardNameCollection.Clear();
            var listCard = _dbMethods.GetAllCards();
            foreach (var card in listCard) CardNameCollection.Add(card.Name);
        }

        public void OnExportCommandExecuted()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text|*.txt",
                Title = "Save Card"
            };
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                var fs = (FileStream)saveFileDialog.OpenFile();
                var card = _dbMethods.GetCardByName(_selectedCard);
                var cardSerialized = JsonSerializer.Serialize(card);
                var path = fs.Name;

                fs.Close();

                File.WriteAllText(path, cardSerialized);
            }
        }
    }
}
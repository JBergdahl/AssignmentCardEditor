using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.IO;
using System.Linq;
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
        private string _nameSearch;
        private string _description;

        public BrowserViewModel(IDbMethods dbMethods)
        {
            _dbMethods = dbMethods;
            InitCardCollectionList();
            DeleteCommand = new RelayCommand(OnDeleteCommandExecuted);
            ExportCommand = new RelayCommand(OnExportCommandExecuted);
        }

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }

        public ObservableCollection<string> CardNameCollection { get; set; } = new();
        public ObservableCollection<string> TempCardNameCollection { get; set; } = new();

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
                        Description = FormatDescriptionText(card.Description);
                    }
                }
            }
        }

        private string FormatDescriptionText(string text)
        {
            if (text.Length >= 23)
            {
                var formattedText = "";
                var currentWord = "";
                var lineLength = 30;
                var counter = 0;

                for (var i = 0; i < text.Length; i++)
                {
                    currentWord += text.ElementAt(i);

                    if (text.ElementAt(i) == ' ')
                    {
                        if (counter >= lineLength)
                        {
                            formattedText += System.Environment.NewLine;
                            counter = 0;
                        }
                        else
                        {
                            formattedText += currentWord;
                            currentWord = "";
                        }
                    }

                    counter++;
                }

                if (!string.IsNullOrWhiteSpace(currentWord)) formattedText += currentWord;

                return formattedText;
            }

            return text;
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

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string NameSearch
        {
            get => _nameSearch;
            set
            {
                if (SetProperty(ref _nameSearch, value))
                {
                    CardNameCollection.Clear();

                    if (_nameSearch.Equals("") || _nameSearch.Length == 0)
                    {
                        foreach (var cardName in TempCardNameCollection)
                        {
                            CardNameCollection.Add(cardName);
                        }
                    }
                    else
                    {
                        foreach (var card in TempCardNameCollection.Where(cardName => cardName.ToUpper().StartsWith(_nameSearch.ToUpper())))
                        {
                            CardNameCollection.Add(card);
                        };
                    }
                }
            }
        }

        private void InitCardCollectionList()
        {
            UpdateCardList();
        }

        public void OnDeleteCommandExecuted()
        {
            if (_selectedCard != null)
            {
                var cardName = _selectedCard;
                _dbMethods.DeleteOneCardByName(cardName);
                CardNameCollection.Remove(cardName);
                TempCardNameCollection.Remove(cardName);
            }
        }

        public void UpdateCardList()
        {
            CardNameCollection.Clear();
            var listCard = _dbMethods.GetAllCards();
            foreach (var card in listCard)
            {
                CardNameCollection.Add(card.Name);
                TempCardNameCollection.Add(card.Name);
            }
            Name = "";
            CardType = "";
            Attack = 0;
            Defense = 0;
            Speed = 0;
            Mana = 0;
            ImagePath = "";
            Description = "";
        }

        public void OnExportCommandExecuted()
        {
            if (_selectedCard != null)
            {
                var saveFileDialog = new SaveFileDialog
                {
                    AddExtension = true,
                    Filter = "json files (*.json)|*.json",
                    Title = "Save Card",
                    FileName = $"character_{_name}"
                };
                var ok = saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "" && ok is true)
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

        public void OnCardCollectionChanged(object? sender, string cardName)
        {
            var card = _dbMethods.GetCardByName(cardName);
            CardNameCollection.Add(card.Name);
            TempCardNameCollection.Add(card.Name);
        }
    }
}
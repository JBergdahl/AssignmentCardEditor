using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CardEditor.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data;
using Microsoft.Win32;

namespace AssignmentCardEditor.ViewModels
{
    public class CardViewModel : ObservableRecipient
    {
        private readonly IDbMethods _dbMethods;
        private int _attack;
        private bool _attackIsValid;
        private string _cardTypeName;
        private List<string> _cardTypeNames;
        private int _defense;
        private bool _defenseIsValid;
        private string _imagePath;
        private int _mana;
        private bool _manaIsValid;
        private string _name;

        private bool _nameIsValid;
        private int _speed;
        private bool _speedIsValid;

        public CardViewModel(IDbMethods dbMethods)
        {
            _dbMethods = dbMethods;
            UploadImageCommand = new RelayCommand(OnUploadImageExecuted);
            SaveCommand = new AsyncRelayCommand(OnSaveExecuted, CanSave);
            ImportCommand = new RelayCommand(OnImportExecuted);
            UpdateCommand = new RelayCommand(OnUpdateExecuted);
        }

        public RelayCommand ImportCommand { get; set; }
        public AsyncRelayCommand SaveCommand { get; set; }
        public RelayCommand UploadImageCommand { get; set; }

        public ObservableCollection<string> TypeNameCollection { get; set; } = new();

        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                {
                    NameIsValid = ValidName(_name);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public string CardTypeName
        {
            get => _cardTypeName;
            set
            {
                if (SetProperty(ref _cardTypeName, value))
                {
                    // Search database for card type + insert default values
                    var cardType = _dbMethods.FindCardTypeByNameNormal(_cardTypeName);
                    Attack = cardType.AttackDefault;
                    CardTypeName = cardType.Name;
                    Defense = cardType.DefenseDefault;
                    Speed = cardType.SpeedDefault;
                    Mana = cardType.ManaDefault;
                }
            }
        }

        public List<string> CardTypeNames
        {
            get => _cardTypeNames;
            set => SetProperty(ref _cardTypeNames, value);
        }

        public RelayCommand UpdateCommand { get; set; }

        public int Attack
        {
            get => _attack;
            set
            {
                if (SetProperty(ref _attack, value))
                {
                    AttackIsValid = ValidAttack(_attack);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public int Defense
        {
            get => _defense;
            set
            {
                if (SetProperty(ref _defense, value))
                {
                    DefenseIsValid = ValidDefense(_defense);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public int Speed
        {
            get => _speed;
            set
            {
                if (SetProperty(ref _speed, value))
                {
                    SpeedIsValid = ValidSpeed(_speed);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public int Mana
        {
            get => _mana;
            set
            {
                if (SetProperty(ref _mana, value))
                {
                    ManaIsValid = ValidMana(_mana);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        public bool NameIsValid
        {
            get => _nameIsValid;
            set => SetProperty(ref _nameIsValid, value);
        }

        public bool AttackIsValid
        {
            get => _attackIsValid;
            set => SetProperty(ref _attackIsValid, value);
        }

        public bool DefenseIsValid
        {
            get => _defenseIsValid;
            set => SetProperty(ref _defenseIsValid, value);
        }

        public bool SpeedIsValid
        {
            get => _speedIsValid;
            set => SetProperty(ref _speedIsValid, value);
        }

        public bool ManaIsValid
        {
            get => _manaIsValid;
            set => SetProperty(ref _manaIsValid, value);
        }

        public void OnUpdateExecuted()
        {
            TypeNameCollection.Clear();
            var listCard = _dbMethods.GetAllCardTypes();
            foreach (var card in listCard) TypeNameCollection.Add(card.Name);
        }

        private async Task OnSaveExecuted()
        {
            // Check if card name already is in database
            var nameIsTaken = await _dbMethods.FindCardByName(_name);

            if (!nameIsTaken)
            {
                // Add to database
                await _dbMethods.AddOneCard(_name, _cardTypeName, _attack, _defense, _speed, _mana, _imagePath);

                // Reset fields
                Name = "Enter name";
                Attack = 0;
                Defense = 0;
                Speed = 0;
                Mana = 0;
                ImagePath = "";
            }
            else
            {
                // Name found in database
                Name = "Name is taken";
            }
        }

        private void OnUploadImageExecuted()
        {
            // Import card
            var op = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png"
            };

            if (op.ShowDialog() == true)
                //ImagePath = new BitmapImage(new Uri(op.FileName));
                ImagePath = op.FileName;
        }


        private void OnImportExecuted()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Text|*.txt",
                Title = "Open Card"
            };

            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                var text = File.ReadAllText(openFileDialog.FileName);

                var card = JsonSerializer.Deserialize<Card>(text);

                if (card != null)
                {
                    Name = card.Name;
                    CardTypeName = card.CardType;
                    Attack = card.Attack;
                    Defense = card.Defense;
                    Speed = card.Speed;
                    Mana = card.Mana;
                    ImagePath = card.ImagePath;
                }
            }
        }

        private bool CanSave()
        {
            return NameIsValid && AttackIsValid && DefenseIsValid && SpeedIsValid && ManaIsValid;
        }

        private bool ValidName(string name)
        {
            return name?.Length is > 1 and < 20 && !name.Equals("Enter name") && !name.Equals("Name is taken");
        }

        private bool ValidAttack(int attack)
        {
            // Add ranges for types
            return attack is > 0 and < 100;
        }

        private bool ValidDefense(int defense)
        {
            // Add ranges for types
            return defense is > 0 and < 100;
        }

        private bool ValidSpeed(int speed)
        {
            // Add ranges for types
            return speed is > 0 and < 100;
        }

        private bool ValidMana(int mana)
        {
            // Add ranges for types
            return mana is > 0 and < 100;
        }
    }
}
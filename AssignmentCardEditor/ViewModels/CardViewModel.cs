using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using AssignmentCardEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data;
using Microsoft.Win32;

namespace AssignmentCardEditor.ViewModels
{
    public class CardViewModel : ObservableObject
    {
        private string _name;
        private int _attack;
        private int _defense;
        private int _speed;
        private int _mana;
        private string _imagePath;

        private readonly IDbMethods _dbMethods;

        private bool _nameIsValid;
        private bool _attackIsValid;
        private bool _defenseIsValid;
        private bool _speedIsValid;
        private bool _manaIsValid;

        public CardViewModel(IDbMethods dbMethods)
        {
            _dbMethods = dbMethods;
            UploadImageCommand = new RelayCommand(OnUploadImageExecuted);
            SaveCommand = new AsyncRelayCommand(OnSaveExecuted, CanSave);
            ImportCommand = new AsyncRelayCommand(OnImportExecuted);
        }

        private async Task OnSaveExecuted()
        {
            // Check if card name already is in database
            var nameIsTaken = await _dbMethods.FindCardByName(_name);

            if (!nameIsTaken)
            {
                // Add to database
                await _dbMethods.AddOneCard(_name, _attack, _defense, _speed, _mana, _imagePath);

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
            {
                //ImagePath = new BitmapImage(new Uri(op.FileName));
                ImagePath = op.FileName;
            }
        }

        private async Task OnImportExecuted()
        {
            // Import card
        }

        private bool CanSave()
        {
            return NameIsValid && AttackIsValid && DefenseIsValid && SpeedIsValid && ManaIsValid;
        }

        public AsyncRelayCommand ImportCommand { get; set; }

        public AsyncRelayCommand SaveCommand { get; set; }
        public RelayCommand UploadImageCommand { get; set; }

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

        private bool ValidName(string name)
        {
            return name?.Length is > 1 and < 20 && !name.Equals("Enter name") && !name.Equals("Name is taken");
        }

        private bool ValidAttack(int attack)
        {
            // Add ranges for types
            return attack > 0;
        }

        private bool ValidDefense(int defense)
        {
            // Add ranges for types
            return defense > 0;
        }

        private bool ValidSpeed(int speed)
        {
            // Add ranges for types
            return speed > 0;
        }

        private bool ValidMana(int mana)
        {
            // Add ranges for types
            return mana > 0;
        }
    }
}
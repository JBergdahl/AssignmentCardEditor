using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AssignmentCardEditor.ViewModels
{
    public class CardViewModel : ObservableObject
    {
        private string _name;
        private int _attack;
        private int _defense;
        private int _speed;
        private int _mana;
        private bool _nameIsValid;

        public RelayCommand SaveCommand { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                {
                    NameIsValid = ValidLengthAndNotInDatabase(_name);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
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

        public bool NameIsValid
        {
            get => _nameIsValid;
            set => SetProperty(ref _nameIsValid, value);
        }

        private bool ValidLengthAndNotInDatabase(string name)
        {
            // Add duplicate check in database
            return name is not null && name.Length > 1;
        }
    }
}

using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data;

namespace AssignmentCardEditor.ViewModels
{
    public class CardTypeViewModel : ObservableObject
    {
        private readonly IDbMethods _dbMethods;

        private int _attackDefault;
        private bool _attackIsValid;
        private int _defenseDefault;
        private bool _defenseIsValid;
        private int _manaDefault;
        private bool _manaIsValid;
        private string _name;
        private bool _nameIsValid;
        private int _speedDefault;
        private bool _speedIsValid;

        public CardTypeViewModel(IDbMethods dbMethods)
        {
            _dbMethods = dbMethods;
            SaveCommand = new AsyncRelayCommand(OnSaveExecuted, CanSave);
        }

        public AsyncRelayCommand SaveCommand { get; set; }

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

        public int AttackDefault
        {
            get => _attackDefault;
            set
            {
                if (SetProperty(ref _attackDefault, value))
                {
                    AttackIsValid = ValidAttack(_attackDefault);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public int DefenseDefault
        {
            get => _defenseDefault;
            set
            {
                if (SetProperty(ref _defenseDefault, value))
                {
                    DefenseIsValid = ValidDefense(_defenseDefault);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public int SpeedDefault
        {
            get => _speedDefault;
            set
            {
                if (SetProperty(ref _speedDefault, value))
                {
                    SpeedIsValid = ValidSpeed(_speedDefault);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public int ManaDefault
        {
            get => _manaDefault;
            set
            {
                if (SetProperty(ref _manaDefault, value))
                {
                    ManaIsValid = ValidMana(_manaDefault);
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
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

        private bool CanSave()
        {
            return NameIsValid && AttackIsValid && DefenseIsValid && SpeedIsValid && ManaIsValid;
        }

        private async Task OnSaveExecuted()
        {
            // Check if card type name already is in database
            var nameIsTaken = await _dbMethods.IsCardTypeNameInDatabase(_name);

            if (!nameIsTaken)
            {
                // Add to database
                var card = await _dbMethods.AddOneCardType(_name, _attackDefault, _defenseDefault, _speedDefault,
                    _manaDefault);

                // Reset fields
                Name = "Enter type name";
                AttackDefault = 0;
                DefenseDefault = 0;
                SpeedDefault = 0;
                ManaDefault = 0;
                CardTypeCollectionChanged?.Invoke(this, card.Name);
            }
            else
            {
                // Name found in database
                Name = "Name is taken";
            }
        }

        public event EventHandler<string> CardTypeCollectionChanged;

        private bool ValidName(string name)
        {
            return name?.Length is > 1 and < 20 && !name.Equals("Enter type name") && !name.Equals("Name is taken");
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
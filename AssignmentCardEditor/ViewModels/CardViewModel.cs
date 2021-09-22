using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AssignmentCardEditor.ViewModels
{
    public class CardViewModel : ObservableObject
    {
        private string _name;
        private int _attack;
        private int _defense;
        private int _speed;
        private int _mana;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
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
    }
}

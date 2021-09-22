using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentCardEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Data;

namespace AssignmentCardEditor.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private CardViewModel _cardViewModel;

        public MainWindowViewModel(IDbMethods dbMethods)
        {
            _cardViewModel = new CardViewModel(dbMethods);
        }

        public CardViewModel CardViewModel
        {
            get => _cardViewModel;
            set => SetProperty(ref _cardViewModel, value);
        }
    }
}

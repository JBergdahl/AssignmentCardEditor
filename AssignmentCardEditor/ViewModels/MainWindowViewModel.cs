using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentCardEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data;

namespace AssignmentCardEditor.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private CardViewModel _cardViewModel;
        private CardTypeViewModel _cardTypeViewModel;
        private BrowserViewModel _browserViewModel;

        public MainWindowViewModel(IDbMethods dbMethods)
        {
            _cardViewModel = new CardViewModel(dbMethods);
            _cardTypeViewModel = new CardTypeViewModel(dbMethods);
            _browserViewModel = new BrowserViewModel(dbMethods);
        }

        public CardViewModel CardViewModel
        {
            get => _cardViewModel;
            set => SetProperty(ref _cardViewModel, value);
        }

        public CardTypeViewModel CardTypeViewModel
        {
            get => _cardTypeViewModel;
            set => SetProperty(ref _cardTypeViewModel, value);
        }

        public BrowserViewModel BrowserViewModel
        {
            get => _browserViewModel;
            set => SetProperty(ref _browserViewModel, value);
        }
    }
}

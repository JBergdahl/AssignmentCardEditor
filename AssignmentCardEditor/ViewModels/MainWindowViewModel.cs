using CommunityToolkit.Mvvm.ComponentModel;
using Data;

namespace AssignmentCardEditor.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private BrowserViewModel _browserViewModel;
        private CardTypeViewModel _cardTypeViewModel;
        private CardViewModel _cardViewModel;

        public MainWindowViewModel(IDbMethods dbMethods)
        {
            _browserViewModel = new BrowserViewModel(dbMethods);
            CardViewModel = new CardViewModel(dbMethods);
            CardTypeViewModel = new CardTypeViewModel(dbMethods);
        }

        public CardViewModel CardViewModel
        {
            get => _cardViewModel;
            set
            {
                if (_cardViewModel is not null)
                    _cardViewModel.CardCollectionChanged -= BrowserViewModel.OnCardCollectionChanged;

                if (SetProperty(ref _cardViewModel, value))
                    if (_cardViewModel is not null)
                        _cardViewModel.CardCollectionChanged += BrowserViewModel.OnCardCollectionChanged;
            }
        }

        public CardTypeViewModel CardTypeViewModel
        {
            get => _cardTypeViewModel;
            set
            {
                if (_cardTypeViewModel is not null)
                    _cardTypeViewModel.CardTypeCollectionChanged -= CardViewModel.OnCardTypeCollectionChanged;

                if (SetProperty(ref _cardTypeViewModel, value))
                    if (_cardTypeViewModel is not null)
                        _cardTypeViewModel.CardTypeCollectionChanged += CardViewModel.OnCardTypeCollectionChanged;
            }
        }

        public BrowserViewModel BrowserViewModel
        {
            get => _browserViewModel;
            set => SetProperty(ref _browserViewModel, value);
        }
    }
}
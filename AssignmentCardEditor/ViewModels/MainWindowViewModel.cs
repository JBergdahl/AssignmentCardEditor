using CommunityToolkit.Mvvm.ComponentModel;
using Data;

namespace AssignmentCardEditor.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private BrowserViewModel _browserViewModel;
        private CardTypeBrowserViewModel _cardTypeBrowserViewModel;
        private CardTypeViewModel _cardTypeViewModel;
        private CardViewModel _cardViewModel;

        public MainWindowViewModel(IDbMethods dbMethods)
        {
            _browserViewModel = new BrowserViewModel(dbMethods);
            _cardTypeBrowserViewModel = new CardTypeBrowserViewModel(dbMethods);
            CardViewModel = new CardViewModel(dbMethods);
            CardTypeViewModel = new CardTypeViewModel(dbMethods);
        }

        public CardTypeBrowserViewModel CardTypeBrowserViewModel
        {
            get => _cardTypeBrowserViewModel;
            set => SetProperty(ref _cardTypeBrowserViewModel, value);
        }

        public CardViewModel CardViewModel
        {
            get => _cardViewModel;
            set
            {
                if (_cardViewModel is not null)
                {
                    _cardViewModel.CardCollectionChanged -= BrowserViewModel.OnCardCollectionChanged;
                }

                if (!SetProperty(ref _cardViewModel, value))
                {
                    return;
                }

                if (_cardViewModel is not null)
                {
                    _cardViewModel.CardCollectionChanged += BrowserViewModel.OnCardCollectionChanged;
                }
            }
        }

        public CardTypeViewModel CardTypeViewModel
        {
            get => _cardTypeViewModel;
            set
            {
                if (_cardTypeViewModel is not null)
                {
                    _cardTypeViewModel.CardTypeCollectionChanged -= CardViewModel.OnCardTypeCollectionChanged;
                }

                if (!SetProperty(ref _cardTypeViewModel, value))
                {
                    return;
                }

                if (_cardTypeViewModel is not null)
                {
                    _cardTypeViewModel.CardTypeCollectionChanged += CardViewModel.OnCardTypeCollectionChanged;
                }
            }
        }

        public BrowserViewModel BrowserViewModel
        {
            get => _browserViewModel;
            set => SetProperty(ref _browserViewModel, value);
        }
    }
}
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Data;

namespace AssignmentCardEditor.ViewModels
{
    public class CardTypeBrowserViewModel : ObservableRecipient
    {
        private readonly IDbMethods _dbMethods;
        private string _selectedCardType;

        public CardTypeBrowserViewModel(IDbMethods dbMethods)
        {
            _dbMethods = dbMethods;
            InitCardTypeList();
        }

        public ObservableCollection<string> CardTypeNameCollection { get; set; } = new();

        public string SelectedCardType
        {
            get => _selectedCardType;
            set => SetProperty(ref _selectedCardType, value);
        }

        private void InitCardTypeList()
        {
            CardTypeNameCollection.Clear();
            var listCardTypes = _dbMethods.GetAllCardTypes();
            foreach (var cardType in listCardTypes)
            {
                CardTypeNameCollection.Add(cardType.Name);
            }
        }
    }
}
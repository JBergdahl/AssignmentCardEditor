using System.Windows;
using AssignmentCardEditor.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AssignmentCardEditor
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<MainWindowViewModel>();
        }

        private void CreateNewCardBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CardView.Visibility = Visibility.Visible;
            CreateNewCardBtn.Visibility = Visibility.Collapsed;
            CardRectangle.Visibility = Visibility.Collapsed;
            CardText.Visibility = Visibility.Collapsed;
        }

        private void CreateNewCardTypeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CardTypeView.Visibility = Visibility.Visible;
            CreateNewCardTypeBtn.Visibility = Visibility.Collapsed;
            CardTypeRectangle.Visibility = Visibility.Collapsed;
            CardTypeText.Visibility = Visibility.Collapsed;
        }

        private void BrowserViewBtn_OnClick(object sender, RoutedEventArgs e)
        {
            BrowserView.Visibility = Visibility.Visible;
            BrowserViewBtn.Visibility = Visibility.Collapsed;
            BrowserRectangle.Visibility = Visibility.Collapsed;
            BrowserText.Visibility = Visibility.Collapsed;
        }
    }
}
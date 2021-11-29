using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace AssignmentCardEditor.Views
{
    /// <summary>
    ///     Interaction logic for CardView.xaml
    /// </summary>
    public partial class CardView : UserControl
    {
        public CardView()
        {
            InitializeComponent();
        }

        private void CardNumbers_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[0-9]+$"))
            {
                e.Handled = true;
            }
        }

        private void CardText_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[a-zA-Z0-9_.-]+$"))
            {
                e.Handled = true;
            }
        }
    }
}
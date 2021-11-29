using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace AssignmentCardEditor.Views
{
    /// <summary>
    ///     Interaction logic for BrowserView.xaml
    /// </summary>
    public partial class BrowserView : UserControl
    {
        public BrowserView()
        {
            InitializeComponent();
        }

        private void SearchInput_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[a-zA-Z0-9_.-]+$"))
            {
                e.Handled = true;
            }
        }
    }
}
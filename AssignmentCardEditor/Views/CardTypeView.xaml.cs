using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssignmentCardEditor.Views
{
    /// <summary>
    /// Interaction logic for CardTypeView.xaml
    /// </summary>
    public partial class CardTypeView : UserControl
    {
        public CardTypeView()
        {
            InitializeComponent();
        }

        private void CardTypeText_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[a-zA-Z0-9_.-]+$"))
            {
                e.Handled = true;
            }
        }

        private void CardTypeNumbers_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[0-9]+$"))
            {
                e.Handled = true;
            }
        }

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (e.Key == Key.Back)
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        textBox.Text = 0.ToString();
                        textBox.SelectAll();
                    }
                }
            }
        }
    }
}

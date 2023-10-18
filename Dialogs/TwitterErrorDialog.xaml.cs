using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace X_Desktop_Alternative.Dialogs
{
    public partial class TwitterErrorDialog : Window
    {
        public DialogsViewModel ViewModel;
        public TwitterErrorDialog()
        {
            InitializeComponent();
            ViewModel ??= new DialogsViewModel(); // Creating the instance
            DataContext = ViewModel; // Setting the data context
            SystemSounds.Exclamation.Play(); // Plays a sound
        }
        private void BtnClose_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Close Dialog Method
        {
            Close();
        }

        private void TitleBarButton_OnMouseEnter(object sender, MouseEventArgs e) // OnMouseOver Color change Method
        {
            if (sender is not Grid element) return; // Verifying that the element is correct
            var colorCode = ViewModel.GLOBAL.Theme.TitleBarButtonHoverColor; // Obtaining the color code
            element.Background = ViewModel.GLOBAL.Theme.HexToBrush(colorCode); // Converting and setting the color
            
        }
        private void TitleBarButton_OnMouseLeave(object sender, MouseEventArgs e) // OnMouseLeave color change Method
        {
            if (sender is not Grid element) return; // Verifying that the element is correct
            var colorCode = ViewModel.GLOBAL.Theme.PrimaryColor; // Obtaining the color code
            element.Background = ViewModel.GLOBAL.Theme.HexToBrush(colorCode); // Converting and setting the color
            
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) // Drag Dialog Method
        {
            DragMove();
        }
    }
}

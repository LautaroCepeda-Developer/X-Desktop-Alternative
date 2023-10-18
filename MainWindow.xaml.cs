using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using X_Desktop_Alternative.ViewModels;
using X_Desktop_Alternative.Views;

namespace X_Desktop_Alternative
{
    public partial class MainWindow : Window
    {
        public readonly MainWindowViewModel ViewModel = new();// Instance of the view model
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;// Setting the data context
        }

        #region // Title Bar Buttons Methods
        private void MainWindow_OnStateChanged(object? sender, EventArgs e) // Title Bar Button Figures Handler
        {
            switch (WindowState)
            {
                case WindowState.Maximized: // Setting the Maximized figure of Title Bar Normal-Maximize Button
                    WindowBody.Margin = new Thickness(7);
                    MaximizePath0.Data = Geometry.Parse("M4 1.2C4 0.537258 4.53726 0 5.2 0H22.8C23.4627 0 24 0.537258 24 1.2C24 1.86274 23.4627 2.4 22.8 2.4H5.2C4.53726 2.4 4 1.86274 4 1.2Z");
                    MaximizePath1.Data = Geometry.Parse("M5.2 20C4.53726 20 4 19.4627 4 18.8V1.2C4 0.537258 4.53726 0 5.2 0C5.86274 0 6.4 0.537258 6.4 1.2V18.8C6.4 19.4627 5.86274 20 5.2 20Z");
                    MaximizePath2.Data = Geometry.Parse("M22.8 20C22.1373 20 21.6 19.4627 21.6 18.8V1.2C21.6 0.537258 22.1373 0 22.8 0C23.4627 0 24 0.537258 24 1.2V18.8C24 19.4627 23.4627 20 22.8 20Z");
                    MaximizePath3.Data = Geometry.Parse("M4 18.8C4 18.1373 4.53726 17.6 5.2 17.6H22.8C23.4627 17.6 24 18.1373 24 18.8C24 19.4627 23.4627 20 22.8 20H5.2C4.53726 20 4 19.4627 4 18.8Z");
                    MaximizePath4.Data = Geometry.Parse("M0 5.2C0 4.53726 0.537258 4 1.2 4H18.8C19.4627 4 20 4.53726 20 5.2C20 5.86274 19.4627 6.4 18.8 6.4H1.2C0.537258 6.4 0 5.86274 0 5.2Z");
                    MaximizePath5.Data = Geometry.Parse("M1.2 24C0.537258 24 0 23.4627 0 22.8V5.2C0 4.53726 0.537258 4 1.2 4C1.86274 4 2.4 4.53726 2.4 5.2V22.8C2.4 23.4627 1.86274 24 1.2 24Z");
                    MaximizePath6.Data = Geometry.Parse("M18.8 24C18.1373 24 17.6 23.4627 17.6 22.8V5.2C17.6 4.53726 18.1373 4 18.8 4C19.4627 4 20 4.53726 20 5.2V22.8C20 23.4627 19.4627 24 18.8 24Z");
                    MaximizePath7.Data = Geometry.Parse("M0 22.8C0 22.1373 0.537258 21.6 1.2 21.6H18.8C19.4627 21.6 20 22.1373 20 22.8C20 23.4627 19.4627 24 18.8 24H1.2C0.537258 24 0 23.4627 0 22.8Z");
                    break;
                case WindowState.Normal: // Setting the Normal figure of Title Bar Normal-Maximize Button
                    WindowBody.Margin = new Thickness(0);
                    MaximizePath0.Data = Geometry.Parse("M0 1.44C0 0.64471 0.64471 0 1.44 0H22.56C23.3553 0 24 0.64471 24 1.44C24 2.23529 23.3553 2.88 22.56 2.88H1.44C0.64471 2.88 0 2.23529 0 1.44Z");
                    MaximizePath1.Data = Geometry.Parse("M1.44 24C0.64471 24 0 23.3553 0 22.56V1.44C0 0.64471 0.64471 0 1.44 0C2.23529 0 2.88 0.64471 2.88 1.44V22.56C2.88 23.3553 2.23529 24 1.44 24Z");
                    MaximizePath2.Data = Geometry.Parse("M22.56 24C21.7647 24 21.12 23.3553 21.12 22.56V1.44C21.12 0.64471 21.7647 0 22.56 0C23.3553 0 24 0.64471 24 1.44V22.56C24 23.3553 23.3553 24 22.56 24Z");
                    MaximizePath3.Data = Geometry.Parse("M0 22.56C0 21.7647 0.64471 21.12 1.44 21.12H22.56C23.3553 21.12 24 21.7647 24 22.56C24 23.3553 23.3553 24 22.56 24H1.44C0.64471 24 0 23.3553 0 22.56Z");
                    MaximizePath4.Data = Geometry.Empty;
                    MaximizePath5.Data = Geometry.Empty;
                    MaximizePath6.Data = Geometry.Empty;
                    MaximizePath7.Data = Geometry.Empty;
                    break;
                case WindowState.Minimized:
                    break;
                default: // Setting the default figure of Title Bar Normal-Maximize Button
                    MaximizePath0.Data = Geometry.Parse("M0 1.44C0 0.64471 0.64471 0 1.44 0H22.56C23.3553 0 24 0.64471 24 1.44C24 2.23529 23.3553 2.88 22.56 2.88H1.44C0.64471 2.88 0 2.23529 0 1.44Z");
                    MaximizePath1.Data = Geometry.Parse("M1.44 24C0.64471 24 0 23.3553 0 22.56V1.44C0 0.64471 0.64471 0 1.44 0C2.23529 0 2.88 0.64471 2.88 1.44V22.56C2.88 23.3553 2.23529 24 1.44 24Z");
                    MaximizePath2.Data = Geometry.Parse("M22.56 24C21.7647 24 21.12 23.3553 21.12 22.56V1.44C21.12 0.64471 21.7647 0 22.56 0C23.3553 0 24 0.64471 24 1.44V22.56C24 23.3553 23.3553 24 22.56 24Z");
                    MaximizePath3.Data = Geometry.Parse("M0 22.56C0 21.7647 0.64471 21.12 1.44 21.12H22.56C23.3553 21.12 24 21.7647 24 22.56C24 23.3553 23.3553 24 22.56 24H1.44C0.64471 24 0 23.3553 0 22.56Z");
                    MaximizePath4.Data = Geometry.Empty;
                    MaximizePath5.Data = Geometry.Empty;
                    MaximizePath6.Data = Geometry.Empty;
                    MaximizePath7.Data = Geometry.Empty;
                    break;
            }
        }
        private void WindowDragZone_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) // Drag Window Method
        {
            DragMove();
        }
        private void BtnMinimize_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Minimize Method
        {
            WindowState = WindowState.Minimized;
        }
        private void BtnNormalMaximize_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Normal-Maximize Method
        {
            WindowState = WindowState switch
            {
                WindowState.Normal => WindowState.Maximized,
                WindowState.Maximized => WindowState.Normal,
                _ => WindowState
            };
        }
        private void BtnClose_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Close Method
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
        #endregion

        #region // Side Bar Buttons Methods
        private void SideBarButton_OnMouseEnter(object sender, MouseEventArgs e) // OnMouseOver Color change Method
        {
            if (sender is not Grid element) return; // Verifying that the element is correct
            var colorCode = ViewModel.GLOBAL.Theme.TitleBarButtonHoverColor; // Obtaining the color code
            element.Background = ViewModel.GLOBAL.Theme.HexToBrush(colorCode); // Converting and setting the color
        }

        private void SideBarButton_OnMouseLeave(object sender, MouseEventArgs e) // OnMouseLeft Color change Method
        {
            if (sender is not Grid element) return; // Verifying that the element is correct
            var colorCode = ViewModel.GLOBAL.Theme.SideBarColor; // Obtaining the color code
            element.Background = ViewModel.GLOBAL.Theme.HexToBrush(colorCode); // Converting and setting the color
        }


        //Views instances
        private readonly HomeView _homeView = new();
        private readonly SettingsView _settingsView = new();

        private void BtnHome_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Render Home View
        {
            BodyRenderer.Navigate(_homeView);
            _homeView.ViewModel.GLOBAL.Token.WasTokensSetted();
            _homeView.ViewModel.GLOBAL.Token.GetTokensFromJson();
            _homeView.ViewModel.GLOBAL.Token.SetTwitterCredentials();
        }

        private void BtnSettings_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Render Settings View
        {
            BodyRenderer.Navigate(_settingsView);
            _settingsView.FillSettingsTokens();
        }
        #endregion
        
    }
}

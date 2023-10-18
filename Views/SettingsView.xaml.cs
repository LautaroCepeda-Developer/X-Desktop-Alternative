using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using X_Desktop_Alternative.Dialogs;
using X_Desktop_Alternative.ViewModels;

namespace X_Desktop_Alternative.Views
{
    public partial class SettingsView : Page
    {
        public SettingsViewModel ViewModel = new();
        private bool cbxChangeEventEnabled = true;
        public SettingsView()
        {
            InitializeComponent();
            DataContext = ViewModel;
            GetCbxItemThemeContent();
            AutoSelectCbxItem();
            FillSettingsTokens();

        }

        public void FillSettingsTokens() // Fill the fields with the saved tokens (if exists)
        {
            // Verifying if exists saved tokens
            if (!ViewModel.GLOBAL.Token.HasTokensSet) return; // if doesn't exists then do nothing

            // Setting the GLOBAL vars with the saved tokens
            ViewModel.GLOBAL.Token.GetTokensFromJson();

            // Setting the text of the Consumer Key Field
            PasswordBox0.Password = ViewModel.GLOBAL.Token.ConsumerKey;
            TextBox0.Text = ViewModel.GLOBAL.Token.ConsumerKey;

            // Setting the text of the Consumer Secret Field
            PasswordBox1.Password = ViewModel.GLOBAL.Token.ConsumerSecret;
            TextBox1.Text = ViewModel.GLOBAL.Token.ConsumerSecret;

            // Setting the text of the Access Token Field
            PasswordBox2.Password = ViewModel.GLOBAL.Token.AccessToken;
            TextBox2.Text = ViewModel.GLOBAL.Token.AccessToken;

            // Setting the text of the Access Secret Field
            PasswordBox3.Password = ViewModel.GLOBAL.Token.AccessSecret;
            TextBox3.Text = ViewModel.GLOBAL.Token.AccessSecret;

            // Setting the GLOBAL twitter credentials
            ViewModel.GLOBAL.Token.SetTwitterCredentials();
        }

        private void GetCbxItemThemeContent() // Changes the value of Auto-Detect ComboBox Item
        {
            CbxItemAutoDetect.Content = ViewModel.GLOBAL.Theme.CurrentTheme is "Auto-Detect (Dark)" or "Auto-Detect (Light)" ? ViewModel.GLOBAL.Theme.CurrentTheme : "Auto-Detect";
        }

        private void AutoSelectCbxItem() // Set the combobox selected item to current theme
        {
            // Disabling the OnSelectionChanged event of ComboBox
            cbxChangeEventEnabled = false;

            // Auto-Selecting the current theme in the ComboBox
            CbxTheme.SelectedIndex = ViewModel.GLOBAL.Theme.CurrentTheme switch
            {
                "Auto-Detect (Dark)" or "Auto-Detect (Light)" => 0,
                "Dark" => 1,
                "Light" => 2,
                _ => CbxTheme.SelectedIndex
            };

            // Enabling the OnSelectionChanged event of ComboBox
            cbxChangeEventEnabled = true;
        }

        private void CbxTheme_OnSelectionChanged(object sender, SelectionChangedEventArgs e) // ComboBox Selection Changed EVENT
        {
            if (!cbxChangeEventEnabled) return; // Checks if event is enabled
            WarnDialog changedDialog = new(); // Instance of warn dialog
            switch (CbxTheme.SelectedIndex)
            {
                case 0: // Index 0 = AutoDetect
                    changedDialog.ShowDialog(); // Restart App Warn
                    ViewModel.GLOBAL.Theme.GetUserOsTheme(); // Try to get the user OS theme
                    ViewModel.GLOBAL.Theme.SetThemeConfig("Auto-Detect",false); // Writes Json Config
                    ViewModel.GLOBAL.Theme.GetThemeConfig(); // Gets Json Config and Update GLOBAL vars
                    GetCbxItemThemeContent(); // Sets content of Auto-Detect ComboBox Item
                    break;
                case 1: // Index 1 = Dark
                    changedDialog.ShowDialog(); // Restart App Warn
                    ViewModel.GLOBAL.Theme.SetDarkTheme(); // Sets the Dark Theme
                    ViewModel.GLOBAL.Theme.SetThemeConfig("Dark",true); // Writes Json Config
                    ViewModel.GLOBAL.Theme.GetThemeConfig(); // Gets Json Config and Update GLOBAL vars
                    break;
                case 2: // Index 2 = Light
                    changedDialog.ShowDialog(); // Restart App Warn
                    ViewModel.GLOBAL.Theme.SetLightTheme(); // Sets the Light Theme
                    ViewModel.GLOBAL.Theme.SetThemeConfig("Light",true); // Writes Json Config
                    ViewModel.GLOBAL.Theme.GetThemeConfig(); // Gets Json Config and Update GLOBAL vars
                    break;
            }
        }



        #region // Tokens Methods
        private void EyeButton_OnMouseEnter(object sender, MouseEventArgs e) // OnMouseOver Color change Method
        {
            if (sender is not Grid element) return; // Verifying that the element is correct
            var colorCode = ViewModel.GLOBAL.Theme.TitleBarButtonHoverColor; // Obtaining the color code
            element.Background = ViewModel.GLOBAL.Theme.HexToBrush(colorCode); // Converting and setting the color
            
        }
        private void EyeButton_OnMouseLeave(object sender, MouseEventArgs e) // OnMouseLeave color change Method
        {
            if (sender is not Grid element) return; // Verifying that the element is correct
            var colorCode = ViewModel.GLOBAL.Theme.BodyColor; // Obtaining the color code
            element.Background = ViewModel.GLOBAL.Theme.HexToBrush(colorCode); // Converting and setting the color
        }

        // State Management Vars
        private bool password0Visible;
        private bool password1Visible;
        private bool password2Visible;
        private bool password3Visible;

        // Show / Hide Token Methods
        private void BtnEye0_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Shows/Hide the Consumer Key Field
        {
            if (!password0Visible)
            {
                // State changing
                password0Visible = true;

                // Shows the token
                TextBox0.Text = PasswordBox0.Password;
                PasswordBox0.Visibility = Visibility.Hidden;
                TextBox0.Visibility = Visibility.Visible;

                // Open Eye
                Eye0Path0.Data = Geometry.Parse("M13 13.5C16.3745 13.5 19.4543 12.8172 21.7089 11.6899C23.9336 10.5775 25.5 8.94827 25.5 7C25.5 5.05173 23.9336 3.42251 21.7089 2.31015C19.4543 1.18283 16.3745 0.5 13 0.5C9.6255 0.5 6.54574 1.18283 4.29111 2.31015C2.06639 3.42251 0.5 5.05173 0.5 7C0.5 8.94827 2.06639 10.5775 4.29111 11.6899C6.54574 12.8172 9.6255 13.5 13 13.5Z");
                Eye0Path1.Data = Geometry.Parse("M17.5 7C17.5 9.48528 15.4853 11.5 13 11.5C10.5147 11.5 8.5 9.48528 8.5 7C8.5 4.51472 10.5147 2.5 13 2.5C15.4853 2.5 17.5 4.51472 17.5 7Z");
                Eye0Path2.Data = Geometry.Parse("M15 7C15 8.10457 14.1046 9 13 9C11.8954 9 11 8.10457 11 7C11 5.89543 11.8954 5 13 5C14.1046 5 15 5.89543 15 7Z");
                Eye0Path3.Data = Geometry.Empty;
            }
            else
            {
                // State changing
                password0Visible = false;

                // Hides the token
                PasswordBox0.Password = TextBox0.Text;
                PasswordBox0.Visibility = Visibility.Visible;
                TextBox0.Visibility = Visibility.Hidden;

                // Closed Eye
                Eye0Path0.Data = Geometry.Parse("M13 15.5C16.3745 15.5 19.4543 14.8172 21.7089 13.6899C23.9336 12.5775 25.5 10.9483 25.5 9C25.5 7.05173 23.9336 5.42251 21.7089 4.31015C19.4543 3.18283 16.3745 2.5 13 2.5C9.6255 2.5 6.54574 3.18283 4.29111 4.31015C2.06639 5.42251 0.5 7.05173 0.5 9C0.5 10.9483 2.06639 12.5775 4.29111 13.6899C6.54574 14.8172 9.6255 15.5 13 15.5Z");
                Eye0Path1.Data = Geometry.Parse("M17.5 9C17.5 11.4853 15.4853 13.5 13 13.5C10.5147 13.5 8.5 11.4853 8.5 9C8.5 6.51472 10.5147 4.5 13 4.5C15.4853 4.5 17.5 6.51472 17.5 9Z");
                Eye0Path2.Data = Geometry.Parse("M15 9C15 10.1046 14.1046 11 13 11C11.8954 11 11 10.1046 11 9C11 7.89543 11.8954 7 13 7C14.1046 7 15 7.89543 15 9Z");
                Eye0Path3.Data = Geometry.Parse("M4.70711 0.707108C5.09763 0.316584 5.7308 0.316584 6.12132 0.707108L21.6777 16.2635C22.0682 16.654 22.0682 17.2871 21.6777 17.6777C21.2871 18.0682 20.654 18.0682 20.2635 17.6777L4.70711 2.12132C4.31658 1.7308 4.31658 1.09763 4.70711 0.707108Z");
            }
        }
        private void BtnEye1_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Shows/Hide the Consumer Secret Field
        {
            if (!password1Visible)
            {
                // State changing
                password1Visible = true;

                // Shows the token
                TextBox1.Text = PasswordBox1.Password;
                PasswordBox1.Visibility = Visibility.Hidden;
                TextBox1.Visibility = Visibility.Visible;

                // Open Eye
                Eye1Path0.Data = Geometry.Parse("M13 13.5C16.3745 13.5 19.4543 12.8172 21.7089 11.6899C23.9336 10.5775 25.5 8.94827 25.5 7C25.5 5.05173 23.9336 3.42251 21.7089 2.31015C19.4543 1.18283 16.3745 0.5 13 0.5C9.6255 0.5 6.54574 1.18283 4.29111 2.31015C2.06639 3.42251 0.5 5.05173 0.5 7C0.5 8.94827 2.06639 10.5775 4.29111 11.6899C6.54574 12.8172 9.6255 13.5 13 13.5Z");
                Eye1Path1.Data = Geometry.Parse("M17.5 7C17.5 9.48528 15.4853 11.5 13 11.5C10.5147 11.5 8.5 9.48528 8.5 7C8.5 4.51472 10.5147 2.5 13 2.5C15.4853 2.5 17.5 4.51472 17.5 7Z");
                Eye1Path2.Data = Geometry.Parse("M15 7C15 8.10457 14.1046 9 13 9C11.8954 9 11 8.10457 11 7C11 5.89543 11.8954 5 13 5C14.1046 5 15 5.89543 15 7Z");
                Eye1Path3.Data = Geometry.Empty;
            }
            else
            {
                // State changing
                password1Visible = false;

                // Hides the token
                PasswordBox1.Password = TextBox1.Text;
                PasswordBox1.Visibility = Visibility.Visible;
                TextBox1.Visibility = Visibility.Hidden;

                // Closed Eye
                Eye1Path0.Data = Geometry.Parse("M13 15.5C16.3745 15.5 19.4543 14.8172 21.7089 13.6899C23.9336 12.5775 25.5 10.9483 25.5 9C25.5 7.05173 23.9336 5.42251 21.7089 4.31015C19.4543 3.18283 16.3745 2.5 13 2.5C9.6255 2.5 6.54574 3.18283 4.29111 4.31015C2.06639 5.42251 0.5 7.05173 0.5 9C0.5 10.9483 2.06639 12.5775 4.29111 13.6899C6.54574 14.8172 9.6255 15.5 13 15.5Z");
                Eye1Path1.Data = Geometry.Parse("M17.5 9C17.5 11.4853 15.4853 13.5 13 13.5C10.5147 13.5 8.5 11.4853 8.5 9C8.5 6.51472 10.5147 4.5 13 4.5C15.4853 4.5 17.5 6.51472 17.5 9Z");
                Eye1Path2.Data = Geometry.Parse("M15 9C15 10.1046 14.1046 11 13 11C11.8954 11 11 10.1046 11 9C11 7.89543 11.8954 7 13 7C14.1046 7 15 7.89543 15 9Z");
                Eye1Path3.Data = Geometry.Parse("M4.70711 0.707108C5.09763 0.316584 5.7308 0.316584 6.12132 0.707108L21.6777 16.2635C22.0682 16.654 22.0682 17.2871 21.6777 17.6777C21.2871 18.0682 20.654 18.0682 20.2635 17.6777L4.70711 2.12132C4.31658 1.7308 4.31658 1.09763 4.70711 0.707108Z");
            }
        }
        private void BtnEye2_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Shows/Hide the Access Token Field
        {
            if (!password2Visible)
            {
                // State changing
                password2Visible = true;

                // Shows the token
                TextBox2.Text = PasswordBox2.Password;
                PasswordBox2.Visibility = Visibility.Hidden;
                TextBox2.Visibility = Visibility.Visible;

                // Open Eye
                Eye2Path0.Data = Geometry.Parse("M13 13.5C16.3745 13.5 19.4543 12.8172 21.7089 11.6899C23.9336 10.5775 25.5 8.94827 25.5 7C25.5 5.05173 23.9336 3.42251 21.7089 2.31015C19.4543 1.18283 16.3745 0.5 13 0.5C9.6255 0.5 6.54574 1.18283 4.29111 2.31015C2.06639 3.42251 0.5 5.05173 0.5 7C0.5 8.94827 2.06639 10.5775 4.29111 11.6899C6.54574 12.8172 9.6255 13.5 13 13.5Z");
                Eye2Path1.Data = Geometry.Parse("M17.5 7C17.5 9.48528 15.4853 11.5 13 11.5C10.5147 11.5 8.5 9.48528 8.5 7C8.5 4.51472 10.5147 2.5 13 2.5C15.4853 2.5 17.5 4.51472 17.5 7Z");
                Eye2Path2.Data = Geometry.Parse("M15 7C15 8.10457 14.1046 9 13 9C11.8954 9 11 8.10457 11 7C11 5.89543 11.8954 5 13 5C14.1046 5 15 5.89543 15 7Z");
                Eye2Path3.Data = Geometry.Empty;
            }
            else
            {
                // State changing
                password2Visible = false;

                // Hides the token
                PasswordBox2.Password = TextBox2.Text;
                PasswordBox2.Visibility = Visibility.Visible;
                TextBox2.Visibility = Visibility.Hidden;

                // Closed Eye
                Eye2Path0.Data = Geometry.Parse("M13 15.5C16.3745 15.5 19.4543 14.8172 21.7089 13.6899C23.9336 12.5775 25.5 10.9483 25.5 9C25.5 7.05173 23.9336 5.42251 21.7089 4.31015C19.4543 3.18283 16.3745 2.5 13 2.5C9.6255 2.5 6.54574 3.18283 4.29111 4.31015C2.06639 5.42251 0.5 7.05173 0.5 9C0.5 10.9483 2.06639 12.5775 4.29111 13.6899C6.54574 14.8172 9.6255 15.5 13 15.5Z");
                Eye2Path1.Data = Geometry.Parse("M17.5 9C17.5 11.4853 15.4853 13.5 13 13.5C10.5147 13.5 8.5 11.4853 8.5 9C8.5 6.51472 10.5147 4.5 13 4.5C15.4853 4.5 17.5 6.51472 17.5 9Z");
                Eye2Path2.Data = Geometry.Parse("M15 9C15 10.1046 14.1046 11 13 11C11.8954 11 11 10.1046 11 9C11 7.89543 11.8954 7 13 7C14.1046 7 15 7.89543 15 9Z");
                Eye2Path3.Data = Geometry.Parse("M4.70711 0.707108C5.09763 0.316584 5.7308 0.316584 6.12132 0.707108L21.6777 16.2635C22.0682 16.654 22.0682 17.2871 21.6777 17.6777C21.2871 18.0682 20.654 18.0682 20.2635 17.6777L4.70711 2.12132C4.31658 1.7308 4.31658 1.09763 4.70711 0.707108Z");
            }
        }
        private void BtnEye3_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Shows/Hide the Access Secret Field
        {
             if (!password3Visible)
             {
                 // State changing
                 password3Visible = true;

                 // Shows the token
                 TextBox3.Text = PasswordBox3.Password;
                 PasswordBox3.Visibility = Visibility.Hidden;
                 TextBox3.Visibility = Visibility.Visible;

                 // Open Eye
                 Eye3Path0.Data = Geometry.Parse("M13 13.5C16.3745 13.5 19.4543 12.8172 21.7089 11.6899C23.9336 10.5775 25.5 8.94827 25.5 7C25.5 5.05173 23.9336 3.42251 21.7089 2.31015C19.4543 1.18283 16.3745 0.5 13 0.5C9.6255 0.5 6.54574 1.18283 4.29111 2.31015C2.06639 3.42251 0.5 5.05173 0.5 7C0.5 8.94827 2.06639 10.5775 4.29111 11.6899C6.54574 12.8172 9.6255 13.5 13 13.5Z");
                 Eye3Path1.Data = Geometry.Parse("M17.5 7C17.5 9.48528 15.4853 11.5 13 11.5C10.5147 11.5 8.5 9.48528 8.5 7C8.5 4.51472 10.5147 2.5 13 2.5C15.4853 2.5 17.5 4.51472 17.5 7Z");
                 Eye3Path2.Data = Geometry.Parse("M15 7C15 8.10457 14.1046 9 13 9C11.8954 9 11 8.10457 11 7C11 5.89543 11.8954 5 13 5C14.1046 5 15 5.89543 15 7Z");
                 Eye3Path3.Data = Geometry.Empty;
             }
             else
             {
                 // State changing
                 password3Visible = false;

                 // Hides the token
                 PasswordBox3.Password = TextBox3.Text;
                 PasswordBox3.Visibility = Visibility.Visible;
                 TextBox3.Visibility = Visibility.Hidden;

                 // Closed Eye
                 Eye3Path0.Data = Geometry.Parse("M13 15.5C16.3745 15.5 19.4543 14.8172 21.7089 13.6899C23.9336 12.5775 25.5 10.9483 25.5 9C25.5 7.05173 23.9336 5.42251 21.7089 4.31015C19.4543 3.18283 16.3745 2.5 13 2.5C9.6255 2.5 6.54574 3.18283 4.29111 4.31015C2.06639 5.42251 0.5 7.05173 0.5 9C0.5 10.9483 2.06639 12.5775 4.29111 13.6899C6.54574 14.8172 9.6255 15.5 13 15.5Z");
                 Eye3Path1.Data = Geometry.Parse("M17.5 9C17.5 11.4853 15.4853 13.5 13 13.5C10.5147 13.5 8.5 11.4853 8.5 9C8.5 6.51472 10.5147 4.5 13 4.5C15.4853 4.5 17.5 6.51472 17.5 9Z");
                 Eye3Path2.Data = Geometry.Parse("M15 9C15 10.1046 14.1046 11 13 11C11.8954 11 11 10.1046 11 9C11 7.89543 11.8954 7 13 7C14.1046 7 15 7.89543 15 9Z");
                 Eye3Path3.Data = Geometry.Parse("M4.70711 0.707108C5.09763 0.316584 5.7308 0.316584 6.12132 0.707108L21.6777 16.2635C22.0682 16.654 22.0682 17.2871 21.6777 17.6777C21.2871 18.0682 20.654 18.0682 20.2635 17.6777L4.70711 2.12132C4.31658 1.7308 4.31658 1.09763 4.70711 0.707108Z");
             }
        }
        #endregion


        private void BtnSaveTokens_OnMouseEnter(object sender, MouseEventArgs e) // OnMouseOver Color change Method
        {
            if (sender is not Grid element) return; // Verifying that the element is correct
            var colorCode = ViewModel.GLOBAL.Theme.TitleBarButtonHoverColor; // Obtaining the color code
            element.Background = ViewModel.GLOBAL.Theme.HexToBrush(colorCode); // Converting and setting the color
            
        }
        private void BtnSaveTokens_OnMouseLeave(object sender, MouseEventArgs e) // OnMouseLeave color change Method
        {
            if (sender is not Grid element) return; // Verifying that the element is correct
            var colorCode = ViewModel.GLOBAL.Theme.WindowColor; // Obtaining the color code
            element.Background = ViewModel.GLOBAL.Theme.HexToBrush(colorCode); // Converting and setting the color
        }

        private void ResetFieldBorderColors() // Resets the border color of all Token Fields
        {
            PasswordBox0.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.DialogBorderColor);
            PasswordBox1.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.DialogBorderColor);
            PasswordBox2.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.DialogBorderColor);
            PasswordBox3.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.DialogBorderColor);

            TextBox0.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.DialogBorderColor);
            TextBox1.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.DialogBorderColor);
            TextBox2.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.DialogBorderColor);
            TextBox3.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.DialogBorderColor);
        }
        
        private void BtnSaveTokens_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Try to save the tokens
        {
            // Verifying that all tokens are hidden
            if (password0Visible || password1Visible || password2Visible || password3Visible)
            {
                HideTokensDialog hideTokensDialog = new(); // Instance of dialog
                hideTokensDialog.ShowDialog(); // Shows a warn as Dialog
            }
            else
            {
                // Valid Token vars
                bool _validToken0 = true;
                bool _validToken1 = true;
                bool _validToken2 = true;
                bool _validToken3 = true;

                // Checking that the Consumer Key field is not empty
                if (PasswordBox0.Password.Trim() == "")
                {
                    _validToken0 = false;
                    PasswordBox0.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.ErrorColor);
                    TextBox0.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.ErrorColor);
                }
                else
                {
                    PasswordBox0.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.CorrectColor);
                    TextBox0.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.CorrectColor);
                }

                // Checking that the Consumer Secret field is not empty
                if (PasswordBox1.Password.Trim() == "")
                {
                    _validToken1 = false;
                    PasswordBox1.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.ErrorColor);
                    TextBox1.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.ErrorColor);
                }
                else
                {
                    PasswordBox1.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.CorrectColor);
                    TextBox1.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.CorrectColor);
                }

                // Checking that the Access Token field is not empty
                if (PasswordBox2.Password.Trim() == "")
                {
                    _validToken2 = false;
                    PasswordBox2.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.ErrorColor);
                    TextBox2.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.ErrorColor);
                }
                else
                {
                    PasswordBox2.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.CorrectColor);
                    TextBox2.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.CorrectColor);
                }

                // Checking that the Access Secret field is not empty
                if (PasswordBox3.Password.Trim() == "")
                {
                    _validToken3 = false;
                    PasswordBox3.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.ErrorColor);
                    TextBox3.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.ErrorColor);
                }
                else
                {
                    PasswordBox3.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.CorrectColor);
                    TextBox3.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.CorrectColor);
                }

                // Verifying that fields are valid
                if (_validToken0 && _validToken1 && _validToken2 && _validToken3)
                {
                    // Save the tokens in a Json
                    ViewModel.GLOBAL.Token.SetTokens(PasswordBox0.Password.Trim(), PasswordBox1.Password.Trim(), PasswordBox2.Password.Trim(), PasswordBox3.Password.Trim());

                    TokenSavedSuccessfullyDialog savedDialog = new(); // Creating an instance of a dialog
                    savedDialog.ShowDialog(); // Showing the Successfully Saved Dialog

                    // Set the GLOBAL twitter credentials
                    ViewModel.GLOBAL.Token.SetTwitterCredentials();

                    // Resetting the border color of all token fields
                    ResetFieldBorderColors();
                }
            }
        }
        

    }
}

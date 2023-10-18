using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tweetinvi;
using Tweetinvi.Exceptions;
using X_Desktop_Alternative.Dialogs;
using X_Desktop_Alternative.ViewModels;

namespace X_Desktop_Alternative.Views
{

    public partial class HomeView : Page
    {
        public HomeViewModel ViewModel = new ();
        private const int charLimit = 280; // Max char limit of normal tweet
        private bool isTweetValid; // Tweet State Var
        private bool enablePostEvent = true;

        public HomeView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }


        private void TextBoxTweet_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            switch (TextBoxTweet.Text.Length)
            {
                case > 0 when TextBoxTweet.Text.Length <= charLimit: // Valid case (Length > 0 && Length <= charLimit) 
                    BorderTextBoxTweet.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.CorrectColor); // Border Color to Green
                    LabelRemainingChars.Foreground = ViewModel.GLOBAL.Theme.HexToBrush("#FFFFFF"); // Char Counter Label Color to White
                    isTweetValid = true; // Updating Tweet State Var
                    break;
                case 0: // Invalid case (Length == 0)
                    BorderTextBoxTweet.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.DialogBorderColor); // Border Color to Default
                    LabelRemainingChars.Foreground = ViewModel.GLOBAL.Theme.HexToBrush("#FFFFFF"); // Char Counter Label Color to White
                    isTweetValid = false; // Updating Tweet State Var
                    break;
                default: // Invalid case (Length > charLimit)
                    BorderTextBoxTweet.BorderBrush = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.ErrorColor); // Border Color to Red
                    LabelRemainingChars.Foreground = ViewModel.GLOBAL.Theme.HexToBrush(ViewModel.GLOBAL.Theme.ErrorColor); // Char Counter Label Color to Red
                    isTweetValid = false; // Updating Tweet State Var
                    break;
            }

            // Updating the Remaining Char Counter
            LabelRemainingChars.Content = (charLimit - TextBoxTweet.Text.Length).ToString();
        }

        private void LabelRemainingChars_OnLoaded(object sender, RoutedEventArgs e)
        {
            LabelRemainingChars.Content = charLimit.ToString(); // Setting the label text to default charLimit
        }



        #region // Buttons Methods
        private void Button_OnMouseEnter(object sender, MouseEventArgs e) // OnMouseOver Color change Method
        {
            if (sender is not Grid element) return; // Verifying that the element is correct
            var colorCode = ViewModel.GLOBAL.Theme.TitleBarButtonHoverColor; // Obtaining the color code
            element.Background = ViewModel.GLOBAL.Theme.HexToBrush(colorCode); // Converting and setting the color
            
        }
        private void Button_OnMouseLeave(object sender, MouseEventArgs e) // OnMouseLeave color change Method
        {
            if (sender is not Grid element) return; // Verifying that the element is correct
            var colorCode = ViewModel.GLOBAL.Theme.WindowColor; // Obtaining the color code
            element.Background = ViewModel.GLOBAL.Theme.HexToBrush(colorCode); // Converting and setting the color
        }
        private async void BtnPost_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) // Post the tweet
        {
            if (!ViewModel.GLOBAL.Token.HasTokensSet || !isTweetValid || !enablePostEvent) return;
            // Disabling event to avoid spam click
            enablePostEvent = false;

            //Post the tweet
            await PublishTweet(TextBoxTweet.Text);
        }
        public async Task PublishTweet(string tweetText)
        {
            if (ViewModel.GLOBAL.Token.TwitterCredentials != null)
            {
                // Getting credentials
                TwitterClient client = new(ViewModel.GLOBAL.Token.TwitterCredentials);
                try
                {
                    // Auth in the account
                    await client.Users.GetAuthenticatedUserAsync();

                    // Posting the tweet
                    var tweet = await client.Tweets.PublishTweetAsync(tweetText);

                    var publishedTweet = await client.Tweets.GetTweetAsync(tweet.Id); // Getting the posted Tweet

                    // Showing the link to the posted tweet
                    TweetHyperLinkText.Text = "Link to your tweet";
                    TweetHyperLinkUri.NavigateUri = new Uri(publishedTweet.Url);

                    enablePostEvent = true; // Enabling the click event for post button
                    TweetPostedSuccessfullyDialog tweetPostedSuccessfullyDialog = new();
                    tweetPostedSuccessfullyDialog.ShowDialog();
                }
                catch (TwitterException exception)
                {
                    switch (exception.StatusCode)
                    {
                        case 401: // Tokens expired or invalid
                        {
                            InvalidTokensDialog invalidTokensDialog = new();
                            invalidTokensDialog.ShowDialog();
                            enablePostEvent = true; // Enabling the click event for post button
                            break;
                        }
                        case 403: // Insufficient Access Level
                        {
                            InsufficientAccessDialog insufficientAccessDialog = new();
                            insufficientAccessDialog.ShowDialog();
                            enablePostEvent = true; // Enabling the click event for post button
                            break;
                        }
                        default: // Other error
                        {
                            TwitterErrorDialog twitterErrorDialog = new();
                            twitterErrorDialog.ShowDialog();
                            enablePostEvent = true; // Enabling the click event for post button
                            break;
                        }
                    }
                }
                catch (Exception) // Other error
                {
                    TwitterErrorDialog twitterErrorDialog = new();
                    twitterErrorDialog.ShowDialog();
                    enablePostEvent = true; // Enabling the click event for post button
                }
            }

        }
        
        #endregion




    }
}

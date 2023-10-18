using System.IO;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace X_Desktop_Alternative.Models;

public class ThemesModel
{
    // Theme vars
    public string? WindowColor              { get; set; }
    public string? PrimaryColor             { get; set; }
    public string? BodyColor                { get; set; }
    public string? SideBarColor             { get; set; }
    public string? SecondaryColor           { get; set; }
    public string? AccentColor              { get; set; }
    public string? CorrectColor             { get; set; }
    public string? ErrorColor               { get; set; }
    public string? TextColor                { get; set; }
    public string? VectorsColor             { get; set; }
    public string? CbxTextColor             { get; set; }
    public string? TitleBarButtonHoverColor { get; set; }
    public string? DialogBorderColor        { get; set; }
    public string? TweetTextBoxBgColor      { get; set; }
    public string? TweetTextBoxFgColor      { get; set; }
    public string? CurrentTheme             { get; set; }
    public string? UserOsTheme              { get; set; }
    public string? SelectedTheme            { get; set; }
    public bool OverrideOsTheme             { get; set; }

    #region // Theme Auto Detection Methods
    public void GetUserOsTheme() // Auto detect the current OS theme and set the same to the app
    {
        // Trying to access the registry
        using var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize\");
        if (key != null)
        {
            // Trying to get the current OS theme
            var themeValue = key.GetValue("SystemUsesLightTheme");
            if (themeValue is int themeSetting)
            {
                switch (themeSetting)
                {
                    case 0: // Registry Value 0 = Dark theme
                        UserOsTheme  = "DarkTheme";
                        CurrentTheme = "Auto-Detect (Dark)";
                        SetDarkTheme(true);
                        break;
                    case 1: // Registry Value 1 = Dark theme
                        UserOsTheme  = "LightTheme";
                        CurrentTheme = "Auto-Detect (Light)";
                        SetLightTheme(true);
                        break;
                    default: // Registry Value ? = Advice & Dark theme
                        UserOsTheme = null;
                        CurrentTheme = "Auto-Detect";
                        ThrowThemeAutoDetectAdvice();
                        break;
                }
            }
            else
            {
                ThrowThemeAutoDetectAdvice();
            }
        }
        else
        {
            ThrowThemeAutoDetectAdvice();
        }

    }
    private void ThrowThemeAutoDetectAdvice() // Shows a Message Box with an advice of a problem auto detecting the OS theme and sets DarkTheme
    {
        MessageBox.Show("The theme of your operative system could not be detected, the dark theme will be used in the application.", "Advice");
        CurrentTheme = "DarkTheme";
        SetDarkTheme();
    }
    #endregion

    #region // Theme Config Methods
    public void SetThemeConfig(string selectedTheme, bool overrideOsTheme) // Sets the theme in config json
    {
        // Reads the json
        string json = File.ReadAllText("config.json");
        
        // Deserealizing Json
        dynamic? config = JsonConvert.DeserializeObject(json);

        // Saving the new values of the json
        config.SelectedTheme = selectedTheme;
        config.OverrideOsTheme = overrideOsTheme;

        // Serealizing the json
        string updatedJson = JsonConvert.SerializeObject(config, Formatting.Indented);

        // Updating the json with the new values
        File.WriteAllText("config.json",updatedJson);
    }

    public void GetThemeConfig() // Gets the config values from the json
    {
        // Reads the json
        string json = File.ReadAllText("config.json");
        
        // Deserealizing Json
        dynamic? config = JsonConvert.DeserializeObject(json);

        // Reading and saving the values into GLOBAL vars
        SelectedTheme = config.SelectedTheme;
        OverrideOsTheme = config.OverrideOsTheme;
    }
    #endregion

    #region // Theme Colors Methods
    public void SetDarkTheme(bool autoDetected = false)
    {
        if (!autoDetected) CurrentTheme = "Dark"; // Checks if the theme was auto-detected
        WindowColor              = "#000000"; // Window (Literally)
        PrimaryColor             = "#0F0F0F"; // Title Bar & Footer & Title Bar Buttons (Grid)
        BodyColor                = "#0A0A0A"; // Body (Frame)
        SideBarColor             = "#000000"; // Sidebar (Grid)
        SecondaryColor           = "#5E5E5E"; // Buttons (Button)
        AccentColor              = "#00FFFF"; // Highlight (Border)
        CorrectColor             = "#00C700"; // Highlight (Border & Label)
        ErrorColor               = "#FF7070"; // Error Messages (Label)
        TextColor                = "#FFFFFF"; // Texts (Label)
        VectorsColor             = "#FFFFFF"; // Tittle Bar Vectors (Path)
        CbxTextColor             = "#000000"; // Combobox Items Text (ComboboxItem)
        TitleBarButtonHoverColor = "#616161"; // Title Bar Buttons HoverColor (Grid)
        DialogBorderColor        = "#1A1A1A"; // Dialog Border (Border)
        TweetTextBoxBgColor      = "#262626"; // TextBox Background (TextBox)
        TweetTextBoxFgColor      = "#F5F5F5"; // TextBox Foreground (TextBox)
    }
    public void SetLightTheme(bool autoDetected = false)
    {
        if (!autoDetected) CurrentTheme = "Light"; // Checks if the theme was auto-detected
        WindowColor              = "#FFFFFF"; // Window (Literally)
        PrimaryColor             = "#FFFFFF"; // Title Bar & Footer & Title Bar Buttons (Grid)
        BodyColor                = "#F2F2F2"; // Body (Frame)
        SideBarColor             = "#FFFFFF"; // Sidebar (Grid)
        SecondaryColor           = "#D6D6D6"; // Buttons (Button)
        AccentColor              = "#FFFF00"; // Highlight (Border)
        CorrectColor             = "#008A00"; // Highlight (Border)
        ErrorColor               = "#B30000"; // Error Messages (Label)
        TextColor                = "#000000"; // Texts (Label)
        VectorsColor             = "#000000"; // Tittle Bar Vectors (Path)
        CbxTextColor             = "#000000"; // Combobox Items Text (ComboboxItem)
        TitleBarButtonHoverColor = "#949494"; // Title Bar Buttons HoverColor (Grid)
        DialogBorderColor        = "#1A1A1A"; // Dialog Border (Border).
        TweetTextBoxBgColor      = "#F5F5F5"; // TextBox Background (TextBox)
        TweetTextBoxFgColor      = "#262626"; // TextBox Foreground (TextBox)
    }
    #endregion

    #region // Color Converter Method
    public static readonly BrushConverter CConverter = new(); // BrushConverter Instance
    public Brush? HexToBrush(string? hex) // Color converter function
    {
        if (hex == null) return (Brush?)CConverter.ConvertFromString("#0000ff"); // Verifying that the string is not empty
        var color = (Brush?)CConverter.ConvertFromString(hex); // Converting the color
        return color; // Returning the converted color
    }
    #endregion

}
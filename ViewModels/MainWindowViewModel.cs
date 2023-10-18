using System.Windows.Controls.Primitives;
using X_Desktop_Alternative.Models;

namespace X_Desktop_Alternative.ViewModels;

public class MainWindowViewModel
{
    // Instance to model
    public MainWindowModel Model { get; }

    // Instance to GLOBAL VAULT
    public GLOBAL_VAULT GLOBAL { get; }

    public MainWindowViewModel()
    {
        // Initializing the instances
        GLOBAL = new GLOBAL_VAULT();
        Model = new MainWindowModel();

        // Loading the theme setted
        HandleTheme();

        // Loading the saved tokens
        HandleTokens();
    }

    private void HandleTokens() // Try to load the saved tokens
    {
        // Verifying if the tokens was setted
        GLOBAL.Token.WasTokensSetted();

        // Getting the tokens and updating the GLOBAL vars
        if (GLOBAL.Token.HasTokensSet)
        {
            GLOBAL.Token.GetTokensFromJson();
        }
    }

    private void HandleTheme() // Try to load the saved theme config
    {
        // Verifying the theme config
        GLOBAL.Theme.GetThemeConfig();

        // checking if the OS theme wil be overrided
        if (!GLOBAL.Theme.OverrideOsTheme)
        {
            GLOBAL.Theme.GetUserOsTheme();
        }
        else switch (GLOBAL.Theme.SelectedTheme) // Getting the last theme selected by user
        {
            case "Dark":
                GLOBAL.Theme.SetDarkTheme(); // Setting Dark Theme
                break;
            case "Light":
                GLOBAL.Theme.SetLightTheme(); // Setting Light Theme
                break;
        }
    }
}
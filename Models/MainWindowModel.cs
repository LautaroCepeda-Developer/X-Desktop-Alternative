namespace X_Desktop_Alternative.Models;

public class MainWindowModel
{
    // Footer vars
    public string? AppCopyright { get; set; }
    public string? AppVersion   { get; set; }

    public MainWindowModel()
    {
        InitializeFooter();
    }

    private void InitializeFooter()
    {
        AppCopyright = "2023 \u00a9 Copyright | l__lau__u | All rights reserved";
        AppVersion = "1.0.0";
    }
}
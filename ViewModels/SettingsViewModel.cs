
using X_Desktop_Alternative.Models;

namespace X_Desktop_Alternative.ViewModels;

public class SettingsViewModel
{
    public MainWindowViewModel MainWindowViewModel { get; }
    public GLOBAL_VAULT GLOBAL { get; }
    public SettingsModel Model { get; }

    public SettingsViewModel()
    {
        MainWindowViewModel ??= new MainWindowViewModel();
        GLOBAL ??= new MainWindowViewModel().GLOBAL;
        Model = new SettingsModel();
    }
}
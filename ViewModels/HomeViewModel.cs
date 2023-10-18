using X_Desktop_Alternative.Models;

namespace X_Desktop_Alternative.ViewModels;

public class HomeViewModel
{
    public MainWindowViewModel MainWindowViewModel { get; }
    public GLOBAL_VAULT GLOBAL { get; }
    public HomeModel Model { get; }

    public HomeViewModel()
    {
        MainWindowViewModel ??= new MainWindowViewModel();
        GLOBAL ??= new MainWindowViewModel().GLOBAL;
        Model = new HomeModel();
    }
}
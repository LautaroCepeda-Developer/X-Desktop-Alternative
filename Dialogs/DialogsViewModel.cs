using X_Desktop_Alternative.ViewModels;

namespace X_Desktop_Alternative.Dialogs;

public class DialogsViewModel
{
    public MainWindowViewModel MainWindowViewModel { get; }
    public GLOBAL_VAULT GLOBAL { get; }
    public DialogsViewModel()
    {
        MainWindowViewModel ??= new MainWindowViewModel();
        GLOBAL = MainWindowViewModel.GLOBAL;
    }


}
using Prism.Mvvm;

namespace Chinwag.ViewModels;

public class MainWindowViewModel : BindableBase
{
    public MainWindowViewModel()
    {
        _title = "Chinwag";
    }
    private string _title;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

}
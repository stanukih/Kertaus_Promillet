using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kertaus_Promillet.ViewModels;

public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
        
    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public MainWindowViewModel()
    {
        RaisePropertyChanged(nameof(alkoholiJuomaPromile));
    }


    public int sukupuoli {get; set;}
    public int tunti {get; set;}

    public string[] alkoholiJuomatList { get; } =
    [
        "Olut 4,7‰",
        "Siideri 4,7‰",
        "Olut 5,5‰",
        "Lonkero 5,5‰",
        "Vahva siideri 8‰",
        "Viini 8‰",
        "Väkeväviini 8‰",
        "Likööri 16‰",
        "Konjakki 40‰",
        "Rommi 40‰",
        "Viski 40‰",
        "Muu"
    ];


    public int _alkoholiJuomaSelectedIndex = -1;

    public int alkoholiJuomaSelectedIndex
    {
        get
        {
            return _alkoholiJuomaSelectedIndex;
        }
        set
        {
            alkoholiJuomaPromileIsEnabled = false;
            _alkoholiJuomaSelectedIndex = value;
            RaisePropertyChanged(nameof(alkoholiJuomaPromileIsEnabled));
            if (value < 2)
            {
                alkoholiJuomaPromile = (decimal)4.7;
                RaisePropertyChanged(nameof(alkoholiJuomaPromile));
                return;
            }
            if (value < 4)
            {
                alkoholiJuomaPromile = (decimal)5.5;
                RaisePropertyChanged(nameof(alkoholiJuomaPromile));
                return;
            }
            if (value < 7)
            {
                alkoholiJuomaPromile = 8;
                RaisePropertyChanged(nameof(alkoholiJuomaPromile));
                return;
            }
            if (value == 7)
            {
                alkoholiJuomaPromile = 16;
                RaisePropertyChanged(nameof(alkoholiJuomaPromile));
                return;
            }
            if (value < 11)
            {
                alkoholiJuomaPromile = 40;
                RaisePropertyChanged(nameof(alkoholiJuomaPromile));
                return;
            }
            
            alkoholiJuomaPromileIsEnabled = true;
            alkoholiJuomaPromile = 0;
            RaisePropertyChanged(nameof(alkoholiJuomaPromile));
            RaisePropertyChanged(nameof(alkoholiJuomaPromileIsEnabled));
        }
    }

    public decimal alkoholiJuomaPromile { get; set; }
    public bool alkoholiJuomaPromileIsEnabled { get; set; } = false;

    public int juomatMäärä { get; set; } = 0;

    public void laske()
    {
        decimal sukupuolikerroin;
        int t = tunti;
        decimal alkoholimäärä = alkoholiJuomaPromile;
        if (sukupuoli==0) sukupuolikerroin = (decimal)0.7;
        else sukupuolikerroin = (decimal)0.6;
        
    }

}
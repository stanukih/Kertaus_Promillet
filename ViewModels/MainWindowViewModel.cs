using System;
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
        RaisePropertyChanged(nameof(alkoholiprosenttia));
    }


    public int sukupuoli {get; set;}
    public int tunti {get; set;}
    public int paino { get; set; } = 0;
    public string[] alkoholiJuomatList { get; } =
    [
        "Olut 4,7%",
        "Siideri 4,7%",
        "Olut 5,5%",
        "Lonkero 5,5%",
        "Vahva siideri 8%",
        "Viini 8%",
        "Väkeväviini 8%",
        "Likööri 16%",
        "Konjakki 40%",
        "Rommi 40%",
        "Viski 40%",
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
            alkoholiprosenttiaIsEnabled = false;
            _alkoholiJuomaSelectedIndex = value;
            RaisePropertyChanged(nameof(alkoholiprosenttiaIsEnabled));
            if (value < 2)
            {
                alkoholiprosenttia = (decimal)4.7;
                RaisePropertyChanged(nameof(alkoholiprosenttia));
                return;
            }
            if (value < 4)
            {
                alkoholiprosenttia = (decimal)5.5;
                RaisePropertyChanged(nameof(alkoholiprosenttia));
                return;
            }
            if (value < 7)
            {
                alkoholiprosenttia = 8;
                RaisePropertyChanged(nameof(alkoholiprosenttia));
                return;
            }
            if (value == 7)
            {
                alkoholiprosenttia = 16;
                RaisePropertyChanged(nameof(alkoholiprosenttia));
                return;
            }
            if (value < 11)
            {
                alkoholiprosenttia = 40;
                RaisePropertyChanged(nameof(alkoholiprosenttia));
                return;
            }
            
            alkoholiprosenttiaIsEnabled = true;
            alkoholiprosenttia = 0;
            RaisePropertyChanged(nameof(alkoholiprosenttia));
            RaisePropertyChanged(nameof(alkoholiprosenttiaIsEnabled));
        }
    }

    public decimal alkoholiprosenttia { get; set; }
    public bool alkoholiprosenttiaIsEnabled { get; set; } = false;

    public decimal juomatMäärä { get; set; } = 0;

    public void laske()
    {
        // decimal sukupuolikerroin;
        // int t = tunti;
        // decimal alkoholimäärä = alkoholiprosenttia;
        // if (sukupuoli==0) sukupuolikerroin = (decimal)0.7;
        // else sukupuolikerroin = (decimal)0.6;
        laskuri l = new laskuri(
            sukupuoli,
            paino,
            alkoholiprosenttia,
            juomatMäärä,
            tunti
            );
        res = $"Promillemäärä on {l.getAlkoholiVeressa():0.00}‰";
        RaisePropertyChanged(nameof(res));
    }

    public string res { get; set; }
}

public class laskuri
{
    public laskuri(
        int sukupuoliI,
        int painoI,
        decimal alkoholiprosenttiaI,
        decimal alkoholimääräI,
        int tuntiI
            )
    {
        sukupuoli = sukupuoliI;
        paino = painoI;
        alkoholiprosenttia = alkoholiprosenttiaI;
        alkoholimäärä = alkoholimääräI;
        tunti = tuntiI;
        alkoholiGr = (alkoholiprosenttia * alkoholimäärä * (decimal)7.89); 
        palamisvauhti = paino / 10;
    }

    public int sukupuoli; //0
    public int paino;
    public decimal alkoholiGr;
    public decimal alkoholiprosenttia;
    public decimal alkoholimäärä;
    public int tunti;
    public decimal palamisvauhti;

    public decimal getAlkoholiVeressa()
    {
        decimal sukupuolikerroin;
        if (sukupuoli==0) sukupuolikerroin = (decimal)0.7; //0 - mies 1 - nainen
        else sukupuolikerroin = (decimal)0.6;
        alkoholiGr -= palamisvauhti * tunti;
        if (alkoholiGr <= 0) return 0;
        return alkoholiGr / (paino * sukupuolikerroin);
    }

}
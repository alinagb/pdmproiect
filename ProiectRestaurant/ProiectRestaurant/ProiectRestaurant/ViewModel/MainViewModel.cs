using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProiectRestaurant.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Picks = GetPicks();
        }

        public List<Pick> Picks { get; set; }

        public ICommand OrderCommand => new Command(() => Application.Current.MainPage.Navigation.PushAsync(new OrderPage()));
        public ICommand DetailCommand => new Command(() => Application.Current.MainPage.Navigation.PushAsync(new DetailPage()));

        private List<Pick> GetPicks()
        {
            return new List<Pick>
            {
                new Pick { Title = "Mic dejun", Image = "IMG01.png",
                    Description = "Ziua bună o recunoști după… o dimineață pe cinste! Savurează un mic dejun delicios din meniul nostru!" },
                new Pick { Title = "Prânz", Image = "IMG03.png",
                    Description = "Vă garantăm un prânz gustos și copios!" }
            };
        }
    }

    public class Pick : BaseViewModel
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        bool _inCos;
        public bool InCos
        {
            get => _inCos;
            set
            {
                if (value != _inCos)
                {
                    _inCos = value;
                    OnPropertyChanged();
                }
            }
        }
    }

    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

}

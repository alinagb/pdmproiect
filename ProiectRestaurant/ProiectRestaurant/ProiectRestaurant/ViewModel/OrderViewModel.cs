using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProiectRestaurant.ViewModel
{
    public class OrderViewModel : BaseViewModel
    {
        public OrderViewModel()
        {
            MenuList = GetMenu();
            OrderList = new ObservableCollection<Pick>();
        }

        public List<Pick> MenuList { get; set; }
        public ObservableCollection<Pick> OrderList { get; set; }

        double _total;
        public double Total
        {
            get => _total;
            set
            {
                if (value != _total)
                {
                    _total = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardMonth { get; set; }
        public string CardYear { get; set; }
        public string CardCode { get; set; }

        public ICommand BackCommand => new Command(Back);

        private void Back(object obj)
        {
            if (obj is string value && value == "Home")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
            {
                Application.Current.MainPage.Navigation.PopAsync();
            }

        }

        public ICommand AddOrderCommand => new Command(AddOrder);

        public ICommand ProcessPaymentCommand => new Command(ProcessPayment);
        private void ProcessPayment(object obj)
        {
            if (obj is string option)
            {
                if (option == "Card")
                {
                    Application.Current.MainPage.Navigation.PushAsync(new CreditCardPaymentPage() { BindingContext = this });
                }
                else if (option == "Plata cu cardul")
                {
                    if (string.IsNullOrWhiteSpace(CardNumber))
                    {
                        Application.Current.MainPage.DisplayAlert("Informații", "Verificați numărul cardului", "Plată confirmată");
                    }
                    else if (CardNumber?.Length != 16)
                    {
                        Application.Current.MainPage.DisplayAlert("Informații", "Verificați numărul cardului, acesta trebuie să aibă 16 cifre", "Plată confirmată");
                    }
                    else if (string.IsNullOrWhiteSpace(CardName))
                    {
                        Application.Current.MainPage.DisplayAlert("Informații", "Verificați titularul cardului", "Plată confirmată");
                    }
                    else if (string.IsNullOrWhiteSpace(CardMonth))
                    {
                        Application.Current.MainPage.DisplayAlert("Informații", "Verificați luna cardului", "Plată confirmată");
                    }
                    else if (string.IsNullOrWhiteSpace(CardYear))
                    {
                        Application.Current.MainPage.DisplayAlert("Informații", "Verificați anul cardului", "Plată confirmată");
                    }
                    else if (string.IsNullOrWhiteSpace(CardCode))
                    {
                        Application.Current.MainPage.DisplayAlert("Informații", "Verificați codul de validare al cardului", "Plată confirmată");
                    }
                    else
                    {
                        Application.Current.MainPage.Navigation.PushAsync(new SuccessPage() { BindingContext = this });
                    }

                }
                else if (option == "Numerar")
                {
                    Application.Current.MainPage.Navigation.PushAsync(new SuccessPage() { BindingContext = this });
                }
            }
        }

        private void AddOrder(object obj)
        {
            var pick = obj as Pick;
            if (pick != null)
            {
                if (pick.InCos)
                {
                    OrderList.Remove(pick);
                    pick.InCos = false;
                }
                else
                {
                    pick.InCos = true;
                    OrderList.Add(pick);
                }
            }
            Total = OrderList.Sum(x => x.Price);
        }

        public ICommand ProcessCommand => new Command(ProcessOrder);
        private void ProcessOrder(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new SelectPaymentPage() { BindingContext = this });
        }

        private List<Pick> GetMenu()
        {
            return new List<Pick>
            {
                new Pick { Title = "Meniu suprem", Image = "IMG03.png", Description = "Carne de vită pe grărar, salata coleslaw, pui crispy și orez.", Price = 23.99  },
                new Pick { Title = "Salată de somon cu avocado", Image = "IMG04.png", Description = "Salată verde, baby spanac, roșii, somon la gratar, felii de avocado, castraveți, măr", Price = 19.99  },
                new Pick { Title = "Risotto cu pui", Image = "IMG05.png", Description = "Ciuperci sălbatice cu risotto și parmezan servit cu pui prăjit crocant.", Price = 25.25  }
            };
        }
    }
}

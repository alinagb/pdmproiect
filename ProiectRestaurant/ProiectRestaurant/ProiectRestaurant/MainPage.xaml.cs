using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProiectRestaurant
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Task.Run(RotireImagine);
        }
        private async void RotireImagine()
        {
            while (true)
            {
                await BannerImg.RelRotateTo(360, 10000, Easing.Linear);
            }
        }
    }
}

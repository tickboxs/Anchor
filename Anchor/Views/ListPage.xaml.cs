using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Anchor.ViewModels;

namespace Anchor.Views
{
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }

        void ListView_ItemTapped(
            Xamarin.Forms.ListView sender,
            Xamarin.Forms.ItemTappedEventArgs e)
        {
            //e.
            Page page;
            switch (e.ItemIndex)
            {
                case 0:
                    page = new LineChartPage();
                    break;
                case 1:
                    page = new BarChartPage();
                    break;
                case 2:
                    page = new PieChartPage();
                    break;
                case 3:
                    page = new RadarChartPage();
                    break;
                case 4:
                    page = new PolarChartPage();
                    break;
                case 5:
                    page = new ScatterChartPage();
                    break;
                default:
                    page = new LineChartPage();
                    break;
            }
            Navigation.PushAsync(page);
        }

    }
}

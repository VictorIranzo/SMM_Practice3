﻿using IncrementalListView.FormsPlugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourFavourites.Components;
using YourFavourites.Data;

namespace YourFavourites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongsPage : ContentPage, BackablePage
    {
        public MainPage MainPage { get; set; }

        public SongsPage(MainPage mainPage)
        {
            this.MainPage = mainPage;

            BindingContext = new SongsListIncrementalView();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SongsListIncrementalView vm = BindingContext as SongsListIncrementalView;

            vm.LoadMoreItemsCommand.Execute(null);
        }

        async void OnSongClick(object sender, ItemTappedEventArgs e)
        {
            Page page = new SongDetails((Song)e.Item, this);
            page.Title = ((Song)e.Item).MainTitle;

            this.MainPage.SetDetailPage(page);
        }
    }
}
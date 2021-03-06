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
    public partial class BooksPage : ContentPage, BackablePage
    {
        public MainPage MainPage { get; set; }

        public BooksPage(MainPage mainPage)
        {
            this.MainPage = mainPage;

            BindingContext = new BooksIncrementalView(BooksManager.Categories.Values.FirstOrDefault());

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BooksIncrementalView vm = BindingContext as BooksIncrementalView;

            vm.LoadMoreItemsCommand.Execute(null);

            foreach (string category in BooksManager.Categories.Keys)
            {
                listPicker.Items.Add(category);
            }
        }

        async void OnBookClicked(object sender, ItemTappedEventArgs e)
        {
            Page page = new BookDetails((Book)e.Item, this);
            page.Title = ((Book)e.Item).MainTitle;

            this.MainPage.SetDetailPage(page);
        }

        async void OnPickerSelectedItem(object sender, EventArgs e)
        {
            BooksIncrementalView booksListIncrementalView = null;

            string selectedCategory = listPicker.Items[listPicker.SelectedIndex];
            string listName = BooksManager.GetListNameByCategory(selectedCategory);

            booksListIncrementalView = new BooksIncrementalView(listName);

            BindingContext = booksListIncrementalView;

            booksListIncrementalView.LoadMoreItemsCommand.Execute(null);
        }
    }
}
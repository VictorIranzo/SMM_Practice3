﻿using AndroidAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourFavourites.Data;
using YourFavourites.Services;

namespace YourFavourites
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieDetails : ContentPage
	{
        readonly BackablePage moviesPage;
        readonly Movie currentMovie;
        bool IsFavourite;

		public MovieDetails (Movie movie, BackablePage moviesPage)
		{
            this.currentMovie = movie;
            this.moviesPage = moviesPage;

            BindingContext = movie;

            FirebaseService firebaseService = new FirebaseService();

            IsFavourite = firebaseService.CheckIfIsFavourite(AccountManager.GetAccountId(), currentMovie).Result;

            InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetFavButton();
        }

        void OnAddFavClicked(Object sender, EventArgs e)
        {
            FirebaseService firebaseService = new FirebaseService();

            if (IsFavourite)
            {
                firebaseService.RemoveFavourite(AccountManager.GetAccountId(), currentMovie);
                IsFavourite = false;
            }
            else
            {
                firebaseService.AddFavourite(AccountManager.GetAccountId(), currentMovie);
                IsFavourite = true;
            }

            SetFavButton();
        }

        private void SetFavButton()
        {
            if (IsFavourite) butAddFavourite.Text = "Remove from favourites";
            else butAddFavourite.Text = "Add to favourites";
        }

        private readonly string YouTubeUrl = "https://www.youtube.com/watch?v=";
        void OnShowTrailerClicked(Object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(YouTubeUrl + this.currentMovie.YouTubeLink));
        }

        protected override bool OnBackButtonPressed()
        {
            this.moviesPage.MainPage.SetDetailPage((Page)this.moviesPage);

            return true;
        }
    }
}
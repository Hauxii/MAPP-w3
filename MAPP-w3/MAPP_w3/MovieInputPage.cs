using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using MAPP_w3.Model;
using Xamarin.Forms;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;

namespace MAPP_w3
{
    public class MovieInputPage : ContentPage
    {
        private Movies _movies;
        private MovieResourceProvider _movieResourceProvider;
        private ActivityIndicator _loading;
        
        private Label _entryLabel = new Label
        {
            HorizontalOptions = LayoutOptions.Start,
            Text = "Enter words in a movie title",
            FontSize = 19
        };

        private Entry _searchEntry = new Entry
        {
            HorizontalOptions = LayoutOptions.Fill,
            Placeholder = "Words in a movie title"
        };

        private Button _displayMovieButton = new Button
        {
            Text = "Search",
            BorderColor = Color.Gray,
            BorderRadius = 10,
            HorizontalOptions = LayoutOptions.Fill
        };

        private Label _displayMovieLabel = new Label
        {
            Text = string.Empty,
            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
        };

        public MovieInputPage(Movies movies)
        {
            this._movies = movies;
            this._movieResourceProvider = new MovieResourceProvider();
            this._loading = new ActivityIndicator();
            _loading.IsVisible = false;
            this.BackgroundColor = Color.White; 
            this.Title = "Search";
            Content = new StackLayout
            {
                Margin = 30,
                VerticalOptions = LayoutOptions.Start,
                Spacing = 10,
                Children = {
                    new StackLayout
                    {
                        Children =
                        {
                            this._entryLabel,
                            this._searchEntry
                        }
                    },
                    this._displayMovieButton,
                    this._displayMovieLabel,
                    this._loading
                }
            };
            this._displayMovieButton.Clicked += this.OnDisplayMovieButtonClicked;
            this._searchEntry.Completed += this.OnDisplayMovieButtonClicked;
        }

        private async void OnDisplayMovieButtonClicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(_searchEntry.Text))
            {
                await DisplayAlert("Invalid input", "Please enter a word from a movie title", "OK");
            }
            else
            {
                _displayMovieButton.IsEnabled = false;
                _loading.IsVisible = true;
                _loading.IsRunning = true;

                await this._movieResourceProvider.GetMoviesByTitle(this._movies, this._searchEntry.Text);

                if(this._movies.MovieList.Count == 0)
                {
                    await DisplayAlert("No movies", "There are no movies that contain these words in their title, try again", "OK");
                } 
                else
                {
                    await this.Navigation.PushAsync(new MovieListPage() { BindingContext = this._movies });
                }
                this._searchEntry.Text = string.Empty;
                _loading.IsRunning = false;
                _displayMovieButton.IsEnabled = true;
            }
        }
    }
}

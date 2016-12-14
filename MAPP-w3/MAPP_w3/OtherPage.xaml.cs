using MAPP_w3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MAPP_w3
{
    public partial class OtherPage : ContentPage
    {
        private Movies _movies;
        private MovieResourceProvider _resource;
        private ActivityIndicator _loading;
        public OtherPage(Movies movies)
        {
            InitializeComponent();
            this._resource = new MovieResourceProvider();
            this._movies = movies;
            //this._loading = new ActivityIndicator();
        }

        private void Listview_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            this.Navigation.PushAsync(new MovieDetailsPage() { BindingContext = e.SelectedItem });
            ((ListView)sender).SelectedItem = null;
        }

        public async Task GetTopRatedMovies()
        {
            LoadingSpinner.IsRunning = true;
            LoadingSpinner.IsVisible = true;

            await this._resource.GetTopRated(this._movies);

            LoadingSpinner.IsVisible = false;
            BindingContext = _movies;
        }
    }
}

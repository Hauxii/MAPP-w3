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
    public partial class PopularPage : ContentPage
    {
        private Movies _movies;
        private MovieResourceProvider _resource;
        public PopularPage(Movies movies)
        {
            InitializeComponent();
            this._resource = new MovieResourceProvider();
            this._movies = movies;
            PopularFlowListView.FlowItemTapped += (sender, e) =>
            {
                if (e.Item == null)
                {
                    return;
                }

                this.Navigation.PushAsync(new MovieDetailsPage() { BindingContext = e.Item });
                ((ListView)sender).SelectedItem = null;
            };
        }


        public async Task GetPopularMovies()
        {
            PopularLoadingSpinner.IsRunning = true;
            PopularLoadingSpinner.IsVisible = true;

            await this._resource.GetTopRated(this._movies);

            PopularLoadingSpinner.IsVisible = false;
            BindingContext = _movies;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MAPP_w3
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            var moviePage = new MovieInputPage(new Model.Movies());
            var movieNavigationPage = new NavigationPage(moviePage);
            movieNavigationPage.Title = "Movies";

            var otherPage = new OtherPage();
            var otherNavigationPage = new NavigationPage(otherPage);
            otherNavigationPage.Title = "Other";

            var tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(movieNavigationPage);
            tabbedPage.Children.Add(otherNavigationPage);

            MainPage = new NavigationPage(tabbedPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

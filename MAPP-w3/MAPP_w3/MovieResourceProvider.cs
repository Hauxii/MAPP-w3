using System;
using MAPP_w3.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using System.IO;

namespace MAPP_w3
{
	public class MovieResourceProvider
	{
		private IApiMovieRequest _movieApi;

		public MovieResourceProvider()
		{
			MovieDbFactory.RegisterSettings(new DBSettings());
			_movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
		}

		public async Task GetMoviesByTitle(Movies movies, string title)
		{
			var movieInfoResponse = await _movieApi.SearchByTitleAsync(title);
			await populateInfoHelper(movies, movieInfoResponse);

			return;
		}

		public async Task GetTopRated(Movies movies)
		{
			var movieInfoResponse = await _movieApi.GetTopRatedAsync();

			await populateInfoHelper(movies, movieInfoResponse);

			return;
		}

        public async Task GetPopular(Movies movies)
        {
            var movieInfoResponse = await _movieApi.GetPopularAsync();

            await populateInfoHelper(movies, movieInfoResponse);

            return;
        }

        //We sincerely apologies for the disgusting code here below, but this was the only solution in figuring out the random nullpointer-exceptions
        private async Task populateInfoHelper(Movies movies, ApiSearchResponse<MovieInfo> res)
		{
		    if (movies == null)
		    {
                //Console.WriteLine("MOVIEINFORESPONSE IS NULL!!!!!!!!!!!!");
            }
		    else
		    {
                movies.ClearList();
                if (res == null)
                {
                    //Console.WriteLine("RES IS NULL!!!!!!!!!!!!");
                }
                else
                {
                    if (res.Results == null)
                    {
                        //Console.WriteLine("RES.RESULT IS NULL!!!!!!!!!");
                    }
                    else
                    {
                        foreach (var m in res.Results)
                        {
                            if (m == null)
                            {
                                //Console.WriteLine("M IS NULL!!!!!!!!");
                            }
                            else
                            {
                                ApiQueryResponse<MovieCredit> movieCreditsResponse = await _movieApi.GetCreditsAsync(m.Id);
                                ApiQueryResponse<DM.MovieApi.MovieDb.Movies.Movie> movieDetailsResponse = await _movieApi.FindByIdAsync(m.Id);
                                if (movieCreditsResponse == null)
                                {
                                    //Console.WriteLine("MOVIECREDITSINFO IS NULL!!!!!!!");
                                }
                                else
                                {
                                    movies.ExtractInfo(m, movieCreditsResponse, movieDetailsResponse);
                                }
                            }

                        }
                    }
                    
                }
            }

			return;
		}

	}
}

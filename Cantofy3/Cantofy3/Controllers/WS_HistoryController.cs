using Cantofy3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Cantofy3.Controllers
{
    public class WS_HistoryController : ApiController
    {
        private DBContext db = new DBContext();
        //HttpContext httpContext = new HttpContext(new Http

        public RoleManager<IdentityRole> RoleManager { get; private set; }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [ResponseType(typeof(List<SearchViewModel>))]
        public IHttpActionResult GetLatestSearches()
        {
            var userId = User.Identity.GetUserId();
            var WordSearches = db.WordSearches.Where(ws => ws.UserId == userId && ws.SearchID != 0).GroupBy(g => g.SearchID).ToList();
            var result = new List<SearchViewModel>();

            foreach (var search in WordSearches)
            {
                var sortedSearch = search.OrderBy(o => o.ID);
                var SearchView = new SearchViewModel();

                SearchView.SearchID = search.Key;

                foreach (var word in sortedSearch)
                {
                    SearchView.Items += word.Word.Item;
                    SearchView.Romanization += word.Word.Romanization + " ";
                    SearchView.Translation += word.Word.Translation + " - ";

                    if (SearchView.Date == DateTime.MinValue)
                    {
                        SearchView.Date = word.Date;
                    }
                }

                result.Add(SearchView);
            }

            result = result.OrderByDescending(o => o.Date).ToList();

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<TranslationViewModel>))]
        public IHttpActionResult GetTopThreeSearchedWords()
        {
            var userId = User.Identity.GetUserId();

            List<IGrouping<int, WordSearch>> DbResult;


            DbResult = db.WordSearches.Where(ws => ws.UserId == userId).GroupBy(x => x.WordId).ToList();

            DbResult = DbResult.OrderByDescending(x => x.Count()).Take(3).ToList();

            List<TranslationViewModel> result = new List<TranslationViewModel>();

            foreach (var item in DbResult)
            {
                result.Add(
                    new TranslationViewModel
                    {
                        Item = item.First().Word.Item,
                        Romanization = item.First().Word.Romanization,
                        Translation = item.First().Word.Translation
                    });
            }

            return Ok(result);
        }
    }
}

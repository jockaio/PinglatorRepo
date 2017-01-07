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
            var WordSearches = db.WordSearches.Where(ws => ws.UserId == userId).GroupBy(g => g.)
            var Searches = db.Words.Where(w => w.ID)
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

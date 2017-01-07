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
    [Authorize]
    public class WS_TranslationController : ApiController
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

        [HttpPost]
        [ResponseType(typeof(List<TranslationViewModel>))]
        public IHttpActionResult GetTranslation(string userInput)
        {
            List<Word> result = new List<Word>();
            string currentString;
            int iterator;
            bool isLast = false;

            for (int i = 0; i < userInput.Length; i++)
            {
                currentString = string.Concat(userInput[i]);
                iterator = i;

                IQueryable<Word> dbResult = db.Words.Where(x => x.Item.Contains(currentString));

                while (dbResult.Any(x => x.Item.Contains(currentString)))
                {
                    if (++iterator >= userInput.Length)
                    {
                        isLast = true;
                        break;
                    }
                    currentString = string.Concat(currentString, userInput[iterator]);
                }

                currentString = currentString.Length > 1 && !isLast ? currentString.Substring(0, currentString.Length - 1) : currentString;

                while (!dbResult.Any(x => x.Item.Equals(currentString)) && currentString.Length > 1)
                {
                    currentString = currentString.Substring(0, currentString.Length - 1);
                }

                if (dbResult.Any(x => x.Item.Equals(currentString)))
                {
                    result.Add(dbResult.Where(x => x.Item.Equals(currentString)).First());
                }
                else
                {
                    result.Add(
                        new Word
                        {
                            Item = currentString.Length > 1 ? currentString.Substring(0, currentString.Length - 1) : currentString
                        }
                        );
                }
                i += currentString.Length - 1;
            }

            List<TranslationViewModel> translation = new List<TranslationViewModel>();
            var userId = User.Identity.GetUserId();
            foreach (var word in result)
            {
                if (word.ID != 0)
                {
                    //Save search stats.
                    db.WordSearches.Add(
                        new WordSearch
                        {
                            WordId = word.ID,
                            UserId = userId,
                            Date = DateTime.Now
                        }
                        );
                }

                //Add translation
                translation.Add(
                    new TranslationViewModel
                    {
                        Item = word.Item,
                        Romanization = word.Romanization,
                        Translation = word.Translation
                    });
            }

            db.SaveChanges();

            return Ok(translation);
        }
    }
}

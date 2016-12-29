using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Pinglator.Models;

namespace Pinglator.Controllers
{
    public class TranslationObjectModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TranslationObjectModels
        public IQueryable<TranslationObjectModel> GetTranslationObjectModels()
        {
            return db.TranslationObjectModels;
        }

        // GET: api/TranslationObjectModels/5
        [ResponseType(typeof(TranslationObjectModel))]
        public IHttpActionResult GetTranslationObjectModel(int id)
        {
            TranslationObjectModel translationObjectModel = db.TranslationObjectModels.Find(id);
            if (translationObjectModel == null)
            {
                return NotFound();
            }

            return Ok(translationObjectModel);
        }

        // GET: api/TranslationObjectModels/"stringoftext"
        //[ResponseType(typeof(List<TranslationObjectModel>))]
        //public IHttpActionResult GetTranslationObjectModel(string text)
        //{
        //    List<TranslationObjectModel> result = new List<TranslationObjectModel>();
        //    string currentString;
        //    int iterator;

        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        currentString = string.Concat(text[i]);
        //        iterator = i;
        //        bool translationFound = false;

        //        IQueryable<TranslationObjectModel> dbResult = db.TranslationObjectModels.Where(x => x.Item.Contains(currentString));

        //        while(!translationFound)
        //        {
        //            if (dbResult.Any(x => x.Item.Equals(currentString)))
        //            {
        //                dbResult = dbResult.Where(x => x.Item.Equals(currentString));
        //            }
        //            else
        //            {
        //                translationFound = true;
        //            }

        //            if (!translationFound)
        //            {
        //                iterator++;
        //                if (iterator < text.Length)
        //                {
        //                    currentString = string.Concat(currentString, text[iterator]);
        //                }
        //            }
        //            else
        //            {
        //                currentString = currentString.Substring(0, currentString.Length - 1);
        //                i = iterator-1;
        //            }

        //            if (iterator >= text.Length)
        //            {
        //                translationFound = true;
        //            }
                    
        //        }

        //        if (dbResult.Any(x => x.Item.Equals(currentString)))
        //        {
        //            result.Add(dbResult.Where(x => x.Item.Equals(currentString)).FirstOrDefault());
        //        }
        //        else
        //        {
        //            result.Add(
        //                new TranslationObjectModel
        //                {
        //                    Item = currentString
        //                }
        //                );
        //        }
        //    }

        //    //TranslationObjectModel translationObjectModel = db.TranslationObjectModels.Where(x => x.Item.Equals(text)).OrderBy(x => x.Item.Length).FirstOrDefault();
        //    //if (translationObjectModel == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    return Ok(result);
        //}

        // GET: api/TranslationObjectModels/"stringoftext"
        [ResponseType(typeof(List<TranslationObjectModel>))]
        public IHttpActionResult GetTranslationObjectModel(string text)
        {
            List<TranslationObjectModel> result = new List<TranslationObjectModel>();
            string currentString;
            int iterator;
            bool isLast = false;

            for (int i = 0; i < text.Length; i++)
            {
                currentString = string.Concat(text[i]);
                iterator = i;

                IQueryable<TranslationObjectModel> dbResult = db.TranslationObjectModels.Where(x => x.Item.Contains(currentString));

                while (dbResult.Any(x => x.Item.Contains(currentString)))
                {
                    if (++iterator >= text.Length)
                    {
                        isLast = true;
                        break;
                    }
                    currentString = string.Concat(currentString, text[iterator]);
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
                        new TranslationObjectModel
                        {
                            Item = currentString.Length > 1 ? currentString.Substring(0, currentString.Length-1) : currentString
                        }
                        );
                }
                i += currentString.Length - 1;
            }

            return Ok(result);
        }

        // PUT: api/TranslationObjectModels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTranslationObjectModel(int id, TranslationObjectModel translationObjectModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != translationObjectModel.ID)
            {
                return BadRequest();
            }

            db.Entry(translationObjectModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslationObjectModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TranslationObjectModels
        [ResponseType(typeof(TranslationObjectModel))]
        public IHttpActionResult PostTranslationObjectModel(TranslationObjectModel translationObjectModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TranslationObjectModels.Add(translationObjectModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = translationObjectModel.ID }, translationObjectModel);
        }

        // DELETE: api/TranslationObjectModels/5
        [ResponseType(typeof(TranslationObjectModel))]
        public IHttpActionResult DeleteTranslationObjectModel(int id)
        {
            TranslationObjectModel translationObjectModel = db.TranslationObjectModels.Find(id);
            if (translationObjectModel == null)
            {
                return NotFound();
            }

            db.TranslationObjectModels.Remove(translationObjectModel);
            db.SaveChanges();

            return Ok(translationObjectModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TranslationObjectModelExists(int id)
        {
            return db.TranslationObjectModels.Count(e => e.ID == id) > 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.Http;
using System.Web.Http.Description;
using BiljettService;

namespace BiljettService.Controllers
{
    public class VisningsSchemaController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private BiljettModel db = new BiljettModel();

        
        // GET: api/VisningsSchema
        public IQueryable<VisningsSchema> GetVisningsSchema()
        {
            return db.VisningsSchema;
        }

        // GET: api/VisningsSchema/5
        [ResponseType(typeof(VisningsSchema))]
        public IHttpActionResult GetVisningsSchema(int id)
        {
            VisningsSchema visningsSchema = db.VisningsSchema.Find(id);
            if (visningsSchema == null)
            {
                Logger.Error("Det gick inte att hämta visningar");
                return NotFound();
                
            }

            return Ok(visningsSchema);
        }


        public IHttpActionResult PutVisningsSchema(VisningsSchema visning) /*Metoden kommer från denna källahttps://www.tutorialsteacher.com/webapi/implement-put-method-in-web-api*/
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (BiljettModel db = new BiljettModel())
            {
                var visningen = db.VisningsSchema.FirstOrDefault(e => e.Id == visning.Id);

                if (visningen != null)
                {
                    visningen.FilmTitel = visning.FilmTitel;
                    visningen.SalongsNamn = visning.SalongsNamn;
                    visningen.Visningstid = visning.Visningstid;
                    db.SaveChanges();
                }

                else
                {
                    Logger.Error("Det gick inte att göra förändringar på visningen");
                    return NotFound();
                }
                return Ok();

            }
        }       

        // POST: api/VisningsSchema
        [ResponseType(typeof(VisningsSchema))]
        public IHttpActionResult PostVisningsSchema(VisningsSchema visningsSchema)
        {
            if (!ModelState.IsValid)
            {
                Logger.Error("Du har skickat fel format");
                return BadRequest(ModelState);
            }

            db.VisningsSchema.Add(visningsSchema);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = visningsSchema.Id }, visningsSchema);
        }

        // DELETE: api/VisningsSchema/5
        [ResponseType(typeof(VisningsSchema))]
        public IHttpActionResult DeleteVisningsSchema(int id)
        {
            VisningsSchema visningsSchema = db.VisningsSchema.Find(id);
            if (visningsSchema == null)
            {
                Logger.Error("Kunde inte hitta visnings ID");
                return NotFound();
            }

            db.VisningsSchema.Remove(visningsSchema);
            db.SaveChanges();

            return Ok(visningsSchema);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VisningsSchemaExists(int id)
        {
            return db.VisningsSchema.Count(e => e.Id == id) > 0;
        }
    }
}
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
using BiljettService;

namespace BiljettService.Controllers
{
    public class BokningarController : ApiController
    {
        private BiljettModel db = new BiljettModel();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        // GET: api/Bokningar
        public IQueryable<Bokningar> GetBokningar()
        {
            return db.Bokningar;
        }

        // GET: api/Bokningar/5
        [ResponseType(typeof(Bokningar))]
        public IHttpActionResult GetBokningar(int id)
        {
            Bokningar bokningar = db.Bokningar.Find(id);
            if (bokningar == null)
            {
                Logger.Error("En bokning med det angivna id:et finns inte!");
                return BadRequest("En bokning med det angivna id:et finns inte!");
            }

            return Ok(bokningar);
        }

        // PUT: api/Bokningar/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBokningar(int id, Bokningar bokningar)
        {
            if (!ModelState.IsValid)
            {
                Logger.Error("Fel format på angivna data");
                return BadRequest(ModelState);
            }

            if (id != bokningar.Id)
            {
                Logger.Error("Kunde inte hitta det angivna Id:et");
                return BadRequest();
            }

            db.Entry(bokningar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BokningarExists(id))
                {
                    Logger.Error("En bokning med det angivna id:et finns inte!");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Bokningar
        [ResponseType(typeof(Bokningar))]
        public IHttpActionResult PostBokningar(Bokningar bokningar)
        {
            if (!ModelState.IsValid)
            {
                Logger.Error("Fel format på angivna data");
                return BadRequest(ModelState);
            }

            db.Bokningar.Add(bokningar);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bokningar.Id }, bokningar);
        }

        // DELETE: api/Bokningar/5
        [ResponseType(typeof(Bokningar))]
        public IHttpActionResult DeleteBokningar(int id)
        {
            Bokningar bokningar = db.Bokningar.Find(id);
            if (bokningar == null)
            {
                Logger.Error("Bokningen finns inte");
                return NotFound();
            }

            db.Bokningar.Remove(bokningar);
            db.SaveChanges();

            return Ok(bokningar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BokningarExists(int id)
        {
            return db.Bokningar.Count(e => e.Id == id) > 0;
        }
    }
}
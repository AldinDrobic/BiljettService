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
    public class BokadePlatserController : ApiController
    {
        private BiljettModel db = new BiljettModel();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        // GET: api/BokadePlatser
        public IQueryable<BokadePlatser> GetBokadePlatser()
        {
            return db.BokadePlatser;
        }

        // GET: api/BokadePlatser/5
        [ResponseType(typeof(BokadePlatser))]
        public IHttpActionResult GetBokadePlatser(int id)
        {
            BokadePlatser bokadePlatser = db.BokadePlatser.Find(id);
            if (bokadePlatser == null)
            {
                Logger.Error("Bokadeplats med det angivna id:et finns inte!");
                return NotFound();
            }

            return Ok(bokadePlatser);
        }

        // PUT: api/BokadePlatser/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBokadePlatser(int id, BokadePlatser bokadePlatser)
        {
            if (!ModelState.IsValid)
            {
                Logger.Error("Fel format på angivna data");
                return BadRequest(ModelState);
            }

            if (id != bokadePlatser.Id)
            {
                Logger.Error("Kunde inte hitta det angivna Id:et");
                return BadRequest();
            }

            db.Entry(bokadePlatser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BokadePlatserExists(id))
                {
                    Logger.Error("En bokning med det angivna id:t finns inte!");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BokadePlatser
        [ResponseType(typeof(BokadePlatser))]
        public IHttpActionResult PostBokadePlatser(BokadePlatser bokadePlatser)
        {
            if (!ModelState.IsValid)
            {
                Logger.Error("Fel format på angivna data");
                return BadRequest(ModelState);
            }

            db.BokadePlatser.Add(bokadePlatser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bokadePlatser.Id }, bokadePlatser);
        }

        // DELETE: api/BokadePlatser/5
        [ResponseType(typeof(BokadePlatser))]
        public IHttpActionResult DeleteBokadePlatser(int id)
        {
            BokadePlatser bokadePlatser = db.BokadePlatser.Find(id);
            if (bokadePlatser == null)
            {
                Logger.Error("Bokadeplatser finns inte");
                return NotFound();
            }

            db.BokadePlatser.Remove(bokadePlatser);
            db.SaveChanges();

            return Ok(bokadePlatser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BokadePlatserExists(int id)
        {
            return db.BokadePlatser.Count(e => e.Id == id) > 0;
        }
    }
}
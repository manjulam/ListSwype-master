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
using ListSwype.Models;

namespace ListSwype.Controllers
{
    public class CommonListController : ApiController
    {
        private listswypeEntities db = new listswypeEntities();

        // GET: api/CommonList
        public IQueryable<commonlist> Getcommonlists()
        {
            return db.commonlists;
        }

        // GET: api/CommonList/5
        [ResponseType(typeof(commonlist))]
        public IHttpActionResult Getcommonlist(int id)
        {
            commonlist commonlist = db.commonlists.Find(id);
            if (commonlist == null)
            {
                return NotFound();
            }

            return Ok(commonlist);
        }

        // PUT: api/CommonList/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcommonlist(int id, commonlist commonlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commonlist.ID)
            {
                return BadRequest();
            }

            db.Entry(commonlist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!commonlistExists(id))
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

        // POST: api/CommonList
        [ResponseType(typeof(commonlist))]
        public IHttpActionResult Postcommonlist(commonlist commonlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.commonlists.Add(commonlist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = commonlist.ID }, commonlist);
        }

        // DELETE: api/CommonList/5
        [ResponseType(typeof(commonlist))]
        public IHttpActionResult Deletecommonlist(int id)
        {
            commonlist commonlist = db.commonlists.Find(id);
            if (commonlist == null)
            {
                return NotFound();
            }

            db.commonlists.Remove(commonlist);
            db.SaveChanges();

            return Ok(commonlist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool commonlistExists(int id)
        {
            return db.commonlists.Count(e => e.ID == id) > 0;
        }
    }
}
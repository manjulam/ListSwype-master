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
    public class CustomListController : ApiController
    {
        private listswypeEntities db = new listswypeEntities();

        // GET: api/CustomList
        public IQueryable<customlist> Getcustomlists()
        {
            return db.customlists;
        }

        // GET: api/CustomList/5
        [ResponseType(typeof(customlist))]
        public IHttpActionResult Getcustomlist(int id)
        {
            customlist customlist = db.customlists.Find(id);
            if (customlist == null)
            {
                return NotFound();
            }

            return Ok(customlist);
        }

        // PUT: api/CustomList/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcustomlist(int id, customlist customlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customlist.id)
            {
                return BadRequest();
            }

            db.Entry(customlist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customlistExists(id))
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

        // POST: api/CustomList
        [ResponseType(typeof(customlist))]
        public IHttpActionResult Postcustomlist(customlist customlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.customlists.Add(customlist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customlist.id }, customlist);
        }

        // DELETE: api/CustomList/5
        [ResponseType(typeof(customlist))]
        public IHttpActionResult Deletecustomlist(int id)
        {
            customlist customlist = db.customlists.Find(id);
            if (customlist == null)
            {
                return NotFound();
            }

            db.customlists.Remove(customlist);
            db.SaveChanges();

            return Ok(customlist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool customlistExists(int id)
        {
            return db.customlists.Count(e => e.id == id) > 0;
        }
    }
}
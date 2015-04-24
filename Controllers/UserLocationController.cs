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
    public class UserLocationController : ApiController
    {
        private listswypeEntities db = new listswypeEntities();

        // GET: api/UserLocation
        public IQueryable<userlocation> Getuserlocations()
        {
            return db.userlocations;
        }

        // GET: api/UserLocation/5
        [ResponseType(typeof(userlocation))]
        public IHttpActionResult Getuserlocation(int id)
        {
            userlocation userlocation = db.userlocations.Find(id);
            if (userlocation == null)
            {
                return NotFound();
            }

            return Ok(userlocation);
        }

        // PUT: api/UserLocation/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuserlocation(int id, userlocation userlocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userlocation.id)
            {
                return BadRequest();
            }

            db.Entry(userlocation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userlocationExists(id))
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

        // POST: api/UserLocation
        [ResponseType(typeof(userlocation))]
        public IHttpActionResult Postuserlocation(userlocation userlocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.userlocations.Add(userlocation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userlocation.id }, userlocation);
        }

        // DELETE: api/UserLocation/5
        [ResponseType(typeof(userlocation))]
        public IHttpActionResult Deleteuserlocation(int id)
        {
            userlocation userlocation = db.userlocations.Find(id);
            if (userlocation == null)
            {
                return NotFound();
            }

            db.userlocations.Remove(userlocation);
            db.SaveChanges();

            return Ok(userlocation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userlocationExists(int id)
        {
            return db.userlocations.Count(e => e.id == id) > 0;
        }
    }
}
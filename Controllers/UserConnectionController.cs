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
    public class UserConnectionController : ApiController
    {
        private listswypeEntities db = new listswypeEntities();

        // GET: api/UserConnection
        public IQueryable<userconnection> Getuserconnections()
        {
            return db.userconnections;
        }

        // GET: api/UserConnection/5
        [ResponseType(typeof(userconnection))]
        public IHttpActionResult Getuserconnection(int id)
        {
            userconnection userconnection = db.userconnections.Find(id);
            if (userconnection == null)
            {
                return NotFound();
            }

            return Ok(userconnection);
        }

        // PUT: api/UserConnection/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuserconnection(int id, userconnection userconnection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userconnection.id)
            {
                return BadRequest();
            }

            db.Entry(userconnection).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userconnectionExists(id))
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

        // POST: api/UserConnection
        [ResponseType(typeof(userconnection))]
        public IHttpActionResult Postuserconnection(userconnection userconnection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.userconnections.Add(userconnection);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userconnection.id }, userconnection);
        }

        // DELETE: api/UserConnection/5
        [ResponseType(typeof(userconnection))]
        public IHttpActionResult Deleteuserconnection(int id)
        {
            userconnection userconnection = db.userconnections.Find(id);
            if (userconnection == null)
            {
                return NotFound();
            }

            db.userconnections.Remove(userconnection);
            db.SaveChanges();

            return Ok(userconnection);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userconnectionExists(int id)
        {
            return db.userconnections.Count(e => e.id == id) > 0;
        }
    }
}
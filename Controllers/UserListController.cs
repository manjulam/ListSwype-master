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
    public class UserListController : ApiController
    {
        private listswypeEntities db = new listswypeEntities();

        // GET: api/UserList
        public IQueryable<userlist> Getuserlists()
        {
            return db.userlists;
        }

        // GET: api/UserList/5
        [ResponseType(typeof(userlist))]
        public IHttpActionResult Getuserlist(int id)
        {
            userlist userlist = db.userlists.Find(id);
            if (userlist == null)
            {
                return NotFound();
            }

            return Ok(userlist);
        }

        // PUT: api/UserList/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuserlist(int id, userlist userlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userlist.id)
            {
                return BadRequest();
            }

            db.Entry(userlist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userlistExists(id))
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

        // POST: api/UserList
        [ResponseType(typeof(userlist))]
        public IHttpActionResult Postuserlist(userlist userlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.userlists.Add(userlist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userlist.id }, userlist);
        }

        // DELETE: api/UserList/5
        [ResponseType(typeof(userlist))]
        public IHttpActionResult Deleteuserlist(int id)
        {
            userlist userlist = db.userlists.Find(id);
            if (userlist == null)
            {
                return NotFound();
            }

            db.userlists.Remove(userlist);
            db.SaveChanges();

            return Ok(userlist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userlistExists(int id)
        {
            return db.userlists.Count(e => e.id == id) > 0;
        }
    }
}
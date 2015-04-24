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
using ListSwype.DTO;
using ListSwype.Repository;

namespace ListSwype.Controllers
{
    public class UserController : ApiController
    {
       private ListSwypeRepository _listRepository;
       private listswypeEntities db = new listswypeEntities();

       public UserController()
        {

            this._listRepository = new ListSwypeRepository();
        }


        // GET: api/User
        public IQueryable<user> Getusers()
        {
            return db.users;
        }

        // GET: api/User/5
        //[ResponseType(typeof(user))]
        //public IHttpActionResult Getuser(int id)
        //{
        //    user user = db.users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}

        // GET: api/User/5
        public UserDTO GetUser(int id)
        {
           return _listRepository.GetUserbyId(id);
            
        }
        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuser(int id, user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        //// POST: api/User
        //[ResponseType(typeof(user))]
        //public IHttpActionResult Postuser(user user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.users.Add(user);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
        //}

        
        public void Post([FromBody]UserDTO user)
        {
            _listRepository.SaveUser(user);
        }
        // DELETE: api/User/5
        [ResponseType(typeof(user))]
        public IHttpActionResult Deleteuser(int id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(int id)
        {
            return db.users.Count(e => e.ID == id) > 0;
        }
    }
}
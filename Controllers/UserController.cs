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

        // GET: api/User/5
        public UserDTO GetUser(int id)
        {
            return _listRepository.GetUserbyId(id);
        }

        // GET: api/User/getuserbyemail/name@example.com/
        [HttpGet]
        [ActionName("GetUserByEmail")]
        public UserDTO GetUserByEmail(string  email)
        {
            return _listRepository.GetUserbyEmail(email);
        }

        // POST: api/User
        public string Post([FromBody]UserDTO user)
        {
            return _listRepository.SaveUser(user);
        }

        // DELETE: api/User/5
        public bool DeleteUser(int id)
        {
            return _listRepository.DeleteUserByID(id);
        }

        // DELETE: api/User/deleteuserbyemail/name@example.com/
        [HttpDelete]
        [ActionName("DeleteUserByEmail")]
        public bool DeleteUserByEmail(string email)
        {
            return _listRepository.DeleteUserByEmail(email);
        }
    }
}
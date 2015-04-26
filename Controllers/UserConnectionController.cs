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
    public class UserConnectionController : ApiController
    {
       private ListSwypeRepository _listRepository;
       private listswypeEntities db = new listswypeEntities();

       public UserConnectionController()
        {
            this._listRepository = new ListSwypeRepository();
        }

        // POST: api/UserConnection
        public string Post([FromBody]UserDTO connection)
        {
            return _listRepository.SaveConnection(connection);
        }

    }
}
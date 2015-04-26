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
    public class CustomListController : ApiController
    {
       private ListSwypeRepository _listRepository;
       private listswypeEntities db = new listswypeEntities();

       public CustomListController()
        {
            this._listRepository = new ListSwypeRepository();
        }

        // GET: api/User/name@example.com/
        public List<CustomItemDTO> GetCustomItems(string  email)
        {
            return _listRepository.GetCustomItemsByEmail(email);
        }

        // POST: api/User
        public bool Post([FromBody]CustomItemDTO item)
        {
            return _listRepository.SaveCustomItem(item);
        }

        
    }
}
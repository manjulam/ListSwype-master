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
    public class UserListController : ApiController
    {
        private ListSwypeRepository _listRepository;
        private listswypeEntities db = new listswypeEntities();

        public UserListController()
        {
            this._listRepository = new ListSwypeRepository();
        }

        // GET: api/userlist/name@example.com/
        public List<UserListItemDTO> GetUserList(string email)
        {
            return _listRepository.GetUserListItemsByEmail(email);
        }

        // POST: api/userlist
        public string Post([FromBody]UserListItemDTO item)
        {
            return _listRepository.SaveUserListItem(item);
        }

        // DELETE: api/User/uniqueid/
        [HttpDelete]
        [ActionName("DeleteUserListItem")]
        public string DeleteUserListItem(string uniqueid,string email)
        {
            return _listRepository.DeleteUserListItemByUniqueID(uniqueid,email);
        }

    }
}
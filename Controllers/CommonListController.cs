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
    public class CommonListController : ApiController
    {
       private ListSwypeRepository _listRepository;
       private listswypeEntities db = new listswypeEntities();

       public CommonListController()
        {
            this._listRepository = new ListSwypeRepository();
        }

        // GET: api/CommonList
       public List<ListItemDTO> GetCommonList()
       {
           return _listRepository.GetAllCommonItems();
       }

    }
}
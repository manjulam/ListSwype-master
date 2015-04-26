using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ListSwype.Base;

namespace ListSwype.DTO
{
    public class UserListItemDTO : BaseDTO
    {

        /// <summary>
        /// Gets or sets the Id field
        /// </summary>
        [DataMember]
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ItemId field
        /// </summary>
        [DataMember]
        public string itemid
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the type field
        /// </summary>
        [DataMember]
        public string type
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the useremail field
        /// </summary>
        [DataMember]
        public string useremail
        {
            get;
            set;
        }
       
    }
}
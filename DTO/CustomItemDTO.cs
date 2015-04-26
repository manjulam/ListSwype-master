using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ListSwype.Base;

namespace ListSwype.DTO
{
    public class CustomItemDTO : BaseDTO
    {
        /// Gets or sets the Id field
        /// </summary>
        [DataMember]
        public int id
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the UserEmail field
        /// </summary>
        [DataMember]
        public string useremail
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ItemName field
        /// </summary>
        [DataMember]
        public string itemname
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ItemImage field
        /// </summary>
        [DataMember]
        public string itemimage
        {
            get;
            set;
        }
        [DataMember]
        public string uniqueid
        {
            get;
            set;
        }
        ///
    }
}

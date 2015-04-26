using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ListSwype.Base;

namespace ListSwype.DTO
{
    public class ListItemDTO : BaseDTO
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
        /// Gets or sets the ItemName field
        /// </summary>
        [DataMember]
        public string ItemName
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ItemImage field
        /// </summary>
        [DataMember]
        public string ItemImage
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the UniqueID field
        /// </summary>
        [DataMember]
        public string UniqueID
        {
            get;
            set;
        }
    }
}
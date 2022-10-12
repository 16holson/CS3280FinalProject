using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280FinalProject.Shared
{
    public class Item
    {
        #region Class Variables
        /// <summary>
        /// Items code
        /// </summary>
        public string itemCode { get; set; }
        /// <summary>
        /// Items description
        /// </summary>
        public string itemDesc { get; set; }
        /// <summary>
        /// Items cost
        /// </summary>
        public string itemCost { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Item()
        {

        }
        #endregion
    }
}

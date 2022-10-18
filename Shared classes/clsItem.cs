using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280FinalProject.Shared
{
    public class clsItem
    {
        #region Class Variables
        /// <summary>
        /// Items code.
        /// </summary>
        public string itemCode { get; set; }
        /// <summary>
        /// Items description.
        /// </summary>
        public string itemDesc { get; set; }
        /// <summary>
        /// Items cost.
        /// </summary>
        public string itemCost { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public clsItem()
        {
            //do nothing
        }

        /// <summary>
        /// Overloaded constructor to make a new item.
        /// </summary>
        /// <param name="itemCode">The item's code.</param>
        /// <param name="itemDesc">The item's description.</param>
        /// <param name="itemCost">The item's cost.</param>
        /// <exception cref="Exception"></exception>
        public clsItem(string itemCode, string itemDesc, string itemCost)
        {
            try
            {
                this.itemCode = itemCode;
                this.itemDesc = itemDesc;
                this.itemCost = itemCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}

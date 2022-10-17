using CS3280FinalProject.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280FinalProject.Items
{
    public class clsItemsLogic
    {
        #region variables
        clsItemsSQL DB;
        #endregion

        /// <summary>
        /// Default constructor for the clsItemsLogic class.
        /// </summary>
        public clsItemsLogic()
        {
            DB = new clsItemsSQL();
        }

        /* 
         * SQL statements to implement
         * (DONE)select ItemCode, ItemDesc, Cost from ItemDesc
         * select distinct(InvoiceNum) from LineItems where ItemCode = 'A'
         * Update ItemDesc Set ItemDesc = 'abcdef', Cost = 123 where ItemCode = 'A'
         * Insert into ItemDesc (ItemCode, ItemDesc, Cost) Values ('ABC', 'blah', 321)
         * Delete from ItemDesc Where ItemCode = 'ABC'
         */
        public List<Item> GetAllItems()
        {
            int rowsReturned = 0;

            DataSet ds = DB.ExecuteSQLStatement("select ItemCode, ItemDesc, Cost from ItemDesc", ref rowsReturned);
            List<Item> items = new List<Item>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Item currentItem = new Item(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
                items.Add(currentItem);
            }

            return items;
        }
    }
}

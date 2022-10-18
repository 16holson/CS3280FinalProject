using CS3280FinalProject.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Windows.Documents;

namespace CS3280FinalProject.Items
{
    public class clsItemsSQL
    {
        #region variables
        /// <summary>
        /// Holds the connection the DB so that we can query it.
        /// </summary>
        clsDataAccess DB;

        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string SQLcommand;
        #endregion


        #region functions
        /// <summary>
        /// Default constructor for the clsItemsSQL class.
        /// </summary>
        public clsItemsSQL()
        {
            DB = new clsDataAccess();
        }
        /* 
         * SQL statements to implement
         * (DONE) select ItemCode, ItemDesc, Cost from ItemDesc
         * (DONE) select distinct(InvoiceNum) from LineItems where ItemCode = 'A'
         * (DONE) Update ItemDesc Set ItemDesc = 'abcdef', Cost = 123 where ItemCode = 'A'
         * (DONE) Insert into ItemDesc (ItemCode, ItemDesc, Cost) Values ('ABC', 'blah', 321)
         * (DONE) Delete from ItemDesc Where ItemCode = 'ABC'
         */

        /// <summary>
        /// Gets all the items that are inside the DB.
        /// </summary>
        /// <returns>A list of object, of the type clsItem, for all of the Items inside the DB.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public List<clsItem> GetAllItemsFromDB()
        {
            try
            {
                int rowsReturned = 0;

                DataSet ds = DB.ExecuteSQLStatement("select ItemCode, ItemDesc, Cost from ItemDesc", ref rowsReturned);
                List<clsItem> items = new List<clsItem>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    clsItem currentItem = new clsItem(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
                    items.Add(currentItem);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This gets a list all the invoice number that have the itemcode specified.
        /// </summary>
        /// <param name="ItemCode">The item code you are looking for.</param>
        /// <returns>A list of strings for all the invoice number that have "ItemCode" inside the invoice.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public List<int> GetAllInvoiceNumsForItemCode(string ItemCode)
        {
            try
            {
                int rowsReturned = 0;

                DataSet ds = DB.ExecuteSQLStatement("select distinct(InvoiceNum) from LineItems where ItemCode = '" + ItemCode + "'", ref rowsReturned);
                List<int> items = new List<int>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    items.Add((int)ds.Tables[0].Rows[i][0]);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an item's data with the supplied data.
        /// </summary>
        /// <param name="ItemCode">The item code of the item you are going to update.</param>
        /// <param name="ItemDescription">The item's updated description.</param>
        /// <param name="ItemCost">The item's updated cost.</param>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public void UpdateItemData(string ItemCode, string ItemDescription, int ItemCost)
        {
            try
            {
                int rowsAffected = DB.ExecuteNonQuery("Update ItemDesc Set ItemDesc = " + "'" + ItemDescription + "', Cost = " + ItemCost + " where ItemCode = '" + ItemCode + "'");
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds an item to the DB using the supplied data.
        /// </summary>
        /// <param name="ItemCode">The item code of the item you are going to update.</param>
        /// <param name="ItemDescription">The item's updated description.</param>
        /// <param name="ItemCost">The item's updated cost.</param>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public void AddItem(string ItemCode, string ItemDescription, int ItemCost)
        {
            try
            {
                int rowsAffected = DB.ExecuteNonQuery("Insert into ItemDesc (ItemCode, ItemDesc, Cost) Values ('" + ItemCode + "', '" + ItemDescription + "', +m " + ItemCost + ")");
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public void DeleteItem(string ItemCode)
        {
            try
            {
                int rowsAffected = DB.ExecuteNonQuery("Delete from ItemDesc Where ItemCode = '" + ItemCode + "'");
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}

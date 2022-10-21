/*
 * Braxton Wright
 * CS 3280
 * Final Project prototype class clsItemsSQL
 * Shawn Cowder
 * Due: November 19, 2022 at 11:59 PM
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the functions for the items window that returns a string representing the SQL statement
 * to query the DB.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Reflection;

namespace CS3280FinalProject.Items
{
    public class clsItemsSQL
    {
        #region functions
        /// <summary>
        /// Default constructor for the clsItemsSQL class.
        /// </summary>
        public clsItemsSQL()
        {
            //do nothing
        }

        /* Statements to implement:
         * (Done) select ItemCode, ItemDesc, Cost from ItemDesc
         * (Done) select distinct(InvoiceNum) from LineItems where ItemCode = 'A'
         * (Done) Update ItemDesc Set ItemDesc = 'abcdef', Cost = 123 where ItemCode = 'A'
         * (Done) Insert into ItemDesc (ItemCode, ItemDesc, Cost) Values ('ABC', 'blah', 321)
         * (Done) Delete from ItemDesc Where ItemCode = 'ABC'
        */

        /// <summary>
        /// Generates a SQL statement that allows you to query the DB to retrieve all the items inside it.
        /// </summary>
        /// <returns>A string that represents the SQL statement.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public string SelectAllItems()
        {
            try
            {
                return "select ItemCode, ItemDesc, Cost " +
                    "from ItemDesc";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates a SQL statement that allows you to query the DB to retrieve all the invoice numbers that have the item inside it.
        /// </summary>
        /// <param name="ItemCode">The ItemCode of the item you want to see if it is connected to any invoice.</param>
        /// <returns>A string that represents the SQL statement.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public string SelectAllInvoicesThatHaveItemCode(string ItemCode)
        {
            try
            {
                return "select distinct(InvoiceNum) " +
                    "from LineItems " +
                    "where ItemCode = '" + ItemCode + "'";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates a SQL statement that allows you to update an item's information inside the DB.
        /// </summary>
        /// <param name="ItemCode">The ItemCode of the item you want to update.</param>
        /// <param name="ItemDescription">The new description of the item.</param>
        /// <param name="ItemCost">The new cost of the item.</param>
        /// <returns>A string that represents the SQL statement.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public string UpdateItemData(string ItemCode, string ItemDescription, float ItemCost)
        {
            try
            {
                return "Update ItemDesc " +
                    "Set ItemDesc = " + "'" + ItemDescription + "', " +
                    "Cost = " + ItemCost + " " +
                    "where ItemCode = '" + ItemCode.ToString() + "'";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates a SQL statement that allows you to insert a new item into the DB.
        /// </summary>
        /// <param name="ItemCode">The ItemCode of the new item.</param>
        /// <param name="ItemDescription">The item's description.</param>
        /// <param name="ItemCost">The item's cost.</param>
        /// <returns>A string that represents the SQL statement.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public string InsertItemData(string ItemCode, string ItemDescription, float ItemCost)
        {
            try
            {
                return "Insert into ItemDesc (ItemCode, ItemDesc, Cost) " +
                    "Values ('" + ItemCode + "', '" + ItemDescription + "', " + ItemCost + ")";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates a SQL statement that allows you to delete an item from the DB.
        /// </summary>
        /// <param name="ItemCode">The ItemCode of the item you want to delete.</param>
        /// <returns>A string that represents the SQL statement.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public string DeleteItemData(string ItemCode)
        {
            try
            {
                return "Delete from ItemDesc " +
                    "Where ItemCode = '" + ItemCode + "'";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public string CheckItemCodeExist(string ItemCode)
        {
            try
            {
                return "Select ItemCode " +
                    "From ItemDesc " +
                    "Where ItemCode = '" + ItemCode + "'";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}

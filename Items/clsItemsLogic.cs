/*
 * Braxton Wright
 * CS 3280
 * Final Project prototype class clsItemsLogic
 * Shawn Cowder
 * Due: November 19, 2022 at 11:59 PM
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the logistic for the items window so that the logistics is not behind the UI.
 * -----------------------------------------------------------------------------------------------------------
 */

using CS3280FinalProject.Shared;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System;
using System.Collections.ObjectModel;

namespace CS3280FinalProject.Items
{
    public class clsItemsLogic
    {
        #region variables
        /// <summary>
        /// Stores the connection the DB so that we can query it.
        /// </summary>
        clsDataAccess DB;

        /// <summary>
        /// Stores a list of SQL commands that this file will make use of.
        /// </summary>
        clsItemsSQL SQL;
        #endregion


        #region functions
        /// <summary>
        /// Default constructor for the clsItemsLogic class.
        /// </summary>
        public clsItemsLogic()
        {
            DB = new clsDataAccess();
            SQL = new clsItemsSQL();
        }

        /// <summary>
        /// Gets all the items that are inside the DB.
        /// </summary>
        /// <returns>A list of object, of the type clsItem, for all of the Items inside the DB.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public ObservableCollection<clsItem> GetAllItemsFromDB()
        {
            try
            {
                int rowsReturned = 0;

                DataSet ds = DB.ExecuteSQLStatement(SQL.SelectAllItems(), ref rowsReturned);
                ObservableCollection<clsItem> items = new ObservableCollection<clsItem>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    float cost = 0;
                    if (float.TryParse(ds.Tables[0].Rows[i][2].ToString(), out cost))
                    {
                        clsItem currentItem = new clsItem(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), cost);
                        items.Add(currentItem);
                    }
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

                DataSet ds = DB.ExecuteSQLStatement(SQL.SelectAllInvoicesThatHaveItemCode(ItemCode), ref rowsReturned);
                List<int> InvoiceNums = new List<int>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InvoiceNums.Add((int)ds.Tables[0].Rows[i][0]);
                }

                return InvoiceNums;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an item's data to the DB using the supplied data.
        /// </summary>
        /// <param name="ItemCode">The item code of the item you are going to update.</param>
        /// <param name="ItemDescription">The item's updated description.</param>
        /// <param name="ItemCost">The item's updated cost.</param>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public void UpdateItemData(string ItemCode, string ItemDescription, int ItemCost)
        {
            try
            {
                //int rowsAffected = DB.ExecuteNonQuery(SQL.UpdateItemData(ItemCode, ItemDescription, ItemCost));
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
        public void AddItem(string ItemCode, string ItemDescription, float ItemCost)
        {
            try
            {
                int rowsAffected = DB.ExecuteNonQuery(SQL.InsertItemData(ItemCode, ItemDescription, ItemCost));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes an item from the DB using the supplied ItemCode.
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteItem(string ItemCode)
        {
            try
            {
                int rowsAffected = DB.ExecuteNonQuery(SQL.DeleteItemData(ItemCode));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
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
using System.Text.RegularExpressions;

namespace CS3280FinalProject.Items
{
    public class clsItemsLogic
    {
        #region Variables
        /// <summary>
        /// Stores the connection the DB so that we can query it.
        /// </summary>
        clsDataAccess DB;

        /// <summary>
        /// Stores a list of SQL commands that this file will make use of.
        /// </summary>
        clsItemsSQL SQL;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for the clsItemsLogic class.
        /// </summary>
        public clsItemsLogic()
        {
            DB = new clsDataAccess();
            SQL = new clsItemsSQL();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Gets all the items that are inside the DB.
        /// </summary>
        /// <returns>A list of objects, of the type clsItem, for all of the Items inside the DB.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public ObservableCollection<Item> GetAllItemsFromDB()
        {
            try
            {
                int rowsReturned = 0;

                DataSet ds = DB.ExecuteSQLStatement(SQL.SelectAllItems(), ref rowsReturned);
                ObservableCollection<Item> items = new ObservableCollection<Item>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Item currentItem = new Item(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
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
        public void UpdateItemData(string ItemCode, string ItemDescription, float ItemCost)
        {
            try
            {
                int rowsAffected = DB.ExecuteNonQuery(SQL.UpdateItemData(ItemCode, ItemDescription, ItemCost));
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

        /// <summary>
        /// Performs a Regex pattern match to see if the supplied text matches "1_or_more_digits" followed by an optional section ".1_or_2_digits"
        /// </summary>
        /// <param name="CostToValidate">The text of the cost to be validated.</param>
        /// <returns>True if the data supplied matches the desired format, and false otherwise.</returns>
        public bool ValidateCostFormat(string CostToValidate)
        {
            //bool test = Regex.Match(CostToValidate, @"^\d+(?:\.\d{1,2})?$").Success;
            //where I found the Regex command with a few modifications https://stackoverflow.com/questions/52810865/regex-to-accept-number-in-fomat-00-00

            return Regex.Match(CostToValidate, @"^\d+(?:\.\d{1,2})?$").Success;
        }

        /// <summary>
        /// This converts a list that contains only 1 data type and set (No objects allowed) to a string.
        /// </summary>
        /// <typeparam name="T">The data type the list contains.</typeparam>
        /// <param name="ListData">The list that contains the data.</param>
        /// <returns>A string separated by a ', ' for every item in the list.</returns>
        public string ConvertSingleItemListToString<T>(List<T> ListData)
        {
            string Result = "";

            for (int i = 0; i < ListData.Count; i++)
            {
                Result += ListData[i].ToString();

                //if it is the last element to be added, don't add a ',' to the end
                if (i == ListData.Count - 2)
                {
                    Result += ", ";
                }
            }

            return Result;
        }

        /// <summary>
        /// Check to see if the ItemCode is already used inside the DB. To prevent the program from crashing when attempting to add an item with the same identifier.
        /// </summary>
        /// <param name="ItemCode">The item code you are seeing if it exists inside the DB.</param>
        /// <returns>True if it is used and False if it is not.</returns>
        public bool ItemCodeIsTaken(string ItemCode)
        {
            string QuerryResult = DB.ExecuteScalarSQL(SQL.CheckItemCodeExist(ItemCode));

            if (QuerryResult == "")
                return false;
            else
                return true;
        }
        #endregion
    }
}
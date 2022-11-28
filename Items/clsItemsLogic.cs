/*
 * Braxton Wright
 * CS 3280
 * Final Project class clsItemsLogic
 * Shawn Cowder
 * Due: December 10, 2022 at 11:59 PM
 * Version: 1.0
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the logistic for the items window so that the logistics is not behind the UI.
 * -----------------------------------------------------------------------------------------------------------
 */

using CS3280FinalProject.Shared;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System;
using System.Collections.ObjectModel;
using System.Windows;

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
                    Item currentItem = new Item(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), int.Parse(ds.Tables[0].Rows[i][2].ToString()));
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
        /// This function determines what failed in the user input when editing an item so the program can display the appropriate error message to them.
        /// </summary>
        /// <param name="Cost">The item's cost the user inputed.</param>
        /// <param name="Description">The item's description the user inputed.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Catches any exceptions that this function comes across.</exception>
        public string GenerateErrorMessage(string Cost, string Description)
        {
            try
            {
                bool CostFailed = Cost == "";
                bool DescriptionFailed = Description == "";

                //generate the appropriate error message
                string message = "Error: \n" + (CostFailed ? "The cost can't be left blank.\n" : "");
                message += (DescriptionFailed ? "The description can't be left blank.\n" : "");
                message += "Please change the above field(s) and try again.";

                return message;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This overloaded function determines what failed in the user input when adding an item so the program can display the appropriate error message to them.
        /// </summary>
        /// <param name="Code">The item's code the user inputed.</param>
        /// <param name="Cost">The item's cost the user inputed.</param>
        /// <param name="Description">The item's description the user inputed.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Catches any exceptions that this function comes across.</exception>
        public string GenerateErrorMessage(string Code, string Cost, string Description)
        {
            try
            {
                bool ItemCodeEmpty = Code == "";
                bool ItemCodeTaken = false;
                if (!ItemCodeEmpty)  //this is because we don't want to query the DB for an empty item code.
                {
                    ItemCodeTaken = ItemCodeIsTaken(Code);
                }
                bool CostFailed = Cost == "";
                bool DescriptionFailed = Description == "";

                //generate the appropriate error message
                string errorMessage = "Error: \n" + (ItemCodeEmpty ? "The item code can't be left blank.\n" : "");
                errorMessage += (!ItemCodeEmpty && ItemCodeTaken ? "The item code \"" + Code + "\" is already being, choose another for this item.\n" : "");
                errorMessage += (CostFailed ? "The cost can't be left blank.\n" : "");
                errorMessage += (DescriptionFailed ? "The description can't be left blank.\n" : "");
                errorMessage += "Please change the above field(s) and try again.";

                return errorMessage;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This converts a list that contains only 1 data type and set (No objects allowed) to a string.
        /// </summary>
        /// <typeparam name="T">The data type the list contains.</typeparam>
        /// <param name="ListData">The list that contains the data.</param>
        /// <returns>A string separated by a ', ' for every item in the list.</returns>
        public string ConvertSingleItemListToString<T>(List<T> ListData)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Check to see if the ItemCode is already used inside the DB. To prevent the program from crashing when attempting to add an item with the same identifier.
        /// </summary>
        /// <param name="ItemCode">The item code you are seeing if it exists inside the DB.</param>
        /// <returns>True if it is used and False if it is not.</returns>
        public bool ItemCodeIsTaken(string ItemCode)
        {
            try
            {
                string QuerryResult = DB.ExecuteScalarSQL(SQL.CheckItemCodeExist(ItemCode));

                if (QuerryResult == "")
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This function will handle any errors that the Items window will come across.
        /// </summary>
        /// <param name="ErrorMessage">The error message that was generated.</param>
        public void HandleException(string ErrorMessage)
        {
            try
            {
                MessageBox.Show(ErrorMessage.Substring(ErrorMessage.LastIndexOf("-> ") + 3));
            }
            catch (Exception ex)
            {
                string SavePath = System.AppDomain.CurrentDomain.BaseDirectory + "Error.txt";

                System.IO.File.AppendAllText(SavePath, Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
        #endregion
    }
}
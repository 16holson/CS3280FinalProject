/*
 * (Insert your name here)
 * CS 3280
 * Final Project prototype class clsMainLogic
 * Shawn Cowder
 * Due: November 19, 2022 at 11:59 PM
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the logistic for the main window so that the logistics is not behind the UI.
 * -----------------------------------------------------------------------------------------------------------
 */

using CS3280FinalProject.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Documents;

namespace CS3280FinalProject.Main
{
    public class clsMainLogic
    {

        #region Variables

        clsDataAccess cDataAccess;

        public int rowsReturned;

        #endregion

        #region Constructor

        public clsMainLogic()
        {
            cDataAccess = new clsDataAccess();
            rowsReturned = 0;
        }

        #endregion

        #region Invoice Information

        /// <summary>
        /// Creates a list of all items available for sale
        /// in the database
        /// </summary>
        /// <returns>List of all Items</returns>
        /// <exception cref="Exception"></exception>
        public List<Shared.Item> ItemList()
        {
            try
            {
                List<Shared.Item> itemList = new List<Shared.Item>();

                DataSet ds = cDataAccess.ExecuteSQLStatement(clsMainSQL.GetItems(), ref rowsReturned);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Item currentItem = new Item(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
                    itemList.Add(currentItem);
                }

                return itemList;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); ; 
            }
           
        }

        /// <summary>
        /// Creates a List of all the items listed on a particular invoice
        /// </summary>
        /// <param name="sInvoiceNum">Selected invoice Num</param>
        /// <returns>Invoice Items List</returns>
        /// <exception cref="Exception"></exception>
        public List<Shared.Item> GetInvoiceItems(string sInvoiceNum)
        {
            try
            {
                List<Shared.Item> lItems = new List<Shared.Item>();

                DataSet ds = cDataAccess.ExecuteSQLStatement(clsMainSQL.GetInvoiceItemInfo(sInvoiceNum), ref rowsReturned);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Item currentItem = new Item(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
                    lItems.Add(currentItem);
                }

                return lItems;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); ;
            }
            
        }

        public string GetNumLineItems(string sInvoiceNum)
        {
            string lineItemsNum = cDataAccess.ExecuteScalarSQL(clsMainSQL.GetNumLineItems(sInvoiceNum));
            return lineItemsNum;
        }

        #endregion

        #region Edit Invoices

        /// <summary>
        /// Creates a new Invoice in the database
        /// Returns the new invoice num from the database
        /// </summary>
        /// <param name="iNewInvoice">Object with Invoice Date and Cost</param>\
        /// <returns>New Invoice Num</returns>
        /// <exception cref="Exception"></exception>
        public string SaveNewInvoice(Shared.Invoice iNewInvoice)
        {
            try
            {
                int success = cDataAccess.ExecuteNonQuery(clsMainSQL.CreateInvoice(iNewInvoice.invoiceDate, iNewInvoice.totalCost));
                string invoiceNum = cDataAccess.ExecuteScalarSQL(clsMainSQL.GetHighestInvoiceNum());
                return invoiceNum;
            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); ;
            }
        }

        /// <summary>
        /// Updates the total cost of the invoice created
        /// </summary>
        /// <param name="iEditedInvoice">Invoice Object</param>
        /// <exception cref="Exception"></exception>
        public void UpdateInvoiceCost(Shared.Invoice iEditedInvoice)
        {
            try
            {
                int success = cDataAccess.ExecuteNonQuery(clsMainSQL.UpdateTotalCost(iEditedInvoice.totalCost, iEditedInvoice.invoiceNum));

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); ;
            }
        }

        /// <summary>
        /// Adds Line Item to Invoice
        /// </summary>
        /// <param name="sInvoiceNum">Current Invoice Number</param>
        /// <param name="sLineItemNum">Line Item Num</param>
        /// <param name="sItemCode">Item Code</param>
        /// <exception cref="Exception"></exception>
        public void AddItemToInvoice(string sInvoiceNum, string sLineItemNum, string sItemCode)
        {
            try
            {
                int success = cDataAccess.ExecuteNonQuery(clsMainSQL.AddItemToInvoice(sInvoiceNum, sLineItemNum, sItemCode));
            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); ;
            }
        }

        /// <summary>
        /// Removes a line item from a particular invoice
        /// </summary>
        /// <param name="sInvoiceNum">Invoice Num</param>
        /// <param name="sItemCode">Item Code</param>
        /// <exception cref="Exception"></exception>
        public void RemoveLineItem(string sInvoiceNum, string sItemCode)
        {
            try
            {
                int success = cDataAccess.ExecuteNonQuery(clsMainSQL.DeleteItemFromInvoice(sInvoiceNum, sItemCode));

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); ;
            }
        }

        /// <summary>
        /// Gets the Invoice information returned from the database
        /// </summary>
        /// <param name="sInvoiceNum">The invoice num</param>
        /// <returns>Invoice Object</returns>
        /// <exception cref="Exception"></exception>
        public Shared.Invoice GetInvoice(string sInvoiceNum)
        {
            try
            {
                Shared.Invoice iInvoice = new Shared.Invoice();

                DataSet ds = cDataAccess.ExecuteSQLStatement(clsMainSQL.GetInvoiceInfo(sInvoiceNum), ref rowsReturned);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Shared.Invoice invoice = new Shared.Invoice();
                    invoice.invoiceNum = dr[0].ToString();
                    invoice.invoiceDate = dr[1].ToString();
                    //Get the date not the time
                    invoice.invoiceDate = invoice.invoiceDate.Substring(0, invoice.invoiceDate.IndexOf(" "));
                    invoice.totalCost = dr[2].ToString();
                    iInvoice = invoice;
                }

                return iInvoice;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); ;
            }
            
            
        }
        #endregion

    }
}

/*
 * Levi Bernards
 * CS 3280
 * Final Project class clsMainLogic
 * Shawn Cowder
 * Due: December 10, 2022 at 11:59 PM
 * Version: 1.0
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the logistic for the main window so that the logistics is not behind the UI.
 * -----------------------------------------------------------------------------------------------------------
 */

using CS3280FinalProject.Shared;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;

namespace CS3280FinalProject.Main
{
    public class clsMainLogic
    {
        #region Variables
        /// <summary>
        /// Data Access object to perform SQL operations
        /// </summary>
        clsDataAccess cDataAccess;

        /// <summary>
        /// Int Variable to store number of rows returned in DataSets
        /// </summary>
        public int rowsReturned;
        #endregion

        #region Constructor
        /// <summary>
        /// Logic class constructor
        /// </summary>
        public clsMainLogic()
        {
            cDataAccess = new clsDataAccess();
            rowsReturned = 0;
        }
        #endregion

        #region Invoice Information
        /// <summary>
        /// Returns a list of invoice Numbers
        /// for all the invoices in the database
        /// </summary>
        /// <returns>List of Invoice Nums</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public List<Shared.Invoice> GetInvoices()
        {
            try
            {
                List<Shared.Invoice> invoiceList = new List<Shared.Invoice>();

                DataSet ds = cDataAccess.ExecuteSQLStatement(clsMainSQL.GetInvoices(), ref rowsReturned);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Invoice currentInvoice = new Invoice();
                    currentInvoice.invoiceNum = Int32.Parse(ds.Tables[0].Rows[i][0].ToString());
                    invoiceList.Add(currentInvoice);
                }
                return invoiceList;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a list of all items available for sale
        /// in the database
        /// </summary>
        /// <returns>List of all Items</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public List<Shared.Item> ItemList()
        {
            try
            {
                List<Shared.Item> itemList = new List<Shared.Item>();

                DataSet ds = cDataAccess.ExecuteSQLStatement(clsMainSQL.GetItems(), ref rowsReturned);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Item currentItem = new Item(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), Int32.Parse(ds.Tables[0].Rows[i][2].ToString()));
                    itemList.Add(currentItem);
                }

                return itemList;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
           
        }

        /// <summary>
        /// Gets the Invoice information returned from the database
        /// Also creates the list of items included in the invoice
        /// </summary>
        /// <param name="sInvoiceNum">The invoice num</param>
        /// <returns>Invoice Object</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public Shared.Invoice GetInvoice(int sInvoiceNum)
        {
            try
            {
                DataSet ds = cDataAccess.ExecuteSQLStatement(clsMainSQL.GetInvoiceInfo(sInvoiceNum), ref rowsReturned);

                
                Shared.Invoice invoice = new Shared.Invoice(Int32.Parse(ds.Tables[0].Rows[0][0].ToString()), ds.Tables[0].Rows[0][1].ToString(), Int32.Parse(ds.Tables[0].Rows[0][2].ToString()));

                invoice.items = GetInvoiceItems(invoice.invoiceNum);

                return invoice;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }


        }

        /// <summary>
        /// Creates a List of all the items listed on a particular invoice
        /// </summary>
        /// <param name="sInvoiceNum">Selected invoice Num</param>
        /// <returns>Invoice Items List</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public ObservableCollection<Item> GetInvoiceItems(int sInvoiceNum)
        {
            try
            {
                ObservableCollection<Item> lItems = new ObservableCollection<Item>();

                DataSet ds = cDataAccess.ExecuteSQLStatement(clsMainSQL.GetInvoiceItemInfo(sInvoiceNum), ref rowsReturned);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Item currentItem = new Item(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), Int32.Parse(ds.Tables[0].Rows[i][2].ToString()));
                    currentItem.lineItemNum = Int32.Parse(ds.Tables[0].Rows[i][3].ToString());
                    lItems.Add(currentItem);
                }

                return lItems;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }

        /// <summary>
        /// Returns the Number of line items on a specific Invoice
        /// </summary>
        /// <param name="sInvoiceNum">Current Invoice</param>
        /// <returns>Num Line Items</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public int GetMaxLineItem(int iInvoiceNum)
        {
            try
            {
                string maxLineItem = cDataAccess.ExecuteScalarSQL(clsMainSQL.GetMaxLineItem(iInvoiceNum));
                int iMaxLineItem;
                if(maxLineItem == "")
                {
                    iMaxLineItem = 1;
                }
                else
                {
                    iMaxLineItem = (Int32.Parse(maxLineItem) + 1);
                }
                return iMaxLineItem;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Edit Invoices
        /// <summary>
        /// Creates a new Invoice in the database
        /// Returns the new invoice num from the database
        /// </summary>
        /// <param name="iNewInvoice">Object with Invoice Date and Cost</param>\
        /// <returns>New Invoice Num</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public int SaveNewInvoice(Shared.Invoice iNewInvoice)
        {
            try
            {
                int success = cDataAccess.ExecuteNonQuery(clsMainSQL.CreateInvoice(iNewInvoice.invoiceDate, iNewInvoice.totalCost));
                int invoiceNum = Int32.Parse(cDataAccess.ExecuteScalarSQL(clsMainSQL.GetHighestInvoiceNum()));
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
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public void UpdateInvoiceInfo(Shared.Invoice iEditedInvoice)
        {
            try
            {
                int success = cDataAccess.ExecuteNonQuery(clsMainSQL.UpdateInvoiceInfo(iEditedInvoice.totalCost, iEditedInvoice.invoiceNum, iEditedInvoice.invoiceDate));

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
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public void AddItemToInvoice(int iInvoiceNum, int iLineItemNum, string sItemCode)
        {
            try
            {
                int success = cDataAccess.ExecuteNonQuery(clsMainSQL.AddItemToInvoice(iInvoiceNum, iLineItemNum, sItemCode));
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
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public void RemoveLineItem(int iInvoiceNum, int iLineItemNum)
        {
            try
            {
                int success = cDataAccess.ExecuteNonQuery(clsMainSQL.DeleteItemFromInvoice(iInvoiceNum, iLineItemNum));

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); ;
            }
        }

        /// <summary>
        /// A Method to update the cost of all invoices after
        /// the Edit window is closed if there is a change
        /// made in the item section of the database
        /// </summary>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public void UpdateAllInvoiceCosts()
        {
            try
            {
                // Get List of all Invoices as they currently are in database
                List<Shared.Invoice> invoiceList = GetInvoices();

                foreach(Shared.Invoice currInvoice in invoiceList)
                {
                    Shared.Invoice invoice = GetInvoice(currInvoice.invoiceNum);
                    invoice.items = GetInvoiceItems(invoice.invoiceNum);
                    int totalCost = 0;
                    foreach(Shared.Item item in invoice.items)
                    {
                        totalCost += item.itemCost;
                    }
                    // if bool is true (at least one item changed)
                    if (invoice.totalCost != totalCost)
                    {
                        // Update invoice in database
                        invoice.totalCost = totalCost;
                        UpdateInvoiceInfo(invoice);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); ;

            }
        }
        #endregion

        #region Error Handling
        /// <summary>
        /// Function to handle error messages
        /// </summary>
        /// <param name="ErrorMessage">Error Message</param>
        public void HandleException(string ErrorMessage)
        {
            try
            {
                MessageBox.Show(ErrorMessage.Substring(ErrorMessage.LastIndexOf("-> ") +3));
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
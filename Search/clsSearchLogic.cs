/*
* Hunter Olson
* CS 3280
* Final Project class clsSearchLogic
* Shawn Cowder
* Due: December 10, 2022 at 11:59 PM
* Version: 1.0
*  ----------------------------------------------------------------------------------------------------------
* This file contains the logistic for the search window so that the logistics is not behind the UI.
* -----------------------------------------------------------------------------------------------------------
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using CS3280FinalProject.Shared;

namespace CS3280FinalProject.Search
{
    public class clsSearchLogic
    {
        #region Class Variables
        /// <summary>
        /// Holds the list of all invoices and their items
        /// </summary>
        public List<Shared.Invoice> invoices;
        /// <summary>
        /// Sql needed for the logic
        /// </summary>
        private clsSearchSQL sql;
        /// <summary>
        /// Executes sql statements
        /// </summary>
        private clsDataAccess dataAccess;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor that sets the connection string to the database
        /// </summary>
        /// <exception cref="Exception">Handles all exceptions</exception>
		public clsSearchLogic()
        {
            try
            {
                invoices = new List<Shared.Invoice>();
                sql = new clsSearchSQL();
                dataAccess = new clsDataAccess();
                populateInvoices();
            }
            catch(Exception ex)
            {
                 HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a list of distinct invoiceNums
        /// </summary>
        /// <returns>List of invoiceNums that is ordered</returns>
        /// <exception cref="Exception">Handles all exceptions</exception>
        public List<int> getInvoiceNumList()
        {
            try
            { 
                return  (from invoice in invoices
                    select invoice.invoiceNum).Distinct()
                                              .OrderBy(i => i)
                                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of distinct invoiceDates
        /// </summary>
        /// <returns>List of invoice dates that is ordered</returns>
        /// <exception cref="Exception">Handles all exceptions</exception>
        public List<String> getInvoiceDateList()
        {
            try
            { 
                return  (from invoice in invoices
                    select invoice.invoiceDate).Distinct()
                                               .OrderBy(i => DateTime.Parse(i))
                                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of distinct invoiceTotals
        /// </summary>
        /// <returns>List of totalCosts that is sorted</returns>
        /// <exception cref="Exception">Handles all exceptions</exception>
        public List<int> getInvoiceTotalList()
        {
            try
            { 
                return  (from invoice in invoices
                         orderby invoice.totalCost
                         select invoice.totalCost).Distinct()
                                                  .OrderBy(i => i)
                                                  .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Sorts invoices based on given parameters
        /// </summary>
        /// <param name="num">invoice number</param>
        /// <param name="date">invoice date</param>
        /// <param name="total">total cost</param>
        /// <returns>List of filtered Invoices</returns>
        /// <exception cref="Exception">Handles all exceptions</exception>
        public List<Invoice> filterList(string num, string date, string total)
        {
            try
            {
                var filteredList = invoices;
                if(num != "")
                {
                    filteredList = filteredList.Where(i => i.invoiceNum.ToString() == num).ToList();
                }
                if(date != "")
                {
                    filteredList = filteredList.Where(i => i.invoiceDate == date).ToList();
                }
                if(total != "")
                {
                    filteredList = filteredList.Where(i => i.totalCost.ToString() == total).ToList();
                }
                return filteredList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Populates the invoices list with all invoices
        /// </summary>
        /// <exception cref="Exception">Handles all exceptions</exception>
        private void populateInvoices()
        {
            try
            {
                //Populate invoices
                DataSet invoicesDS = new DataSet();
                int iRef = 0;
                invoicesDS = dataAccess.ExecuteSQLStatement(sql.getInvoices(), ref iRef);
                foreach(DataRow dr in invoicesDS.Tables[0].Rows)
                {
                    Shared.Invoice invoice = new Shared.Invoice();
                    invoice.invoiceNum = Int32.Parse(dr[0].ToString());
                    invoice.invoiceDate = dr[1].ToString();
                    //Get the date not the time
                    invoice.invoiceDate = invoice.invoiceDate.Substring(0, invoice.invoiceDate.IndexOf(" "));
                    invoice.totalCost = Int32.Parse(dr[2].ToString());
                    invoices.Add(invoice);
                }
                //Populate each invoice in invoices with their items
                DataSet itemsDS = new DataSet();
                foreach(Invoice invoice in invoices)
                {
                    itemsDS = dataAccess.ExecuteSQLStatement(sql.getItems(invoice.invoiceNum), ref iRef);
                    foreach(DataRow dr in itemsDS.Tables[0].Rows)
                    {
                        Shared.Item item = new Shared.Item();
                        item.itemCode = dr[0].ToString();
                        item.itemDesc = dr[1].ToString();
                        item.itemCost = Int32.Parse(dr[2].ToString());
                        invoice.items.Add(item);
                    }
                }
                

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        /// <param name="sMessage">The message generated.</param>
        public void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
        #endregion
    }
}


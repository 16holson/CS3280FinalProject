
﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
﻿/*
 * Hunter Olson
 * CS 3280
 * Final Project class clsSearchSQL
 * Shawn Cowder
 * Due: December 10, 2022 at 11:59 PM
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the functions for the search window that returns a string representing the SQL statement
 * to query the DB.
 * -----------------------------------------------------------------------------------------------------------
 */

namespace CS3280FinalProject.Search
{
    public class clsSearchSQL
    {
        #region Methods
        /// <summary>
        /// Returns the SQL to get all invoices
        /// </summary>
        /// <returns></returns>
        public string getInvoices()
        {
            try
            { 
                return "SELECT * FROM Invoices";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Gets all the items for a given invoice number
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string getItems(int invoiceNum)
        {
            try
            { 
                return "SELECT " +
                       "ID.ItemCode as ItemCode, " +
                       "ID.ItemDesc as ItemDesc, " +
                       "ID.Cost as Cost " +
                       "FROM " +
                       "Invoices as I, LineItems as LI, ItemDesc as ID " +
                       "WHERE " +
                       "I.InvoiceNum = LI.InvoiceNum AND " +
                       "LI.ItemCode = ID.ItemCode AND " +
                       "I.InvoiceNum = " + invoiceNum;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}

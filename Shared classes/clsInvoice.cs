/*
 * Braxton Wright, Hunter Olson, and Levi Bernards
 * CS 3280
 * Final Project prototype class clsInvoice
 * Shawn Cowder
 * Due: November 19, 2022 at 11:59 PM
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * (Insert short description of what this class does)
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace CS3280FinalProject.Shared
{
    public class clsInvoice
    {
        #region Class Variables
        /// <summary>
        /// Invoices Number
        /// </summary>
        public string invoiceNum { get; set; }
        /// <summary>
        /// Invoices Date
        /// </summary>
        public string invoiceDate { get; set;}
        /// <summary>
        /// Invoices Total Cost
        /// </summary>
        public string totalCost { get; set; }
        /// <summary>
        /// List of items in the invoice
        /// </summary>
        public List<clsItem> items { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public clsInvoice()
        {
            try
            {
                items = new List<clsItem>();
            }
            catch(Exception ex)
            {
                 HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
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

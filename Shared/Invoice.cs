/*
 * Braxton Wright, Hunter Olson, and Levi Bernards
 * CS 3280
 * Final Project class Invoice
 * Shawn Cowder
 * Due: December 10, 2022 at 11:59 PM
 * Version: 1.0
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the variables and functions that are required make an invoice for the UI.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace CS3280FinalProject.Shared
{
    public class Invoice : INotifyPropertyChanged
    {
        #region Class Variables
        /// <summary>
        /// Invoices Number
        /// </summary>
        public int invoiceNum { get; set; }
        /// <summary>
        /// Invoices Date
        /// </summary>
        public string invoiceDate { get; set;}
        /// <summary>
        /// Invoices Total Cost
        /// </summary>
        public int totalCost { get; set; }
        /// <summary>
        /// List of items in the invoice
        /// </summary>
        public ObservableCollection<Item> items { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Invoice()
        {
            try
            {
                items = new ObservableCollection<Item>();
            }
            catch(Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                           MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Constructor to create an invoice when the follow
        /// parameters are passed through the parenthesis
        /// </summary>
        /// <param name="sInvoiceNum">Invoice Num</param>
        /// <param name="sInvoiceDate">Invoice Date</param>
        /// <param name="sInvoiceCost">Invoice Cost</param>
        public Invoice(int sInvoiceNum, string sInvoiceDate, int sInvoiceCost)
        {
            try
            {
                items = new ObservableCollection<Item>();
                invoiceNum = sInvoiceNum;
                invoiceDate = sInvoiceDate;
                totalCost = sInvoiceCost;
            }
            catch (Exception ex)
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

        #region INotifyPropertyChanged Members
        /// <summary>
        /// This is the contract we have to make with the compiler because we are implementing the interface "INotifyPropertyChanged".  So we must have this event defined.  We will raise this event anytime one of our properties changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
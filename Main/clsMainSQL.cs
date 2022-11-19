/*
 * (Insert your name here)
 * CS 3280
 * Final Project prototype class clsItemsSQL
 * Shawn Cowder
 * Due: November 19, 2022 at 11:59 PM
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the functions for the main window that returns a string representing the SQL statement
 * to query the DB.
 * -----------------------------------------------------------------------------------------------------------
 */

using CS3280FinalProject.Shared;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls.Primitives;

namespace CS3280FinalProject.Main
{
    public static class clsMainSQL
    {

        #region InvoiceCreation

        /// <summary>
        /// Returns a SQL String to create a new invoice
        /// </summary>
        /// <param name="iInvoiceDate">New Invoice Date</param>
        /// <param name="fTotalCost">New Invoice Cost</param>
        /// <returns>string sSQL</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public static string CreateInvoice(string sInvoiceDate, float fTotalCost)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices(InvoiceDate, TotalCost) Values(#"
                              + sInvoiceDate + "#, " + fTotalCost + ")";
                return sSQL;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a SQL String to add a specific Item to 
        /// and invoice.
        /// </summary>
        /// <param name="iInvoiceNum">Current Invoice Num</param>
        /// <param name="iLineItemNum">Line Item Number</param>
        /// <param name="sItemCode">New Item Code</param>
        /// <returns>string sSQL</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public static string AddItemToInvoice(int iInvoiceNum, int iLineItemNum, string sItemCode)
        {
            try
            {
                string sSQL = "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) Values(" +
                              iInvoiceNum + ", " + iLineItemNum + ", '" + sItemCode + "')";
                return sSQL;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a SQL String to remove a specific
        /// item from an invoice
        /// </summary>
        /// <param name="iInvoiceNum">Current Invoice num</param>
        /// <param name="iLineItem">Line Item to Delete</param>
        /// <returns>string sSQL</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public static string DeleteItemFromInvoice(int iInvoiceNum, int iLineItemNum)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems WHERE InvoiceNum = " + iInvoiceNum + " AND LineItemNum = " + iLineItemNum;
                return sSQL;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a string to update the 
        /// Total Cost of an invoice
        /// </summary>
        /// <param name="fTotalCost">New Cost of Invoice</param>
        /// <param name="iInvoiceNum">Current Invoice Num</param>
        /// <returns>string sSQL</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public static string UpdateTotalCost(float fTotalCost, int iInvoiceNum)
        {
            try
            {
                string sSQL = "UPDATE Invoices Set TotalCost = " + fTotalCost + " WHERE InvoiceNum = " + iInvoiceNum;
                return sSQL;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

        #region Invoice Information

        /// <summary>
        /// Returns a SQL String to get a
        /// List of all Invoice Numbers in the database
        /// </summary>
        /// <returns>string sSQL</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public static string GetInvoices()
        {
            try
            {
                string sSQL = "SELECT invoiceNum FROM Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a SQL String to get a List of 
        /// all the Items in the database
        /// </summary>
        /// <returns>string sSQL</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public static string GetItems()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost from ItemDesc";
                return sSQL;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a SQL string to get the information
        /// regarding a specific invoice from the database
        /// </summary>
        /// <param name="iInvoiceNum">Current Invoice</param>
        /// <returns>string sSQL</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public static string GetInvoiceInfo(int iInvoiceNum)
        {
            try
            {
                string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + iInvoiceNum;
                return sSQL;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a SQL string to get the total number of 
        /// line items on a specific invoice num
        /// </summary>
        /// <param name="iInvoiceNum">Current Invoice</param>
        /// <returns>string sSQL</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public static string GetNumLineItems(int iInvoiceNum)
        {
            try
            {
                string sSQL = "SELECT COUNT(*) FROM LineItems WHERE InvoiceNum = " + iInvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a SQL string to get the specific item
        /// info for a specific invoices
        /// </summary>
        /// <param name="iInvoiceNum">Current Invoice</param>
        /// <returns>string sSQL</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public static string GetInvoiceItemInfo(int iInvoiceNum)
        {
            try
            {
                string sSQL = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc WHERE" +
                              " LineItems.ItemCode = ItemDesc.ItemCode AND LineItems.InvoiceNum = " + iInvoiceNum;
                return sSQL;

            }
            catch (System.Exception ex)
            {

                throw new Exception(MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a SQL string to get the highest
        /// invoice number in the database
        /// </summary>
        /// <returns>string sSQL</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public static string GetHighestInvoiceNum()
        {
            try
            {
                string sSQL = "SELECT MAX(InvoiceNum) FROM Invoices";
                return sSQL;

            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion
    }
}

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
using System.Collections.Generic;
using System.Windows.Controls.Primitives;

namespace CS3280FinalProject.Main
{
    public static class clsMainSQL
    {

        #region InvoiceCreation

        public static string CreateInvoice(string sInvoiceDate, string sTotalCost)
        {
            string sSQL = "INSERT INTO Invoices(InvoiceDate, TotalCost) Values(#"
                          + sInvoiceDate + ", " + sTotalCost + ")";
            return sSQL;
        }

        public static string AddItemToInvoice(string sInvoiceNum, string sLineItemNum, string sItemCode)
        {
            string sSQL = "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) Values(" +
                          sInvoiceNum + ", " + sLineItemNum + ", " + sItemCode + ")";
            return sSQL;
        }

        public static string DeleteItemFromInvoice(string sInvoiceNum, string sItemCode)
        {
            string sSQL = "DELETE FROM LineItems WHERE InvoiceNum = " + sInvoiceNum + " AND ItemCode = " + sItemCode;
            return sSQL;
        }

        public static string UpdateTotalCost(string sTotalCost, string sInvoiceNum)
        {
            string sSQL = "UPDATE Invoices Set TotalCost = " + sTotalCost + " WHERE InvoiceNum = " + sInvoiceNum;
            return sSQL;
        }

        #endregion

        #region Invoice Information

        public static string GetItems()
        {
            string sSQL = "SELECT ItemCode, ItemDesc, Cost from ItemDesc";
            return sSQL;
        }


        public static string GetInvoiceInfo(string sInvoiceNum)
        {
            string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + sInvoiceNum;
            return sSQL;
        }

        
        public static string GetInvoiceItemInfo(string sInvoiceNum)
        {
            string sSQL = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc WHERE" +
                          " LineItems.ItemCode = ItemDesc.ItemCode AND LineItems.InvoiceNum = " + sInvoiceNum;
            return sSQL;
        }

        #endregion
    }
}

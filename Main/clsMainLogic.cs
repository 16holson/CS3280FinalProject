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
using System.Collections.Generic;
using System.Data;
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

        public List<Shared.Item> ItemList()
        {
            List<Shared.Item> itemList = new List<Shared.Item>();

            DataSet ds = cDataAccess.ExecuteSQLStatement(clsMainSQL.GetItems(), ref rowsReturned);

            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Item currentItem = new Item(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
                itemList.Add(currentItem);
            }

            return itemList;
        }

        public List<Shared.Item> GetAllItems(string sInvoiceNum)
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


        public void SaveNewInvoice(Shared.Invoice iNewInvoice)
        {
            int success = cDataAccess.ExecuteNonQuery(clsMainSQL.CreateInvoice(iNewInvoice.invoiceDate, iNewInvoice.totalCost));
        }


        public void EditInvoice(Shared.Invoice iEditedInvoice)
        {
            int success = cDataAccess.ExecuteNonQuery(clsMainSQL.UpdateTotalCost(iEditedInvoice.totalCost, iEditedInvoice.invoiceNum));
        }

        public void AddLineItem(string sInvoiceNum, string sLineItemNum, string sItemCode)
        {
            int success = cDataAccess.ExecuteNonQuery(clsMainSQL.AddItemToInvoice(sInvoiceNum, sLineItemNum, sItemCode));
        }

        public void RemoveLineItem(string sInvoiceNum, string sItemCode)
        {
            int success = cDataAccess.ExecuteNonQuery(clsMainSQL.DeleteItemFromInvoice(sInvoiceNum, sItemCode));
        }

        public Shared.Invoice GetInvoice(string sInvoiceNum)
        {
            Shared.Invoice iInvoice = new Shared.Invoice();

            DataSet ds = cDataAccess.ExecuteSQLStatement(clsMainSQL.GetInvoiceInfo(sInvoiceNum), ref rowsReturned);

            //Shared.Invoice iInvoice = new Shared.Invoice(ds.Tables[0].Rows)
            //Item currentItem = new Item(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
            
            

            return iInvoice;
        }

    }
}

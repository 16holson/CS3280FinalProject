/*
 * Braxton Wright
 * CS 3280
 * Final Project prototype Window wndItems
 * Shawn Cowder
 * Due: November 19, 2022 at 11:59 PM
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the event listeners for the Items window.
 * -----------------------------------------------------------------------------------------------------------
 */

using System.Reflection;
using System;
using System.Windows;
using CS3280FinalProject.Shared;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.ComponentModel;
using System.Configuration;
using System.Linq;

namespace CS3280FinalProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region variables
        //private variables
        private clsItemsLogic ItemLogic;

        /// <summary>
        /// Tells if the user has made a change in the data stored.
        /// </summary>
        private bool bHasItemsBeenChanged;

        /// <summary>
        /// Stores a ObservableCollection of clsItems to manipulate.
        /// </summary>
        private ObservableCollection<clsItem> Items;

        //properties
        /// <summary>
        /// This Getter/Setter manipulates the variable HasItemsBeenChanged (not yet completed)
        /// </summary>
        public bool HasItemsBeenChanged { get; set; }
        #endregion


        #region functions
        /// <summary>
        /// Default constructor for the wndItems window.
        /// </summary>
        public wndItems()
        {
            InitializeComponent();
            ItemLogic = new clsItemsLogic();
            bHasItemsBeenChanged = false;
            FillDataGrid();
        }

        /// <summary>
        /// Fills the data grid with the columns and data that is retrieved from the DB.
        /// </summary>
        private void FillDataGrid()
        {
            Items = ItemLogic.GetAllItemsFromDB();
            datagridItems.ItemsSource = Items;
            //sorts the items by their itemCode in ascending order (how this is done can be found here https://stackoverflow.com/questions/34421719/wpf-datagrid-automatic-sorting-by-chosen-column)
            datagridItems.Items.SortDescriptions.Add(new SortDescription("itemCode", ListSortDirection.Ascending));
        }

        /// <summary>
        /// This event listeners listens for when the user press a button to add a new item.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //need to implement a check to make sure that the item for the codecode is not taken.
                float cost;
                if (float.TryParse(txtItemCost.Text, out cost))
                {
                    ItemLogic.AddItem(txtItemCode.Text, txtItemDesc.Text, cost);  //add the item to the DB
                    //Add the item to the Items ObservableCollection (and because Items is a ObservableCollection, it is the source of the DataGrid "datagridItems", and the clsItems has the interface "INotifyPropertyChanged",
                    //also to the DataGrid "datagridItems")
                    Items.Add(new clsItem(txtItemCode.Text, txtItemDesc.Text, cost));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listeners listens for when the user press a button to save a set of change to an item.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnSaveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ItemLogic.UpdateItemData();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// This event listeners listens for when the user press a button to delete an item.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datagridItems.SelectedItem != null)
                {
                    clsItem SelectedItem = (clsItem)datagridItems.SelectedItem;  //get the selected item from the datagrid

                    List<int> InvoicesForGivenItemCode = ItemLogic.GetAllInvoiceNumsForItemCode(SelectedItem.itemCode);  //finds out if any invoices are connected to that item

                    //make sure that the item is not attached to an invoice
                    if (InvoicesForGivenItemCode.Count == 0)  //continue with the deleting
                    {
                        ItemLogic.DeleteItem(SelectedItem.itemCode);  //remove the item from the DB
                        //remove the item from the Items ObservableCollection (and because Items is a ObservableCollection, it is the source of the DataGrid "datagridItems", and the clsItems has the interface "INotifyPropertyChanged",
                        //also from the DataGrid "datagridItems")
                        Items.Remove((clsItem)datagridItems.SelectedItem);
                    }
                    else  //don't delete it because it is attached to at least one invoice
                    {
                        MessageBox.Show("Unable to delete item, because it is attached to these invoice(s) \"" + ConvertSingleItemListToString<int>(InvoicesForGivenItemCode) + "\"", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private static string ConvertSingleItemListToString<T>(List<T> ListData)
        {
            string Result = "";

            for(int i = 0; i < ListData.Count; i++)
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
    }
}

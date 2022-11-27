/*
 * Braxton Wright
 * CS 3280
 * Final Project Window wndItems
 * Shawn Cowder
 * Due: December 10, 2022 at 11:59 PM
 * Version: 1.0
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners and functions for the Items window.
 * -----------------------------------------------------------------------------------------------------------
 */

using System.Reflection;
using System;
using System.Windows;
using CS3280FinalProject.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace CS3280FinalProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region Variables
        /// <summary>
        /// An instance of the class clsItemsLogic used to contain non-UI based logic.
        /// </summary>
        private clsItemsLogic ItemLogic;

        /// <summary>
        /// Stores a ObservableCollection of clsItems to manipulate.
        /// </summary>
        private ObservableCollection<Item> Items;

        /// <summary>
        /// Stores the reference to the current item in the "Items" ObservableCollection so the program knows what to edit.
        /// </summary>
        private Item CurrentEditingItem;

        /// <summary>
        /// Stores a true/false value that will signify if item(s) inside the DB has been modified.
        /// </summary>
        private bool ItemsChanged;

        /// <summary>
        /// This getter returns a boolean that represents if item(s) have been changed/added to the DB and a update is required.
        /// </summary>
        public bool HasItemsChanged
        {
            get { return ItemsChanged; }
        }

        /// <summary>
        /// Holds the mode of what the window is currently doing so it knows what to enable and what to disable.
        /// </summary>
        private bool AddMode;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for the wndItems window.
        /// </summary>
        public wndItems()
        {
            try
            {
                InitializeComponent();
                ItemLogic = new clsItemsLogic();
                ItemsChanged = false;
                AddMode = true;
                FillDataGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Fills the data grid with the rows of data that is retrieved from the DB.
        /// </summary>
        private void FillDataGrid()
        {
            try
            {
                Items = ItemLogic.GetAllItemsFromDB();
                datagridItems.ItemsSource = Items;
                //sorts the items by their itemCode in ascending order (how this is done can be found here https://stackoverflow.com/questions/34421719/wpf-datagrid-automatic-sorting-by-chosen-column)
                datagridItems.Items.SortDescriptions.Add(new SortDescription("itemCode", ListSortDirection.Ascending));
            }
            catch (Exception ex)
            {
                ItemLogic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This function contains the necessary code to quickly change between two different modes, adding items and editing items.
        /// </summary>
        private void ChangeMode()
        {
            if (AddMode)  //if the program is in the adding items mode, change to edit item mode
            {
                //hide/disable unnecessary buttons to editing an item
                txtItemCode.IsReadOnly = true;  //prevent the user from altering the Item's code
                //controls for editing item
                btnCancel.Visibility = Visibility.Visible;
                btnSaveChanges.Visibility = Visibility.Visible;
                //other controls that are not required for editing an item
                btnAddItem.Visibility = Visibility.Collapsed;
                btnEditItem.IsEnabled = false;
                btnDeleteItem.IsEnabled = false;
                AddMode = false;  //change the internal mode variable so that it knows what mode it is in

                //populate the fields with the data for that item
                Item SelectedItem = (Item)datagridItems.SelectedItem;  //get the selected item from the datagrid
                txtItemCode.Text = SelectedItem.itemCode.ToString();
                txtItemCost.Text = SelectedItem.itemCost.ToString();
                txtItemDesc.Text = SelectedItem.itemDesc.ToString();

                datagridItems.IsEnabled = false;  //prevent the user from selecting any items inside the datagrid
            }
            else  //if the program is in the edit item mode, change to adding items mode
            {
                //revert the changes to prior of editing an item
                txtItemCode.IsReadOnly = false;  //allow the user to alter the Item's code for adding items
                //controls for editing item
                btnCancel.Visibility = Visibility.Collapsed;
                btnSaveChanges.Visibility = Visibility.Collapsed;
                //other controls that are not required for editing an item
                btnAddItem.Visibility = Visibility.Visible;
                btnEditItem.IsEnabled = true;
                btnDeleteItem.IsEnabled = true;
                AddMode = true;  //change the internal mode variable so that it knows what mode it is in

                //clear the data from the fields
                txtItemCode.Text = "";
                txtItemCost.Text = "";
                txtItemDesc.Text = "";

                datagridItems.IsEnabled = true;  //allow the user to select any items inside the datagrid so they can edit a different item
            }
        }
        #endregion

        #region Event Listeners
        /// <summary>
        /// This event listener listens for when a user press a key and validates that it is not the space bar.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void txtItemCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                //Stop the character from being entered into the textbox because space characters are not allowed for the item's codes.
                e.Handled = true;
            }
        }

        /// <summary>
        /// This event listener listens for when a user press a key and validates that it is one of the following keys 0-9, backspace, left and right arrow, or a tab.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void txtItemCost_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Right || e.Key == Key.Left || e.Key == Key.Tab)
                {
                    //do nothing allow the key to be accepted
                }
                else
                {
                    //Stop the character from being entered into the textbox because it neither a digit, a backspace, a left/right arrow, or a tab.
                    e.Handled = true;
                }
            }
            catch(Exception ex)
            {
                ItemLogic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener listens for when the user changes the selection inside the datagrid so that it knows when to enable/disable the buttons for editing/deleting an item from it.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void datagridItems_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (datagridItems.SelectedItem != null)  //enable the two buttons
                {
                    btnEditItem.IsEnabled = true;
                    btnDeleteItem.IsEnabled = true;
                }
                else  //disable the two buttons
                {
                    btnEditItem.IsEnabled = false;
                    btnDeleteItem.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                ItemLogic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener listens for when the user press a button to add a new item.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //need to implement a check to make sure that the code is not taken by another.
                if (txtItemCode.Text != "" && !ItemLogic.ItemCodeIsTaken(txtItemCode.Text) && txtItemCost.Text != "" && txtItemDesc.Text != "")
                {
                    int cost = int.Parse(txtItemCost.Text);

                    ItemLogic.AddItem(txtItemCode.Text, txtItemDesc.Text, cost);  //add the item to the DB
                    //Add the item to the Items ObservableCollection (and because Items is a ObservableCollection, it is the source of the DataGrid "datagridItems", and the clsItems has the interface "INotifyPropertyChanged",
                    //also to the DataGrid "datagridItems")
                    Items.Add(new Item(txtItemCode.Text, txtItemDesc.Text, cost));

                    ItemsChanged = true;  //once everything has been added, set the changed variable to tell the user that there has been a change in the items

                    //Set the contents of the user input to be empty so they can add a new item to it
                    txtItemCode.Text = "";
                    txtItemDesc.Text = "";
                    txtItemCost.Text = "";
                }
                else
                {
                    //display the appropriate error message(s)
                    string ErrorMessage = ItemLogic.GenerateErrorMessage(txtItemCode.Text, txtItemCost.Text, txtItemDesc.Text);

                    MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                ItemLogic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener listens for when the user press a button to save a set of change to an item.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datagridItems.SelectedItem != null)
                {
                    ChangeMode();
                    CurrentEditingItem = (Item)datagridItems.SelectedItem;
                }
            }
            catch (Exception ex)
            {
                ItemLogic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener listens for when the user press a button to cancel any edits they are doing to an item and revert the controls to add items.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeMode();
            }
            catch (Exception ex)
            {
                ItemLogic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener listens for when the user press a button to save the changes to an item.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //perform checks on the data to make sure it is valid
                if (txtItemCost.Text != "" && txtItemDesc.Text != "")  //validate that the price is in the correct format and that the description is not empty.
                {
                    float cost = float.Parse(txtItemCost.Text);

                    ItemLogic.UpdateItemData(txtItemCode.Text, txtItemDesc.Text, cost);  //save the changes to the database
                    //update the item from the Items ObservableCollection (and because Items is a ObservableCollection, it is the source of the DataGrid "datagridItems", and the clsItems has the interface "INotifyPropertyChanged",
                    //also from the DataGrid "datagridItems")
                    CurrentEditingItem.itemCost = int.Parse(txtItemCost.Text);
                    CurrentEditingItem.itemDesc = txtItemDesc.Text;

                    ChangeMode();

                    ItemsChanged = true;  //once everything has been added, set the changed variable to tell the user that there has been a change in the items
                }
                else  //one or both of the conditions failed, so display a message box to the user to tell them what to do
                {
                    //display the appropriate error message(s)
                    string ErrorMessage = ItemLogic.GenerateErrorMessage(txtItemCost.Text, txtItemDesc.Text);

                    MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                ItemLogic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener listens for when the user press a button to delete an item.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datagridItems.SelectedItem != null)
                {
                    Item SelectedItem = (Item)datagridItems.SelectedItem;  //get the selected item from the datagrid

                    List<int> InvoicesForGivenItemCode = ItemLogic.GetAllInvoiceNumsForItemCode(SelectedItem.itemCode);  //finds out if any invoices are connected to that item

                    //make sure that the item is not attached to an invoice
                    if (InvoicesForGivenItemCode.Count == 0)  //continue with the deleting
                    {
                        ItemLogic.DeleteItem(SelectedItem.itemCode);  //remove the item from the DB
                        //remove the item from the Items ObservableCollection (and because Items is a ObservableCollection, it is the source of the DataGrid "datagridItems", and the clsItems has the interface "INotifyPropertyChanged",
                        //also from the DataGrid "datagridItems")
                        Items.Remove((Item)datagridItems.SelectedItem);  //remove the item from the datagrid

                        ItemsChanged = true;  //set the changed variable to tell the user that the list of items has changed
                    }
                    else  //don't delete it because it is attached to at least one invoice
                    {
                        MessageBox.Show("Unable to delete item, because it is attached to these invoice number(s) \"" + ItemLogic.ConvertSingleItemListToString<int>(InvoicesForGivenItemCode) + "\"", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ItemLogic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
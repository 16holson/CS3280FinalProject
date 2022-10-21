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
        //private variables
        private clsItemsLogic ItemLogic;

        /// <summary>
        /// Stores a ObservableCollection of clsItems to manipulate.
        /// </summary>
        private ObservableCollection<clsItem> Items;

        /// <summary>
        /// Stores the reference to the current item in the "Items" ObservableCollection so the program knows what to edit
        /// </summary>
        private clsItem CurrentEditingItem;

        //properties
        /// <summary>
        /// Tells the user if anything has been changed in the data stored inside the DB and if the main window needs to be refreshed.
        /// </summary>
        public bool HasItemsBeenChanged { get; set; }

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
            InitializeComponent();
            ItemLogic = new clsItemsLogic();
            HasItemsBeenChanged = false;
            AddMode = true;
            FillDataGrid();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Fills the data grid with the rows of data that is retrieved from the DB.
        /// </summary>
        private void FillDataGrid()
        {
            Items = ItemLogic.GetAllItemsFromDB();
            datagridItems.ItemsSource = Items;
            //sorts the items by their itemCode in ascending order (how this is done can be found here https://stackoverflow.com/questions/34421719/wpf-datagrid-automatic-sorting-by-chosen-column)
            datagridItems.Items.SortDescriptions.Add(new SortDescription("itemCode", ListSortDirection.Ascending));
        }

        /// <summary>
        /// This event listener 
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void txtItemCost_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Decimal || e.Key == Key.OemPeriod)) && e.Key != Key.Space || e.Key == Key.Back || e.Key == Key.Right || e.Key == Key.Left)
            {
                //do nothing allow the key to be accepted
            }
            else
            {
                //Stop the character from being entered into the textbox because it neither a digit or a '.'.
                e.Handled = true;
            }

        }

        /// <summary>
        /// This event listener listens for when the user changes the selection inside the datagrid so that it knows when to enable/disable the buttons for editing/deleting an item from it.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void datagridItems_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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
                if (txtItemCode.Text != "" && !ItemLogic.ItemCodeIsTaken(txtItemCode.Text) && ItemLogic.ValidateCostFormat(txtItemCost.Text) && txtItemDesc.Text != "")
                {
                    float cost = float.Parse(txtItemCost.Text);

                    ItemLogic.AddItem(txtItemCode.Text, txtItemDesc.Text, cost);  //add the item to the DB
                    //Add the item to the Items ObservableCollection (and because Items is a ObservableCollection, it is the source of the DataGrid "datagridItems", and the clsItems has the interface "INotifyPropertyChanged",
                    //also to the DataGrid "datagridItems")
                    Items.Add(new clsItem(txtItemCode.Text, txtItemDesc.Text, cost));

                    HasItemsBeenChanged = true;  //once everything has been added, set the changed variable to tell the user that there has been a change in the items

                    //Set the contents of the user input to be empty so they can add a new item to it
                    txtItemCode.Text = "";
                    txtItemDesc.Text = "";
                    txtItemCost.Text = "";
                }
                else
                {
                    bool ItemCodeEmpty = txtItemCode.Text == "" ? true : false;
                    bool ItemCodeTaken = false;
                    if (!ItemCodeEmpty)  //this is because we don't want to query the DB for an empty item code.
                    {
                        ItemCodeTaken = ItemLogic.ItemCodeIsTaken(txtItemCode.Text);
                    }
                    bool CostFailed = !ItemLogic.ValidateCostFormat(txtItemCost.Text);  //condition reversed because it returns false if it fails
                    bool DescriptionFailed = txtItemDesc.Text == "" ? true : false;

                    //display the appropriate error message(s)
                    string errorMessage = "Error: \n" + (ItemCodeEmpty ? "The item code can't be left blank.\n" : "");
                    errorMessage += (!ItemCodeEmpty && ItemCodeTaken ? "The item code \"" + txtItemCode.Text + "\" is already being used inside the DB.\n" : "");
                    errorMessage += (CostFailed ? "The cost has to be in this format \"OneOrMoreDigits\" with an optional '.OneOrTwoDigits' after it.\n" : "");
                    errorMessage += (DescriptionFailed ? "The description can't be left empty.\n" : "");
                    errorMessage += "Please change the above field(s) and try again.";

                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
                    CurrentEditingItem = (clsItem)datagridItems.SelectedItem;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //STILL NEED TO TEST
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
                if (ItemLogic.ValidateCostFormat(txtItemCost.Text) && txtItemDesc.Text != "")  //validate that the price is in the correct format and that the description is not empty.
                {
                    float cost = float.Parse(txtItemCost.Text);

                    ItemLogic.UpdateItemData(txtItemCode.Text, txtItemDesc.Text, cost);  //save the changes to the database
                    //update the item from the Items ObservableCollection (and because Items is a ObservableCollection, it is the source of the DataGrid "datagridItems", and the clsItems has the interface "INotifyPropertyChanged",
                    //also from the DataGrid "datagridItems")
                    CurrentEditingItem.itemCost = cost;
                    CurrentEditingItem.itemDesc = txtItemDesc.Text;

                    ChangeMode();

                    HasItemsBeenChanged = true;  //once everything has been added, set the changed variable to tell the user that there has been a change in the items
                }
                else  //one or both of the conditions failed, so display a message box to the user to tell them what to do
                {
                    bool CostFailed = !ItemLogic.ValidateCostFormat(txtItemCost.Text);  //condition reversed because it returns false if it fails
                    bool DescriptionFailed = txtItemDesc.Text == "" ? true : false;

                    //display the appropriate error message(s)
                    string errorMessage = "Error: \n" + (CostFailed ? "The cost has to be in this format \"OneOrMoreDigits\" with an optional '.OneOrTwoDigits' after it.\n" : "");
                    errorMessage += (DescriptionFailed ? "The description can't be empty.\n" : "");
                    errorMessage += "Please change the above field(s) and try again.";
                    
                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This function contains the necessary code to quickly change between two different modes, adding items and editing items.
        /// </summary>
        private void ChangeMode()
        {
            if(AddMode)  //if the program is in the adding items mode, change to edit item mode
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
                clsItem SelectedItem = (clsItem)datagridItems.SelectedItem;  //get the selected item from the datagrid
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
                    clsItem SelectedItem = (clsItem)datagridItems.SelectedItem;  //get the selected item from the datagrid

                    List<int> InvoicesForGivenItemCode = ItemLogic.GetAllInvoiceNumsForItemCode(SelectedItem.itemCode);  //finds out if any invoices are connected to that item

                    //make sure that the item is not attached to an invoice
                    if (InvoicesForGivenItemCode.Count == 0)  //continue with the deleting
                    {
                        ItemLogic.DeleteItem(SelectedItem.itemCode);  //remove the item from the DB
                        //remove the item from the Items ObservableCollection (and because Items is a ObservableCollection, it is the source of the DataGrid "datagridItems", and the clsItems has the interface "INotifyPropertyChanged",
                        //also from the DataGrid "datagridItems")
                        Items.Remove((clsItem)datagridItems.SelectedItem);  //remove the item from the datagrid

                        HasItemsBeenChanged = true;  //set the changed variable to tell the user that the list of items has changed
                    }
                    else  //don't delete it because it is attached to at least one invoice
                    {
                        MessageBox.Show("Unable to delete item, because it is attached to these invoice(s) \"" + ItemLogic.ConvertSingleItemListToString<int>(InvoicesForGivenItemCode) + "\"", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}

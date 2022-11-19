/*
 * (Insert your name here)
 * CS 3280
 * Final Project prototype Window wndMain
 * Shawn Cowder
 * Due: November 19, 2022 at 11:59 PM
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the Main window.
 * -----------------------------------------------------------------------------------------------------------
 */

using CS3280FinalProject.Items;
using CS3280FinalProject.Search;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace CS3280FinalProject.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        #region Variables

        /// <summary>
        /// Bool value to know whether items were changed in the Item Window
        /// </summary>
        public bool bItemsChanged;

        /// <summary>
        /// Used to Let the date be changed
        /// </summary>
        public bool bInvoiceCreation;

        /// <summary>
        /// Bool to protect items from being removed from the database
        /// </summary>
        public bool bRemove;

        /// <summary>
        /// List to hold all items in the database
        /// </summary>
        List<Shared.Item> lItemList;

        /// <summary>
        /// List of items to add when the Save Invoice Button is selected
        /// </summary>
        List<Shared.Item> lItemsToAdd;

        /// <summary>
        /// List of items to remove when the Save Invoice Button is selected
        /// </summary>
        List<Shared.Item> lItemsToRemove;

        /// <summary>
        /// Object to perform logic class operations
        /// </summary>
        clsMainLogic logic;

        /// <summary>
        /// Invoice object to store information for the current invoice
        /// </summary>
        Shared.Invoice currentInvoice;
        #endregion


        #region Constructor
        /// <summary>
        /// Default constructor for the wndMain window.
        /// </summary>
        public wndMain()
        {
            InitializeComponent();
            bItemsChanged = false;
            logic = new clsMainLogic();
            fillItemCB();
            currentInvoice = new Shared.Invoice();
            MainItemDisplay.ItemsSource = currentInvoice.items;

            lItemsToAdd = new List<Shared.Item>();
            lItemsToRemove = new List<Shared.Item>();
        }


        #endregion


        #region UI Functions

        #region Menu Buttons

        /// <summary>
        /// (Braxton Wright, added the code to hide the main window and display the search window)
        /// This event listener listens for when the user clicks the Search item inside the menu.  
        /// It will then take them to the Search window.
        /// 
        /// (Levi Bernards, Created to perform operations on the Main Window)
        /// Upon closing the Search screen, The program will copy the selected invoice
        /// into the currentInvoice object.  It will then update the screen by changing
        /// Labels, allowing the Edit Invoice button to be pressed and displaying the
        /// items in the DataGrid
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void MenuSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndSearch Search = new wndSearch();
                Search.Owner = this;  //this sets it so that the search window owner is this window (so it loads where this window is currently)

                this.Hide();  //hide this from the user
                Search.ShowDialog();  //open the search window and pause here in the code until the window is closed
                this.Show();  //shows this window to the user

                // This is how the user gets the chosen invoice num from the user
                bool bVerify = false;
                if (Search.selectedNum != null)
                {
                    List<Shared.Invoice> lInvoiceList = logic.GetInvoices();

                    foreach(Shared.Invoice currInvoice in lInvoiceList)
                    {
                        if(currInvoice.invoiceNum == Search.selectedNum)
                        {
                            bVerify = true;
                        }
                    }
                    if (bVerify == true)
                    {
                        int selectedInvoiceNum = Search.selectedNum;
                        currentInvoice = logic.GetInvoice(selectedInvoiceNum);
                        updateInvoiceLabels(currentInvoice);
                        MainItemDisplay.ItemsSource = currentInvoice.items;

                        //Allow the option to edit the invoice
                        EditInvoiceButton.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        ///  (Braxton Wright, Created to allow the Edit Items window to open when button is clicked)
        /// This event listener listens for when the user clicks the Edit Items item inside the menu.  
        /// It will then take them to the Items window.
        /// 
        /// (Levi Bernards, Main Window Functions)
        /// If any items are changed, the main window will update
        /// the Item Combo Box and if there is a current invoice being displayed,
        /// The item information regarding that will be updated as well
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void MenuEditItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndItems EditItems = new wndItems();
                EditItems.Owner = this;  //this sets it so that the edit items window owner is this window (so it loads where this window is currently)

                this.Hide();  //hide this from the user
                              //open the edit items window and pause here in the code until the window is closed
                EditItems.ShowDialog();
                this.Show();  //shows this window to the user

                bItemsChanged = EditItems.HasItemsChanged;

                //see if there has been any changes to the items inside the DB so you can see if you are required to update this window's list of items
                if (bItemsChanged == true)
                {
                    //Refill Combo Box
                    fillItemCB();
                }
            }
            catch (Exception ex)
            {
                logic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        #endregion

        #region Invoice Creation/Changing

        /// <summary>
        /// Opens up the DatePicker for the user to use
        /// Also updates labels to show That the invoice num
        /// is TBD and the Cost is blank.  No other functionalities
        /// available until a date is chosen
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void CreateInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Disable and Enable buttons that apply
                CreateInvoiceButton.IsEnabled = false;
                EditInvoiceButton.IsEnabled = false;
                MenuSearch.IsEnabled = false;
                MenuEditItems.IsEnabled = false;


                //Force User to select a Date
                InvoiceDatePicker.IsEnabled = true;
                InvoiceDatePicker.SelectedDate = DateTime.Now;
                bInvoiceCreation = true;
                CancelInvoiceBtn.IsEnabled = true;

                //Empty DataGrid
                currentInvoice = new Shared.Invoice();
                currentInvoice.invoiceNum = -1;
                currentInvoice.totalCost = 0;
                MainItemDisplay.ItemsSource = currentInvoice.items;

                // Show TBD as the Invoice Num
                InvoiceNumLabel.Content = "Invoice Num: TBD";
                TotalCostLabel.Content = "Total Cost: __";

            }
            catch (Exception ex)
            {
                logic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Opens up Combo Box and allows addition and subtraction
        /// of items from the invoice.  Add Invoice and Edit Invoice
        /// Buttons are locked as well
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void EditInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Disable buttons that apply
                CreateInvoiceButton.IsEnabled = false;
                EditInvoiceButton.IsEnabled = false;
                MenuSearch.IsEnabled = false;
                MenuEditItems.IsEnabled = false;

                // Enable buttons that apply
                SaveInvoiceButton.IsEnabled = true;
                CancelInvoiceBtn.IsEnabled = true;
                bRemove = true;

                // Enable the Combo Box for user use
                InvoiceItemComboBox.IsEnabled = true;

                if(MainItemDisplay.SelectedIndex != -1)
                {
                    RemoveItemButton.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                logic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// For New Invoices, Creates the invoice in the database,
        /// and adds all the selected items to the LineItem grid.
        /// Disables all buttons that aren't needed at this time
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void SaveInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentInvoice.invoiceNum == -1)
                {
                    // Create Invoice in Database, and get invoice Number back
                    currentInvoice.invoiceNum = logic.SaveNewInvoice(currentInvoice);

                    // Update Items associated with Invoice in database
                    int count = 1;
                    foreach (Shared.Item item in lItemsToAdd)
                    {
                        logic.AddItemToInvoice(currentInvoice.invoiceNum, count, item.itemCode);
                        count++;
                    }
                    lItemsToAdd.Clear();
                }
                else
                {
                    int lineItemNum;
                    // Remove Items from the database
                    foreach(Shared.Item item in lItemsToRemove)
                    {
                        bool checker = false;
                        lineItemNum = 0;
                        foreach (Shared.Item invoiceItem in currentInvoice.items)
                        {
                            if(checker != true)
                            {
                                if(invoiceItem == item)
                                {
                                    checker = true;
                                    lineItemNum = invoiceItem.lineItemNum;
                                }
                            }
                        }
                        logic.RemoveLineItem(currentInvoice.invoiceNum, lineItemNum);
                    }
                    lItemsToRemove.Clear();

                    // Add Items to the Database
                    lineItemNum = Int32.Parse(logic.GetMaxLineItem(currentInvoice.invoiceNum).ToString()) + 1;
                    foreach(Shared.Item item in lItemsToAdd)
                    {
                        logic.AddItemToInvoice(currentInvoice.invoiceNum, lineItemNum, item.itemCode);
                        lineItemNum++;
                    }
                    lItemsToAdd.Clear();

                    // Update Invoice Cost
                    logic.UpdateInvoiceCost(currentInvoice);
                }

                // Bools = false
                bInvoiceCreation = false;
                bRemove = false;

                // Disable Information Changing Buttons
                AddItemButton.IsEnabled = false;
                RemoveItemButton.IsEnabled = false;
                SaveInvoiceButton.IsEnabled = false;
                InvoiceItemComboBox.IsEnabled = false;
                MainItemDisplay.IsEnabled = false;
                CancelInvoiceBtn.IsEnabled = false;

                //Enable Edit option, Create option and Menu buttons
                EditInvoiceButton.IsEnabled = true;
                CreateInvoiceButton.IsEnabled = true;
                MenuEditItems.IsEnabled = true;
                MenuSearch.IsEnabled = true;

                InvoiceItemComboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                logic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

        #region Adding/Removing Items

        /// <summary>
        /// Event Listener for when the date is changed by the user
        /// Creates a new invoice object and saves the selected date
        /// Also enables use of buttons for invoice creation
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void DateChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (bInvoiceCreation == true)
                {
                    // Enable Date picker
                    InvoiceDatePicker.IsEnabled = false;

                    // Create a new invoice, save date value
                    currentInvoice = new Shared.Invoice();
                    currentInvoice.invoiceDate = InvoiceDatePicker.SelectedDate.Value.Date.ToString();

                    // Enable the Combo Box and Save Button for user use
                    InvoiceItemComboBox.IsEnabled = true;
                    SaveInvoiceButton.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                logic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }

        /// <summary>
        /// Listener for if an item is selected in the Data Grid
        /// Enables the Remove Item Button
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void DataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (bRemove == true && MainItemDisplay.SelectedIndex != -1)
                {
                    // Enable Remove Item Button
                    RemoveItemButton.IsEnabled = true;
                }
                else
                {
                    RemoveItemButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                logic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Listener for if a selection is changed in the combo box
        /// Enables the Add Item Button
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void ItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (InvoiceItemComboBox.SelectedItem != null)
                {
                    // Enable Add Item button
                    AddItemButton.IsEnabled = true;
                    ItemCostLabel.Content = "Item Cost: $" + ((Shared.Item)InvoiceItemComboBox.SelectedItem).itemCost;
                }
                else
                {
                    ItemCostLabel.Content = "Item Cost: $";
                }
            }
            catch (Exception ex)
            {

                logic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds an item to the invoice
        /// Depending on whether the invoice already exists or not,
        /// the changes are made.
        /// Corresponding labels are updated as well
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InvoiceItemComboBox.SelectedIndex >= 0)
                {
                    //Object to hold item selected
                    Shared.Item tempItem = (Shared.Item)InvoiceItemComboBox.SelectedItem;

                    // Update cost Variable and UI Label
                    currentInvoice.totalCost += tempItem.itemCost;
                    TotalCostLabel.Content = "Total Cost: $" + currentInvoice.totalCost;

                    // Add item to the temporary lists
                    currentInvoice.items.Add(tempItem);
                    lItemsToAdd.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                logic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Removes an item from the invoice
        /// Depending on whether the invoice already exists or not,
        /// the change is made.
        /// Labels updated accordingly
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainItemDisplay.SelectedIndex >= 0)
                {
                    //Object to hold item selected
                    Shared.Item tempItem = (Shared.Item)MainItemDisplay.SelectedItem;
                    currentInvoice.items.Remove(tempItem);

                    // Update cost variable and Label
                    currentInvoice.totalCost -= tempItem.itemCost;
                    TotalCostLabel.Content = "Total Cost: $" + currentInvoice.totalCost;

                    if (lItemsToAdd.Contains(tempItem))
                    {
                        lItemsToAdd.Remove(tempItem);
                    }
                    else
                    {
                        lItemsToRemove.Add(tempItem);
                    }

                    RemoveItemButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {

                logic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        private void CancelInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(currentInvoice.invoiceNum == -1)
                {
                    // Reset currentInvoice variables
                    currentInvoice.invoiceNum = 0;
                    currentInvoice.invoiceDate = "";
                    currentInvoice.totalCost = 0;
                    currentInvoice.items.Clear();

                    TotalCostLabel.Content = "Total Cost: $";

                    // Disable Edit Invoice Button
                    EditInvoiceButton.IsEnabled = false;
                }
                else
                {
                    // Reload original invoice from database
                    currentInvoice = logic.GetInvoice(currentInvoice.invoiceNum);

                    MainItemDisplay.ItemsSource = currentInvoice.items;

                    TotalCostLabel.Content = "Total Cost: $" + currentInvoice.totalCost;

                    EditInvoiceButton.IsEnabled = true;
                }

                lItemsToAdd.Clear();
                lItemsToRemove.Clear();

                CreateInvoiceButton.IsEnabled = true;
                MenuSearch.IsEnabled = true;
                MenuEditItems.IsEnabled = true;

                AddItemButton.IsEnabled = false;
                RemoveItemButton.IsEnabled = false;
                SaveInvoiceButton.IsEnabled = false;
                CancelInvoiceBtn.IsEnabled = false;

                InvoiceItemComboBox.SelectedIndex = -1;
                ItemCostLabel.Content = "Item Cost: $";
            }
            catch (Exception ex)
            {

                logic.HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

        #endregion


        #region Screen Functions

        /// <summary>
        /// Empties and fills the General Item List
        /// It then casts it onto the ItemComboBox
        /// </summary>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public void fillItemCB()
        {
            try
            {
                lItemList = logic.ItemList();
                InvoiceItemComboBox.ItemsSource = lItemList;
                InvoiceItemComboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates Invoice Num, Date and Total Cost
        /// Labels in the main screen
        /// </summary>
        /// <param name="currentInvoice">Shared.Invoice currentInvoice</param>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public void updateInvoiceLabels(Shared.Invoice currentInvoice)
        {
            try
            {
                //Update Labels
                InvoiceNumLabel.Content = "Invoice: " + currentInvoice.invoiceNum;
                InvoiceDatePicker.SelectedDate = DateTime.Parse(currentInvoice.invoiceDate);
                TotalCostLabel.Content = "Total Cost: $" + currentInvoice.totalCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Displays the selected invoice on the screen
        /// </summary>
        /// <param name="itemList">List of items in the invoice</param>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public void fillDataGrid(ObservableCollection<Shared.Item> itemList)
        {
            try
            {
                MainItemDisplay.ItemsSource = itemList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

    }
}
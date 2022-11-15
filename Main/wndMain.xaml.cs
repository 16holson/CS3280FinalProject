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
        /// string to hold the selected invoice num
        /// </summary>
        public string selectedInvoiceNum;

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

        List<Shared.Item> lInvoiceItems;

        /// <summary>
        /// Object to perform logic class operations
        /// </summary>
        clsMainLogic logic;

        /// <summary>
        /// Invoice object to store information for the current invoice
        /// </summary>
        Shared.Invoice currentInvoice;

        /// <summary>
        /// Integer to hold the total cost for an invoice that
        /// is being created
        /// </summary>
        int totalcost;
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
        }


        #endregion


        #region UI Functions

        #region Menu Buttons

        /// <summary>
        /// (Braxton Wright, created in order to let his portion be completed)
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
                        selectedInvoiceNum = Search.selectedNum;
                        updateScreen(selectedInvoiceNum);

                        //Allow the option to edit the invoice
                        EditInvoiceButton.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// (Braxton Wright, Created to allow the window to open when button is clicked)
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
                              //open the edit items window and pause here in the code until the window is closed (the stuff before the "EditItems.ShowDialog()" catches the dialogresult the window returns to determine if any items have been modified)
                EditItems.ShowDialog();
                this.Show();  //shows this window to the user

                bItemsChanged = EditItems.HasItemsChanged;

                //see if there has been any changes to the items inside the DB so you can see if you are required to update this window's list of items
                if (bItemsChanged == true)
                {
                    //Refill Combo Box
                    fillItemCB();
                    if (selectedInvoiceNum != null)
                    {
                        updateScreen(selectedInvoiceNum);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                // Show TBD as the Invoice Num
                InvoiceNumLabel.Content = "Invoice Num: TBD";
                selectedInvoiceNum = "-1";
                TotalCostLabel.Content = "Total Cost: __";

                //Empty DataGrid
                MainItemDisplay.ItemsSource = null;
                lInvoiceItems = new List<Shared.Item>();
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Opens up Combo Box and allows addition and subtraction
        /// of items from the invoice.  Add Invoice and Edit Invoice
        /// Buttons are locked as well
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                bRemove = true;

                // Enable the Combo Box for user use
                InvoiceItemComboBox.IsEnabled = true;
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// For New Invoices, Creates the invoice in the database,
        /// and adds all the selected items to the LineItem grid.
        /// Disables all buttons that aren't needed at this time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedInvoiceNum == "-1")
                {
                    //Save total cost to object
                    currentInvoice.totalCost = totalcost.ToString();

                    // Create Invoice in Database, and get invoice Number back
                    selectedInvoiceNum = logic.SaveNewInvoice(currentInvoice);

                    //Save current Invoice Number in object
                    currentInvoice.invoiceNum = selectedInvoiceNum;

                    // Update Items associated with Invoice in database
                    int count = 1;
                    foreach (Shared.Item item in lInvoiceItems)
                    {
                        logic.AddItemToInvoice(selectedInvoiceNum, count.ToString(), item.itemCode);
                        count++;
                    }
                }

                // Bools = false
                bInvoiceCreation = false;
                bRemove = false;

                // Disable Information Changing Buttons
                AddItemButton.IsEnabled = false;
                RemoveItemButton.IsEnabled = false;
                SaveInvoiceButton.IsEnabled = false;
                InvoiceItemComboBox.IsEnabled = false;

                //Enable Edit option, Create option and Menu buttons
                EditInvoiceButton.IsEnabled = true;
                CreateInvoiceButton.IsEnabled = true;
                MenuEditItems.IsEnabled = true;
                MenuSearch.IsEnabled = true;
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

        #region Adding/Removing Items

        /// <summary>
        /// Event Listener for when the date is changed by the user
        /// Creates a new invoice object and saves the selected date
        /// Also enables use of buttons for invoice creation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }

        /// <summary>
        /// Listener for if an item is selected in the Data Grid
        /// Enables the Remove Item Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Enable Remove Item Button
                RemoveItemButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Listener for if a selection is changed in the combo box
        /// Enables the Add Item Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Enable Add Item button
                AddItemButton.IsEnabled = true;
            }
            catch (Exception ex)
            {

                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds an item to the invoice
        /// Depending on whether the invoice already exists or not,
        /// the changes are made.
        /// Corresponding labels are updated as well
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InvoiceItemComboBox.SelectedIndex >= 0)
                {
                    //Object to hold item selected
                    Shared.Item tempItem = (Shared.Item)InvoiceItemComboBox.SelectedItem;

                    // Add item to the temporary list
                    lInvoiceItems.Add(tempItem);

                    // If the invoice still isn't in the database
                    if (selectedInvoiceNum == "-1")
                    {
                        // Cast list to Datagrid
                        updateDataGrid(lInvoiceItems);
                        totalcost = 0;
                        //Update Cost on Main Menu
                        foreach (Shared.Item item in lInvoiceItems)
                        {
                            totalcost += Int32.Parse(item.itemCost);
                        }
                        TotalCostLabel.Content = "Total Cost: $" + totalcost.ToString();
                    }
                    // If invoice already exists
                    else
                    {
                        int count = Int32.Parse(logic.GetNumLineItems(selectedInvoiceNum));
                        count++;

                        // Add item to the database record
                        logic.AddItemToInvoice(selectedInvoiceNum, count.ToString(), tempItem.itemCode);

                        // Update Cost in the Database Record
                        currentInvoice = logic.GetInvoice(selectedInvoiceNum);
                        currentInvoice.totalCost = (Int32.Parse(currentInvoice.totalCost) + Int32.Parse(tempItem.itemCost)).ToString();
                        logic.UpdateInvoiceCost(currentInvoice);
                    }

                    AddItemButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Removes an item from the invoice
        /// Depending on whether the invoice already exists or not,
        /// the change is made.
        /// Labels updated accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainItemDisplay.SelectedIndex >= 0)
                {
                    //Object to hold item selected
                    Shared.Item tempItem = (Shared.Item)MainItemDisplay.SelectedItem;

                    lInvoiceItems.Remove(tempItem);

                    if (selectedInvoiceNum == "-1")
                    {
                        updateDataGrid(lInvoiceItems);
                        totalcost = 0;
                        foreach (Shared.Item item in lInvoiceItems)
                        {
                            totalcost += Int32.Parse(item.itemCost);
                        }
                        TotalCostLabel.Content = "Total Cost: $" + totalcost.ToString();
                    }
                    else
                    {
                        // Remove Item from line Items
                        logic.RemoveLineItem(selectedInvoiceNum, tempItem.itemCode);

                        // Find correct cost for invoice
                        currentInvoice.totalCost = (Int32.Parse(currentInvoice.totalCost) - Int32.Parse(tempItem.itemCost)).ToString();

                        // Update Cost in Database
                        logic.UpdateInvoiceCost(currentInvoice);
                        currentInvoice = logic.GetInvoice(selectedInvoiceNum);
                        TotalCostLabel.Content = "Total Cost: " + currentInvoice.totalCost;

                        // Update Datagrid
                        lInvoiceItems = logic.GetInvoiceItems(currentInvoice.invoiceNum);
                        updateDataGrid(lInvoiceItems);
                    }
                    RemoveItemButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {

                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        #endregion

        #endregion


        #region Screen Functions

        /// <summary>
        /// Empties and fills the General Item List
        /// It then casts it onto the ItemComboBox
        /// </summary>
        public void fillItemCB()
        {
            try
            {
                if (lItemList != null)
                {
                    lItemList.Clear();
                }
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
        public void updateInvoiceLabels(Shared.Invoice currentInvoice)
        {
            try
            {
                //Update Labels
                InvoiceNumLabel.Content = "Invoice: " + selectedInvoiceNum;
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
        public void updateDataGrid(List<Shared.Item> itemList)
        {
            try
            {
                MainItemDisplay.ItemsSource = null;
                MainItemDisplay.ItemsSource = itemList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Saves a copy of the current invoice. then it updates the
        /// screen by callilng updateInvoiceLabels() and updateDataGrid()
        /// </summary>
        /// <param name="sInvoiceNum">string currentInvoiceNum</param>
        public void updateScreen(string sInvoiceNum)
        {
            try
            {
                //Update the Datagrid with the invoice selected
                currentInvoice = logic.GetInvoice(selectedInvoiceNum);
                lInvoiceItems = logic.GetInvoiceItems(selectedInvoiceNum);

                //Update Screen
                updateInvoiceLabels(currentInvoice);
                updateDataGrid(lInvoiceItems);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        #endregion


        #region Error Handling
        /// <summary>
        /// Function to handle error messages
        /// </summary>
        /// <param name="ErrorMessage">Error Message</param>
        public void HandleException(string ErrorMessage)
        {
            try
            {
                MessageBox.Show(ErrorMessage.Substring(ErrorMessage.LastIndexOf("-> ")));
            }
            catch (Exception ex)
            {
                string SavePath = System.AppDomain.CurrentDomain.BaseDirectory + "Error.txt";

                System.IO.File.AppendAllText(SavePath, Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        #endregion
    }
}
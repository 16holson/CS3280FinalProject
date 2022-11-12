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

        public bool bItemsChanged;

        List<Shared.Item> lItemList;

        clsMainLogic logic;
        #endregion

        #region Functions
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

        //Created by Braxton Wright
        /// <summary>
        /// This event listener listens for when the user clicks the Search item inside the menu.  It will then take them to the Search window.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void MenuSearch_Click(object sender, RoutedEventArgs e)
        {
            wndSearch Search = new wndSearch();
            Search.Owner = this;  //this sets it so that the search window owner is this window (so it loads where this window is currently)

            this.Hide();  //hide this from the user
            Search.ShowDialog();  //open the search window and pause here in the code until the window is closed
            this.Show();  //shows this window to the user

            // This is how the user gets the chosen invoice num from the user
            selectedInvoiceNum = Search.selectedNum;

            InvoiceNumLabel.Content = "Invoice: " + selectedInvoiceNum;
        }

        //Created by Braxton Wright
        /// <summary>
        /// This event listener listens for when the user clicks the Edit Items item inside the menu.  It will then take them to the Items window.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void MenuEditItems_Click(object sender, RoutedEventArgs e)
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
                //perform an update/reassign to your list
            }
        }


        private void CreateInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Disable and Enable buttons that apply
                CreateInvoiceButton.IsEnabled = false;
                EditInvoiceButton.IsEnabled = false;
                AddItemButton.IsEnabled = true;
                SaveInvoiceButton.IsEnabled = true;

                // Show TBD as the Invoice Num
                InvoiceNumLabel.Content = "Invoice #: TBD";

                // Enable the Combo Box for user use
                InvoiceItemComboBox.IsEnabled = true;


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void SaveInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /// Pulls information from Window
                /// 
                /// Queries Database for New Invoice Number
                /// (Find biggest invoice Num, add 1)
                /// 
                /// Create New Invoice with invoiceNum, Date and TotalCost
                /// (CreateInvoice)
                /// 
                /// Add Items to Invoice using ItemCode and InvoiceNum
                /// AddToInvoice
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void EditInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Disable and Enable buttons that apply
                CreateInvoiceButton.IsEnabled = false;
                EditInvoiceButton.IsEnabled = false;
                AddItemButton.IsEnabled = true;
                SaveInvoiceButton.IsEnabled = true;

                // Enable the Combo Box for user use
                InvoiceItemComboBox.IsEnabled = true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion


        #region Screen Functions

        public void fillItemCB()
        {
            lItemList = logic.ItemList();
            InvoiceItemComboBox.ItemsSource = lItemList;
        }

        #endregion



    }
}
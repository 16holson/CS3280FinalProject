
﻿using CS3280FinalProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
﻿/*
 * (Insert your name here)
 * CS 3280
 * Final Project prototype Window wndSearch
 * Shawn Cowder
 * Due: November 19, 2022 at 11:59 PM
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the Search window.
 * -----------------------------------------------------------------------------------------------------------
 */

using System.Windows;
using System.Windows.Controls;

namespace CS3280FinalProject.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml 
    /// Make sure to create a new instance of this window when showing it
    /// </summary>
    public partial class wndSearch : Window
    {
        #region Class Variables
        /// <summary>
        /// Holds the value of the selected invoice
        /// </summary>
        public int selectedNum { get; set; }
        /// <summary>
        /// Logic used for this window
        /// </summary>
        private clsSearchLogic logic;

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();
                selectedNum = -1;
                logic = new clsSearchLogic();
                populateWindow();
            }
            catch(Exception ex)
            {
                 logic.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            
        }
        #endregion

        #region UI Control Methods
        /// <summary>
        /// Sets the selectedNum for the main window to use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                if(invoicesDataGrid.SelectedValue != null)
                {
                    selectInvoiceLbl.Visibility = Visibility.Hidden;
                    selectedNum = ((Invoice)(invoicesDataGrid.SelectedValue)).invoiceNum;
                    //Go back to the main window
                    this.Hide();
                }
                else
                {
                    selectInvoiceLbl.Visibility = Visibility.Visible;
                }
            }
            catch(Exception ex)
            {
                 logic.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Changes the filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            { 
                //Sends the selected value or "" if there isn't a selected value
                invoicesDataGrid.ItemsSource = logic.filterList(numberCB.SelectedValue?.ToString() ?? "", dateCB.SelectedValue?.ToString() ?? "", totalChargeCB.SelectedValue?.ToString() ?? "");
            }
            catch(Exception ex)
            {
                 logic.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Clears the selected values and resets the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                populateWindow();
            }
            catch(Exception ex)
            {
                 logic.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Populates the ComboBoxes and DataGrid with values from the database
        /// Call this function before showing this window
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void populateWindow()
        {
            try
            { 
                //Populate the datagrid with the invoices
                invoicesDataGrid.ItemsSource = logic.invoices;
                invoicesDataGrid.SelectedIndex = -1;
                numberCB.ItemsSource = logic.getInvoiceNumList();
                numberCB.SelectedIndex = -1;
                dateCB.ItemsSource = logic.getInvoiceDateList();
                dateCB.SelectedIndex = -1;
                totalChargeCB.ItemsSource = logic.getInvoiceTotalList();
                totalChargeCB.SelectedIndex = -1;
                selectInvoiceLbl.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion
    }
}

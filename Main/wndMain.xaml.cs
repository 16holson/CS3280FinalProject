/*
 * (Insert your name here)
 * CS 3280
 * Final Project prototype Window wndMain
 * Shawn Cowder
 * Due: November 19, 2022 at 11:59 PM
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the event listeners for the Main window.
 * -----------------------------------------------------------------------------------------------------------
 */

using CS3280FinalProject.Items;
using CS3280FinalProject.Search;
using System.Windows;

namespace CS3280FinalProject.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        #region Variables
        /// <summary>
        /// Stores an instance of the wndSearch window.
        /// </summary>
        wndSearch Search;

        /// <summary>
        /// Stores an instance of the wndItems window.
        /// </summary>
        wndItems EditItems;
        #endregion


        #region Functions
        public wndMain()
        {
            InitializeComponent();
            Search = new wndSearch();  //make a new instance of the search window
            EditItems = new wndItems();  //make a new instance of the edit items window
        }
        private void MenuSearch_Click(object sender, RoutedEventArgs e)
        {
            Search.Owner = this;  //this sets it so that the search window owner is this window (so it loads where this window is currently)

            this.Hide();  //hide this from the user
            Search.ShowDialog();  //open the search window and pause here in the code until the window is closed
            this.Show();  //shows this window to the user
        }

        private void MenuEditItems_Click(object sender, RoutedEventArgs e)
        {
            EditItems.Owner = this;  //this sets it so that the edit items window owner is this window (so it loads where this window is currently)

            this.Hide();  //hide this from the user
            EditItems.ShowDialog();  //open the edit items window and pause here in the code until the window is closed
            this.Show();  //shows this window to the user
        }
        #endregion
    }
}

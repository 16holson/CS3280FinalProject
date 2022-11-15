/*
 * Braxton Wright, Hunter Olson, and Levi Bernards
 * CS 3280
 * Final Project class Item
 * Shawn Cowder
 * Due: December 10, 2022 at 11:59 PM
 * Version: 1.0
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the variables and functions that are required make an item.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.ComponentModel;
using System.Reflection;

namespace CS3280FinalProject.Shared
{
    public class Item : INotifyPropertyChanged
    {
        #region Variables
        /// <summary>
        /// Private item code
        /// </summary>
        private string sItemCode;

        /// <summary>
        /// Private items description
        /// </summary>
        private string sItemDesc;

        /// <summary>
        /// Private items cost
        /// </summary>
        private string sItemCost;
        #endregion

        #region Properties
        /// <summary>
        /// Public item code
        /// </summary>
        public string itemCode
        {
            get
            {
                return sItemCode;
            }
            set
            {
                sItemCode = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("itemCode"));
            }
        }

        /// <summary>
        /// Public item description
        /// </summary>
        public string itemDesc
        {
            get
            {
                return sItemDesc;
            }

            set
            {
                sItemDesc = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("itemDesc"));
            }
        }

        /// <summary>
        /// Public item cost
        /// </summary>
        public string itemCost
        {
            get
            {
                return sItemCost;
            }
            set
            {
                sItemCost = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("itemCost"));
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Item()
        {

        }

        /// <summary>
        /// Overloaded constructor to make a new item.
        /// </summary>
        /// <param name="itemCode">The item's code.</param>
        /// <param name="itemDesc">The item's description.</param>
        /// <param name="itemCost">The item's cost.</param>
        /// <exception cref="Exception"></exception>
        public Item(string itemCode, string itemDesc, string itemCost)
        {
            try
            {
                this.itemCode = itemCode;
                this.itemDesc = itemDesc;
                this.itemCost = itemCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// This is the contract we have to make with the compiler because we are implementing the interface "INotifyPropertyChanged".  So we must have this event defined.  We will raise this event anytime one of our properties changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
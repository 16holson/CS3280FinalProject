/*
 * Braxton Wright, Hunter Olson, and Levi Bernards
 * CS 3280
 * Final Project prototype class clsItems
 * Shawn Cowder
 * Due: November 19, 2022 at 11:59 PM
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the variables and functions that are required to make an instance of a item from the
 * DB.  It contains the necessary constructors, variable, properties, and event to allow the user to
 * manipulate a copy of the data from the DB instead of constantly querying it to get data.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.ComponentModel;
using System.Reflection;

namespace CS3280FinalProject.Shared
{
    public class clsItem : INotifyPropertyChanged
    {
        #region Class Variables
        //Private variables
        /// <summary>
        /// Items code.
        /// </summary>
        private string sitemCode;

        /// <summary>
        /// Items description.
        /// </summary>
        private string sitemDesc;

        /// <summary>
        /// Items cost.
        /// </summary>
        private float sitemCost;

        //Properties for the event PropertyChanged.  This tells the DataGrid that is attached to the data that the data has changed.
        public string itemCode
        {
            get { return sitemCode; }
            set
            {
                sitemCode = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("itemCode"));
            }
        }

        public string itemDesc
        {
            get { return sitemDesc; }
            set
            {
                sitemDesc = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("itemDesc"));
            }
        }

        public float itemCost
        {
            get { return sitemCost; }
            set
            {
                sitemCost = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("itemCost"));
            }
        }
        #endregion


        #region Constructors and PropertyChangedEventHandler Event
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public clsItem()
        {
            //do nothing
        }

        /// <summary>
        /// Overloaded constructor to make a new item.
        /// </summary>
        /// <param name="itemCode">The item's code.</param>
        /// <param name="itemDesc">The item's description.</param>
        /// <param name="itemCost">The item's cost.</param>
        /// <exception cref="Exception"></exception>
        public clsItem(string itemCode, string itemDesc, float itemCost)
        {
            try
            {
                this.sitemCode = itemCode;
                this.sitemDesc = itemDesc;
                this.sitemCost = itemCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This is the contract we have to make with the compiler because we are implementing the interface "INotifyPropertyChanged".  So we must have this event defined.  We will raise this event anytime one of our properties changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}

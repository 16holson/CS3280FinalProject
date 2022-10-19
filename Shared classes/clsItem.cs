using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        //properties for the event PropertyChanged.  This tells the DataGrid that data has changed.
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

        #region functions
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
        /// This is the contract you make with the compiler because we are implementing the interface so
        /// we must have this event defined.  We will raise this event anytime one of our properties changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}

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
    public class Item : INotifyPropertyChanged
    {
        #region Class Variables
        /// <summary>
        /// Items code
        /// </summary>
        private string sItemCode;
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
        /// Items description
        /// </summary>
        private string sItemDesc;
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
        /// Items cost
        /// </summary>
        private string sItemCost;
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

        #region PropertyChanged Stuff

        /// <summary>
        /// This is the contract we have to make with the compiler because we are implementing the interface "INotifyPropertyChanged".  So we must have this event defined.  We will raise this event anytime one of our properties changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}

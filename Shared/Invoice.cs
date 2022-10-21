using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CS3280FinalProject.Shared
{
    public class Invoice
    {
        #region Class Variables
        /// <summary>
        /// Invoices Number
        /// </summary>
        public string invoiceNum { get; set; }
        /// <summary>
        /// Invoices Date
        /// </summary>
        public string invoiceDate { get; set;}
        /// <summary>
        /// Invoices Total Cost
        /// </summary>
        public string totalCost { get; set; }
        /// <summary>
        /// List of items in the invoice
        /// </summary>
        public List<Item> items { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Invoice()
        {
            try
            {
                items = new List<Item>();
            }
            catch(Exception ex)
            {
                 HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
        #endregion
    }
}

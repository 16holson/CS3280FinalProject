using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CS3280FinalProject.Shared;

namespace CS3280FinalProject.Search
{
    public class clsSearchLogic
    {
        #region Class Variables
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string sConnectionString;
        /// <summary>
        /// Holds the list of all invoices and their items
        /// </summary>
        public List<Shared.Invoice> invoices;
        private clsSearchSQL sql;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor that sets the connection string to the database
        /// </summary>
		public clsSearchLogic()
        {
            try
            {
                sConnectionString = sConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Directory.GetCurrentDirectory() + "\\Invoice.accdb";
                invoices = new List<Shared.Invoice>();
                sql = new clsSearchSQL();
                populateInvoices();
            }
            catch(Exception ex)
            {
                 HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a list of distinct invoiceNums
        /// </summary>
        /// <returns></returns>
        public List<String> getInvoiceNumList()
        {
            try
            { 
                return  (from invoice in invoices
                    select invoice.invoiceNum).Distinct()
                                              .OrderBy(i => Int32.Parse(i))
                                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns a list of distinct invoiceDates
        /// </summary>
        /// <returns></returns>
        public List<String> getInvoiceDateList()
        {
            try
            { 
                return  (from invoice in invoices
                    select invoice.invoiceDate).Distinct()
                                               .OrderBy(i => DateTime.Parse(i))
                                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns a list of distinct invoiceTotals
        /// </summary>
        /// <returns></returns>
        public List<String> getInvoiceTotalList()
        {
            try
            { 
                return  (from invoice in invoices
                         orderby Int32.Parse(invoice.totalCost)
                         select invoice.totalCost).Distinct()
                                                  .OrderBy(i => Int32.Parse(i))
                                                  .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Sorts invoices based on given parameters
        /// </summary>
        /// <param name="num">invoice number</param>
        /// <param name="date">invoice date</param>
        /// <param name="total">total cost</param>
        /// <returns></returns>
        public List<Invoice> filterList(string num, string date, string total)
        {
            try
            {
                var filteredList = invoices;
                if(num != "")
                {
                    filteredList = filteredList.Where(i => i.invoiceNum == num).ToList();
                }
                if(date != "")
                {
                    filteredList = filteredList.Where(i => i.invoiceDate == date).ToList();
                }
                if(total != "")
                {
                    filteredList = filteredList.Where(i => i.totalCost == total).ToList();
                }
                return filteredList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Helper Methods

        private void populateInvoices()
        {
            try
            {
                //Populate invoices
                DataSet invoicesDS = new DataSet();
                int iRef = 0;
                invoicesDS = ExecuteSQLStatement(sql.getInvoices(), ref iRef);
                foreach(DataRow dr in invoicesDS.Tables[0].Rows)
                {
                    Shared.Invoice invoice = new Shared.Invoice();
                    invoice.invoiceNum = dr[0].ToString();
                    invoice.invoiceDate = dr[1].ToString();
                    //Get the date not the time
                    invoice.invoiceDate = invoice.invoiceDate.Substring(0, invoice.invoiceDate.IndexOf(" "));
                    invoice.totalCost = dr[2].ToString();
                    invoices.Add(invoice);
                }
                //Populate each invoice in invoices with their items
                DataSet itemsDS = new DataSet();
                foreach(Invoice invoice in invoices)
                {
                    itemsDS = ExecuteSQLStatement(sql.getItems(invoice.invoiceNum), ref iRef);
                    foreach(DataRow dr in itemsDS.Tables[0].Rows)
                    {
                        Shared.Item item = new Shared.Item();
                        item.itemCode = dr[0].ToString();
                        item.itemDesc = dr[1].ToString();
                        item.itemCost = dr[2].ToString();
                        invoice.items.Add(item);
                    }
                }
                

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statement that is passed in and executes it.  The resulting values
        /// are returned in a DataSet.  The number of rows returned from the query will be put into
        /// the reference parameter iRetVal.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <param name="iRetVal">Reference parameter that returns the number of selected rows.</param>
        /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
		private DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal)
        {
            try
            {
                //Create a new DataSet
                DataSet ds = new DataSet();

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Fill up the DataSet with data
                        adapter.Fill(ds);
                    }
                }

                //Set the number of values returned
                iRetVal = ds.Tables[0].Rows.Count;

                //return the DataSet
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statement that is passed in and executes it.  The resulting single 
        /// value is returned.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns a string from the scalar SQL statement.</returns>
		private string ExecuteScalarSQL(string sSQL)
        {
            try
            {
                //Holds the return value
                object obj;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Execute the scalar SQL statement
                        obj = adapter.SelectCommand.ExecuteScalar();
                    }
                }

                //See if the object is null
                if (obj == null)
                {
                    //Return a blank
                    return "";
                }
                else
                {
                    //Return the value
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statement that is a non query and executes it.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns the number of rows affected by the SQL statement.</returns>
        private int ExecuteNonQuery(string sSQL)
        {
            try
            {
                //Number of rows affected
                int iNumRows;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                    cmd.CommandTimeout = 0;

                    //Execute the non query SQL statement
                    iNumRows = cmd.ExecuteNonQuery();
                }

                //return the number of rows affected
                return iNumRows;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
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

using CS3280FinalProject.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CS3280FinalProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region variables
        //private variables
        /// <summary>
        /// Stores a reference to the ObservableCollection of items that are in the DB.
        /// </summary>
        private ObservableCollection<Item> items;

        private clsItemsLogic ItemsLogic;

        private bool bHasItemsBeenChanged;
        
        //properties
        public bool HasItemsBeenChanged { get; set; }

        #endregion


        #region functions
        /// <summary>
        /// Default constructor for the wndItems window
        /// </summary>
        public wndItems()
        {
            InitializeComponent();
            ItemsLogic = new clsItemsLogic();
            //this.items = ItemCollection;
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            //items.Add(new Item(ItemDescripton, ItemCost));

        }

        private void btnSaveItem_Click(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion
    }
}

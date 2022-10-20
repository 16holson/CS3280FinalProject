using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CS3280FinalProject.Items.Custom_user_controls
{
    /// <summary>
    /// Interaction logic for AddNewItem.xaml
    /// </summary>
    public partial class AddNewItem : UserControl
    {
        #region Delegates
        /// <summary>
        /// Delegate for the AddButton click event.
        /// </summary>
        public delegate void AddButtonClickDelegate();

        /// <summary>
        /// Delegate for the CancleButton click event.
        /// </summary>
        public delegate void CancelButtonClickDelegate();

        #endregion

        #region Events
        /// <summary>
        /// Create the event that is raised when the btnAdd button is clicked.
        /// </summary>
        public event AddButtonClickDelegate AddButtonClick;

        /// <summary>
        /// Create the event that is raised when the btnCancel button is clicked.
        /// </summary>
        public event CancelButtonClickDelegate CancelButtonClick;
        #endregion


        #region Functions
        /// <summary>
        /// Default constructor for this user control
        /// </summary>
        public AddNewItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This event listener listens for when the user clicks the Cancel button and delegates the event to the programmer's event handler.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Button ClickedButton = (Button)sender;

            if (CancelButtonClick != null)
            {
                CancelButtonClick();
            }
        }


        /// <summary>
        /// This event listener listens for when the user clicks the Add button and delegates the event to the programmer's event handler.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Button ClickedButton = (Button)sender;

            if (AddButtonClick != null)
            {
                AddButtonClick();
            }
        }
        #endregion
    }
}

#pragma checksum "..\..\..\..\Main\wndMain.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A7CE42E04905EC2AED63A554C6F3D8351F9D2C17"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CS3280FinalProject.Main;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CS3280FinalProject.Main {
    
    
    /// <summary>
    /// wndMain
    /// </summary>
    public partial class wndMain : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem MenuSearch;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem MenuEditItems;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateInvoiceButton;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditInvoiceButton;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label InvoiceNumLabel;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label InvoiceDateLabel;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker InvoiceDatePicker;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TotalCostLabel;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox InvoiceItemComboBox;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddItemButton;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemoveItemButton;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ItemCostLabel;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid MainItemDisplay;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SaveErrorLabel;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveInvoiceButton;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\Main\wndMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelInvoiceBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CS3280FinalProject;component/main/wndmain.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Main\wndMain.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MenuSearch = ((System.Windows.Controls.MenuItem)(target));
            
            #line 22 "..\..\..\..\Main\wndMain.xaml"
            this.MenuSearch.Click += new System.Windows.RoutedEventHandler(this.MenuSearch_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MenuEditItems = ((System.Windows.Controls.MenuItem)(target));
            
            #line 23 "..\..\..\..\Main\wndMain.xaml"
            this.MenuEditItems.Click += new System.Windows.RoutedEventHandler(this.MenuEditItems_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CreateInvoiceButton = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\..\Main\wndMain.xaml"
            this.CreateInvoiceButton.Click += new System.Windows.RoutedEventHandler(this.CreateInvoiceButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.EditInvoiceButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\..\Main\wndMain.xaml"
            this.EditInvoiceButton.Click += new System.Windows.RoutedEventHandler(this.EditInvoiceButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.InvoiceNumLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.InvoiceDateLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.InvoiceDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.TotalCostLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.InvoiceItemComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 60 "..\..\..\..\Main\wndMain.xaml"
            this.InvoiceItemComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ItemSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.AddItemButton = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\..\..\Main\wndMain.xaml"
            this.AddItemButton.Click += new System.Windows.RoutedEventHandler(this.AddItemButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.RemoveItemButton = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\Main\wndMain.xaml"
            this.RemoveItemButton.Click += new System.Windows.RoutedEventHandler(this.RemoveItemButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ItemCostLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.MainItemDisplay = ((System.Windows.Controls.DataGrid)(target));
            
            #line 79 "..\..\..\..\Main\wndMain.xaml"
            this.MainItemDisplay.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGridSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.SaveErrorLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 15:
            this.SaveInvoiceButton = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\..\Main\wndMain.xaml"
            this.SaveInvoiceButton.Click += new System.Windows.RoutedEventHandler(this.SaveInvoiceButton_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.CancelInvoiceBtn = ((System.Windows.Controls.Button)(target));
            
            #line 90 "..\..\..\..\Main\wndMain.xaml"
            this.CancelInvoiceBtn.Click += new System.Windows.RoutedEventHandler(this.CancelInvoiceBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "..\..\..\..\Search\wndSearch.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8F5B51D64449508D13E3B307B83BD04132A77D68"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CS3280FinalProject.Search;
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


namespace CS3280FinalProject.Search {
    
    
    /// <summary>
    /// wndSearch
    /// </summary>
    public partial class wndSearch : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 68 "..\..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox numberCB;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox dateCB;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox totalChargeCB;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid invoicesDataGrid;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button selectInvoice;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clearFilter;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label selectInvoiceLbl;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CS3280FinalProject;component/search/wndsearch.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Search\wndSearch.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.numberCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 68 "..\..\..\..\Search\wndSearch.xaml"
            this.numberCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.filter_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dateCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 72 "..\..\..\..\Search\wndSearch.xaml"
            this.dateCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.filter_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.totalChargeCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 76 "..\..\..\..\Search\wndSearch.xaml"
            this.totalChargeCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.filter_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.invoicesDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.selectInvoice = ((System.Windows.Controls.Button)(target));
            
            #line 110 "..\..\..\..\Search\wndSearch.xaml"
            this.selectInvoice.Click += new System.Windows.RoutedEventHandler(this.selectInvoice_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.clearFilter = ((System.Windows.Controls.Button)(target));
            
            #line 111 "..\..\..\..\Search\wndSearch.xaml"
            this.clearFilter.Click += new System.Windows.RoutedEventHandler(this.clearFilter_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.selectInvoiceLbl = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


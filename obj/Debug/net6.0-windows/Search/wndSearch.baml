   M S B A M L   `   `   `  ????M  ICS3280FinalProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:CS3280FinalProject.SearchCS3280FinalProject.Search  5   	   )    #CS3280FinalProject.Search.wndSearch   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:CS3280FinalProject.Search     5   	     ? Title$  	wndSearch??5   	   $	??450??5	   	   $	??800??6    ? WindowStartupLocation$ CenterOwner=?5
   	    ? 
ResizeMode$ CanMinimize=?5   	      windowStyle#??[  5   	    ? 	Resources 5        gradient ?? EndPoint ?? 
StartPoint ? Color ? Offset ?? 
TargetType  buttonStyle	 ?? BasedOn&   System.Windows.Controls.DataGrid  dataGridStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader  dataGridColumnHeaderStyle  
labelStyle
 ?? Property ?? Value 
 topLbl  
warningLbl  comboBoxStyle 	 
insideGrid??%?  &	       '?? ?     7  '  ?     7  '  ?     7  '?? ?     7  &	 L    7  &	 	    7  '?? ?    7  &		 ?    ?? 5   
   $	 0,1&?6/   $	 0,0&?6>   .??5      ? $
 #444??6   $	 0.0Q?6(   ? 5      $
 #888??6   $	 1.0Q?6(   5      ?? 5   
   " ??6   8	   6M   ?? 5   
   "  6   8	   6Z   ?? 5   
   "  6   8	   6r   ?? 5   
   " ??6   8	   .?5      ?? 
 ? ??$ 	10,10,0,0q?6'   5      ?? 5   
   " ??6   8	   .?5      ?? 
 ? ??$ 0q?6'   ?? 5      
 ? ??$ Center=?64   ?? 5      
 ? ??$	 Top=?62   ?? 5      
 ? ??$ 20(?6)   5       ?? 5!   
   " ??6#   8	   .?5"      ?? 
 ? ??$ Center=?64   ?? 5#      
 ? ??$	 Top=?62   ?? 5$      
 ? ??
 ?  ??6+   ?? 5%      
 ? y?$ Hidden=?6+   5&      ?? 5(   
   " ??6   8	   6Q   ?? 5*   
   " ?6#   .?5+      ?? 
 ? ??$
 Left=?64   ?? 5,      
 ? ??$	 Top=?62   ?? 5-      
 ? ??$ 10,0,0,0q?6'   5.      50      .??51      ?  ? ColumnDefinitions 52   
   ?? 53      $	??.5* ?6   ?? 54      $	??.5* ?6   ?? 55      $??2* ?6    ? RowDefinitions 57   
   ?? 58      $??1* ?6   ?? 59      $??1.75* ?6   ?? 5:      $??1.75* ?6   ?? 5;      $??1.75* ?6   ?? 5<      $??2.25* ?6   ?? 5=      $	??10* ?6   .+?5@   
   ?? $??Search Invoices??6   $??0??6*   $??0??67   $??3??6G   $??2??6[   #??[ 6l   ?? 5C   
   $??Invoice Number:??6   $??1??6*   $??0??67   ?? 5D   
   -   5        $??numberCB??5D   0   $??1??6   $??1??6    6@   ?? 5G   
   $??Invoice Date??6   $??2??6'   $??0??64   ?? 5H   
   -   5        $??dateCB??5H   0   $??2??6   $??1??6    6>   ?? 5K   
   $??Total Invoice Charge??6   $??3??6/   ?? 5L   
   -   5        $??totalChargeCB??5L   0   $??3??6   $??1??6    6E     5O   
   -   5        $??invoicesDataGrid??5O   0   $??2??6   $??1??6#   $??5??6H      Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5Q      ,  &System.Windows.Controls.DataGridColumn   Header$ Invoice Number??6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   ?? *
invoiceNum+  5R      $ Invoice Date??6%    6;   ?? *invoiceDate+  5S      $ 
Total Cost??6%    69   ?? *	totalCost+   RowDetailsTemplate 5V      ?? 5W      .R?5X      ? #??[	 6    5Y      ?? 5Z      ?? 5[       5]      ?? 5^      .+?5a        $??1??6#   ??63   ?? *items+ 5b        5c   "   $ 	Item Code??65    6H   ??  ?? Path$ itemCode?  5d   "   $ Item Description??65    6O   ?? $ itemDesc?  5e   "   $ 	Item Cost??65    6H   ?? $ itemCost? 5h      ?? 5i      .R?5j      ? #??[	 6    5k      ?? 5l      ?? 5m       5o      ?? 5p      .+?5s        $??1??6#   ??63   ?? *items+ 5t        5u   "   $ 	Item Code??65    6H   ?? $ itemCode?  5v   "   $ Item Description??65    6O   ?? $ itemDesc?  5w   "   $ 	Item Cost??65    6H   ?? $ itemCost?5y      5z      5{      5}      5~      5      5?      ?? 5?   
   -   5        $??selectInvoice??5?   *   $??Select Invoice??6   $??0??6?   $??4??6O   6\   ?? 5?   
   -   5        $??clearFilter??5?   )   $??Clear Filters??6   $??1??6<   $??4??6L   6Y   ?? 5?   
   -   5        $??selectInvoiceLbl??5?   2   $??Plese Select an Invoice??6   $??5??6J   #??[ 6W   5?      5?      
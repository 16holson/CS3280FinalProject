   M S B A M L   `   `   `  ????  CS3280FinalProject'    !CS3280FinalProject.Items.wndItems   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          ?Name$  wndManipulateItems??5   	    ? Title$ wndItems??5	   	   $
??auto??5
   	   $	??450??5   	   $	??600??5   	    ? WindowStartupLocation$ CenterOwner=?5   	    ? 
ResizeMode$ CanMinimize=?5   	      windowStyle#??[  5   	    ? 	Resources 5        ItemsButtons ?? 
TargetType  buttonStyle ?? BasedOn ?? Property ?? Value 
 Labels  
labelStyle  
TextInputs  textBoxStyle&   System.Windows.Controls.DataGrid  dataGridStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader  dataGridColumnHeaderStyle??%?  &	       7  &	 ?     7  &	 E    7  '  A    7  '  c    7  ?? 5   
   " ??6%   8   .?5      ??  ? ??$ Stretch=?64   ?? 5       ? ??$ 15(?6)   ?? 5       ? ??$ 10,10,10,10q?6'   5      ?? 5   
   " ??6   8   .?5      ??  ? ??$ 10,0,0,0q?6'   ?? 5       ? ??$ Center=?62   ?? 5       ? ??$ right=?64   5      ?? 5   
   " ??6#   8   .?5      ??  ? ??$ 15(?6)   ?? 5       ? ??$ Center=?62   ?? 5       ? ??$ Stretch=?64   ?? 5       ? ??$ 	10,0,10,0q?6'   ?? 5        ?@??TextWrapping$
 Wrap=?6-   5!      ?? 5"   
   "  6   8   6/   ?? 5#   
   "  6   8   6;   5$      .??5'      ? 	 ? ShowGridLines$	 False??6   
 ? ColumnDefinitions
 5)   
   ?? 5*      $??* ?6   ?? 5+      $
??auto ?6   .+?5/   
   ? $??0??6   $	 False??6    ? RowDefinitions 50      ?? 51      $
??auto ?6    ?? 52      $
??auto ?6    .+?56      ? $??0??6   $	 False??6#   $
??auto??69    57      ?? 58      $
??auto ?6$   ?? 59      $
??auto ?6$   ?? 5:      $
??auto ?6$   .+?5>      ? $??0??6   $	 False??6$   
 5?      ?? 5@      $
??auto ?6+   ?? 5A      $??* ?6+    5C      ?? 5D      $
??auto ?6(   ?? 5E      $
??auto ?6(   ?? 5F      $
??auto ?6(   ?? 5G      $
??auto ?6(   .+?5J      ?? -   5         ??Name$ lblCode??5J      $??0??6-   $??Code:??6:   #??[ 6J   ?? 5K      -   5         ??Name$ txtItemCode??5K      $?? ??63   $
??Left=?6;   $
??auto??6V   $??75??6c   $	??315??6q   #??[ 6?   $??1??6?    ?? 	MaxLength$ 4??6?   ?? 5M      -   5        $ lblCost??5M      $??1??6-   $??Cost:??6:   #??[ 6J   ?? 5N      -   5        $ txtItemCost??5N      $?? ??63   $
??Left=?6;   $
??auto??6V   $??75??6c   $	??315??6q   #??[ 6?   $??1??6?   $??1??6?   ?? 5P      -   5        $ lblDescription??5P      $??2??64   $??Description:??6A   $
??Left=?6X   #??[ 6s   ?? 5Q      -   5        $ txtItemDesc??5Q      #??[ 63   $??4??6W   $??2??6d   5R      ? 5U      $??2??6   $
??auto??6$   $	 False??62   
 5V      ?? 5W      $??* ?6+   ?? 5X      $??* ?6+   .+?5[      ?? -   5         ??Name$ 
btnAddItem??5[      $??0??61   $??2??6A   $??Add New Item??6U   #??[ 6?   ?? 5]      -	   5        $ 	btnCancel??5]      $??0??60   $??Cancel??6@   #??[ 6i   $y?	Collapsed=?6?   ?? 5^      -
   5        $ btnSaveChanges??5^      $??1??65   $??Save Changes??6E   #??[ 6y   $y?	Collapsed=?6?   5_      5`      ? 5c      $??1??6   $	 False??6     5d      ?? 5e      $
??auto ?6$   ?? 5f      $
??auto ?6$   .+?5i      ?? -   5        $ btnEditItem??5i      $??0??6.   $??	Edit item??6;   ${?False??6i   #??[ 6{   ?? 5j      -   5        $ btnDeleteItem??5j      $??1??60   $??Delete item??6=   ${?False??6o   #??[ 6?   5k      5l        5o   
   -   5          Name$ datagridItems??5o      $??1??6*      AutoGenerateColumns$ False??6:   $??Center=?6V      CanUserResizeColumns$ False??6s      CanUserAddRows$ False??6?      CanUserDeleteRows$ False??6?      CanUserResizeRows$ False??6?      
IsReadOnly$
 True??6?   $??0,0,0,0q?5p   E      Columns 5q      0  *System.Windows.Controls.DataGridTextColumn  5r      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Item Code??6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   ?? *
itemCode+  5s      $ 	Item Cost??6%    68   ?? *
itemCost+  5t      $ Item Description??6%    6?   ?? *
itemDesc+5v      5w      5x      
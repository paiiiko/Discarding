   M S B A M L   `   `   `  ????E  ADiscarding, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Discarding
Discarding  5   	       Discarding.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Discarding     5   	     ? Title$  
MainWindow??5   	   $	??450??6   $	??800??6)   
???????.??5
      ?  ? RowDefinitions 5   
   ?? 5      $
??0.1* ?6   ?? 5       ? ColumnDefinitions 5   
   ?? 5      ?? 5      ?? 5      .+?5   
   ?? -   5        $
??json??5      $$??Добавить файл JSON??6   $??40??6:   $??0??6Y   $??0??6f   ?? 5   
   -   5        $??first??5      $+??%Первичная обработка??6   $??40??6<   $??0??6\   $??1??6i   ?? 5   
   -   5        $??merge??5      $#??Срастить ячейки??6   $??40??68   $??0??6X   $??2??6e   &   System.Windows.Controls.DataGrid  5   
   -   5        $??content??5      $??1??6"   $??3??61      	RowHeight$ 20Q?6E      RowBackground ?#d3d3d3   SelectionMode$ Extended=?6l      GridLinesVisibility$ Vertical=?6?      AlternatingRowBackground ?#ffffff   AutoGenerateColumns$ False??6?   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Статья??6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   ??  ?? Path$ 
VendorCode?  5      $
 Позиция??6%    66   ?? $
 Name?  5      $
 Количество??6%    69   ?? $ Amount?  5      $
 Ед. Измерения??6%    6<   ?? $ Units?  5      $
 Комментарий??6%    6:   ?? $ Comments?  5       $
 Дата??6%    63   ?? $
 Date?  5!      $
 Сотрудник??6%    68   ?? $
 From?5#      5$      5%      
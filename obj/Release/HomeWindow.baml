   M S B A M L   `   `   `  ���� [  WZonaTools.XPlorerBar, Version=1.1.0.0, Culture=neutral, PublicKeyToken=3a9a0d2a58f5f967Y@clr-namespace:ZonaTools.XPlorerBar;assembly=ZonaTools.XPlorerBarZonaTools.XPlorerBar   
Invoice365   Invoice365.HomeWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Rxpbar@clr-namespace:ZonaTools.XPlorerBar;assembly=ZonaTools.XPlorerBar     -     � Title$/  )Invoice365 - invoice365.ewebcomputing.com��$	��632��$	��871�� � WindowState$ 	Maximized=� � AllowsTransparency .  .��]� $
��Auto��$
��Auto����. 
�������.+��� $
��Auto��$��31�� �� Orientation$ 
Horizontal=�$��Bottom=�
�����F�$��Stretch=�$��RightToLeft=�.+��� $	��552��$��23��$d��^Copyright (c) 2010 - EWeb Computing Solutions Inc. Website:http://invoice365.ewebcomputing.com��$��Arial)�$��10(�$��Center=�$��Center=�$��Center=�$��RightToLeft=��� 
�����F�$��Stretch=�$��Stretch=�$	��180��$
��Auto�� J� 	AllowDrop . $
��Left=�$��LeftToRight=� � 	Resources  �� 
TargetType �� Property �� Value	 �� Triggers
 P� Property P� Value��%  '��       �� " ��.���  � ��
 �������  �@��TextDecorations$ 	Underline{�	 P� 
 �@��IsMouseOver . . ���  � �� �#B59D47P� 
 �@��	IsEnabled .  . ���  � ��# � ��P� 
 �@��	IsEnabled . . ���  �@�Cursor$
 Hand��.+�%   ZonaTools.XPlorerBar.XPlorerBar  -     Name$ ZT_XPlorerBar��$��Stretch=�$	��168��'   !ZonaTools.XPlorerBar.ThemeManager   Theme$
 Blue��$
��Auto��   AllowMultipleExpands ��  �� ElementName$ chMultipleExpands�� �� Path$ 	IsChecked�.@�)   #ZonaTools.XPlorerBar.XPlorerSection  $��Customers/Billers��   
IsExpanded .    	IsPrimary �� $ 	chPrimary��$ 	IsChecked�.���� $ Vertical=�.+�&    ZonaTools.XPlorerBar.XPlorerItem  -      	ItemImage$  /Resources/user--arrow.png��   ItemText$ Manage Customers��  -   $" /Resources/user-business.png��$ Manage Billers��  -   $$ /Resources/database-export.png��$ Export Customers��  -   $$ /Resources/database-export.png��$ Export Billers��  $��Invoice/Inventory�� .  �� $ 	chPrimary��$ 	IsChecked�.���� $ Vertical=�.+�  -   $ /Resources/new.png��$ Create New Invoice��  -   $ /Resources/receipt.png��$ Manage Invoices��  -	   $ /Resources/luggage.png��$ Manage Inventory��  -
   $$ /Resources/database-export.png��$ Export Invoices��  -   $$ /Resources/database-export.png��$ Export Inventory��  $��Weekly Reports�� .  �� $ 	chPrimary��$ 	IsChecked�.���� $ Vertical=�.+�  -   $ /Resources/moneys.png��$ Sales By Products��  -   $ /Resources/moneys.png��$ Sales By Customer��  -   $ /Resources/moneys.png��$ Sales By Biller��  -   $ /Resources/calculator.png��$ Due By Customer��  $��Daily Reports�� .  �� $ 	chPrimary��$ 	IsChecked�.���� $ Vertical=�.+�  -   $ /Resources/moneys.png��$ Sales By Products��  -   $ /Resources/moneys.png��$ Sales By Customer��  -   $ /Resources/moneys.png��$ Sales By Biller��  -   $ /Resources/calculator.png��$ Due By Customer��  $��System�� .  �� $ 	chPrimary��$ 	IsChecked�.���� $ Vertical=�.+�  -   $ /Resources/chm.gif��$ Invoice365 Help��  -   $ i365.ico��$ About Invoice365�� � -   $��ContentFrame��$
��Auto��$
��Auto��
�������  � Source$ NewInvoice.xaml?�
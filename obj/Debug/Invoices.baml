   M S B A M L   `   `   `  ����X  TInvoice365, Version=1.0.3989.21010, Culture=neutral, PublicKeyToken=db2d1b37798ad79b'clr-namespace:Invoice365
Invoice365  5      [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5   �       Invoice365.Invoices   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      *localclr-namespace:Invoice365     5      @d2http://schemas.microsoft.com/expression/blend/2008     5   e   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   Omy@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     6�      Invoice365.Inv365Sortable    Initialized$  Window_Initialized��6    N� Title$ Invoices��6   $��Center=�67    � 	Resources 5       �� 
TargetType �� Property �� Value��%W   '��       �� 5   
   " ��6   .�5      ��  � ��$ Stretch=�6;   5	      5
      .��5      (� $	��Top=�6   $��Center=�6&    (� Stretch$
 Fill=�6C   .��5   
   � $
��1064��6    � ShowGridLines .  ��5      �� 5       � ColumnDefinitions 5      �� 5      $
��496* �6#   �� 5      $
��568* �6#   	 � RowDefinitions	 5      �� 5      $	��25* �6    �� 5      $
��Auto �6    .+�5      �� -   5        $��
rectangle2��5   ;   
�����F�$��0,-1,0,2q�6)   
 J� OpacityMask
 �	#FF318E8E
���   �$��2��6t   $
��Left=�6�   $	��839��6�   �� 5      -   5        $��invView��5   7   $��Arial)�5   X   $��14(�5      $��0,90,225,77q�6"   }�.  J� 	AllowDrop .  � IsSharedSizeScope .  
������ $��2q�5   L   
�����p�$��1��5      $��2��6!    �� View 5      �� 5      .P�5      ��  �� Width$	 140��6)   $
��Date��65   �� 5       $	 100��6)   $��
Invoice No��65   �� 5!      $	 100��6)   $��Customer��65   �� 5"      $	 100��6)   $��
Created By��65   �� 5#      $	 100��6)   $��Biller��65   5%      5'      �� 5(      -   5        $��
rectangle1��5(   G   ���#FFFFFF$��86��6'   $��
0,28,225,0q�63   
���   �$	��Top=�6h   $��2��6�   $��2��6�   �� 5)      -   5        $��
barcodeLbl��5)   r   $��
Invoice No��6   $
��Bold"�6)   
���   �$��28��6N   $��137,31,298,457q�6Z   $��61.105��6�   $��1��6�   �� 5*      -   5        $��	invoiceNo��5*   8   $��23��6   $��0,30,180,463q�6"   $��3��6I   $	��112��6V   $��Right=�6b   $��1��6~   6�   �� 5+      -   5        $��label4��5+   s   $��Customer��6   $
��Bold"�6'   
���   �$��27.775��6L   $��357,34,71,454q�6\   $��68��6�   $��1��6�   7  1System.Windows.Forms.Integration.WindowsFormsHost  5,      -   5        $��windowsFormsHost2��5,   a   $��20.775��6   $��Right=�6/   $��0,34,493,461q�6K    K� ZIndex$	 500��6z      TabIndex$ 8��6�   $	��149��6�   $��1��6�   $��2��6�      Child. 5-      #  System.Windows.Forms.ComboBox  -   5        customer��"  System.Windows.Forms.Control   	AllowDrop .    AutoCompleteMode$ SuggestAppend=�5-   A      AutoCompleteSource$ 	ListItems=�6b      Margin+  %System.Windows.Forms.PaddingConverter$ 162,58,197,0 6�      TabIndex$ 1��6�   5.      �� 5/      -	   5        $��label9��5/   w   $��Invoice Search��6   $
��Bold"�6-   
�������$��23.331��6R   $��403,1,395,5q�6b   $��2��6�   �� 50      -
   5        $
��save��50   q   $��Print Invoice��6   $��12(�6-   $
��Bold"�6;   $��22��6M   $��281,454,120,40q�6Y   $��11��6}   $��1��6�   6�   �� 51      -   5        $��remove��51   n   $��Edit Invoice��6   $��12(�6,   $
��Bold"�6:   $��22��6L   $��0,454,564,40q�6X   $��12��6|   $��1��6�   $��Right=�6�   $��97��6�   $��2��6�   6�   52      53       N� 
Background 54      �� 55   
   57      
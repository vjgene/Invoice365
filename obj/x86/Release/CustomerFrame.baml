   M S B A M L   `   `   `  ���� [  WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms  X TInvoice365, Version=1.0.4614.15477, Culture=neutral, PublicKeyToken=db2d1b37798ad79b'clr-namespace:Invoice365
Invoice365    Invoice365.CustomerFrame   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     *localclr-namespace:Invoice365     [ WWPFLocalizeExtension, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1949495244fd89a8;lex1http://schemas.root-project.org/xaml/presentation    Invoice365.Inv365Sortable    Initialized$  Window_Initialized�� N� 
Foreground �	#FF35496A N� Title 6  0WPFLocalizeExtension.Extensions.LocTextExtension  *$"Invoice365:CustomerFrame:Dashboard+ � 	Resources  �� 
TargetType �� Property �� Value��%2   '��       �� " ��.���  � ��$ Stretch=�.��]� $
��Auto��$
��Auto����. 
������ $��Center=�$	��Top=�.+�(� $	��Top=�$��Center=� (� Stretch$
 Fill=�.��� $	��Top=�$��Center=�$	��556��$
��1111��$��-1q� � RowDefinitions �� $
��268* ��� $
��288* �	 � ColumnDefinitions	 �� $��226.407* ��� $	��102 ��� $
��181* ��� $��215.297 ��� $
��178* �.+��� -   $��
rectangle2��$��4��$��18,12,27,232q�
 J� OpacityMask
 �	#FF318E8E
���   �
�����F��� -   $��custView��$��19,257,27,34q�}�.  J� 	AllowDrop .  � IsSharedSizeScope . $��2q����	#FF809AB2$��4��$��Arial)�$��14(�$��13��$��2�� �� View �� .P���  �� Width$	 150����  *" Invoice365:CustomerFrame:Company+�� $	 150����  * Invoice365:CustomerFrame:Phone+�� $	 150����  *$"Invoice365:CustomerFrame:FirstName+�� $	 150����  *#!Invoice365:CustomerFrame:LastName+�� $	 150����  *" Invoice365:CustomerFrame:Address+�� $	 150����  *Invoice365:CustomerFrame:City+�� $	 150����  * Invoice365:CustomerFrame:State+���� �� -   $��title��
�������$��23.331��$��1��$��	77,13,0,0q�$	��Top=�$
��Bold"�$
��Left=�$��93��$��2����  *&$Invoice365:CustomerFrame:AddCustomer+�� -   $��savecustomer��$��12(�$��Normal"�$��291,0,224,13q�$��11��$��22��$��Bottom=�$��3��.��  8 1http://schemas.root-project.org/xaml/presentation  �  �WPFLocalizeExtension.BaseExtensions.BaseLocalizeExtension`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   ResourceIdentifierKey$+ %Invoice365:CustomerFrame:SaveCustomer���� -   $��deleteCustomer��$��12(�$��Normal"�$��22��$
��Left=�$��	27,0,0,12q�$��12��$��Bottom=�$��138.966��$��2��.��  8 1http://schemas.root-project.org/xaml/presentation  $- 'Invoice365:CustomerFrame:DeleteCustomer���� -   $��tabControl1��$	��158��$
��Left=�$��	21,70,0,0q�$	��Top=�$	��825��$��4��$��0q����� .@��� -   $��billing����  *)'Invoice365:CustomerFrame:BillingAddress+.���� -   $��canvas1��$	��118��$	��818��.+��� -	   $��label7��$
��Bold"�
���   � �� Left$ 33��
 �� Top$ 87��.��  8 1http://schemas.root-project.org/xaml/presentation  $$ Invoice365:CustomerFrame:City1���� -
   $��label4��$
��Bold"�
���   �$��28��$ 28��$ 16��.��  8 1http://schemas.root-project.org/xaml/presentation  $&  Invoice365:CustomerFrame:Street1���� -   $��street1��$��23��$��4��$ 93��$ 16��$	��131���� -   $��label1��$
��Bold"�
���   �$��28��$ 29��$ 51��.��  8 1http://schemas.root-project.org/xaml/presentation  $&  Invoice365:CustomerFrame:Street2���� -   $��street2��$��23��$��5��$ 93��$ 51��$	��132��tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration 7  1System.Windows.Forms.Integration.WindowsFormsHost  XmyOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration  -   $��
cityCBHost��$��20.775��   TabIndex$ 1��$��128,42,50,0q�$	��Top=�$	��131��$	 -35��$ 46��   Child. #   System.Windows.Forms.ComboBox  -      AutoCompleteMode$ SuggestAppend=�   AutoCompleteSource$ 	ListItems=�city��"   System.Windows.Forms.Control   TabIndex$ 1��   	AllowDrop .    Margin+   %System.Windows.Forms.PaddingConverter$ 162,58,197,0 �� -   $��label8��$
��Bold"�
���   �$��28��$��46.297��$	 275��$ 15��.��  8 1http://schemas.root-project.org/xaml/presentation  $% Invoice365:CustomerFrame:State1���� -   $��state��$��23��$��7��$	 335��$ 20��$	��127���� -   $��label10��$
��Bold"�
���   �$��53��$	 515��$ 25��.��  8 1http://schemas.root-project.org/xaml/presentation  $" Invoice365:CustomerFrame:Fax���� -   $	��fax��$��9��$	 576��$ 23��$	��126���� -   $��label11��$
��Bold"�
���   �$��28��$��53��$	 515��$ 57��.��  8 1http://schemas.root-project.org/xaml/presentation  $$ Invoice365:CustomerFrame:Email���� -   $��email��$��23��$��10��$	 578��$ 55��$	��126���� -   $��label3��$	 274��$ 51��$
��Bold"�
���   �$��28��$��46.297����  *Invoice365:CustomerFrame:Zip+�� -   $	��zip��$	 334��$ 56��$��23��$��7��$	��127��tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration   XmyOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration  -   $��customerCBHost��$��20.775��$ 1��$	 580��$ 87��$	��125��.   -   $ SuggestAppend=�$ 	ListItems=�
customerCB��$ 1�� . $ 162,58,197,0 �� -   $��label2��$
��Bold"�
���   �$��28��$��68��$	 511��$ 86��.��  8 1http://schemas.root-project.org/xaml/presentation  $' !Invoice365:CustomerFrame:Company1���� -   $��shipping����  **(Invoice365:CustomerFrame:ShippingAddress+.���� $��Stretch=�$��Stretch=�.+��� -   $��label6��$ 15��$ 82��$
��Bold"�
���   ���  * Invoice365:CustomerFrame:City2+�� -   $��label9��$ 10��$ 11��$
��Bold"�
���   �$��28����  *#!Invoice365:CustomerFrame:Street11+�� -   $��	s_street1��$ 75��$ 11��$��23��$��4��$	��131���� -   $��label12��$ 11��$ 46��$
��Bold"�
���   �$��28����  *#!Invoice365:CustomerFrame:Street21+�� -    $��	s_street2��$ 75��$ 46��$��23��$��5��$	��132���� -!   $��s_city��$ 75��$ 82��$��6��$��129.402���� -"   $��label13��$	 258��$ 80��$
��Bold"�
���   �$��28��$��53����  *!Invoice365:CustomerFrame:Phone2+�� -#   $��s_phone��$	 315��$ 82��$��23��$��8��$	��125���� -$   $��label14��$	 257��$ 10��$
��Bold"�
���   �$��28��$��46.297����  *!Invoice365:CustomerFrame:State2+�� -%   $��s_state��$	 317��$ 15��$��23��$��7��$	��127���� -&   $��label15��$	 497��$ 20��$
��Bold"�
���   �$��53����  *Invoice365:CustomerFrame:Fax1+�� -'   $��s_fax��$	 558��$ 18��$��9��$	��126���� -(   $��label16��$	 497��$ 52��$
��Bold"�
���   �$��28��$��53����  *!Invoice365:CustomerFrame:Email1+�� -)   $��s_email��$	 560��$ 50��$��23��$��10��$	��126���� -*   $��label17��$	 256��$ 46��$
��Bold"�
���   �$��28��$��46.297����  *Invoice365:CustomerFrame:Zip1+�� -+   $��s_zip��$	 316��$ 51��$��23��$��7��$	��127���� -,   $��firstNameLabel��$
��Bold"�
���   �$��28��$��63��$��2��$��273,46,73,194q�.��  8 1http://schemas.root-project.org/xaml/presentation  $) #Invoice365:CustomerFrame:FirstName1���� --   $��	firstName��$��23��$��2��$	��129��$��1��$��2��$��46,42,172,203q��� -.   $��lastNameLabel��$
��Bold"�
���   �$��63��$��2��$��98,48,84,204q�.��  8 1http://schemas.root-project.org/xaml/presentation  $( "Invoice365:CustomerFrame:LastName1���� -/   $��lastName��$��3��$	��131��$��2��$��2��$��178,44,152,202q��� -0   $��label5��$
��Bold"�
���   �$��53��$��34,43,220,203q�.��  8 1http://schemas.root-project.org/xaml/presentation  $% Invoice365:CustomerFrame:Phone1��tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration   XmyOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration  -1   $��phoneCBHost��$��20.775��$ 1��$��
91,42,85,0q�$	��Top=�$	��131��.   -2   $ SuggestAppend=�$ 	ListItems=�	phone��$ 1�� . $ 162,58,197,0 ����  N� 
Background �� 
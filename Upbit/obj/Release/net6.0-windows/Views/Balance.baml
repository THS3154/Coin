   M S B A M L   `   `   `  ���� @  <Upbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Upbit.ConverterUpbit.Converter  M ITextBoxPlaceHolder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TextBoxPlaceHolder;assembly=TextBoxPlaceHolderTextBoxPlaceHolder     Upbit.Views.Balance   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  (convclr-namespace:Upbit.Converter  _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  Icustom<clr-namespace:TextBoxPlaceHolder;assembly=TextBoxPlaceHolder  $	��180��!  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True � 	Resources +  %Upbit.Converter.IsVisibilityConverter   isVisiblityConverter*  $Upbit.Converter.TwoMultipleConverter  TwoMultipleConverter!  Upbit.Converter.FloorNumber  FloorNumber"  Upbit.Converter.SubConverter  SubConverter#  Upbit.Converter.PlusConverter  PlusConverter  ListviewHeader �� 
TargetType �� Property �� Value��%�   &	        &	      &	 
     &	      &	      &	                �� " ��.���  � �� �� *MyColors.ColorText+��  � ��$
 Bold"���  � �� �� *MyColors.ColorBack+.��� $��5q� � RowDefinitions �� $
��auto ��� $��1* �.+�� $��0��.+���  �� Orientation$ Vertical=�.+��� $��내 보유자산��$
��Bold"�$��14(��  �� $��1* ��� $��5 ��� $��1* ��� $��5 �.+��� $��0��$ Vertical=�$��0,0,5,0q�.+��� $��	보유KRW���� $��16(�$
��Bold"�$��0,0,0,5q����� *MyAccount.Balance+ �� 	Converter# [ � $��0��.+��� $��	총매수��$
��Left=��� $��Right=����� *MyAccount.AvgBuyPrice+# [ � $��1��.+��� $��	총평가��$
��Left=��� $��Right=����� *MyAccount.NowPrice+# [ �� $��1��$��2q�
��������� $��3��$��2q�
��������� $��2��$ Vertical=�$��0,0,5,0q�.+��� $��총 보유자산���� $��16(�$
��Bold"�$��0,0,0,5q���`�  `� 	Converter# [ .1��� 	 �� Path$	 MyAccount.NowPrice��� $	 MyAccount.Balance�� $��0��.+��� $��평가손익��$
��Left=��� $��Right=����� *MyAccount.Color+��`� # [ .1��� $	 MyAccount.NowPrice��� $	 MyAccount.AvgBuyPrice�� $��1��.+��� $��	수익률��$
��Left=��� $��Right=����� *MyAccount.Rate+
 �� StringFormat$
 	{0:0.##}%������ *MyAccount.Color+� $��1��.+���  K� ZIndex$ 1��$��Center=�$��Center=����� *MyColors.ColorText+���� *Lang.LNotAccess+y��� *NoneAccessView+�� -    ��Name$ 
MyListView��$��Disabled=�$��0q����� *
Accounts+���� *MyColors.ColorText+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ MouseDoubleClick��*	  $Microsoft.Xaml.Behaviors.TriggerBase 	  Actions. 2
  ,Microsoft.Xaml.Behaviors.InvokeCommandAction
   
  Command �� *CommandSendCoin+ 
  CommandParameter ��  �� ElementName$ 
MyListView��$	 SelectedItem����� " ��.���  � ��$ Stretch=���  � ��$ 0q���  � ��$ 0q���  � ��$ 0q����� .R�� $��0,10,0,0q�.+��� $ Vertical=�.+�� .+��� $ 
Horizontal=�.+��� $
��Bold"�$��14(����� *
CoinName+�� $
��Bold"�$��14(����� *
Currency+$
 ({0})��� .+��� $��	수익률���� $��Right=����� *Color+���� *Rate+$
 	{0:0.##}%��� .+��� $��평가손익���� $��Right=����� *Color+��`� # [ .1��� $	 
TotalPrice��� $
	 Cost�� .+��� $��	평단가���� $��Right=����� *AvgBuyPrice+# [ � .+��� $��보유수량���� $��Right=����� *TotalBalance+
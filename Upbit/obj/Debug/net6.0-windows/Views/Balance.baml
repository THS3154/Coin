   M S B A M L   `   `   `  ����@  <Upbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Upbit.ConverterUpbit.Converter  5      M ITextBoxPlaceHolder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TextBoxPlaceHolder;assembly=TextBoxPlaceHolderTextBoxPlaceHolder 5          Upbit.Views.Balance   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5      (convclr-namespace:Upbit.Converter  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      Icustom<clr-namespace:TextBoxPlaceHolder;assembly=TextBoxPlaceHolder  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5	       � 	Resources 5
      +  %Upbit.Converter.IsVisibilityConverter   isVisiblityConverter*  $Upbit.Converter.TwoMultipleConverter  TwoMultipleConverter!  Upbit.Converter.FloorNumber  FloorNumber"  Upbit.Converter.SubConverter  SubConverter#  Upbit.Converter.PlusConverter  PlusConverter  ListviewHeader �� 
TargetType �� Property �� Value��%[  &	        &	      &	 &     &	 9     &	 L     &	 _       5   
   6%     5   
   6$     5   
   6     5   
   6     5   
   6   �� 5   
   " ��6'   .�5      ��  � �� 6+   �� *MyColors.ColorText+�� 5       � ��$
 Bold"�6+   �� 5       � �� 6+   �� *MyColors.ColorBack+5      5      .��5      � $��5q�6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��3 �6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��1* �6   .+�5    
   � $��0��6   $��0��6   .+�5!      ��  �� Orientation$ Vertical=�6   .+�5"      �� $��내 보유자산��6   $
��Bold"�6*   $��14(�6<   � 5#       5$      �� 5%      $	��150 �6+   �� 5&      $��5 �6+   �� 5'      $	��150 �6+   .+�5*      �� $��0��6!   $ Vertical=�61   $��0,0,5,0q�6I   .+�5+      �� $��	보유KRW��6$   �� 5,      $��16(�6o   $
��Bold"�6}   $��0,0,0,5q�6�   ��6$   �� *MyAccount.Balance+ �� 	Converter# [ 6�   � 5-      .+�5.      �� $��	총매수��6(   $
��Left=�63   �� 5/      $��Right=�6w   ��6(   �� *MyAccount.AvgBuyPrice+# [ 6w   50      � 51      .+�52      �� $��	총평가��6(   $
��Left=�63   �� 53      $��Right=�6t   ��6(   �� *MyAccount.NowPrice+# [ 6t   54      55      �� 58      $��2��6!   $ Vertical=�61   $��0,0,5,0q�6H   .+�59      �� $��총 보유자산��6$   �� 5:      $��16(�6$   $
��Bold"�62   $��0,0,0,5q�6E   ��5;      `� 5<   "   	 `� 	Converter#	 [ 6/   .1�5=   &   �� 
 �� Path$
 MyAccount.NowPrice�6.   �� 5>   &   $
 MyAccount.Balance�6.   5?   #   5A      � 5B      .+�5C      �� $��평가손익��6(   $
��Left=�64   �� 5D      $��Right=�6(   ��6D   �� *MyAccount.Color+��5E   "   `� 5F   &   #	 [ 63   .1�5G   *   �� $
 MyAccount.NowPrice�62   �� 5H   *   $
 MyAccount.AvgBuyPrice�62   5I   '   5K      5L      � 5M      .+�5N      �� $��	수익률��6(   $
��Left=�64   �� 5O      $��Right=�6�   ��6(   �� *MyAccount.Rate+ �� StringFormat$ 	{0:0.##}%����6b   �� *MyAccount.Color+6�   5P      5Q      5R      5S      5T      � 5V   
   $��1��6   .+�5X      ��  K� ZIndex$ 1��6   $��Center=�6S   $��Center=�6n   ��6)   �� *MyColors.ColorText+��6�   �� *Lang.LNotAccess+y�6�   �� *NoneAccessView+�� 5Y      -   5         ��Name$ 
MyListView��5[      $��Disabled=�5Z      $��0q�6L   ��5Y      �� *
Accounts+��69   �� *MyColors.ColorText+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5\      +  %Microsoft.Xaml.Behaviors.EventTrigger  5]         	EventName$ MouseDoubleClick��6%   *	  $Microsoft.Xaml.Behaviors.TriggerBase 	  Actions. 5^      2
  ,Microsoft.Xaml.Behaviors.InvokeCommandAction
   
  Command 60   �� *CommandSendCoin+ 
  CommandParameter 6T   ��  �� ElementName$ 
MyListView��$
 SelectedItem�5_      ��5a      �� 5b      .R�5c      �� $ 
Horizontal=�6%   5d      ��5f      �� 5g      " ��6   .�5h      ��  � ��$ Stretch=�6G   �� 5i       � ��$ 0q�6<   �� 5j       � ��$ 0q�63   �� 5k       � ��$ 0q�64   5l      ��5n      �� 5o      .R�5p      � $��0,0,10,0q�6   $	��150��61   .+�5q      �� $ Vertical=�6)   .+�5r   "   � .+�5s   &   �� $ 
Horizontal=�61   .+�5t   *   �� $
��Bold"�6N   $��14(�6`   ��64   �� *
CoinName+6`   �� 5u   *   $
��Bold"�6d   $��14(�6v   ��64   �� *
Currency+$ ({0})��6v   5v   '   5w   #   � 5x   "   .+�5y   &   �� $��	수익률��60   �� 5z   &   $��Right=�6|   ��60   �� *Color+��6M   �� *Rate+$ 	{0:0.##}%��6|   5{   #   � 5|   "   .+�5}   &   �� $��평가손익��60   �� 5~   &   $��Right=�6M   ��60   �� *Color+��5   *   `� 5�   .   #	 [ 6;   .1�5�   2   �� $
 
TotalPrice�6:   �� 5�   2   $

 Cost�6:   5�   /   5�   '   5�   #   � 5�   "   .+�5�   &   �� $��	평단가��60   �� 5�   &   $��Right=�6u   ��60   �� *AvgBuyPrice+# [ 6u   5�   #   � 5�   "   .+�5�   &   �� $��보유수량��60   �� 5�   &   $��Right=�6N   ��60   �� *TotalBalance+6N   5�   #   5�      5�      5�      5�      5�      � 5�   
   $��0��6   $��1��6   $��2��6,   ���#333$��1q�6R   5�      5�      5�      
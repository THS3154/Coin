   M S B A M L   `   `   `  ����@  <Upbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Upbit.ConverterUpbit.Converter  5      G COrderTextbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:OrderTextbox;assembly=OrderTextboxOrderTextbox 5          Upbit.Views.CoinOrder   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5      (convclr-namespace:Upbit.Converter  5      9ot0clr-namespace:OrderTextbox;assembly=OrderTextbox  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5       � 	Resources 5
       �� MergedDictionaries �� Source)   #Upbit.Converter.DivByThreeConverter   DivByThreeConverter'   !Upbit.Converter.DivByTwoConverter  DivByTwoConverter(   "Upbit.Converter.OrderViewConverter  OrderViewConverter#   Upbit.Converter.IsReverseBool  IsReverseBool)   #Upbit.Converter.BidAskLangConverter  BidAskLangConverter<   6Upbit.Converter.IsChanceOrderViewBtnVisiblityConverter * &IsChanceOrderViewBtnVisiblityConverter �� 
TargetType  	BidButton �� Property �� Value �� 
TargetType ��Name	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName  	AskButton  OrderButton 	 ChanceButton 
 OrderListBoxLeft  OrderListBoxRight�� 5   
    5      �� 5      $F @pack://application:,,,/OrderTextbox;component/ROrderTextbox.xaml?�6%   %  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 #    &	 �    &		 �    &	
 �    &	 &      5      6(     5      6%     5      6&     5      6!     5      6'     5      6:   �� 5      " ��6   .�5      ��  � �� 6/   �� *MyColors.ColorBidBack+�� 5       � �� 6/   �� *MyColors.ColorBid+�� 5       � ��$ 0q�64   �� 5       � �� 60   �� *MyColors.ColorBid+�� 5       � �� 5      �� 5      " ��6*   .R�5      �� $ border��6%   $��Stretch=�65   $��Stretch=�6S   #��z��6o   ._�5   "   � .+�5   &   �� $	 
Background��60   #��z��6D   �� 5    &   $��Center=�67   $��Center=�6T   5!   #   5"      
 5$      P� 5%   "    �@J�IsMouseOver$
 True��6A   . �5&   &   �� $ 
Background��6-    �@J�Opacity$	 0.8Q�6Y   5'   #   5)      5+      5,      �� 5.      " ��6   .�5/      ��  � �� 6/   �� *MyColors.ColorAskBack+�� 50       � �� 6/   �� *MyColors.ColorAsk+�� 51       � ��$ 0q�64   �� 52       � �� 60   �� *MyColors.ColorAsk+�� 53       � �� 54      �� 55      " ��6*   .R�56      �� $ border��6%   $��Stretch=�65   $��Stretch=�6S   #��z��6o   ._�57   "   � .+�58   &   �� $	 
Background��60   #��z��6D   �� 59   &   $��Center=�67   $��Center=�6T   5:   #   5;      
 5=      P� 5>   "    �@J�IsMouseOver$
 True��6A   . �5?   &   �� $ 
Background��6-    �@J�Opacity$	 0.8Q�6Y   5@   #   5B      5D      5F      �� 5H      " ��6   .�5I      ��  � ��
 ���� 6/   �� 5J       � �� 6/   �� *	BtnBorder+�� 5K       � ��$ 1q�64   �� 5L       � �� 60   �� *	BtnBorder+�� 5M       � �� 5N      �� 5O      " ��6*   .R�5P      �� $ border��6%   $��Stretch=�65   $��Stretch=�6S   #��z��6o   #��z��6�   ._�5Q   "   � .+�5R   &   �� $	 
Background��60   
������ 6D   �� 5S   &   $��Center=�67   $��Center=�6T   5T   #   5U      
 5W      P� 5X   "    �@J�IsMouseOver$
 True��6A   . �5Y   &   �� $ 
Background��6-    �@J�Opacity$	 0.5Q�6Y   �� 5Z   &   $ 
Background��6-    6U   �� *	BtnBack+ � ��5[   #   5]      5_      5a      �� 5c      " ��6   .�5d      ��  � �� 6/   �� *MyColors.ColorBack+�� 5e       � �� 6/   �� *MyColors.ColorText+�� 5f       � ��$ 0q�64   �� 5g       � �� 60   �� *MyColors.ColorText+�� 5h       � �� 5i      �� 5j      " ��6*   .R�5k      �� $ border��6%   $��Stretch=�65   $��Stretch=�6S   #��z��6o   ._�5l   "   � .+�5m   &   �� $	 
Background��60   #��z��6D   �� 5n   &   $��Center=�67   $��Center=�6T   5o   #   5p      
 5r      P� 5s   "    �@J�IsMouseOver$
 True��6A   . �5t   &   �� $ 
Background��6-    �@J�Opacity$	 0.8Q�6Y   �� 5u   &   $ 
Background��6-    6U   �� *MyColors.ColorBack+ � ��5v   #   5x      5z      5{      �� 5}      " ��6   .�5~      ��  � ��$ Center=�66   �� 5       � ��$
 Left=�68   5�      �� 5�      " ��6   .�5�      ��  � ��$ Center=�66   �� 5�       � ��$ Right=�68   5�      5�      .��5�      � -   5         �Name$ MainGrid��5�       � RowDefinitions 5�   
   �� 5�      $��30 �6   �� 5�      $��1* �6   .+�5�   
   � -   5        $ 
ButtonGrid��5�      $��0��6   .+�5�      �� $��Center=�6   $��Center=�66    �� Orientation$ 
Horizontal=�6Q   .+�5�      �� #��[ 6   $��T��6  ��6<   �� *Lang.LCoinBuy+��6^   �� *ActualHeight+ �� ElementName$ 
ButtonGrid����6�   �� *ActualWidth+$ MainGrid�� �� 	Converter# [  ��6�   �� *CommandBidAskType+6  �� 5�      #��[ 6   $��F��6  ��6<   �� *Lang.LCoinSell+��6_   �� *ActualHeight+$ 
ButtonGrid����6�   �� *ActualWidth+$ MainGrid��# [  ��6�   �� *CommandBidAskType+6  �� 5�      #��[	 6   $��O��6"  ��6?   �� *Lang.LOrderList+��6c   �� *ActualHeight+$ 
ButtonGrid����6�   �� *ActualWidth+$ MainGrid��# [  ��6�   �� *CommandBidAskType+6"  5�      5�      � 5�   
   $��1��6   ��6   �� *	GridColor+.+�5�      � y�6   �� *	OrderView+# [  5�      �� 5�      $��30 �6$   �� 5�      $��* �6$   �� 5�      $��30 �6$   .+�5�      �� $��0��6   $��Right=�6*   $	��Top=�6F   $ 
Horizontal=�6^   $��5q�6w   .+�5�      	� $��m��6g   ��6"   �� *Lang.LMarket+��6C   �� *CommandIsMarket+6~   	� 5�      $��l��6f    d� 	IsChecked$
 TrueY�6{   $��15,0,0,0q�6�   ��6"   �� *Lang.LLimit+��6B   �� *CommandIsMarket+6�   5�      �� 5�      $��1��6   $ Vertical=�6*   $��Stretch=�6A   $	��Top=�6_   $��5,0,5,5q�6w   .+�5�         OrderTextbox.COrderTextbox  $��0,2,0,2q�6'   $��Stretch=�68     OrderTextbox#��[ 6V      TextboxEnable$ False��6     Prefix 6�   �� *Lang.LOrderPrice+   Subffix 6�   �� *AcitiveOrderCurrency+   	TextValue 6�   �� *ActiveOrderPrice+ �� Mode$ TwoWay=�   BorderColor 6  �� *	BtnBorder+  5�      $��0,2,0,2q�6'   #��[ 69    6k   �� *Lang.LQuantity+ 6�   �� *	OrderCoin+ 6�   �� *
MarketSell+ 6�   �� *StrQuantity+$ TwoWay=� 6�   �� *	BtnBorder+  5�      $��0,2,0,2q�6'   #��[ 69    6k   �� *Lang.LPrice+ 6�   �� *
Currency+ 6�   �� *
IsMarket+# [  6�   �� *
StrPrice+$ TwoWay=� 6  �� *	BtnBorder+  5�      $��0,2,0,2q�6'   #��[ 69    6k   �� *Lang.LTotalPrice+ 6�   �� *
Currency+ 6�   �� *	MarketBuy+ 6�   �� *StrTotalPrice+$ TwoWay=� 6   �� *	BtnBorder+5�      �� 5�      #��[ 6   $��2��6>   ��6K   �� *	BtnText+{�6g   �� *ActiveOrder+��6�   �� *CommandCoinOrder+y�6�   �� *	OrderView+# [ 5�      � 5�      y�6   �� *OrderListView+# [  5�      �� 5�      $��30 �6$   �� 5�      $��* �6$   .+�5�      �� $��0��6   $��Right=�6*   $	��Top=�6F   $ 
Horizontal=�6^   $��5q�6w   .+�5�      	� $
��wait��6w   $
 TrueY�6�   ��6"   �� *Lang.LNotConclusion+��6J   �� *CommandSelectedOrderList+6�   	� 5�      $
��done��6t   $��15,0,0,0q�6�   ��6"   �� *Lang.LConclusion+��6G   �� *CommandSelectedOrderList+6�   5�      �� 5�      -   5         ��Name$ OrderListBox��5�      $��1��6R   $��Stretch=�6_   $��Stretch=�6{   $��0q�6�   $��5,0,5,5q�6�   ��60   �� *	OrderList+��5�      �� 5�      .R�5�      � ��6#   �� *ActualWidth+$ OrderListBox�� 5�   "   �� 5�   &   $��30 �64   �� 5�   &   $��30 �64   �� 5�   &   $��30 �64   �� 5�   &   $��30 �64   �� 5�   &   $��30 �64    � ColumnDefinitions 5�   "   �� 5�   &   $��1* �67   �� 5�   &   $��1* �67   �� 5�   &   $��30 �67   .+�5�   "   �� $��0��6)   $��2��66   $��5��6F   $��Stretch=�5�   )   $��Stretch=�6G   ��5�   W   �� *(&DataContext.Lang.LChanceOrderBtnCancel+$ MainGrid��y�5�   c   �� *remaining_volume+# [ ��5�   )   �� * DataContext.CommandOrderCancel+$ MainGrid����6q   �� *uuid+�� 5�   "   #��[
 6,   $��0��6V   $��0��6c   ��6s   �� *side+# [ �� 5�   "   #��[
 6,   $��1��6V   $��0��6c   ��6s   �� *$"DataContext.Lang.LChanceMarketName+$ MainGrid���� 5�   "   #��[
 6,   $��2��6V   $��0��6c   ��6s   �� *$"DataContext.Lang.LChanceOrderPrice+$ MainGrid���� 5�   "   #��[
 6,   $��3��6V   $��0��6c   ��6s   �� *'%DataContext.Lang.LChanceOrderQuantity+$ MainGrid���� 5�   "   #��[
 6,   $��4��6V   $��0��6c   ��6s   �� *,*DataContext.Lang.LChanceOrderNotConclusion+$ MainGrid���� 5�   "   #��[ 6,   $��0��6W   $��1��6d   ��6t   �� *
created_at+�� 5�   "   #��[ 6,   $��1��6W   $��1��6d   ��6t   �� *market+�� 5�   "   #��[ 6,   $��2��6W   $��1��6d   ��6t   �� *price+�� 5�   "   #��[ 6,   $��3��6W   $��1��6d   ��6t   �� *volume+�� 5�   "   #��[ 6,   $��4��6W   $��1��6d   ��6t   �� *remaining_volume+5�      5�      5�      5�      5�      5�      5�      
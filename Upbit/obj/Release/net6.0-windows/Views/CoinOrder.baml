   M S B A M L   `   `   `  ���� @  <Upbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Upbit.ConverterUpbit.Converter  G COrderTextbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:OrderTextbox;assembly=OrderTextboxOrderTextbox     Upbit.Views.CoinOrder   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  (convclr-namespace:Upbit.Converter  9ot0clr-namespace:OrderTextbox;assembly=OrderTextbox  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True � 	Resources  �� MergedDictionaries �� Source)   #Upbit.Converter.DivByThreeConverter   DivByThreeConverter'   !Upbit.Converter.DivByTwoConverter  DivByTwoConverter(   "Upbit.Converter.OrderViewConverter  OrderViewConverter#   Upbit.Converter.IsReverseBool  IsReverseBool)   #Upbit.Converter.BidAskLangConverter  BidAskLangConverter<   6Upbit.Converter.IsChanceOrderViewBtnVisiblityConverter * &IsChanceOrderViewBtnVisiblityConverter �� 
TargetType  	BidButton �� Property �� Value �� 
TargetType ��Name	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName  	AskButton  OrderButton 	 ChanceButton 
 OrderListBoxLeft  OrderListBoxRight��  �� $F @pack://application:,,,/OrderTextbox;component/ROrderTextbox.xaml?�%�  &	        &	      &	 
     &	      &	      &	      &	      &	 �    &	 \    &		     &	
 �    &	 ,                �� " ��.���  � �� �� *MyColors.ColorBidBack+��  � �� �� *MyColors.ColorBid+��  � ��$ 0q���  � �� �� *MyColors.ColorBid+��  � �� �� " ��.R��� $ border��$��Stretch=�$��Stretch=�#��z��._�� .+��� $	 
Background��#��z���� $��Center=�$��Center=�
 P�  �@J�IsMouseOver$
 True��. ��� $ 
Background�� �@J�Opacity$	 0.8Q��� " ��.���  � �� �� *MyColors.ColorAskBack+��  � �� �� *MyColors.ColorAsk+��  � ��$ 0q���  � �� �� *MyColors.ColorAsk+��  � �� �� " ��.R��� $ border��$��Stretch=�$��Stretch=�#��z��._�� .+��� $	 
Background��#��z���� $��Center=�$��Center=�
 P�  �@J�IsMouseOver$
 True��. ��� $ 
Background�� �@J�Opacity$	 0.8Q��� " ��.���  � ��
 ���� ��  � �� �� *	BtnBorder+��  � ��$ 1q���  � �� �� *	BtnBorder+��  � �� �� " ��.R��� $ border��$��Stretch=�$��Stretch=�#��z��#��z��._�� .+��� $	 
Background��
������ �� $��Center=�$��Center=�
 P�  �@J�IsMouseOver$
 True��. ��� $ 
Background�� �@J�Opacity$	 0.5Q��� $ 
Background�� �� *	BtnBack+ � ���� " ��.���  � �� �� *MyColors.ColorBack+��  � �� �� *MyColors.ColorText+��  � ��$ 0q���  � �� �� *MyColors.ColorText+��  � �� �� " ��.R��� $ border��$��Stretch=�$��Stretch=�#��z��._�� .+��� $	 
Background��#��z���� $��Center=�$��Center=�
 P�  �@J�IsMouseOver$
 True��. ��� $ 
Background�� �@J�Opacity$	 0.8Q��� $ 
Background�� �� *MyColors.ColorBack+ � ���� " ��.���  � ��$ Center=���  � ��$
 Left=��� " ��.���  � ��$ Center=���  � ��$ Right=�.��� -    �Name$ MainGrid�� � RowDefinitions �� $��30 ��� $��1* �.+�� -   $ 
ButtonGrid��$��0��.+��� $��Center=�$��Center=� �� Orientation$ 
Horizontal=�.+��� #��[ $��T������ *Lang.LCoinBuy+���� *ActualHeight+ �� ElementName$ 
ButtonGrid������ *ActualWidth+$ MainGrid�� �� 	Converter# [  ���� *CommandBidAskType+�� #��[ $��F������ *Lang.LCoinSell+���� *ActualHeight+$ 
ButtonGrid������ *ActualWidth+$ MainGrid��# [  ���� *CommandBidAskType+�� #��[	 $��O������ *Lang.LOrderList+���� *ActualHeight+$ 
ButtonGrid������ *ActualWidth+$ MainGrid��# [  ���� *CommandBidAskType+� $��1������ *	GridColor+.+�� y��� *	OrderView+# [  �� $��30 ��� $��* ��� $��30 �.+��� $��0��$��Right=�$	��Top=�$ 
Horizontal=�$��5q�.+�	� $��m������ *Lang.LMarket+���� *CommandIsMarket+	� $��l�� d� 	IsChecked$
 TrueY�$��15,0,0,0q����� *Lang.LLimit+���� *CommandIsMarket+�� $��1��$ Vertical=�$��Stretch=�$	��Top=�$��5,0,5,5q�.+�   OrderTextbox.COrderTextbox  $��0,2,0,2q�$��Stretch=�  OrderTextbox#��[    TextboxEnable$ False��   Prefix �� *Lang.LOrderPrice+   Subffix �� *AcitiveOrderCurrency+   	TextValue �� *ActiveOrderPrice+ �� Mode$ TwoWay=�   BorderColor �� *	BtnBorder+  $��0,2,0,2q�#��[  �� *Lang.LQuantity+ �� *	OrderCoin+ �� *
MarketSell+ �� *StrQuantity+$ TwoWay=� �� *	BtnBorder+  $��0,2,0,2q�#��[  �� *Lang.LPrice+ �� *
Currency+ �� *
IsMarket+# [  �� *
StrPrice+$ TwoWay=� �� *	BtnBorder+  $��0,2,0,2q�#��[  �� *Lang.LTotalPrice+ �� *
Currency+ �� *	MarketBuy+ �� *StrTotalPrice+$ TwoWay=� �� *	BtnBorder+�� #��[ $��2������ *	BtnText+{��� *ActiveOrder+���� *CommandCoinOrder+y��� *	OrderView+# [ � y��� *OrderListView+# [  �� $��30 ��� $��* �.+��� $��0��$��Right=�$	��Top=�$ 
Horizontal=�$��5q�.+�	� $
��wait��$
 TrueY����� *Lang.LNotConclusion+���� *CommandSelectedOrderList+	� $
��done��$��15,0,0,0q����� *Lang.LConclusion+���� *CommandSelectedOrderList+�� -    ��Name$ OrderListBox��$��1��$��Stretch=�$��Stretch=�$��0q�$��5,0,5,5q����� *	OrderList+���� .R�� ���� *ActualWidth+$ OrderListBox�� �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 � � ColumnDefinitions �� $��1* ��� $��1* ��� $��30 �.+��� $��0��$��2��$��5��$��Stretch=�$��Stretch=����� *(&DataContext.Lang.LChanceOrderBtnCancel+$ MainGrid��y��� *remaining_volume+# [ ���� * DataContext.CommandOrderCancel+$ MainGrid������ *uuid+�� #��[
 $��0��$��0������ *side+# [ �� #��[
 $��1��$��0������ *$"DataContext.Lang.LChanceMarketName+$ MainGrid���� #��[
 $��2��$��0������ *$"DataContext.Lang.LChanceOrderPrice+$ MainGrid���� #��[
 $��3��$��0������ *'%DataContext.Lang.LChanceOrderQuantity+$ MainGrid���� #��[
 $��4��$��0������ *,*DataContext.Lang.LChanceOrderNotConclusion+$ MainGrid���� #��[ $��0��$��1������ *
created_at+�� #��[ $��1��$��1������ *market+�� #��[ $��2��$��1������ *price+�� #��[ $��3��$��1������ *volume+�� #��[ $��4��$��1������ *remaining_volume+
   M S B A M L   `   `   `  ����	  Coins    Coins.Views.SideMenu   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      $��65��5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5       � 	Resources 5       �� MergedDictionaries �� Source �� 
TargetType   BtnIcon �� Property �� Value �� 
TargetType ��Name	 �� CornerRadius
 �� Triggers P� Property P� Value �� 
TargetName  MenuButtonHoverBackColor�� 5   
    5	      �� 5
      $E ?pack://application:,,,/PublicColor;component/DynamicColors.xaml?�6&   %  &	        �� 5      " ��6   .�5      ��  � ��$ 55��6*   �� 5       � ��$ 55��6+   �� 5       � ��$ Stretch=�68   �� 5       � ��$ Stretch=�66   �� 5       � �� 5      �� 5      " ��6*   .R�5      �� $ border��6%   $	 8��65   $��0q�6F   #��z��6Z   ._�5   "   �� $��Center=�63   $��Center=�6P   5      
 5      P� 5   "    �@J�IsMouseOver$
 True��6A   . �5   &   �� $ border��6-   # �  6W    � ��5   #   5      5      5       5!      .��5&      �   	MainColor#���  6    � RowDefinitions 5'   
   �� 5(      $��60 �6   �� 5)      $��5 �6   �� 5*      $��1* �6   �� 5+      $��5 �6   �� 5,      $��60 �6    � ColumnDefinitions 5.   
   �� 5/      $��1* �6   .+�52   
   �� $��0��6   $��0��6   #���   6.   ��6P   �� *Dashboard.BackColor+��6{   �� *CommandViewer+��6�   �� *	Dashboard+.��53      �� .��54      � .+�55      �� $��50��6@   $��50��6K   ��6   �� *Dashboard.Image+6K   56      57      58      � 5:   
   $��1��6   $��	10 2 10 2q�6     
TitleColor#���  6/   � 5;   
   $��2��6   .+�5<      �� 
������ $��0q�6S   $��Disabled=�6g   ��60   �� *
MarketList+��5=      �� 5>      " ��6   .�5?      ��  � ��$ Stretch=�6G   �� 5@       � ��$ 0q�6<   �� 5A       � ��$ 0q�63   �� 5B       � ��$ 0q�64   5C      ��5E      �� 5F      .R�5G      � .+�5H      �� #���   6%   ��6H   �� *	BackColor+��6i   �� *DataContext.CommandViewer+ �� RelativeSource ��  �� AncestorType" >���6�   �� .��5I   "   �� .��5J   &   � .+�5K   *   ��   MenuButtonTextColor#���  6P   ��64   �� *
MarketName+y�6�   �� *IsText+�� 5L   *   $��50��6I   $��50��6T   ��60   �� *Image+y�6`   �� *	IsImage+5M   '   5N   #   5O      5Q      5R      5U      5V      � 5W   
   $��3��6   $��	10 2 10 2q�6   #���  6/   �� 5Y   
   $��4��6   #���   6   ��6@   �� *Setting.BackColor+��6i   �� *CommandViewer+��6�   �� *	Setting+.��5Z      �� .��5[      � .+�5\      �� $��50��6=   $��50��6H   ��6   �� *Setting.Image+6H   5]      5^      5_      5`      5a      
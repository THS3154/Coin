   M S B A M L   `   `   `  ����	  Coins    Coins.Views.UpbitViewer   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5	      $��1* �6    � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      $
��auto �6   �� 5      $��1* �6   .+�5   
   �� $��1��6   $��0��6&   $��2��66   $��Stretch=�6K   $��Stretch=�6i   !  Prism.Regions.RegionManager   
RegionName$ 	CRBalance��6�   �� 5   
   $��2��6   $��0��6&   $ 
CRCoinList��67   �� 5   
   $��2��6   $��1��6&   $ 
CRCoinView��68   5      5      
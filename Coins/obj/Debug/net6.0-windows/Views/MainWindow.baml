   M S B A M L   `   `   `  ����C  ?TitleBar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:TitleBar;assembly=TitleBarTitleBar  5   	   	 Coins   Coins.Views.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   9TitleBar(clr-namespace:TitleBar;assembly=TitleBar   5   	   -   5          �Name$  MyWindow��5
   	   !  Prism.Mvvm.ViewModelLocator   AutoWireViewModel True5   	   
������  � AllowsTransparency$
 True��5   	    � WindowStyle$
 None=�5	   	    � 
ResizeMode$ 	CanResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   $	��800��5   !   $
��1400��6.   ��5   	   ��  �� Source# Z����5   	   �� # Z�� � Title 5   	   �� *Title+ � 	Resources 5      	 �� MergedDictionaries
 �� Source�� 5   
   	 5      �� 5      $A
 ;pack://application:,,,/TitleBar;component/TitleBarView.xaml?�6&   �� 5      $E
 ?pack://application:,,,/PublicColor;component/DynamicColors.xaml?�6&   %    5      '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      GlassFrameThickness$ 0q�6      CornerRadius$ 10��6/      CaptionHeight$ 0Q�6A   .��5      � $��0q�6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5       $��1* �6   .+�5#   
      TitleBar.TitleBarModel  $��0��6!      MyTitle#��[  60     
TitleColor   ContentColor# �  6]     	MainColor   	BackColor# �  5$   !     MainHoverColor   HoverBackColor# �  6I      
WindowForm 5#   �   %  System.Windows.Markup.Reference  *
MyWindow+   	TitleName 5$   {   �� *Title+�� 5(   
   $��1��6   $��1q�6   #���  62   
�������._�5)      �� -   5         ��Name$ 
RegionBase��5)      !  Prism.Regions.RegionManager   
RegionName$
 Main��62   5*      54      55      
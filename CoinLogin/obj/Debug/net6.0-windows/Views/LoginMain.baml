   M S B A M L   `   `   `  ����D  @CoinLogin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CoinLogin	CoinLogin  5          CoinLogin.Views.LoginMain   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5      %mystyleclr-namespace:CoinLogin  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5	      �� 5
      $@ :pack://application:,,,/CoinLogin;component/MyResource.xaml?�6&   �� 5      $E ?pack://application:,,,/PublicColor;component/DynamicColors.xaml?�6&   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $
��1.4* �6   .+�5   
   �� $��0��6   ��5      �� 5       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5      �  � Color$ #6BC59C��6#    � Offset$ 0Q�63   � 5      $ #6Ba59C��6#   $ 1Q�63   5      ._�5      � .+�5      �� .+�5      � 	 � Points	 �    -1��� ;X�����	#339CD5C26;   � 5       '	 �    ���f��9z �� ��
�����	#339CD5C26@   � 5!      '	 �    �� �� ��
�t� �������	#339CD5C26@   � 5"      #	 �   �������� ��������	#339CD5C26>   A� 5#      $��
120 20 0 0q�6   $��20��62   $��20��6=   ���	#339CD5C26I   A� 5$      $��
80 350 0 0q�6   $��20��62   $��20��6=   ���	#339CD5C26I   A� 5%      $��220 450 0 0q�6   $��20��63   $��20��6>   ���	#339CD5C26J   5&      �� 5(      $��Center=�6   .+�5)      �� $��회원가입��6       	titleText#��[  6,   �� 5*      $D��>회원가입을 원하시면 아래 버튼을 눌러주세요.��6      
normalText#��[ 6@   $��20 30q�6d   
 J� Opacity$	
 0.8Q�6s   �� 5+      $��회원가입��6    
 button#��[ 6N   ��6,   �� *CommandSingUp+6N   5,      5-      5.      �� 51   
   $��1��6   
�������._�52      �� -   5         ��Name$ 
RegionBase��52      !  Prism.Regions.RegionManager   
RegionName$ 	LoginView��62   53      54      55      
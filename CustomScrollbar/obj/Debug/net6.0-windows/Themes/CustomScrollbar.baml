   M S B A M L   `   `   `  ����J  FCustomScrollbar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CustomScrollbarCustomScrollbar  5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ScrollThumbs  �� 
TargetType �� Property �� Value �Name ��Name �� CornerRadius �� Triggers P� Property P� Value	 �� 
TargetName
 �� 
TargetType ]�Name ]� Thumb n�Name ]� IncreaseRepeatButton ��Name J� Opacity ]� DecreaseRepeatButton P� 
SourceName  ButtonSelectBrush  	DarkBrush �� Angle�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      'localclr-namespace:CustomScrollbar  5      %�	  &	        '�� �    �� 5      "  n�6!   .�5   
   ��  � �� 5      �� 5	      .R�5
      � $
 Grid��6   $��Stretch=�6)   $��Stretch=�6G   .+�5      �� $��Stretch=�6$   $��Stretch=�6B   $��6��6^   $
��Auto��6h   
������ 6v   �� 5      $ 
Rectangle1��6!   $ 5 5 5 5��65   $��Stretch=�6L   $��Stretch=�6j   $
��Auto��6�   $
��Auto��6�   #��z��6�   5       5      P� 5       �@�Tag$ 
Horizontal��61   . �5      �� $	 
Rectangle1��6%    � ��$
 Auto��6N   �� 5      $	 
Rectangle1��6%    � ��$ 7��6O   5      5      5      �� 5   
    � ��$ 4��6"   5      �� 5      "  ��6'   .�5   
   ��  �@��IsFlicksEnabled$ False��63   �� 5   
    � �� �	#AAA8341A6'   �� 5   
    � ��
 �����6'   �� 5   
    � ��$ 4��6"   �� 5    
    � �� 5!      �� 5"      "
 ��6"   .R�5#      �� $ GridRoot��6   $��6��6/   $ 5��6:   $��Right=�6L   ._�5$      � .+�5%      �� ���	#11333333$ 5��6<   ]� 5'      $ 
PART_Track��6$   $��0��68   $
��true��6E   $|�False��6`    5(   "   n� 5)   &   $ Thumb��6,   #��z��6;   #���   5*       5,   "   �� 5-   &   $ PageUp��63   $��ScrollBar.PageDownCommand��6C   $ 0Q�6g   $|�False��6s    5/   "   �� 50   &   $ PageDown��63   $��ScrollBar.PageUpCommand��6E   $ 0Q�6g   $|�False��6s   52      53      55       57      P� 58      $ Thumb��6"    �@J�IsMouseOver$
 true��6L   . �59      �� # �  6%   $	 Thumb��5:   -    � ��6@   5;      P� 5<      $ Thumb��6"    �@n�
IsDragging$
 true��6K   . �5=      �� # �  6%   $	 Thumb��6I    � ��6\   5>      P� 5@       � {�$ false��67   . �5A      �� $	 Thumb��6%    � y�$ 	Collapsed=�6N   5B      P� 5C       �@��Orientation$ 
Horizontal=�69   . �5D      �� $	 GridRoot��6%    �@�LayoutTransform 5E   "   �� 5F   &   $	 -90Q�66   5H      �� 5I      $	 
PART_Track��6%    �@�LayoutTransform 5J   "   �� 5K   &   $	 -90Q�66   5M      �� 5N       � ��$
 Auto��66   �� 5O       � ��$ 12��67   �� 5P      $	 Thumb��6%    �@�Tag$ 
Horizontal��6G   �� 5Q      $	 PageDown��6%    � ��$ ScrollBar.PageLeftCommand��6N   �� 5R      $	 PageUp��6%    � ��$  ScrollBar.PageRightCommand��6L   5S      5U      5W      5X      5Y      
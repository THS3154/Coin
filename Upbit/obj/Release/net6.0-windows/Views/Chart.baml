   M S B A M L   `   `   `  ���� @  <Upbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Upbit.ConverterUpbit.Converter  clr-namespace:UpbitUpbit      Upbit.Views.Chart   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  (convclr-namespace:Upbit.Converter  localclr-namespace:Upbit  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True � 	Resources -   'Upbit.Converter.MultipleHeightConverter   MultipleHeightConverter(   "Upbit.Converter.MultipleYConverter  MultipleYConverter+   %Upbit.Converter.CandleMarginConverter  CandleMarginConverter/  )Upbit.Converter.CandleGridXWidthConverter  CandleGridXWidthConverter)  #Upbit.Converter.ChartYAxisConverter  ChartYAxisConverter �� 
TargetType  	HoverLine �� Triggers P� Property P� Value �� Property �� Value  BtnTickType��%�   &	        &	      &	 
     &	      &	      &	      &	 f               �� " �� P�  �@J�IsMouseOver$
 True��. ���  � ��
 ��  ��� " ��.���  � ��$ 30����  � ��$ 30��.���  � RowDefinitions �� $��30 ��� $��30 ��� $��1* ��� ���� *GridVolumeHeight+�� $��30 �	 � ColumnDefinitions	 �� $��1* ��� $��80 �.+�� $��0��$��2��$��0��$��Right=�.+�� 	 �� $��1* ��� $	��160 �.+�� $��0��$��Right=�.+��� 
 �� Orientation$
 
Horizontal=�.+��� $
��Auto������ *	TestNum+�� $
��Auto������ *	PrintOp+�� $
��Auto������ *	PrintTp+�� $
��Auto������ *	PrintHp+�� $
��Auto������ *	PrintLp+�� $
��Auto������ *	PrintTime+� $��1��.+��� $
 
Horizontal=�.+��� -    ��Name$ ComboMin�� �� SelectedIndex$ 0��$��30������ *ListTickMin+y��� *VisibleComboBoxMin+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ SelectionChanged��*	  $Microsoft.Xaml.Behaviors.TriggerBase 	  Actions. 2
  ,Microsoft.Xaml.Behaviors.InvokeCommandAction
   
  Command �� *CommandSelectedMin+ 
  CommandParameter ��  �� ElementName$ ComboMin���� #��[ $��Center=�$��Center=�$��minutes������ *	Lang.LMin+���� *CommandSelectedTickType+�� #��[ $��Center=�$��Center=�$
��days������ *	Lang.LDay+���� *CommandSelectedTickType+�� #��[ $��Center=�$��Center=�$��weeks������ *
Lang.LWeek+���� *CommandSelectedTickType+�� #��[ $��Center=�$��Center=�$��months������ *Lang.LMonth+���� *CommandSelectedTickType+� -    �Name$ MyGridYSide��$��1��$��1��$��3��$
}�true��.+��� .+��� ���� *PrintChartYAxis+���� .R��� ���� " ��.���  �@��Left �� *X+��  �@��Top �� *Y+���� .R��� $
 
Horizontal=�$��Center=����� $ MyGridYSide�� �� Path$ ActualWidth�.+��  � Points �   ���  z ���  $� $��0��$��Center=����� *	BackColor+�� $��16�� �� Padding$ 5,0,0,0q�$��Center=�$��Stretch=����� $ MyGridYSide��$ ActualWidth����� *CursorValue+���� *Color+���� *	BackColor+� -   $ MyGridXSide��$��4��$��0��$��2��$
}�true��.+��� .+��� ���� *PrintChartXAxis+���� .R��� ���� " ��.���  �@��Left �� *X+��  �@��Top �� *Y+���� .R��� $
 
Horizontal=�$��Center=�$
��auto��.+��  �   ���  z ���  $� $��0��$��Center=����� *	BackColor+�� $
��auto��$��16��$ 5,0,0,0q�$��Center=�$��Stretch=����� *CursorValue+���� *Color+���� *	BackColor+� -   $ mygrid��$��2��$��0��$��Stretch=�$��Stretch=�
������    $ 	MouseDown��. 
   
  PassEventArgsToCommand$
 True�� �� *DataContext.CommandMouseDown+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType UserControl  $ MouseUp��. 
   �� *DataContext.CommandMouseUp+ �� $ FindAncestor=� UserControl �� $ mygrid��  $ 	MouseMove��. 
  $
 True�� �� *DataContext.CommandMouseMove+ �� $ FindAncestor=� UserControl  $ 
MouseLeave��. 
   �� *DataContext.CommandMouseLeave+ �� $ FindAncestor=� UserControl �� $ mygrid��  $ 
MouseWheel��. 
  $
 True�� �� *DataContext.CommandMouseWheel+ �� $ FindAncestor=� UserControl.+��� -    ��Name$ mycanvas��$
}�True��.+��� ���� *	HorLine+���� .R��� ���� .R��� #��[ 	 �� X1 �� *FromX+	 �� X2 �� $ mycanvas��$ ActualWidth�	 �� Y1 �� *FromY+	 �� Y2 �� *ToY+���� *	LineColor+���� *LineThickness+�� ���� *	VerLine+���� .R��� ���� .R��� #��[  �� *FromX+ �� *ToX+ �� *FromY+ �� $ mycanvas��$ ActualHeight����� *	LineColor+���� *LineThickness+�� ���� *PrintChartXAxis+���� .R��� ���� .R��� $��1�� �� *X+ �� *X+ �� *Y+ �� $ mycanvas��$ ActualHeight����� *	BackColor+�� ���� *	PrintTick+���� .R��� ���� " ��.���  �@��Left `�   `� 	Converter#  [ .1��� $ DataContext.TickWidth� �� $ FindAncestor=� UserControl�� $ X���  �@��Top `� #  [ .1��� $ DataContext.MaxPrice� �� $ FindAncestor=� UserControl�� $ HP��� $! DataContext.ChartTickHeight� �� $ FindAncestor=� UserControl���� .R�� .+��� $	��Top=�$��0������ *DataContext.TickWidth+ �� $ FindAncestor=� UserControl���� *Colors+��`� #  [  .1��� $! DataContext.ChartTickHeight� �� $ FindAncestor=� UserControl�� $ Candle���`� #  [ .1��� $ CandleStartPoint��� $! DataContext.ChartTickHeight� �� $ FindAncestor=� UserControl�� $��1������ *Colors+��`� #  [  .1��� $! DataContext.ChartTickHeight� �� $ FindAncestor=� UserControl�� $
 Tail��� ���� *PrintChartYAxis+���� .R��� ���� .R��� $��1�� �� *X+ �� $ mycanvas��$ ActualWidth� �� *Y+! �� 	Converter#! [  �� *Y+#! [ ���� *	BackColor+� $��3��.+��� -   $ myvolumecanvas��$
}�True��.+��� ���� *	HorLine+���� .R��� ���� .R��� #��[  �� *FromX+ �� $ myvolumecanvas��$ ActualWidth� �� *FromY+ �� *ToY+���� *	LineColor+���� *LineThickness+�� ���� *	VerLine+���� .R��� ���� .R��� #��[  �� *FromX+ �� *ToX+ �� *FromY+ �� $ myvolumecanvas��$ ActualHeight����� *	LineColor+���� *LineThickness+�� ���� *	PrintTick+���� .R��� ���� " ��.���  �@��Left `� #  [ .1��� $ DataContext.TickWidth� �� $ FindAncestor=� UserControl�� $ X���  �@��Top `� #  [ .1��� $ DataContext.MaxVolume� �� $ FindAncestor=� UserControl�� $ Volume��� $" DataContext.VolumeTickHeight� �� $ FindAncestor=� UserControl���� .R�� .+��� $	��Top=�$��0������ *DataContext.TickWidth+ �� $ FindAncestor=� UserControl���� *Colors+��`� #  [  .1��� $" DataContext.VolumeTickHeight� �� $ FindAncestor=� UserControl�� $ Volume��� ���� *PrintChartXAxis+���� .R��� ���� .R��� $��1�� �� *X+ �� *X+ �� *Y+ �� $ mycanvas��$ ActualHeight����� *	BackColor+   $ SizeChanged��. 
   �� *CommandGridLines+ �� $ mygrid��  $ Loaded��. 
   �� *CommandGridLines+ �� $ mygrid��
   M S B A M L   `   `   `  ����B  >Options, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:OptionsOptions  5   	   b ^Syncfusion.SfChart.WPF, Version=15.2460.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89eGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPFSyncfusion.UI.Xaml.Charts 5   	       Options.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Options     5   	   ^
syncfusionGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPF     5   	   d `Syncfusion.OlapChart.WPF, Version=15.2460.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=15.2350.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89i	 eSyncfusion.RichTextBoxAdv.WPF, Version=15.2460.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89e
 aSyncfusion.OlapShared.WPF, Version=15.2460.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.Wpf, Version=15.2460.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=15.2350.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89e aSyncfusion.SfCellGrid.WPF, Version=15.2460.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89h dSyncfusion.SfSpreadsheet.WPF, Version=15.2460.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89i eSyncfusion.RichTextRibbon.WPF, Version=15.2460.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Olap.Base, Version=15.2460.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Chart.Wpf, Version=15.2460.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=15.2460.0.43, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89Echart!http://schemas.syncfusion.com/wpf   	  
         5   	   k gTelerik.Windows.Controls.Input, Version=2016.2.613.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2016.2.613.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Atelerik1http://schemas.telerik.com/2008/xaml/presentation   5	   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  iOption��5   	   $	��800��6   $
��1350��6&   ���#1C1C1C.��5      � .+�5   
   1  +Telerik.Windows.Controls.RadAutoCompleteBox  -   5          Name$ 	symbolBox��5   %   $
��Left=�68   $��	10,10,0,0q�6S   $	��Top=�6f   $	��503��6~   $��42��6�      SelectionMode$ Multiple=�5   %    J� RenderTransformOrigin$ 0.568,5.479&�6?   $��24(�6c   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dpStart��5      
�������   DisplayDateEnd$ 
2017-12-31X�5         DisplayDateStart$ 
2017-01-03X�61      DisplayDate$ 
2017-01-03��6O   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Future Date��5   !   $��608,22,544,717q�6N   5        5   
   -   5        $ dpExpiration��5      
�������$ 
2017-12-31X�5      $ 
2017-01-03X�61   $ 
2017-01-03��6O   $ Expiration Date��5   !   $��837,22,313,717q�6R   5%      � 5'   
   $��10,225,10,139q�6   .+�5(      '  !Syncfusion.UI.Xaml.Charts.SfChart  -   5        	  Name$	 	mainChart��5(      $��Right=�6/   ���#262626$��1��5)   -   )  #Syncfusion.UI.Xaml.Charts.ChartBase
   Palette$
 Metro=�5*         PrimaryAxis 5,      4  .Syncfusion.UI.Xaml.Charts.DateTimeCategoryAxis  5-      -   5          Name$ primaryMain��5-   1   $��16(�6F   )  #Syncfusion.UI.Xaml.Charts.ChartAxis   Header$ Dates��6T      ShowGridLines .  
����� �   EdgeLabelsDrawingMode$	 Fit=�5.   O   $
��Bold"�5/   %   $��0q�67   $��27��6B      LabelFormat$ MMM dd��6N      LabelsIntersectAction$
 Auto=�51   �   /  )Syncfusion.UI.Xaml.Charts.ChartAxisBase2D   ZoomPosition 50   1   ��  �� ElementName$ 	navigator�� �� Path$ ZoomPosition� �� Mode$ TwoWay=�   
ZoomFactor 51   =   �� $ 	navigator��$ 
ZoomFactor�$ TwoWay=�6�      SecondaryAxis 53      -	  'Syncfusion.UI.Xaml.Charts.NumericalAxis	  54      -   5         	 Name$ secondaryMain��54   *   $��10(�6A   $ Secondary Axis��6O   $
��Bold"�55   &   
����� �68   58      59      8
  2Syncfusion.UI.Xaml.Charts.SfDateTimeRangeNavigator
  5;   
   -   5         
 Name$ 	navigator��5;   )   $
��Left=�6<   $��
43,660,0,0q�6W   $	��Top=�6k   $��99��6�   $
��1289��6�    
  ShowGridLines .  0  *Syncfusion.UI.Xaml.Charts.SfRangeNavigator   ScrollbarVisibility$ Hidden=�6�      Content 5<      /  )Syncfusion.UI.Xaml.Charts.SfLineSparkline  5=      -   5          Name$ navigatorChart��5=   (   5?      � 5@   
   $��10,104,10,544q�6    � ColumnDefinitions 5A      �� 5B      $
��116* �6#   �� 5C      $	��31* �6#   .+�5E      �� $��2��6   .��5F      � $��0,0,0,0q�6   .+�5G      �� -	   5         ��Name$ lblInfo��5G   R   $��Hidden=�6    $
��Left=�6c   $
��Wrap=�5H      $��Bottom=�60   $
��1324��6K   ���	#FF002FF5$��22(�6o   5J      5K      5L      5M      �� 5N   
   -
   5          ��Name$  btnQuery��5N      $��Absolute��6#   $
��Left=�66   $��1072,31,0,0q�6Q   $	��Top=�6f   $	��102��6~   6�   �� 5O   
   -   5        $  btnQueryPercent��5O      $��Percent��6*   $
��Left=�6<   $��1224,31,0,0q�6W   $	��Top=�6l   $��90��6�   6�   5S      5T      
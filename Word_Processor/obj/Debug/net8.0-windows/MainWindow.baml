   M S B A M L   `   `   `  ����  Word_Processor    Word_Processor.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Word Processor��5   	   $	��600��6    $	��800��6-   .��5      ]� .+�5	      o� $	��Top=�6	   .@�5
      m� $��_File��6   .@�5      m� $��_Save��6    m� Command 6   �� *SaveCommand+m� 5      $��_Load��6    6   �� *LoadCommand+�� 5      m� 5      $��
_Auto-Save��6    m� IsCheckable$
 True��5       m� 	IsChecked 5      �� *IsAutoSaveEnabled+ �� Mode$ TwoWay=�5      m� 5      $��Theme��6   .@�5      m� $��_ChangeTheme��6   .@�5      m� -   5        $��	Dark Mode��5      6$   m� 5      -   5        $��
Light Mode��5      6%   5      5      5      �� 5       �� Padding$ 10q�6      StatusBarBrush#��[  6   ._�5      � $��0q�6
    � RowDefinitions 5      �� 5      $��* �6   �� 5       $
��Auto �6   .+�5$      �� $��0��6    �� AcceptsReturn$
 True��5%       �� TextWrapping$
 Wrap=�5&      "	 �� VerticalScrollBarVisibility$
	 Auto=�5'      $��16(�5(      ��5)      �� *DocumentText+
 �� UpdateSourceTrigger$
 PropertyChanged=��� 5,      $��1��6   #��[  5-   	   $ 5q�5.   	   ._�5/      ��  �� Orientation$ 
Horizontal=�50      $��Right=�51      $��0,10,0,0q�52      .+�53      �� $��Word Count:��6   $
��Bold"�6%   �� 54      $	��5,0q�6-   ��6   �� *	WordCount+6-   �� 56      $	��5,0q�58      ��56      �� *	LastSaved+ �� StringFormat$ Last saved: {0:T}�� � ToolTip 57      �� *LastSavedTooltip+58      �� 5:      $��16(�5;      $	��5,0q�5=      ��5:      �� *SaveStatusIcon+ 5<      �� *SaveStatusMessage+5=      5@      5B      5C      5D      5E      5F      
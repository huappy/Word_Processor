   M S B A M L   `   `   `  ����   Word_Processor    Word_Processor.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Word Processor��$	��600��$	��800��.��]� .+�o� $	��Top=�.@�m� $��_File��.@�m� $��_Save�� m� Command �� *SaveCommand+m� $��_Load�� �� *LoadCommand+�� m� $��
_Auto-Save�� m� IsCheckable$
 True�� m� 	IsChecked �� *IsAutoSaveEnabled+ �� Mode$ TwoWay=���  �� Padding$ 10q�   StatusBarBrush#��[  ._�� $��0q� � RowDefinitions �� $��* ��� $
��Auto �.+��� $��0�� �� AcceptsReturn$
 True�� �� TextWrapping$
 Wrap=�"	 �� VerticalScrollBarVisibility$
	 Auto=�$��16(����� *DocumentText+
 �� UpdateSourceTrigger$
 PropertyChanged=��� $��1��#��[  $ 5q�._���  �� Orientation$ 
Horizontal=�$��Right=�$��0,10,0,0q�.+��� $��Word Count:��$
��Bold"��� $	��5,0q����� *	WordCount+�� $	��5,0q����� *	LastSaved+ �� StringFormat$ Last saved: {0:T}�� � ToolTip �� *LastSavedTooltip+�� $��16(�$	��5,0q����� *SaveStatusIcon+ �� *SaveStatusMessage+
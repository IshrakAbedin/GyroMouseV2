   M S B A M L   `   `   `  ����F  BGyroMouseV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:GyroMouse	GyroMouse  5   	       GyroMouse.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:GyroMouse     5   	     � Title$  GyroMouseV2��5   	   $	��400��6   $	��600��6*    � 
ResizeMode$ CanMinimize=�66    � WindowStartupLocation$ CenterScreen=�6O   .��5	      � ��5
   
   �� 5       �� ImageSource$&  Resource/GyroMouseBackground.png��6    � RowDefinitions 5   
   �� 5      $
��169* �6   �� 5      $	��16* �6   .+�5   
   �� -   5         ��Name$ Button_Connect��5      $��CONNECT��6)   $��Right=�6;   $��0,0,200.6,264.4q�6W   $��Bottom=�6p   $	��134��6�   $��47��6�   $��Franklin Gothic Heavy)�6�   $
��Bold"�6�   $��20(�6�   ���	#7F00A8FF
�������
������� � ToolTip$ Click to connect��6A  �� 5   
   -   5        $ Button_Disconnect��5      $��
DISCONNECT��6,   $
��Left=�6A   $��
251,80,0,0q�6\   $	��Top=�6p   $	��134��6�   $��47��6�   $��Franklin Gothic Heavy)�6�   $
��Bold"�6�   $��20(�6�    J� RenderTransformOrigin$ 2.021,1.271&�6  
����������	#7EFF3700
�������$ Click to disconnect��6e  �� 5   
   -   5         ��Name$ CheckBox_YInverted��5      $��Invert Y-Axis��6/   $
��Left=�6G   $��
426,64,0,0q�6b   $	��Top=�6v   $��18��6�   $	��134��6�   $��Franklin Gothic Heavy)�6�   $
��Bold"�6  $��18(�6)  ���	#FF8ECFE5���	#FFFFC000$ Invert vertical movement��6|  ��67  Z� 6|  �� 5   
   -   5        $ Button_Refresh��5      $��REFRESH��6)   $��Right=�6;   $��0,0,405.6,0.4q�6W   $��Bottom=�6n   $	��168��6�   $��39��6�   $��Franklin Gothic Heavy)�6�   $
��Bold"�6�   $��18(�6�   
����������	#9CFF9A00
�������$ Refresh port list��6?  �� 5   
   -   5        	 ��Name$	 ListBox_Ports��5      $
��Left=�6)   $	��222��6D   $	��Top=�6Q   $	��170��6i   $��Franklin Gothic Heavy)�6u   $��24(�6�   $��	10,62,0,0q�6�   ���	#FFFFC400���	#CC323232$��Italic$�6�   ��6�   Z� 6�   �� 5   
   $��Ports��6   $��Right=�6    $��0,10,436.6,0q�6<   $	��Top=�6R   $��47��6j   $	��105��6v   $��Franklin Gothic Heavy)�6�   $��36(�6�   ���	#FFFF9A00���	#FFB6DCF6$ COM port list��6�   �� 5   
   $��5,6.6,4.6,5.4q�6   $��1��6.   $
��Auto=�6;   
���   �
���   �.��5      �� -   5        
 ��Name$
 TextBlock_Log��5      $
��Wrap=�6/   $�� ��6C   $��1,1,1,1q�6K   $��Italic$�6\   ��5      �� 5       �� EndPoint$ 0.5,1&�6*    �� 
StartPoint$ 0.5,0&�6;   .��5      �  � Color$ 	#FF515151��6'    � Offset$ 0Q�69   � 5      $ 	#FF262626��6'   $ 1Q�69   5      5      5       �� 5!   
   -   5        $ CheckBox_Safety��5!      $��Safety��6,   $
��Left=�6=   $��
426,87,0,0q�6X   $	��Top=�6l   $��23��6�   $��78��6�   $��Franklin Gothic Heavy)�6�   $��18(�6  ���	#FF8ECFE5���	#FFFFC000$E ?Turn on and take mouse pointer to top left corner to disconnect��6Y  ��6  Z� 6Y  �� 5"   
   -   5         ��Name$ Rectangle_Status��5"      ���	#7F8B0000$
��Left=�6?   $��10��6Z   $��259,0,0,0.4q�6f   $��Bottom=�6{   $	��308��6�   $ 14.292,5.45&�6�   $* $Blue: Connected | Gray: Disconnected��6�   ��5#      �� 5$      $ 0.5,1&�6&   $ 0.5,0&�67   .��5%      � $ 	#FF00544B��6#   $ 1Q�65   � 5&      $ 	#FFA0D4EF��6#   5'      5)      �� 5*   
   -	   5        $ Button_Help��5*      $��!��6&   $
��Left=�62   $��312,211,0,0q�6M   $	��Top=�6b   $��48��6z   $��43��6�   $��Franklin Gothic Heavy)�6�   $
��Bold"�6�   $��36(�6�   $ 2.021,1.271&�6�   
����������	#7FFF9A00
�������$
 Help��6P  �� 5+   
   -
   5        $ Button_About��5+      $��?��6'   $
��Left=�63   $��259,211,0,0q�6N   $	��Top=�6c   $��48��6{   $��43��6�   $��Franklin Gothic Heavy)�6�   $
��Bold"�6�   $��36(�6�   $ 2.021,1.271&�6�   
����������	#7FFF9A00
�������$ About��6R  �� 5,   
   -   5        $ CheckBox_ClickMode��5,      $��
Click Mode��6/   $
��Left=�6D   $��
426,41,0,0q�6_   $	��Top=�6s   $��18��6�   $	��118��6�   $��Franklin Gothic Heavy)�6�   $
��Bold"�6  $��18(�6&  ���	#FF8ECFE5���	#FFFFC000$ Invert vertical movement��6y  ��64  Z� 6y  �� 5-   
   ���	#7FFF9A00$
��Left=�6%   $��69��6@   $��
423,39,0,0q�6L   $	��Top=�6`   $	��135��6x    K� ZIndex$	 -10��6�   ���	#FFFFC000 J� Effect 5.      3  -System.Windows.Media.Effects.DropShadowEffect  5/      51      53      54      
   M S B A M L   `   `   `  ����M  ITaxpayerAlerter.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:TaxpayerAlerter.UI.ViewModelsTaxpayerAlerter.UI.ViewModels  5   	   )    #TaxpayerAlerter.UI.Views.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   :vm+clr-namespace:TaxpayerAlerter.UI.ViewModels     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��450��6)    � WindowStartupLocation$ CenterScreen=�65    � 
ResizeMode$ CanMinimize=�6Z    � DataContext 5	      1   +TaxpayerAlerter.UI.ViewModels.MainViewModel  5
   
   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      �� 5      .+�5   
   �� $��0��6   $��Выберите дату��5      $��Center=�5      $��Center=�5      $��Roboto Light)�5      $��30(�5      $��40q�5      (  "System.Windows.Controls.DatePicker  5   
   $��1��6   $��Roboto Light)�5      $��20(�5      $��53, 55q�5         IsTodayHighlighted$
 True��5      $��Center=�5         DisplayDateStart 
2023-01-015       
�������   SelectedDate 5      �� *SelectedDate+   DisplayDateEnd 5!      �� *	DateEnd+5"      �� 5#   
   $��2��6   $
��ОК��5$      $��Roboto Light)�5%      $��20(�5&      $��80, 40q�5'      	 � Cursor$
	 Hand��5)      
���������5(      �� *OKButtonClick+5*      5+      5,      
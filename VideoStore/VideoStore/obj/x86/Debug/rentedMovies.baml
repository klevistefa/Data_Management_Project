   M S B A M L   `   `   `  ����  
VideoStore    VideoStore.rentedMovies   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  rentedMovies��5      .��5	      � -   5        .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgRentedFilms��5
         
IsReadOnly .    AutoGenerateColumns .  $
��Left=�6X   $��	10,42,0,0q�6s   $	��Top=�6�   $	��760��6�   $	��340��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Member ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   ��  �� Path$ 	member_id�6]     5      $ 
First Name��6%    69   �� $ member_first_name�6f     5      $ 	Last Name��6%    68   �� $ member_last_name�6d     5      $ Film ID��6%    66   �� $ film_id�6Y     5      $ Title��6%    64   �� $ 
film_title�6Z     5      $ Stock��6%    64   �� $ 
film_stock�6Z     5      $ 	Rent Date��6%    68   �� $ 	rent_date� �� StringFormat$ d��6m     5      $ Return Date��6%    6:   �� $ return_date�$ d��6q   5      �� 5   
   $��Rented Movies��6   $
��Left=�6(   $��
321,10,0,0q�6C   $	��Top=�6W   $��18(�6o   �� 5   
   $��GroupBox��6   $
��Left=�6%   $��
10,387,0,0q�6@   $	��Top=�6T   $	��153��6l   $	��760��6y   .��5      � .+�5      �� $��	Member ID��6   $
��Left=�6,   $��	10,10,0,0q�6G   $	��Top=�6Z   �� 5      $��
First Name��6   $
��Left=�6-   $��	10,42,0,0q�6H   $	��Top=�6[   �� 5      $��First Title��6   $
��Left=�6.   $��
10,101,0,0q�6I   $	��Top=�6]   �� 5      $��Film ID��6   $
��Left=�6*   $��	10,71,0,0q�6E   $	��Top=�6X   �� 5      -   5        	 ��Name$	 txtMemberID��5   ,   
 �� 
IsReadOnly
 . $
��Left=�6A   $��23��6\   $��95,7,0,0q�6h    �� TextWrapping$
 Wrap=�6z   $�� ��6�   $	��Top=�6�   $	��150��6�   �� 5      -   5        $	 txtFirstName��5   ,   
 . $
��Left=�6B   $��23��6]   $��	95,37,0,0q�6i   $
 Wrap=�6|   $�� ��6�   $	��Top=�6�   $	��150��6�   �� 5      -   5        $	 	txtFilmID��5   ,   
 . $
��Left=�6?   $��23��6Z   $��	95,67,0,0q�6f   $
 Wrap=�6y   $�� ��6�   $	��Top=�6�   $	��150��6�   �� 5       -   5        $	 txtTitle��5    ,   
 . $
��Left=�6>   $��23��6Y   $��	95,98,0,0q�6e   $
 Wrap=�6x   $�� ��6�   $	��Top=�6�   $	��150��6�   �� 5!      $��	Rent Date��6   $
��Left=�6,   $��
280,10,0,0q�6G   $	��Top=�6[   �� 5"      $��Return Date��6   $
��Left=�6.   $��
280,42,0,0q�6I   $	��Top=�6]   �� 5#      -   5        $	 txtRentDate��5#   ,   
 . $
��Left=�6A   $��23��6\   $��	369,7,0,0q�6h   $
 Wrap=�6{   $�� ��6�   $	��Top=�6�   $	��150��6�   �� 5$      -   5        $	 txtReturnDate��5$   ,   
 . $
��Left=�6C   $��23��6^   $��
369,37,0,0q�6j   $
 Wrap=�6~   $�� ��6�   $	��Top=�6�   $	��150��6�   �� 5%      -	   5         ��Name$ 	btnReturn��5%      $��Return Movie��6,   $
��Left=�6C   $��	555,7,0,0q�6^   $	��Top=�6q   $	��183��6�   �� 5&      -
   5        $ btnPostpone��5&      $��Postpone��6.   $
��Left=�6A   $��
555,42,0,0q�6\   $	��Top=�6p   $	��183��6�   �� 5'      -   5         ��Name$ lblError��5'      
���  ��$�� ��6;   $
��Left=�6F   $��
555,85,0,0q�6a   $	��Top=�6u   $	��183��6�   $��23��6�   �� 5(      -   5        $ 
lblSuccess��5(      
��� � �$�� ��6?   $
��Left=�6J   $��
555,85,0,0q�6e   $	��Top=�6y   $	��183��6�   $��23��6�   5*      5+      5-      5.      
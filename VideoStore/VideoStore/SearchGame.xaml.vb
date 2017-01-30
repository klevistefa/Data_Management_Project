Imports System.Data

Class SearchGame

    Private db As DBTransaction
    Private dTableGames As DataTable

    'Sub that filters the list depending on what you write in the textbox
    Private Sub txtFilter_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtFilter.TextChanged
        dTableGames.DefaultView.RowFilter = "game_title LIKE '*" & txtFilter.Text.ToString() & "*'"
    End Sub

    'Sub that fills the data grid with all the games in the database
    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        db = New DBTransaction()
        dTableGames = New DataTable()

        dTableGames = db.getAllGames()
        dgGames.ItemsSource = dTableGames.DefaultView
    End Sub
End Class

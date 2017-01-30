Imports System.Data

Class SearchMovie

    Private db As DBTransaction
    Private dTableMovies As DataTable

    'Sub that filters the list depending on what you write in the textbox
    Private Sub txtFilter_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtFilter.TextChanged
        dTableMovies.DefaultView.RowFilter = "film_title LIKE '*" & txtFilter.Text.ToString() & "*'"
    End Sub

    'Sub that fills the data grid with all the movies in the database
    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        db = New DBTransaction()
        dTableMovies = New DataTable()

        dTableMovies = db.getAllMovies()
        dgMovies.ItemsSource = dTableMovies.DefaultView
    End Sub
End Class

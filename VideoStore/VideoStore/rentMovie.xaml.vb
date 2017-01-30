Imports System.Data

Class returnMovie

    Private db As DBTransaction
    Private dTableMembers As DataTable
    Private dTableMovies As DataTable

    'Sub that fills two tables with data from the database when the grid is loaded. One with the movies and the other with the members
    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        db = New DBTransaction()
        dTableMembers = New DataTable()
        dTableMovies = New DataTable()

        dTableMovies = db.getAllMovies()
        dTableMembers = db.getAllMembers()

        dgMovies.ItemsSource = dTableMovies.DefaultView
        dgMembers.ItemsSource = dTableMembers.DefaultView

        lblError.Content = ""
        lblSuccess.Content = ""
    End Sub

    'Sub that fills the fields for the movie with the selected movie
    Private Sub dgMovies_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dgMovies.MouseDoubleClick
        Dim dr As DataRowView = dgMovies.SelectedItem
        txtFilmId.Text = dr.Item("film_id")
        txtFilmTitle.Text = dr.Item("film_title")
        txtFilmStock.Text = dr.Item("film_stock")
        lblError.Content = ""
        lblSuccess.Content = ""
    End Sub

    'Sub that fills the fields for the members with the selected member
    Private Sub dgMembers_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dgMembers.MouseDoubleClick
        Dim dr As DataRowView = dgMembers.SelectedItem
        txtMemberID.Text = dr.Item("member_id")
        txtFirstName.Text = dr.Item("member_first_name")

        Dim result As Integer = 0

        result = db.getRentedMoviesFromMemberId(CType(txtMemberID.Text.ToString, Integer))
        txtRented.Text = result

        lblError.Content = ""
        lblSuccess.Content = ""
    End Sub

    'Event handler for text changing in the textbox which handles data table filtering
    Private Sub txtMovieFilter_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtMovieFilter.TextChanged
        dTableMovies.DefaultView.RowFilter = "film_title LIKE '*" & txtMovieFilter.Text & "*'"
    End Sub

    'Event handler for text changing in the textbox which handles data table filtering
    Private Sub txtMemberFilter_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtMemberFilter.TextChanged
        dTableMembers.DefaultView.RowFilter = "member_first_name LIKE '*" & txtMemberFilter.Text & "*'"
    End Sub

    'Button that's used to rent a movie to a member
    Private Sub btnRent_Click(sender As Object, e As RoutedEventArgs) Handles btnRent.Click
        If txtMemberID.Text.Length <> 0 And txtFilmId.Text.Length <> 0 Then
            If CType(txtRented.Text, Integer) < 2 Then
                If CType(txtFilmStock.Text.ToString, Integer) <> 0 Then
                    Dim fID As Integer = CType(txtFilmId.Text, Integer)
                    Dim mID As Integer = CType(txtMemberID.Text, Integer)
                    Dim pr As String = txtPrice.Text.ToString()
                    pr = pr.Substring(0, pr.IndexOf("L"))
                    Dim price As Double = CType(pr, Double)
                    Dim rs As Integer = db.rentMovie(fID, Mid, price)
                    Dim rs1 As Integer = db.decrementFilmStock(fID)
                    If rs = 1 And rs1 = 1 Then
                        dTableMovies = db.getAllMovies()
                        dgMovies.ItemsSource = dTableMovies.DefaultView
                        lblSuccess.Content = "OK"
                        lblError.Content = ""
                        clearFields()
                    Else
                        lblError.Content = "Try again later"
                        lblSuccess.Content = ""
                    End If
                Else
                    lblError.Content = "No Stock"
                    lblSuccess.Content = ""
                End If
                
            Else
                lblError.Content = "Limit rents"
                lblSuccess.Content = ""
            End If
        Else
            lblError.Content = "Film & Member?"
            lblSuccess.Content = ""
        End If

    End Sub

    'Sub that's used to clear the fields
    Private Sub clearFields()
        txtFilmStock.Text = ""
        txtFilmTitle.Text = ""
        txtFirstName.Text = ""
        txtMemberID.Text = ""
        txtRented.Text = ""
        txtFilmId.Text = ""
    End Sub
End Class

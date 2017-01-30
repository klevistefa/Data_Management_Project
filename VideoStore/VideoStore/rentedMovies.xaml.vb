Imports System.Data
Imports System.Windows.Threading

Class rentedMovies

    Private db As DBTransaction
    Private dTableRentedFilms As DataTable

    'Sub that fills the data grid with the list of all rented movies when the grid is loaded
    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        db = New DBTransaction()
        dTableRentedFilms = New DataTable()

        dTableRentedFilms = db.getAllRentedMovies()
        dgRentedFilms.ItemsSource = dTableRentedFilms.DefaultView

        lblError.Content = ""
        lblSuccess.Content = ""
        clearFields()
    End Sub

    'Sub that fills the fields with the data of the selected movie
    Private Sub dgRentedFilms_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dgRentedFilms.MouseDoubleClick
        Dim dr As DataRowView = dgRentedFilms.SelectedItem
        txtFilmID.Text = dr.Item("film_id")
        txtFirstName.Text = dr.Item("member_first_name")
        txtMemberID.Text = dr.Item("member_id")
        txtRentDate.Text = dr.Item("rent_date")
        txtReturnDate.Text = dr.Item("return_date")
        txtTitle.Text = dr.Item("film_title")

        lblError.Content = ""
        lblSuccess.Content = ""
    End Sub

    'Sub that clears the fields
    Private Sub clearFields()
        txtFilmID.Text = ""
        txtFirstName.Text = ""
        txtMemberID.Text = ""
        txtRentDate.Text = ""
        txtReturnDate.Text = ""
        txtTitle.Text = ""
    End Sub

    'Button that returns a movie 
    Private Sub btnReturn_Click(sender As Object, e As RoutedEventArgs) Handles btnReturn.Click
        If txtFilmID.Text.Length <> 0 And txtMemberID.Text.Length <> 0 Then
            Dim fID As Integer = CType(txtFilmID.Text, Integer)
            Dim mID As Integer = CType(txtMemberID.Text, Integer)
            Dim rDate As Date = CType(txtRentDate.Text, Date)

            Dim rs As Integer = db.returnMovie(fID, mID, rDate)
            Dim rs1 As Integer = db.incrementMovieStock(fID)

            If rs > 0 Then
                dgRentedFilms.ItemsSource = Nothing
                dTableRentedFilms.Dispose()
                dTableRentedFilms = db.getAllRentedMovies()
                dgRentedFilms.ItemsSource = dTableRentedFilms.DefaultView
                lblSuccess.Content = "Returned"
                lblError.Content = ""
                clearFields()
            Else
                lblError.Content = "Please try again."
                lblSuccess.Content = ""
            End If
        Else
            lblError.Content = "Please select a rent."
            lblSuccess.Content = ""
        End If
    End Sub

    'Button that postpones the movie's deadline with one day
    Private Sub btnPostpone_Click(sender As Object, e As RoutedEventArgs) Handles btnPostpone.Click
        If txtFilmID.Text.Length <> 0 And txtMemberID.Text.Length <> 0 Then
            Dim fID As Integer = CType(txtFilmID.Text, Integer)
            Dim mID As Integer = CType(txtMemberID.Text, Integer)
            Dim rDate As Date = CType(txtRentDate.Text, Date)

            Dim rs As Integer = db.postponeRent(fID, mID, rDate)

            If rs > 0 Then
                dTableRentedFilms.Clear()
                dTableRentedFilms = db.getAllRentedMovies()
                dgRentedFilms.ItemsSource = dTableRentedFilms.DefaultView
                lblSuccess.Content = "Postponed"
                lblError.Content = ""
                clearFields()
            Else
                lblError.Content = "Please try again"
                lblSuccess.Content = ""
            End If
        Else
            lblError.Content = "Please select a rent"
            lblSuccess.Content = ""
        End If
    End Sub
End Class

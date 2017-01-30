Imports System.Data

Class sellMovie

    Private db As DBTransaction
    Private dtableMovies As DataTable
    Private dtableMembers As DataTable

    'Button that clears all the fields
    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        clearFields()
    End Sub

    'Sub that clears all the fields
    Private Sub clearFields()
        txtFilmId.Text = ""
        txtCost.Text = ""
        txtDiscount.Text = ""
        txtFilmFilter.Text = ""
        txtMemberId.Text = ""
        txtMoviesBought.Text = ""
        txtPrice.Text = ""
        txtRegistrationDate.Text = ""
        txtStock.Text = ""
    End Sub

    'Sub that fills two data grids when the grid is loaded. One with the movies in the database and the other with the members in the database
    Private Sub Grid1_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Grid1.Loaded
        db = New DBTransaction()
        dtableMovies = New DataTable()

        dtableMovies = db.getAllMovies()
        dgMovies.ItemsSource = dtableMovies.DefaultView

        dtableMembers = New DataTable()
        dtableMembers = db.getAllMembers()
        dgMembers.ItemsSource = dtableMembers.DefaultView

        lblError2.Content = ""
        lblSuccess.Content = ""
    End Sub

    'Sub that filters the list depending on what you write in the textbox
    Private Sub txtFilmFilter_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles txtFilmFilter.TextChanged
        dtableMovies.DefaultView.RowFilter = "film_title LIKE '*" & txtFilmFilter.Text & "*'"
    End Sub

    'Sub that filters the list depending on what you write in the textbox
    Private Sub txtMemberFilter_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles txtMemberFilter.TextChanged
        dtableMembers.DefaultView.RowFilter = "member_first_name LIKE '*" & txtMemberFilter.Text & "*'"
    End Sub

    'Sub that fills the fields for the movies with the selected movie
    Private Sub dgMovies_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles dgMovies.MouseDoubleClick
        Dim dataRow As DataRowView = dgMovies.SelectedItem

        txtFilmId.Text = dataRow.Item("film_id").ToString()
        txtStock.Text = dataRow.Item("film_stock").ToString()
        txtPrice.Text = dataRow.Item("film_price").ToString()
        txtCost.Text = dataRow.Item("film_cost").ToString()

        lblError2.Content = ""
        lblSuccess.Content = ""

        If txtMemberId.Text.Length <> 0 Then

            Dim moviesBought As Integer = CType(txtMoviesBought.Text, Integer)
            If moviesBought Mod 10 = 0 And moviesBought > 0 Then
                txtPrice.Text = "0"
            ElseIf CType(txtDiscount.Text.ToString, Integer) <> 0 And txtPrice.Text.Length <> 0 Then
                Dim pr As Double = CType(txtPrice.Text.ToString, Double)
                pr = pr * 0.9
                txtPrice.Text = pr.ToString
            Else
                txtPrice.Text = dataRow.Item("film_price")
            End If

        End If
    End Sub

    'Sub that fills the fields for the members with the selected member
    Private Sub dgMembers_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles dgMembers.MouseDoubleClick
        Dim dataRow As DataRowView = dgMembers.SelectedItem

        txtMemberId.Text = dataRow.Item("member_id").ToString()
        txtMoviesBought.Text = dataRow.Item("member_films_bought").ToString()
        txtRegistrationDate.Text = dataRow.Item("member_registration_date").ToString()
        txtDiscount.Text = dataRow.Item("member_discount")

        lblSuccess.Content = ""
        lblError2.Content = ""

        Dim moviesBought As Integer = CType(txtMoviesBought.Text, Integer)
        If moviesBought Mod 10 = 0 And moviesBought > 0 Then
            txtPrice.Text = "0"
        ElseIf CType(txtDiscount.Text.ToString, Integer) <> 0 And txtPrice.Text.Length <> 0 Then
            Dim pr As Double = getRealPrice()
            pr = pr * 0.9
            txtPrice.Text = pr.ToString
        Else
            txtPrice.Text = getRealPrice().ToString
        End If

    End Sub

    'Button that checks for discount
    Private Sub btnCheck_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnCheck.Click
        If txtMoviesBought.Text.Length <> 0 Then
            Dim moviesBought As Integer = CType(txtMoviesBought.Text, Integer)

            If moviesBought Mod 10 = 0 And moviesBought > 0 Then
                lblSuccess.Content = "Success"
                lblError2.Content = ""
                txtPrice.Text = "0"
            Else
                lblError2.Content = "No discount"
                lblSuccess.Content = ""
            End If
        Else
            lblError2.Content = "Select a member"
            lblSuccess.Content = ""
        End If
    End Sub

    'Button used to sell a movie
    Private Sub btnSellMovie_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSellMovie.Click
        If txtFilmId.Text.Length <> 0 And CType(txtStock.Text.ToString, Integer) <> 0 Then
            Dim filmId As Integer = CType(txtFilmId.Text, Integer)
            Dim stock As Integer = CType(txtStock.Text, Integer)
            Dim price As Double = CType(txtPrice.Text, Integer)
            Dim cost As Double = CType(txtCost.Text, Integer)
            Dim memberId As Integer = 0

            If txtMemberId.Text.Length <> 0 Then
                memberId = CType(txtMemberId.Text, Integer)
            End If

            If db.sellMovie(MainWindow.getId(), filmId, Date.Now, price, cost, memberId) = 3 And db.decrementFilmStock(filmId) = 1 Then
                lblSuccess.Content = "Sold"
                lblError2.Content = ""
                clearFields()
                dgMovies.ItemsSource = db.getAllMovies.DefaultView
                dgMembers.ItemsSource = db.getAllMembers.DefaultView

            Else
                lblSuccess.Content = ""
                lblError2.Content = "Wrong"

            End If

        Else
            lblError2.Content = "No stock"
            lblSuccess.Content = ""
        End If
    End Sub

    'Gets the movie real price
    Private Function getRealPrice() As Double
        Dim drt As DataRowView = dgMovies.SelectedItem
        Dim price As Double = drt.Item("film_price")

        Return price
    End Function
End Class

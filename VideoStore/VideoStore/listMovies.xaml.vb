Imports System.Data
Imports System.IO

Class listMovies

    Private db As DBTransaction = New DBTransaction
    Private moviesTable As DataTable

    'Sub that fills the datagrid with the movie's data from the datatable when the grid is loaded
    Private Sub Grid1_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Grid1.Loaded
        moviesTable = db.getAllMovies()
        dgMovies.ItemsSource = moviesTable.DefaultView
    End Sub

    'Sub used do fill the fields with the selected movie from the data grid
    Private Sub dgMovies_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles dgMovies.MouseDoubleClick
        Dim dataRow As DataRowView = dgMovies.SelectedItem

        txtID.Text = dataRow.Item(0).ToString()
        txtName.Text = dataRow.Item(1).ToString()
        txtCategories.Text = dataRow.Item(2).ToString()
        dtpRelease.Text = dataRow.Item(3).ToString()
        txtRuntime.Text = dataRow.Item(4).ToString()
        txtRating.Text = dataRow.Item(5).ToString()
        txtDescripton.Text = dataRow.Item(6).ToString()
        txtStock.Text = dataRow.Item(7).ToString()
        txtPrice.Text = dataRow.Item(8).ToString()
        lblError.Content = ""
        lblSuccess.Content = ""
    End Sub

    'Button used to modify the selected movie with the data in the fields
    Private Sub btnModify_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnModify.Click
        If txtID.Text.Length <> 0 Then

            Dim id As Integer = CType(txtID.Text, Integer)
            Dim name As String = txtName.Text
            Dim category As String = txtCategories.Text
            Dim releaseDate As Date = dtpRelease.Text
            Dim runtime As String = txtRuntime.Text
            Dim rating As String = txtRating.Text
            Dim description As String = txtDescripton.Text
            Dim stock As String = txtStock.Text
            Dim price As String = txtPrice.Text
            Dim img() As Byte = Nothing

            If name.Length = 0 Or category.Length = 0 Or runtime.Length = 0 Or rating.Length = 0 Or
                description.Length = 0 Or stock.Length = 0 Or price.Length = 0 Or txtImage.Text.Length = 0 Then
                lblError.Content = "Please fill in all the fields!"
                lblSuccess.Content = ""
            Else
                Dim fs As FileStream = New FileStream(txtImage.Text, FileMode.Open, FileAccess.Read)
                Dim br As BinaryReader = New BinaryReader(fs)
                img = br.ReadBytes(fs.Length())

                Dim rs As Integer = db.editMovie(id, name, category, releaseDate, runtime, CType(rating, Double), description,
                                                 CType(stock, Double), CType(price, Double), img)

                If rs = 1 Then
                    lblSuccess.Content = "Movie has been modified!"
                    lblError.Content = ""
                    dgMovies.ItemsSource = db.getAllMovies().DefaultView
                    clearFields()
                Else
                    lblError.Content = "Something wrong has happened."
                    lblSuccess.Content = ""
                End If
            End If
        Else
            lblError.Content = "Please select a movie."
            lblSuccess.Content = ""
        End If

    End Sub

    'Button used to delete the selected movie from the database
    Private Sub btnDelete_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnDelete.Click
        If txtID.Text.Length <> 0 Then

            Dim id As Integer = CType(txtID.Text, Integer)
            Dim result As Integer = MessageBox.Show("Do you really want do delete this movie?", "Delete movie", MessageBoxButton.YesNo)

            If result = vbYes Then
                Dim rs As Integer = db.deleteMovie(id)

                If rs = 1 Then
                    lblSuccess.Content = "Movie has been deleted."
                    lblError.Content = ""
                    dgMovies.ItemsSource = db.getAllMovies().DefaultView
                Else
                    lblError.Content = "Something wrong has happened."
                    lblSuccess.Content = ""
                End If

            End If
        Else
            lblError.Content = "Please select a movie."
            lblSuccess.Content = ""
        End If
    End Sub

    'Sub used to clear fields
    Private Sub clearFields()
        txtID.Text = ""
        txtCategories.Text = ""
        txtDescripton.Text = ""
        txtName.Text = ""
        txtPrice.Text = ""
        txtRating.Text = ""
        txtRuntime.Text = ""
        txtStock.Text = ""
        txtImage.Text = ""
    End Sub

    'Button used to browse and image for the movie
    Private Sub btnBrowse_Click(sender As Object, e As RoutedEventArgs) Handles btnBrowse.Click
        Dim dlg As Microsoft.Win32.OpenFileDialog = New Microsoft.Win32.OpenFileDialog
        dlg.DefaultExt = ".jpg"
        dlg.Filter = "Image File (*.png, *.jpg)|*.png; *.jpg"

        Dim result As Boolean = dlg.ShowDialog

        If result = True Then
            txtImage.Text = dlg.FileName
        End If
    End Sub

    'Sub to handle the event of changing text in a text field and filters the data table rows
    Private Sub TextBox_TextChanged_1(sender As Object, e As TextChangedEventArgs)
        moviesTable.DefaultView.RowFilter = "film_title LIKE '*" & txtFilter.Text.ToString() & "*'"
    End Sub
End Class


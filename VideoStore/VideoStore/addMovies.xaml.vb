Imports System.IO

Class addMovies
    'Button used to add the data in the fields in the movies table in the database
    Private Sub btnAddMovie_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnAddMovie.Click

        Dim name As String = txtName.Text
        Dim category As String = txtCategory.Text
        Dim releaseDate As Date = dtpReleaseDate.Text
        Dim runtime As String = txtRuntime.Text
        Dim rating As String = txtRating.Text
        Dim txtRange As New TextRange(txtDescription.Document.ContentStart, txtDescription.Document.ContentEnd)
        Dim description As String = txtRange.Text
        Dim stock As String = txtStock.Text
        Dim price As String = txtPrice.Text
        Dim cost As String = txtCost.Text

        Dim img() As Byte = Nothing

        If txtImage.Text.Length <> 0 Then

            Dim fs As FileStream = New FileStream(txtImage.Text, FileMode.Open, FileAccess.Read)
            Dim br As BinaryReader = New BinaryReader(fs)
            img = br.ReadBytes(fs.Length())

        End If

        If name.Length = 0 Or category.Length = 0 Or runtime.Length = 0 Or rating.Length = 0 Or
            description.Length = 0 Or stock.Length = 0 Or price.Length = 0 Or cost.Length = 0 Then
            lblError.Content = "Please fill in all the fields!"

        Else
            Dim newMovie As Movies = New Movies(name, category, releaseDate, runtime, CType(rating, Double),
                                                description, CType(stock, Integer), CType(price, Double), CType(cost, Double), img)

            Dim db As DBTransaction = New DBTransaction
            If db.addMovie(newMovie) = 1 Then
                lblError.Content = ""
                lblSuccess.Content = "Insert is successful"
                resetFields()
            Else
                lblError.Content = "Something happened. Please try again later."
            End If
        End If
    End Sub
    'Button used to browse an image from the computer
    Private Sub btnBrowse_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnBrowse.Click
        Dim dlg As Microsoft.Win32.OpenFileDialog = New Microsoft.Win32.OpenFileDialog
        dlg.DefaultExt = ".jpg"
        dlg.Filter = "Image File (*.png, *.jpg)|*.png; *.jpg"

        Dim result As Boolean = dlg.ShowDialog

        If result = True Then
            txtImage.Text = dlg.FileName
        End If

    End Sub
    'Sub used to set the present date in the date picker
    Private Sub Grid1_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Grid1.Loaded
        dtpReleaseDate.SelectedDate = DateTime.Today
    End Sub
    'Sub used for clearing all the fields
    Private Sub resetFields()
        txtName.Text = ""
        txtCategory.Text = ""
        txtCost.Text = ""
        txtPrice.Text = ""
        txtStock.Text = ""
        txtRuntime.Text = ""
        txtImage.Text = ""
        txtDescription.Document.Blocks.Clear()
        txtRating.Text = ""
    End Sub
End Class

Imports System.IO
Imports System.Data

Class addGame

    Private db As DBTransaction
    Private dTableCom As DataTable

    'Button used to add the data in the fields in the database table of the movies
    Private Sub btnAddGame_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnAddGame.Click

        Dim name As String = txtName.Text
        Dim category As String = txtCategory.Text
        Dim releaseDate As Date = dtpReleaseDate.Text
        Dim producer As String = txtProducer.Text
        Dim rating As String = txtRating.Text
        Dim txtRange As New TextRange(txtDescription.Document.ContentStart, txtDescription.Document.ContentEnd)
        Dim description As String = txtRange.Text
        Dim stock As String = txtStock.Text
        Dim stockUsed As String = txtStockUsed.Text
        Dim price As String = txtPrice.Text
        Dim cost As String = txtCost.Text
        Dim compatibility As Integer = cmbCompatibility.SelectedValue

        Dim img() As Byte = Nothing

        If txtImage.Text.Length <> 0 Then

            Dim fs As FileStream = New FileStream(txtImage.Text, FileMode.Open, FileAccess.Read)
            Dim br As BinaryReader = New BinaryReader(fs)
            img = br.ReadBytes(fs.Length())

        End If

        If name.Length = 0 Or category.Length = 0 Or producer.Length = 0 Or rating.Length = 0 Or
            description.Length = 0 Or stock.Length = 0 Or price.Length = 0 Or cost.Length = 0 Then
            lblError.Content = "Please fill in all the fields!"

        Else
            Dim newGame As Games = New Games(name, category, releaseDate, producer, CType(rating, Double),
                                                description, compatibility, CType(stock, Integer), CType(stockUsed, Integer), CType(price, Double), CType(cost, Double), img)

            Dim db As DBTransaction = New DBTransaction
            If db.addGame(newGame) = 1 Then
                lblError.Content = ""
                lblSuccess.Content = "Insert is successful"
                resetFields()
            Else
                lblError.Content = "Something happened. Please try again later."
            End If
        End If
    End Sub

    'Button used to browse a image from the computer and filling the field with its path of location
    Private Sub btnBrowse_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnBrowse.Click
        Dim dlg As Microsoft.Win32.OpenFileDialog = New Microsoft.Win32.OpenFileDialog
        dlg.DefaultExt = ".jpg"
        dlg.Filter = "Image File (*.png, *.jpg)|*.png; *.jpg"

        Dim result As Boolean = dlg.ShowDialog

        If result = True Then
            txtImage.Text = dlg.FileName
        End If

    End Sub

    'Sub the clears all the fields
    Private Sub resetFields()
        txtName.Text = ""
        txtCategory.Text = ""
        txtCost.Text = ""
        txtPrice.Text = ""
        txtStock.Text = ""
        txtProducer.Text = ""
        txtImage.Text = ""
        txtDescription.Document.Blocks.Clear()
        txtRating.Text = ""
    End Sub

    'Sub that loads the drop-down list with all the compatibilites that exist in the database
    Private Sub Grid1_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Grid1.Loaded
        db = New DBTransaction()
        dTableCom = New DataTable()

        dTableCom = db.getCompatibilities()
        cmbCompatibility.ItemsSource = dTableCom.DefaultView
        cmbCompatibility.DisplayMemberPath = "compatibility_name"
        cmbCompatibility.SelectedValuePath = "compatibility_id"
        cmbCompatibility.SelectedIndex = 0

        dtpReleaseDate.SelectedDate = DateTime.Today
    End Sub
End Class

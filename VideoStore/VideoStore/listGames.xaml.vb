Imports System.Data
Imports System.IO

Class listGames

    Private db As DBTransaction = New DBTransaction
    Private gamesTable As DataTable
    Private compTable As DataTable

    'Sub that fills the datagrid with the game's data from the datatable when the grid is loaded
    Private Sub Grid1_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Grid1.Loaded
        db = New DBTransaction()
        gamesTable = New DataTable()
        compTable = New DataTable()

        gamesTable = db.getAllGames()
        dgGame.ItemsSource = gamesTable.DefaultView

        compTable = db.getCompatibilities()
        cmbCompatibility.ItemsSource = compTable.DefaultView
        cmbCompatibility.DisplayMemberPath = "compatibility_name"
        cmbCompatibility.SelectedValuePath = "compatibility_id"
    End Sub

    'Sub used do fill the fields with the selected game from the data grid
    Private Sub dgGame_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles dgGame.MouseDoubleClick
        Dim dataRow As DataRowView = dgGame.SelectedItem

        txtID.Text = dataRow.Item("game_id").ToString()
        txtName.Text = dataRow.Item("game_title").ToString()
        txtCategories.Text = dataRow.Item("game_categories").ToString()
        dtpRelease.Text = dataRow.Item("game_release_date").ToString()
        txtProducer.Text = dataRow.Item("game_producer").ToString()
        txtRating.Text = dataRow.Item("game_rating").ToString()
        txtDescription.Text = dataRow.Item("game_description").ToString()
        txtStock.Text = dataRow.Item("game_stock_new").ToString()
        txtStockUsed.Text = dataRow.Item("game_stock_used").ToString
        txtPrice.Text = dataRow.Item("game_price").ToString()

        cmbCompatibility.SelectedIndex = CType(dataRow.Item("compatibility_id"), Integer) - 1
        lblSuccess.Content = ""
        lblError.Content = ""
    End Sub

    'Button used to modify the selected game with the data in the fields
    Private Sub btnModify_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnModify.Click
        If txtID.Text.Length <> 0 Then

            Dim id As Integer = CType(txtID.Text, Integer)
            Dim name As String = txtName.Text
            Dim category As String = txtCategories.Text
            Dim releaseDate As Date = dtpRelease.Text
            Dim producer As String = txtProducer.Text
            Dim rating As String = txtRating.Text
            Dim description As String = txtDescription.Text
            Dim stock As String = txtStock.Text
            Dim stockUsed As String = txtStockUsed.Text
            Dim price As String = txtPrice.Text
            Dim compatibility As Integer = cmbCompatibility.SelectedValue
            Dim img() As Byte = Nothing

            If name.Length = 0 Or category.Length = 0 Or producer.Length = 0 Or rating.Length = 0 Or
                description.Length = 0 Or stock.Length = 0 Or price.Length = 0 Or txtImage.Text.Length = 0 Then
                lblError.Content = "Please fill in all the fields!"
                lblSuccess.Content = ""
            Else
                Dim fs As FileStream = New FileStream(txtImage.Text, FileMode.Open, FileAccess.Read)
                Dim br As BinaryReader = New BinaryReader(fs)
                img = br.ReadBytes(fs.Length())

                Dim rs As Integer = db.editGame(id, name, category, releaseDate, producer, CType(rating, Double), description,
                                                 CType(stock, Double), CType(stockUsed, Integer), CType(price, Double), compatibility, img)

                If rs = 1 Then
                    lblSuccess.Content = "Game has been modified successfuly!"
                    lblError.Content = ""
                    dgGame.ItemsSource = db.getAllGames().DefaultView
                    clearFields()
                Else
                    lblError.Content = "Something wrong has happened. Please try again later!"
                    lblSuccess.Content = ""
                End If
            End If
        Else
            lblSuccess.Content = ""
            lblError.Content = "Please select a game from the list."
        End If

    End Sub

    'Sub used to clear the fields
    Private Sub clearFields()
        txtCategories.Text = ""
        txtDescription.Text = ""
        txtName.Text = ""
        txtID.Text = ""
        txtPrice.Text = ""
        txtProducer.Text = ""
        txtRating.Text = ""
        txtStock.Text = ""
        txtStockUsed.Text = ""
        cmbCompatibility.SelectedIndex = 0
        txtImage.Text = ""
    End Sub

    'Button used to browse an image for the game
    Private Sub btnBrowse_Click(sender As Object, e As RoutedEventArgs) Handles btnBrowse.Click
        Dim dlg As Microsoft.Win32.OpenFileDialog = New Microsoft.Win32.OpenFileDialog
        dlg.DefaultExt = ".jpg"
        dlg.Filter = "Image File (*.png, *.jpg)|*.png; *.jpg"

        Dim result As Boolean = dlg.ShowDialog

        If result = True Then
            txtImage.Text = dlg.FileName
        End If
    End Sub

    'Button used to delete the selected game from the database
    Private Sub btnDelete_Click(sender As Object, e As RoutedEventArgs) Handles btnDelete.Click
        If txtID.Text.Length <> 0 Then

            Dim id As Integer = CType(txtID.Text, Integer)
            Dim result As Integer = MessageBox.Show("Do you really want do delete this game?", "Delete game", MessageBoxButton.YesNo)

            If result = vbYes Then
                Dim rs As Integer = db.deleteGame(id)

                If rs = 1 Then
                    lblSuccess.Content = "Game has beed deleted successfully."
                    lblError.Content = ""
                    dgGame.ItemsSource = db.getAllGames().DefaultView
                    clearFields()

                Else
                    lblError.Content = "Something wrong has happened. Please try again later."
                    lblSuccess.Content = ""
                End If

            End If
        Else
            lblError.Content = "Please select a game from the list."
            lblSuccess.Content = ""
        End If
    End Sub

    'Sub to handle the event of changing text in a text field and filters the data table rows
    Private Sub txtFilter_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtFilter.TextChanged
        gamesTable.DefaultView.RowFilter = "game_title LIKE '*" & txtFilter.Text.ToString & "*'"
    End Sub
End Class

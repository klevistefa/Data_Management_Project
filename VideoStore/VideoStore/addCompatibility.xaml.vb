Imports System.Data

Class AddCompatibility

    Private db As DBTransaction
    Private dTableComp As DataTable

    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        db = New DBTransaction()
        dTableComp = New DataTable()

        dTableComp = db.getCompatibilities()
        dgCompatibility.ItemsSource = dTableComp.DefaultView

        lblError.Content = ""
        lblSuccess.Content = ""
        txtCompatibility.Text = ""
    End Sub

    'Subroutine to collect information about the new compatibility and call a funtion to insert it into the database
    Private Sub btnAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnAdd.Click
        If txtCompatibility.Text.Length <> 0 Then
            Dim name As String = txtCompatibility.Text.ToString
            Dim rs As Integer = db.insertCompatibility(name)

            If rs = 1 Then
                lblSuccess.Content = "Inserted with success."
                txtCompatibility.Text = ""
                txtID.Text = ""
                txtName.Text = ""
                dTableComp.Clear()
                dgCompatibility.ItemsSource = dTableComp.DefaultView
                dTableComp = db.getCompatibilities()
                dgCompatibility.ItemsSource = dTableComp.DefaultView
            Else
                lblError.Content = "Error"
            End If
        Else
            lblError.Content = "Please fill the name."
        End If
    End Sub

    'Sub to clear fields when compatibility text field has focus
    Private Sub txtCompatibility_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtCompatibility.GotFocus
        lblError.Content = ""
        lblSuccess.Content = ""
    End Sub

    Private Sub dgCompatibility_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dgCompatibility.MouseDoubleClick
        Dim dr As DataRowView = dgCompatibility.SelectedItem
        txtID.Text = dr.Item(0)
        txtName.Text = dr.Item(1)

        lblError.Content = ""
        lblSuccess.Content = ""
    End Sub

    'Subroutine to collect information about the compatibility and call a funtion to modify it
    Private Sub btnModify_Click(sender As Object, e As RoutedEventArgs) Handles btnModify.Click
        If txtID.Text.Length <> 0 Then
            Dim id As Integer = CType(txtID.Text, Integer)
            Dim name As String = txtName.Text.ToString
            Dim rs As Integer = db.updateCompatibility(id, name)

            If rs = 1 Then
                lblSuccess.Content = "Updated."
                txtID.Text = ""
                txtName.Text = ""
                dTableComp.Clear()
                dgCompatibility.ItemsSource = dTableComp.DefaultView
                dTableComp = db.getCompatibilities()
                dgCompatibility.ItemsSource = dTableComp.DefaultView
            Else
                lblError.Content = "Error, try again."
            End If
        Else
            lblError.Content = "Select a Compatibility"
        End If
    End Sub
End Class

Imports System.Data

Class listMember
    Private db As DBTransaction = New DBTransaction
    Private membersTable As DataTable

    ''Sub that fills the datagrid with the member's data from the datatable when the grid is loaded
    Private Sub Grid1_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Grid1.Loaded
        membersTable = db.getAllMembers
        dgMembers.ItemsSource = membersTable.DefaultView
    End Sub

    'Button used to modify the selected member in the database with the new data in fields
    Private Sub btnModify_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnModify.Click
        If txtID.Text.Length <> 0 Then

            Dim id As Integer = CType(txtID.Text, Integer)
            Dim fname As String = txtFname.Text
            Dim lname As String = txtLname.Text
            Dim gender As Char
            If rdbMale.IsChecked = True Then
                gender = "M"
            Else
                gender = "F"
            End If
            Dim registrationDate As Date = dtpRegistration.Text
            Dim age As String = txtAge.Text
            Dim email As String = txtEmail.Text
            Dim phone As String = txtPhone.Text

            If fname.Length = 0 Or lname.Length = 0 Or email.Length = 0 Or phone.Length = 0 Or age.Length = 0 Then
                lblError.Content = "Please fill in all the fields!"
                lblSuccess.Content = ""
            Else
                Dim rs As Integer = db.editMember(id, fname, lname, gender, age, registrationDate, email, phone)


                If rs = 1 Then
                    lblSuccess.Content = "Member has been modified successfuly!"
                    lblError.Content = ""
                    dgMembers.ItemsSource = db.getAllMembers().DefaultView
                    clearFields()

                Else
                    lblError.Content = "Something wrong has happened. Please try again later!"
                    lblSuccess.Content = ""
                End If

            End If
        Else
            lblSuccess.Content = ""
            lblError.Content = "Please select a member from the list."
        End If
    End Sub

    'Sub used do fill the fields with the selected member from the data grid
    Private Sub dgMembers_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles dgMembers.MouseDoubleClick

        Dim dataRow As DataRowView = dgMembers.SelectedItem
        txtID.Text = dataRow.Item("member_id").ToString
        txtFname.Text = dataRow.Item("member_first_name").ToString
        txtLname.Text = dataRow.Item("member_last_name").ToString
        If dataRow.Item("member_gender").ToString = "M" Then
            rdbMale.IsChecked = True
        Else
            rdbFemale.IsChecked = True
        End If
        txtAge.Text = dataRow.Item("member_age").ToString
        dtpRegistration.Text = dataRow.Item("member_registration_date").ToString
        txtEmail.Text = dataRow.Item("member_email").ToString
        txtPhone.Text = dataRow.Item("member_phone_number").ToString

    End Sub

    'Button used to delete the member from the database
    Private Sub btnDelete_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnDelete.Click
        If txtID.Text.Length <> 0 Then

            Dim id As Integer = CType(txtID.Text, Integer)
            Dim result As Integer = MessageBox.Show("Do you really want do delete this member?", "Delete member", MessageBoxButton.YesNo)

            If result = vbYes Then
                Dim rs As Integer = db.deleteMember(id)

                If rs = 1 Then
                    lblSuccess.Content = "Member has beed deleted successfully."
                    lblError.Content = ""
                    dgMembers.ItemsSource = db.getAllMembers().DefaultView
                Else
                    lblError.Content = "Something wrong has happened. Please try again later."
                    lblSuccess.Content = ""
                End If

            End If
        Else
            lblError.Content = "Please select a member from the list."
            lblSuccess.Content = ""
        End If
    End Sub

    'Sub used to clear all the fields
    Sub clearFields()
        txtAge.Text = ""
        txtEmail.Text = ""
        txtFname.Text = ""
        txtID.Text = ""
        txtLname.Text = ""
        txtPhone.Text = ""
    End Sub
End Class

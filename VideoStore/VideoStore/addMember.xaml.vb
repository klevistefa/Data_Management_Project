Imports System.IO

Class addMember

    'Button used to add all the data in the fields in the member's table in the dattabase
    Private Sub btnAddMember_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnAddMember.Click
        Dim fname As String = txtFname.Text
        Dim lname As String = txtLname.Text
        Dim email As String = txtEmail.Text
        Dim phone As String = txtPhone.Text
        Dim age As String = txtAge.Text

        Dim gender As String
        If rdbMale.IsChecked Then
            gender = "M"
        Else
            gender = "F"
        End If

        Dim dateOf As Date = dtpRegistration.Text

        If fname.Length = 0 Or lname.Length = 0 Or email.Length = 0 Or phone.Length = 0 Or age.Length = 0 Then
            lblError.Content = "Please fill in all the fields!"
            lblSuccess.Content = ""
        Else
            Dim newMember As Member = New Member(fname, lname, gender, age, dateOf, email, phone, 0)

            Dim db As DBTransaction = New DBTransaction
            If db.addMember(newMember) = 1 Then
                lblError.Content = ""
                lblSuccess.Content = "Insert is successful"
                resetFields()
            Else
                lblSuccess.Content = ""
                lblError.Content = "Something happened. Please try again later."
            End If
        End If

    End Sub
    'Sub the clears all the fields
    Private Sub resetFields()

        txtFname.Text = ""
        txtLname.Text = ""
        txtAge.Text = ""
        txtEmail.Text = ""
        txtPhone.Text = ""

    End Sub
    'Sub the sets the present date in the date picker
    Private Sub Grid1_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Grid1.Loaded
        dtpRegistration.SelectedDate = DateTime.Today
    End Sub
End Class

Imports System.IO

Class addEmployee
    'Button used to add the text in the fields in the database table
    Private Sub btnAddEmployee_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnAddEmployee.Click
        Dim fname As String = txtFname.Text
        Dim lname As String = txtLname.Text
        Dim uname As String = txtUsername.Text
        Dim pass As String = txtUsername.Text
        Dim salary As String = txtSalary.Text
        Dim age As String = txtAge.Text

        Dim gender As Char
        If rdbMale.IsChecked Then
            gender = "M"
        Else
            gender = "F"
        End If

        Dim dateOf As Date = dtpEmployment.Text

        If fname.Length = 0 Or lname.Length = 0 Or uname.Length = 0 Or pass.Length = 0 Or salary.Length = 0 Or age.Length = 0 Then
            lblError.Content = "Please fill in all the fields!"
            lblSuccess.Content = ""
        Else
            Dim newEmployee As Employees = New Employees(fname, lname, gender, age, dateOf, CType(salary, Double), uname, pass)

            Dim db As DBTransaction = New DBTransaction
            If db.addEmployee(newEmployee) = 1 Then
                lblError.Content = ""
                lblSuccess.Content = "Insert is successful"
                resetFields()
            Else
                lblSuccess.Content = ""
                lblError.Content = "Something happened. Please try again later."
            End If
        End If
    End Sub
    'Sub to clear all the fields
    Private Sub resetFields()

        txtFname.Text = ""
        txtLname.Text = ""
        txtAge.Text = ""
        txtSalary.Text = ""
        txtUsername.Text = ""
        txtPass.Text = ""

    End Sub
    'Sub that sets the present date in the date picker 
    Private Sub Grid1_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Grid1.Loaded
        dtpEmployment.SelectedDate = DateTime.Today
    End Sub
End Class

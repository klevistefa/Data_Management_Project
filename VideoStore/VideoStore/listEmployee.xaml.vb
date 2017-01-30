Imports System.Data

Class listEmployee
    Private db As DBTransaction = New DBTransaction
    Private employeesTable As DataTable

    'Sub that fills the datagrid with the employee's data from the datatable when the grid is loaded
    Private Sub Grid1_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Grid1.Loaded
        employeesTable = db.getAllEmployees
        dgEmployees.ItemsSource = employeesTable.DefaultView
    End Sub

    'Button used to modify the selected employee with the data in the fields
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
            Dim employmentDate As Date = dtpEmployment.Text
            Dim salary As String = txtSalary.Text
            Dim age As String = txtAge.Text
            Dim username As String = txtUsername.Text
            Dim pass As String = txtPassword.Text

            If fname.Length = 0 Or lname.Length = 0 Or username.Length = 0 Or pass.Length = 0 Or salary.Length = 0 Or age.Length = 0 Then
                lblError.Content = "Please fill in all the fields!"
                lblSuccess.Content = ""
            Else
                Dim rs As Integer = db.editEmployee(id, fname, lname, gender, age, employmentDate, CType(salary, Double), username, pass)


                If rs = 1 Then
                    lblSuccess.Content = "Employee has been modified!"
                    lblError.Content = ""
                    dgEmployees.ItemsSource = db.getAllEmployees().DefaultView
                    clearFields()
                Else
                    lblError.Content = "Something wrong has happened."
                    lblSuccess.Content = ""
                End If

            End If
        Else
            lblSuccess.Content = ""
            lblError.Content = "Please select an employee."
        End If

    End Sub

    'Sub used do fill the fields with the selected employee from the data grid
    Private Sub dgEmployees_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles dgEmployees.MouseDoubleClick
        Dim dataRow As DataRowView = dgEmployees.SelectedItem
        txtID.Text = dataRow.Item("employee_id").ToString
        txtFname.Text = dataRow.Item("employee_first_name").ToString
        txtLname.Text = dataRow.Item("employee_last_name").ToString
        If dataRow.Item("employee_gender").ToString = "M" Then
            rdbMale.IsChecked = True
        Else
            rdbFemale.IsChecked = True
        End If
        txtAge.Text = dataRow.Item("employee_age").ToString
        dtpEmployment.Text = dataRow.Item("employee_date_employment").ToString
        txtSalary.Text = dataRow.Item("employee_salary").ToString
        txtUsername.Text = dataRow.Item(7).ToString
        txtPassword.Text = dataRow.Item(8).ToString
        lblError.Content = ""
        lblSuccess.Content = ""
    End Sub

    'Sub used to clear the fields
    Private Sub clearFields()
        txtID.Text = ""
        txtAge.Text = ""
        txtFname.Text = ""
        txtLname.Text = ""
        txtPassword.Text = ""
        txtSalary.Text = ""
        txtUsername.Text = ""
    End Sub

    'Button used to delete from the database the selected employee from the datagrid
    Private Sub btnDelete_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnDelete.Click
        If txtID.Text.Length <> 0 Then

            Dim id As Integer = CType(txtID.Text, Integer)
            Dim result As Integer = MessageBox.Show("Do you really want do delete this employee?", "Delete employee", MessageBoxButton.YesNo)

            If result = vbYes Then
                Dim rs As Integer = db.deleteEmployee(id)

                If rs = 1 Then
                    lblSuccess.Content = "Employee has beed deleted."
                    lblError.Content = ""
                    dgEmployees.ItemsSource = db.getAllEmployees().DefaultView
                    clearFields()
                Else
                    lblError.Content = "Something wrong has happened."
                    lblSuccess.Content = ""
                End If

            End If
        Else
            lblError.Content = "Please select a employee."
            lblSuccess.Content = ""
        End If
    End Sub
End Class

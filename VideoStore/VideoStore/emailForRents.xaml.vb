Imports System.Data
Imports System.Net.Mail

Class emailForRents

    Private db As DBTransaction
    Private dTableRents As DataTable

    'Sub used to fill the table with all the passed rents when the page is opened

    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        db = New DBTransaction()
        dTableRents = New DataTable()

        dTableRents = db.getPassedRents()
        dgMembers.ItemsSource = dTableRents.DefaultView

        lblError.Content = ""
        lblSuccess.Content = ""
    End Sub
    'Filling the fields with the data of the selected member
    Private Sub dgMembers_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dgMembers.MouseDoubleClick
        Dim dr As DataRowView = dgMembers.SelectedItem
        txtEmail.Text = dr.Item("member_email")
        txtFirstName.Text = dr.Item("member_first_name")
        txtLastName.Text = dr.Item("member_last_name")
        txtTitle.Text = dr.Item("film_title")

        lblError.Content = ""
        lblSuccess.Content = ""
    End Sub
    'Button used to send an email to a member that has  not returned the movie in time
    Private Sub btnSend_Click(sender As Object, e As RoutedEventArgs) Handles btnSend.Click
        If txtTitle.Text.Length <> 0 Then
            Dim fName As String = txtFirstName.Text.ToString()
            Dim lName As String = txtLastName.Text.ToString()
            Dim email As String = txtEmail.Text.ToString()
            Dim title As String = txtTitle.Text.ToString()

            Dim message As New MailMessage
            Dim smtp As New SmtpClient

            message.To.Add(email)
            message.From = New MailAddress("provavideoteka@gmail.com", "Video Store")
            message.Subject = "Rent Reminder"
            message.Body = "Hi " & fName & " " & lName & vbCrLf & "The deadline for returning the movies has passed. Please return the following movie as soon as possible: " & title

            smtp.Host = "smtp.gmail.com"
            smtp.Port = 587
            smtp.EnableSsl = True
            smtp.Credentials = New Net.NetworkCredential("provavideoteka@gmail.com", "prova123")
            smtp.Send(message)

            lblSuccess.Content = "Email sent."
            lblError.Content = ""

        Else
            lblError.Content = "Select a rent."
            lblSuccess.Content = ""
        End If
    End Sub
End Class

Class myAccount

    Private db As DBTransaction

    'Button used to modify the the password of the logged in employee
    Private Sub btnModify_Click(sender As Object, e As RoutedEventArgs) Handles btnModify.Click
        db = New DBTransaction()

        Dim pas As String = psd1.Password
        Dim pas1 As String = psd2.Password
        Dim id As Integer = MainWindow.getId()

        If pas.Length = 0 Or pas1.Length = 0 Then
            lblError.Content = "Please fill in both passwords."
        Else
            If pas <> pas1 Then
                lblError.Content = "Password must match."
            Else
                Dim rs As Integer = db.modifyEmployeePassword(pas, id)

                If rs = 1 Then
                    lblSuccess.Content = "Password modified."
                Else
                    lblError.Content = "Try again later."
                End If
            End If
        End If
    End Sub
End Class
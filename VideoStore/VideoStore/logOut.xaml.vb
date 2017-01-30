Imports FirstFloor.ModernUI.Windows.Controls

Class logOut

    'Button used to close the current window and open the log-in window
    Private Sub btnLogOut_Click(sender As Object, e As RoutedEventArgs) Handles btnLogOut.Click
        Dim newWindow As New LoginWindow()
        newWindow.Show()
        newWindow.Width = 250
        newWindow.Height = 350

        Dim mainMenu(1) As FirstFloor.ModernUI.Presentation.LinkGroup
        mainMenu(1) = New FirstFloor.ModernUI.Presentation.LinkGroup
        mainMenu(1).DisplayName = "Log In As:"

        newWindow.MenuLinkGroups.Add(mainMenu(1))

        Dim admin As New FirstFloor.ModernUI.Presentation.Link
        admin.DisplayName = "Administrator"
        admin.Source = New Uri("/AdministratorLogin.xaml", UriKind.Relative)

        Dim emp As New FirstFloor.ModernUI.Presentation.Link
        emp.DisplayName = "Employee"
        emp.Source = New Uri("/EmployeeLogin.xaml", UriKind.Relative)

        Dim guest As New FirstFloor.ModernUI.Presentation.Link
        guest.DisplayName = "Guest"
        guest.Source = New Uri("/GuestLogin.xaml", UriKind.Relative)

        mainMenu(1).Links.Add(admin)
        mainMenu(1).Links.Add(emp)
        mainMenu(1).Links.Add(guest)

        Dim currentWindow As ModernWindow = ModernWindow.GetWindow(Me)
        currentWindow.Close()
    End Sub
End Class

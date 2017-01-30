Imports FirstFloor.ModernUI.Windows.Controls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security

Class EmployeeLogin
    Dim objConnection As SqlConnection
    Dim objCommand As SqlCommand
    Dim objDataAdapter As SqlDataAdapter
    Dim objDataSet As DataSet
    Dim db As DBTransaction

    'Button used for logging in as an employee
    Private Sub btnLogin_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnLogin.Click

        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Password

        If username.Length = 0 Or password.Length = 0 Then
            lblError.Content = "Please fill in all the fields."
        Else
            Dim connString As String = Utility.GetConnectionString()
            objConnection = New SqlConnection(connString)

            objCommand = New SqlCommand()
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT * FROM Employees WHERE employee_username=@username AND employee_password=@password"
            objCommand.Parameters.AddWithValue("@username", username)
            objCommand.Parameters.AddWithValue("@password", password)

            objConnection.Open()
            objDataAdapter = New SqlDataAdapter()
            objDataAdapter.SelectCommand = objCommand
            objDataSet = New DataSet()
            objDataAdapter.Fill(objDataSet, "employee")
            objConnection.Close()

            Dim result As Integer = objDataSet.Tables(0).Rows.Count

            'Checking if the inputs of the user match a row in the database table of administrator
            If result = 0 Then
                lblError.Content = "Invalid credentials"
            Else
                'Getting the id of the employee logged in
                Dim dr As DataRow = objDataSet.Tables(0).Rows(0)
                Dim id As Integer = dr.Item(0)


                'Opening a window with all the menus and functions of the administrator
                Dim newWindow As New MainWindow(id)
                newWindow.Width = 1050
                newWindow.Height = 700

                Dim mainMenu(8) As FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(1) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(1).DisplayName = "Movies"
                mainMenu(2) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(2).DisplayName = "Games"
                mainMenu(3) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(3).DisplayName = "Sell Product"
                mainMenu(4) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(4).DisplayName = "Members"
                mainMenu(5) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(5).DisplayName = "Rented Movies"
                mainMenu(6) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(6).DisplayName = "My Sales"
                mainMenu(7) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(7).DisplayName = "Message/E-Mail"
                mainMenu(8) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(8).DisplayName = "Help"

                newWindow.MenuLinkGroups.Add(mainMenu(1))
                newWindow.MenuLinkGroups.Add(mainMenu(2))
                newWindow.MenuLinkGroups.Add(mainMenu(3))
                newWindow.MenuLinkGroups.Add(mainMenu(4))
                newWindow.MenuLinkGroups.Add(mainMenu(5))
                newWindow.MenuLinkGroups.Add(mainMenu(6))
                newWindow.MenuLinkGroups.Add(mainMenu(7))
                newWindow.MenuLinkGroups.Add(mainMenu(8))
                newWindow.Show()

                Dim listMovies As New FirstFloor.ModernUI.Presentation.Link
                listMovies.DisplayName = "List all movies"
                listMovies.Source = New Uri("/listMovies.xaml", UriKind.Relative)

                Dim addMovies As New FirstFloor.ModernUI.Presentation.Link
                addMovies.DisplayName = "Add new movie"
                addMovies.Source = New Uri("/addMovies.xaml", UriKind.Relative)

                mainMenu(1).Links.Add(listMovies)
                mainMenu(1).Links.Add(addMovies)

                Dim listGames As New FirstFloor.ModernUI.Presentation.Link
                listGames.DisplayName = "List all games"
                listGames.Source = New Uri("/listGames.xaml", UriKind.Relative)

                Dim addGame As New FirstFloor.ModernUI.Presentation.Link
                addGame.DisplayName = "Add new game"
                addGame.Source = New Uri("/addGame.xaml", UriKind.Relative)

                Dim comp As New FirstFloor.ModernUI.Presentation.Link
                comp.DisplayName = "Compatibilities"
                comp.Source = New Uri("/addCompatibility.xaml", UriKind.Relative)

                mainMenu(2).Links.Add(listGames)
                mainMenu(2).Links.Add(addGame)
                mainMenu(2).Links.Add(comp)

                Dim sellMovie As New FirstFloor.ModernUI.Presentation.Link
                sellMovie.DisplayName = "Sell Movie"
                sellMovie.Source = New Uri("/sellMovie.xaml", UriKind.Relative)

                Dim sellGame As New FirstFloor.ModernUI.Presentation.Link
                sellGame.DisplayName = "Sell Game"
                sellGame.Source = New Uri("/sellGame.xaml", UriKind.Relative)

                mainMenu(3).Links.Add(sellMovie)
                mainMenu(3).Links.Add(sellGame)

                Dim listMember As New FirstFloor.ModernUI.Presentation.Link
                listMember.DisplayName = "List all members"
                listMember.Source = New Uri("/listMember.xaml", UriKind.Relative)

                Dim addMember As New FirstFloor.ModernUI.Presentation.Link
                addMember.DisplayName = "Add new member"
                addMember.Source = New Uri("/addMember.xaml", UriKind.Relative)

                mainMenu(4).Links.Add(listMember)
                mainMenu(4).Links.Add(addMember)

                Dim rentedMovies As New FirstFloor.ModernUI.Presentation.Link
                rentedMovies.DisplayName = "All rented movies"
                rentedMovies.Source = New Uri("/rentedMovies.xaml", UriKind.Relative)

                Dim returnMovie As New FirstFloor.ModernUI.Presentation.Link
                returnMovie.DisplayName = "Rent Movie"
                returnMovie.Source = New Uri("/rentMovie.xaml", UriKind.Relative)

                mainMenu(5).Links.Add(rentedMovies)
                mainMenu(5).Links.Add(returnMovie)

                Dim filmSales As New FirstFloor.ModernUI.Presentation.Link
                filmSales.DisplayName = "Film Sales"
                filmSales.Source = New Uri("/filmSales.xaml", UriKind.Relative)

                Dim gameSales As New FirstFloor.ModernUI.Presentation.Link
                gameSales.DisplayName = "Game Sales"
                gameSales.Source = New Uri("/gameSales.xaml", UriKind.Relative)

                mainMenu(6).Links.Add(filmSales)
                mainMenu(6).Links.Add(gameSales)

                Dim passedDates As New FirstFloor.ModernUI.Presentation.Link
                passedDates.DisplayName = "Passed Dates"
                passedDates.Source = New Uri("/emailForRents.xaml", UriKind.Relative)

                mainMenu(7).Links.Add(passedDates)

                Dim manual As New FirstFloor.ModernUI.Presentation.Link
                manual.DisplayName = "Manual"
                manual.Source = New Uri("/manual.xaml", UriKind.Relative)

                Dim account As New FirstFloor.ModernUI.Presentation.Link
                account.DisplayName = "My Account"
                account.Source = New Uri("/myAccount.xaml", UriKind.Relative)

                Dim logout As New FirstFloor.ModernUI.Presentation.Link
                logout.DisplayName = "Log Out"
                logout.Source = New Uri("/logOut.xaml", UriKind.Relative)

                mainMenu(8).Links.Add(manual)
                mainMenu(8).Links.Add(account)
                mainMenu(8).Links.Add(logout)

                Dim currentWindow As ModernWindow = ModernWindow.GetWindow(Me)
                currentWindow.Close()
            End If
        End If
    End Sub

    Private Sub txtUsername_GotFocus(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles txtUsername.GotFocus
        lblError.Content = ""
    End Sub

    Private Sub txtPassword_GotFocus(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles txtPassword.GotFocus
        lblError.Content = ""
    End Sub

    'Check if any employees in database
    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        db = New DBTransaction()
        Dim dataT As DataTable = db.getAllEmployees()

        If dataT.Rows.Count = 0 Then
            mainGrid.Children.Clear()
        End If
    End Sub
End Class

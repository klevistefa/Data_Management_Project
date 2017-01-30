Imports FirstFloor.ModernUI.Windows.Controls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security
Imports System.IO

Class AdministratorLogin
    Dim objConnection As SqlConnection
    Dim objCommand As SqlCommand
    Dim objDataAdapter As SqlDataAdapter
    Dim objDataSet As DataSet

    'Button used for logging in as an administrator
    Private Sub btnLogin_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Password

        If username.Length = 0 Or password.Length = 0 Then
            lblError.Content = "Please fill in all the fields."
        Else
            Dim connString As String = Utility.GetConnectionString()
            objConnection = New SqlConnection(connString)
            objCommand.Connection = objConnection

            objCommand.Parameters.Clear()
            objCommand.CommandText = "SELECT * FROM administrator WHERE admin_username=@username AND admin_password=@password"
            objCommand.Parameters.AddWithValue("@username", username)
            objCommand.Parameters.AddWithValue("@password", password)

            objConnection.Open()
            objDataAdapter = New SqlDataAdapter()
            objDataAdapter.SelectCommand = objCommand
            objDataSet = New DataSet()
            objDataAdapter.Fill(objDataSet, "admin")
            objConnection.Close()

            Dim result As Integer = objDataSet.Tables(0).Rows.Count

            'Checking if the inputs of the user match a row in the database table of administrator
            If result = 0 Then
                lblError.Content = "Invalid credentials"
            Else
                'Opening a window with all the menus and functions of the administrator
                Dim newWindow As New MainWindow(0)
                newWindow.Width = 1150
                newWindow.Height = 700

                Dim mainMenu(9) As FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(1) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(1).DisplayName = "Movies"
                mainMenu(2) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(2).DisplayName = "Games"
                mainMenu(3) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(3).DisplayName = "Sell Product"
                mainMenu(4) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(4).DisplayName = "Members"
                mainMenu(5) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(5).DisplayName = "Employees"
                mainMenu(6) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(6).DisplayName = "Rented Movies"
                mainMenu(7) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(7).DisplayName = "Sales Report"
                mainMenu(8) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(8).DisplayName = "Message/E-mail"
                mainMenu(9) = New FirstFloor.ModernUI.Presentation.LinkGroup
                mainMenu(9).DisplayName = "Help"

                newWindow.MenuLinkGroups.Add(mainMenu(1))
                newWindow.MenuLinkGroups.Add(mainMenu(2))
                newWindow.MenuLinkGroups.Add(mainMenu(3))
                newWindow.MenuLinkGroups.Add(mainMenu(4))
                newWindow.MenuLinkGroups.Add(mainMenu(5))
                newWindow.MenuLinkGroups.Add(mainMenu(6))
                newWindow.MenuLinkGroups.Add(mainMenu(7))
                newWindow.MenuLinkGroups.Add(mainMenu(8))
                newWindow.MenuLinkGroups.Add(mainMenu(9))

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
                sellMovie.DisplayName = "Sell movie"
                sellMovie.Source = New Uri("/sellMovie.xaml", UriKind.Relative)

                Dim sellGame As New FirstFloor.ModernUI.Presentation.Link
                sellGame.DisplayName = "Sell game"
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


                Dim listEmployee As New FirstFloor.ModernUI.Presentation.Link
                listEmployee.DisplayName = "List all employees"
                listEmployee.Source = New Uri("/listEmployee.xaml", UriKind.Relative)

                Dim addEmployee As New FirstFloor.ModernUI.Presentation.Link
                addEmployee.DisplayName = "Add new employee"
                addEmployee.Source = New Uri("/addEmployee.xaml", UriKind.Relative)

                mainMenu(5).Links.Add(listEmployee)
                mainMenu(5).Links.Add(addEmployee)

                Dim rentedMovies As New FirstFloor.ModernUI.Presentation.Link
                rentedMovies.DisplayName = "All rented movies"
                rentedMovies.Source = New Uri("/rentedMovies.xaml", UriKind.Relative)

                Dim returnMovie As New FirstFloor.ModernUI.Presentation.Link
                returnMovie.DisplayName = "Rent Movie"
                returnMovie.Source = New Uri("/rentMovie.xaml", UriKind.Relative)

                Dim sendEmail As New FirstFloor.ModernUI.Presentation.Link
                sendEmail.DisplayName = "Passed Dates"
                sendEmail.Source = New Uri("/emailForRents.xaml", UriKind.Relative)

                mainMenu(6).Links.Add(rentedMovies)
                mainMenu(6).Links.Add(returnMovie)
                mainMenu(8).Links.Add(sendEmail)

                Dim movieSales As New FirstFloor.ModernUI.Presentation.Link
                movieSales.DisplayName = "Movie sales"
                movieSales.Source = New Uri("/salesMovies.xaml", UriKind.Relative)

                Dim gameSales As New FirstFloor.ModernUI.Presentation.Link
                gameSales.DisplayName = "Game Sales"
                gameSales.Source = New Uri("/salesGames.xaml", UriKind.Relative)

                Dim accounting As New FirstFloor.ModernUI.Presentation.Link
                accounting.DisplayName = "Accounting Panel"
                accounting.Source = New Uri("/AccountingPanel.xaml", UriKind.Relative)

                mainMenu(7).Links.Add(movieSales)
                mainMenu(7).Links.Add(gameSales)
                mainMenu(7).Links.Add(accounting)

                Dim manual As New FirstFloor.ModernUI.Presentation.Link
                manual.DisplayName = "Manual"
                manual.Source = New Uri("/manual.xaml", UriKind.Relative)

                Dim account As New FirstFloor.ModernUI.Presentation.Link
                account.DisplayName = "My Account"
                account.Source = New Uri("/adminAccount.xaml", UriKind.Relative)

                Dim logOut As New FirstFloor.ModernUI.Presentation.Link
                logOut.DisplayName = "Log Out"
                logOut.Source = New Uri("/logOut.xaml", UriKind.Relative)


                mainMenu(9).Links.Add(manual)
                mainMenu(9).Links.Add(account)
                mainMenu(9).Links.Add(logOut)

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

    'When loaded this window checks if the database and a folder called reports exists
    'If not it creates them
    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        Dim ut As Utility = New Utility()

        objConnection = New SqlConnection("server=localhost;Integrated Security=SSPI")
        objCommand = New SqlCommand()
        objCommand.Connection = objConnection
        objCommand.CommandType = CommandType.Text

        Dim databaseSt As Boolean = Utility.CheckDatabaseExists("localhost", "CambridgeProject")

        Dim filePath As String = Directory.GetCurrentDirectory() & "\databaseScripts.sql"
        Dim filePathTables As String = Directory.GetCurrentDirectory() & "\databaseTableScripts.sql"

        If databaseSt = False Then
            Dim file As FileInfo = New FileInfo(filePath)
            Dim file1 As FileInfo = New FileInfo(filePathTables)
            Dim script As String = file.OpenText().ReadToEnd()
            Dim rscript As String = script.Replace("GO", "")
            Dim script1 As String = file1.OpenText().ReadToEnd()
            Dim rscript1 As String = script1.Replace("GO", "")

            objCommand.CommandText = rscript

            objCommand.Connection.Open()
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = rscript1
            objCommand.ExecuteNonQuery()
            objConnection.Close()

            Dim path As String = Directory.GetCurrentDirectory()
            path = path.Substring(0, 1)
            path = path & ":\Reports"

            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
        End If

    End Sub
End Class
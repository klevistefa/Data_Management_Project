Imports FirstFloor.ModernUI.Windows.Controls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security

Class GuestLogin
    'Button that's used to log in as a gues
    Private Sub btnGuest_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnGuest.Click

        'Opening a new window with the guest's options and functions
        Dim newWindow As New MainWindow(-1)

        Dim mainMenu(4) As FirstFloor.ModernUI.Presentation.LinkGroup
        mainMenu(1) = New FirstFloor.ModernUI.Presentation.LinkGroup
        mainMenu(1).DisplayName = "Movies"
        mainMenu(2) = New FirstFloor.ModernUI.Presentation.LinkGroup
        mainMenu(2).DisplayName = "Games"
        mainMenu(3) = New FirstFloor.ModernUI.Presentation.LinkGroup
        mainMenu(3).DisplayName = "Log out"
        

        newWindow.MenuLinkGroups.Add(mainMenu(1))
        newWindow.MenuLinkGroups.Add(mainMenu(2))
        newWindow.MenuLinkGroups.Add(mainMenu(3))

        newWindow.Show()

        Dim newMovies As New FirstFloor.ModernUI.Presentation.Link
        newMovies.DisplayName = "New Movies"
        newMovies.Source = New Uri("/NewMovies.xaml", UriKind.Relative)

        Dim trMovies As New FirstFloor.ModernUI.Presentation.Link
        trMovies.DisplayName = "Top rated"
        trMovies.Source = New Uri("/TopRatedMovies.xaml", UriKind.Relative)

        Dim searchMovie As New FirstFloor.ModernUI.Presentation.Link
        searchMovie.DisplayName = "Search Movie"
        searchMovie.Source = New Uri("/SearchMovie.xaml", UriKind.Relative)

        Dim topSoldMovies As New FirstFloor.ModernUI.Presentation.Link
        topSoldMovies.DisplayName = "Top Sold Movies"
        topSoldMovies.Source = New Uri("/TopSoldMovies.xaml", UriKind.Relative)

        mainMenu(1).Links.Add(newMovies)
        mainMenu(1).Links.Add(trMovies)
        mainMenu(1).Links.Add(topSoldMovies)
        mainMenu(1).Links.Add(searchMovie)

        Dim newGames As New FirstFloor.ModernUI.Presentation.Link
        newGames.DisplayName = "New games"
        newGames.Source = New Uri("/NewGames.xaml", UriKind.Relative)

        Dim trGames As New FirstFloor.ModernUI.Presentation.Link
        trGames.DisplayName = "Top rated"
        trGames.Source = New Uri("/TopRatedGames.xaml", UriKind.Relative)

        Dim searchGame As New FirstFloor.ModernUI.Presentation.Link
        searchGame.DisplayName = "Search game"
        searchGame.Source = New Uri("/SearchGame.xaml", UriKind.Relative)

        Dim topSoldGames As New FirstFloor.ModernUI.Presentation.Link
        topSoldGames.DisplayName = "Top Sold Games"
        topSoldGames.Source = New Uri("/TopSoldGames.xaml", UriKind.Relative)

        mainMenu(2).Links.Add(newGames)
        mainMenu(2).Links.Add(trGames)
        mainMenu(2).Links.Add(topSoldGames)
        mainMenu(2).Links.Add(searchGame)

        Dim logOut As New FirstFloor.ModernUI.Presentation.Link
        logOut.DisplayName = "Log out"
        logOut.Source = New Uri("/logOut.xaml", UriKind.Relative)

        mainMenu(3).Links.Add(logOut)

        Dim currentWindow As ModernWindow = ModernWindow.GetWindow(Me)
        currentWindow.Close()
    End Sub
End Class

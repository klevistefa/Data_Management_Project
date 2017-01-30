Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Class NewGames

    Private db As DBTransaction
    Private gamesTable As DataTable
    Private view As CollectionView

    'Sub fired when page is loaded and calls two others subs to fill the page
    Private Sub Grid1_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Grid1.Loaded
        fillDataTable()
        showPosition()
    End Sub

    'Sub to show navigation in the page
    Private Sub showPosition()
        txtPosition.Text = view.CurrentPosition + 1 & " of " & view.Count
        showPhoto()
    End Sub

    'Sub to fill the data table with new games from database
    Private Sub fillDataTable()
        gamesTable = New DataTable()
        db = New DBTransaction()

        gamesTable = db.getNewGamesTable()
        view = CollectionViewSource.GetDefaultView(gamesTable)
        DataContext = view
    End Sub

    'Button that moves to the next in row
    Private Sub btnNext_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnNext.Click
        If view.CurrentPosition + 1 < view.Count Then
            view.MoveCurrentToNext()
            showPosition()
        End If
    End Sub

    'Button that moves to the last in row
    Private Sub btnLast_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnLast.Click
        view.MoveCurrentToLast()
        showPosition()
    End Sub

    'Button that moves to the previous in row
    Private Sub btnPrev_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnPrev.Click
        If view.CurrentPosition - 1 > -1 Then
            view.MoveCurrentToPrevious()
            showPosition()
        End If
    End Sub

    'Button that moves to the first in row
    Private Sub btnFirst_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnFirst.Click
        view.MoveCurrentToFirst()
        showPosition()
    End Sub

    'Sub that shows the photo of the game in the field for the image
    Private Sub showPhoto()
        Dim dr As DataRow = gamesTable.Rows(view.CurrentPosition)
        Dim img() As Byte = dr.Item("game_image")

        Dim bitMap As BitmapImage = New BitmapImage()
        Dim ms As MemoryStream = New MemoryStream(img)

        bitMap.BeginInit()
        bitMap.StreamSource = ms
        bitMap.EndInit()

        imgGame.Source = bitMap
    End Sub
End Class

Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Class TopSoldMovies
    Private db As DBTransaction
    Private moviesTable As DataTable
    Private view As CollectionView

    'Sub to show navigation in the page
    Private Sub showPosition()
        txtPosition.Text = view.CurrentPosition + 1 & " of " & view.Count
        showPhoto()
    End Sub

    'Sub to fill the data table with top sold movies from database
    Private Sub fillDataTable()
        moviesTable = New DataTable()
        db = New DBTransaction()

        moviesTable = db.getMostSoldMoview()
        view = CollectionViewSource.GetDefaultView(moviesTable)
        DataContext = view
    End Sub

    'Button that moves to the next in the row
    Private Sub btnNext_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnNext.Click
        If view.CurrentPosition + 1 < view.Count Then
            view.MoveCurrentToNext()
            showPosition()
        End If
    End Sub

    'Button that moves to the last in the row
    Private Sub btnLast_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnLast.Click
        view.MoveCurrentToLast()
        showPosition()
    End Sub

    'Button that moves to the previous in the row
    Private Sub btnPrev_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnPrev.Click
        If view.CurrentPosition - 1 > -1 Then
            view.MoveCurrentToPrevious()
            showPosition()
        End If
    End Sub

    'Button that moves to the first in the row
    Private Sub btnFirst_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnFirst.Click
        view.MoveCurrentToFirst()
        showPosition()
    End Sub

    'Sub that shows the photo of the movie in the image field
    Private Sub showPhoto()
        Dim dr As DataRow = moviesTable.Rows(view.CurrentPosition)
        Dim img() As Byte = dr.Item("film_image")

        Dim bitMap As BitmapImage = New BitmapImage()
        Dim ms As MemoryStream = New MemoryStream(img)

        bitMap.BeginInit()
        bitMap.StreamSource = ms
        bitMap.EndInit()

        imgFilm.Source = bitMap
    End Sub

    'Sub fired when page is loaded and calls two others subs to fill the page
    Private Sub Page_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        fillDataTable()
        showPosition()
    End Sub
End Class

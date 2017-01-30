Imports System.Data

Class MySales

    Private db As DBTransaction
    Private fSalesDataTable As DataTable

    'Sub that fills the table with all the sales of the movies of the employee logged in
    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        db = New DBTransaction()
        fSalesDataTable = New DataTable()

        fSalesDataTable = db.getMyFilmSales(MainWindow.getId())
        dgSales.ItemsSource = fSalesDataTable.DefaultView

        Dim total As Integer = fSalesDataTable.Rows.Count

        txtSales.Text = total.ToString
    End Sub
End Class

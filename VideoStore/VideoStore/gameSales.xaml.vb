Imports System.Data

Class gameSales

    Private db As DBTransaction
    Private gSalesDataTable As DataTable

    'Sub that fill the table with all the sales of the games of the employee logged in
    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        db = New DBTransaction()
        gSalesDataTable = New DataTable()

        gSalesDataTable = db.getMyGameSales(MainWindow.getId())
        dgSales.ItemsSource = gSalesDataTable.DefaultView

        Dim total As Integer = gSalesDataTable.Rows.Count

        txtSales.Text = total.ToString
    End Sub
End Class

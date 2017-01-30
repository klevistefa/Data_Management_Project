Imports System.Data
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Class salesGames

    Private db As DBTransaction
    Private dTableSales As DataTable

    'Sub that's used to initialize some variable to be filled with the games' sales
    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        db = New DBTransaction()
        dTableSales = New DataTable()

        dgSales.ItemsSource = Nothing

        dtpEnd.SelectedDate = Date.Now
        dtpBegin.SelectedDate = Date.Now.AddDays(-1)

        btnReport.IsEnabled = False
    End Sub

    'Button used to show games' sales between the selected interval
    Private Sub btnShowSales_Click(sender As Object, e As RoutedEventArgs) Handles btnShowSales.Click
        Dim date1 As Date = dtpBegin.Text
        Dim date2 As Date = dtpEnd.Text
        dTableSales.Clear()
        dTableSales = db.getAllGameSalesTable(date1, date2)

        dgSales.ItemsSource = dTableSales.DefaultView()

        Dim total As Double = 0.0

        If dTableSales.Rows.Count <> 0 Then
            total = dTableSales.Compute("SUM(game_price)", "")
        End If

        txtTotalSales.Text = total.ToString()

        btnReport.IsEnabled = True
    End Sub

    'Sub fired when clicked the button generate in order to generate a PDF report of the games' sales for the selected date interval
    Private Sub btnReport_Click(sender As Object, e As RoutedEventArgs) Handles btnReport.Click
        Dim fileName As String = InputBox("Enter the filename:", "Report name:")
        fileName = fileName & ".pdf"
        Dim path As String = Utility.getDrive() & ":\Reports\" & fileName

        While File.Exists(path)
            fileName = InputBox("File already exists. Enter a new one:", "Report name:")
            fileName = fileName & ".pdf"
            path = Utility.getDrive() & ":\Reports\" & fileName
        End While

        Dim pdfRepoert As New Document()
        Dim pdfWriter As PdfWriter = pdfWriter.GetInstance(pdfRepoert, New FileStream(path, FileMode.Create))

        pdfRepoert.Open()

        Dim pdfTable1 As New PdfPTable(2)

        Dim cellOne As PdfPCell = New PdfPCell(New Phrase("Video Store Game Sales"))
        cellOne.Colspan = 2
        cellOne.HorizontalAlignment = 1
        cellOne.Border = Rectangle.NO_BORDER
        pdfTable1.AddCell(cellOne)

        Dim cellTwo As PdfPCell = New PdfPCell(New Phrase("Begin date: " & dtpBegin.Text.ToString()))
        cellTwo.Border = Rectangle.NO_BORDER
        pdfTable1.AddCell(cellTwo)
        Dim cellThree As PdfPCell = New PdfPCell(New Phrase("End date: " & dtpEnd.Text.ToString()))
        cellThree.Border = Rectangle.NO_BORDER
        pdfTable1.AddCell(cellThree)

        pdfRepoert.Add(pdfTable1)

        pdfRepoert.Add(New Paragraph("   "))
        pdfRepoert.Add(New Paragraph("   "))

        Dim salesTable As New PdfPTable(4)
        salesTable.AddCell("Title")
        salesTable.AddCell("Price")
        salesTable.AddCell("Cost")
        salesTable.AddCell("Sale Date")

        Dim index As Integer = 0
        While index < dTableSales.Rows.Count
            salesTable.AddCell(dTableSales.Rows(index).Item("game_title"))
            salesTable.AddCell(dTableSales.Rows(index).Item("game_price"))
            salesTable.AddCell(dTableSales.Rows(index).Item("game_cost"))
            salesTable.AddCell(dTableSales.Rows(index).Item("game_sale_date"))

            index += 1
        End While

        pdfRepoert.Add(salesTable)

        pdfRepoert.Add(New Paragraph("   "))
        pdfRepoert.Add(New Paragraph("   "))

        Dim totalSales As New PdfPTable(2)
        totalSales.WidthPercentage = 50
        Dim c1 As PdfPCell = New PdfPCell(New Phrase("Total Sales:"))
        c1.Border = Rectangle.NO_BORDER
        Dim c2 As PdfPCell = New PdfPCell(New Phrase(txtTotalSales.Text.ToString() & " LEK"))
        c2.Border = Rectangle.NO_BORDER

        totalSales.AddCell(c1)
        totalSales.AddCell(c2)

        pdfRepoert.Add(totalSales)

        pdfRepoert.Close()

        System.Diagnostics.Process.Start(path)
    End Sub
End Class

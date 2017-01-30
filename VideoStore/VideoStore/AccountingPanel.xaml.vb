Imports System.Data
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Class AccountingPanel

    Private db As DBTransaction
    Private dTable As DataTable

    Dim salaries As Double = 0.0
    Dim bonuses As Double = 0.0
    Dim index As Integer = 0
    Dim income As Double = 0.0

    Dim mSales As Double
    Dim mCost As Double
    Dim gSales As Double
    Dim gCost As Double
    Dim mRents As Double

    'Subroutine fired when loading the window in order to fill needed information
    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        Dim thisMonth As Integer = Month(Date.Now())
        db = New DBTransaction()
        dTable = New DataTable()
        dTable = db.getAllEmployees()

        While index < dTable.Rows.Count
            salaries += dTable.Rows(index).Item("employee_salary")
            bonuses += dTable.Rows(index).Item("employee_bonus")
            index += 1
        End While

        Dim mSales As Double = db.getTotalMovieSales(Date.Now())
        Dim mCost As Double = db.getTotalMoviesCost(Date.Now())
        Dim gSales As Double = db.getTotalGamesSales(Date.Now())
        Dim gCost As Double = db.getTotalGamesCost(Date.Now())
        Dim mRents As Double = db.getTotalMovieRents(Date.Now())

        lblMonth.Content = "Accouting Panel For Month: " & MonthName(Month(Now))
        txtMovieSales.Text = mSales.ToString()
        txtGameSales.Text = gSales.ToString()
        txtMovieCost.Text = mCost.ToString()
        txtGameCost.Text = gCost.ToString()
        txtMovieRents.Text = mRents.ToString()
        dgEmployees.ItemsSource = dTable.DefaultView()
        txtEmployeeBonuses.Text = bonuses.ToString()
        txtEmployeeSalaries.Text = salaries.ToString()
    End Sub

    'Subroutine fired when clicked the button calculate in order to calculate month income
    Private Sub btnCalculate_Click(sender As Object, e As RoutedEventArgs) Handles btnCalculate.Click
        If txtInternet.Text.Length = 0 Or txtEnergy.Text.Length = 0 Then
            lblStatus.Content = "Please fill the internet and energy."
        Else
            Dim internet As Double = CType(txtInternet.Text, Double)
            Dim energy As Double = CType(txtEnergy.Text, Double)
            income = (mSales + gSales + mRents) - (mCost + gCost + energy + internet + salaries + bonuses)
            txtIncome.Text = "Total income this month: " & income & " LEK"
        End If
    End Sub

    'Subroutine fired when clicked the button generate in order to generate a PDF report and close this month accounting
    Private Sub btnGenerate_Click(sender As Object, e As RoutedEventArgs) Handles btnGenerate.Click
        Dim mr As Integer = MessageBox.Show("Do you want to close this month accounting generating a month report and reseting employees bonuses?", "Accounting", MessageBoxButton.YesNo)
        If mr = vbYes Then
            If txtIncome.Text.Length <> 0 Then
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

                Dim cellOne As PdfPCell = New PdfPCell(New Phrase("Video Store Month Report"))
                cellOne.Colspan = 2
                cellOne.HorizontalAlignment = 1
                cellOne.Border = Rectangle.NO_BORDER
                pdfTable1.AddCell(cellOne)
                pdfRepoert.Add(pdfTable1)

                pdfRepoert.Add(New Paragraph("   "))
                pdfRepoert.Add(New Paragraph("   "))

                Dim p1 As Paragraph = New Paragraph("Total Movie Sales:  " & txtMovieSales.Text.ToString() & " LEK")
                Dim p2 As Paragraph = New Paragraph("Total Games Sales:  " & txtGameSales.Text.ToString() & " LEK")
                Dim p3 As Paragraph = New Paragraph("Total Movie Rents:  " & txtMovieRents.Text.ToString() & " LEK")
                Dim p4 As Paragraph = New Paragraph("Total Movie Costs:  " & txtMovieCost.Text.ToString() & " LEK")
                Dim p5 As Paragraph = New Paragraph("Total Game Costs:  " & txtGameCost.Text.ToString() & " LEK")
                Dim p6 As Paragraph = New Paragraph("Employee Salaries:  " & txtEmployeeSalaries.Text.ToString() & " LEK")
                Dim p7 As Paragraph = New Paragraph("Employee Bonuses:  " & txtEmployeeBonuses.Text.ToString() & " LEK")
                Dim p8 As Paragraph = New Paragraph("Energy Bill:  " & txtEnergy.Text.ToString() & " LEK")
                Dim p9 As Paragraph = New Paragraph("Internet Bill:  " & txtInternet.Text.ToString() & " LEK")
                Dim p10 As Paragraph = New Paragraph("Month Income:  " & txtIncome.Text.ToString() & " LEK")

                pdfRepoert.Add(p1)
                pdfRepoert.Add(p2)
                pdfRepoert.Add(p3)
                pdfRepoert.Add(p4)
                pdfRepoert.Add(p5)
                pdfRepoert.Add(p6)
                pdfRepoert.Add(p7)
                pdfRepoert.Add(p8)
                pdfRepoert.Add(p9)
                pdfRepoert.Add(p10)

                pdfRepoert.Add(New Paragraph("   "))
                pdfRepoert.Add(New Paragraph("   "))

                pdfRepoert.Close()

                System.Diagnostics.Process.Start(path)
                db.zeroBonuses()
                clearFields()
            Else
                lblStatus.Content = "Please fill the internet and energy bill and calculate this month total income."
            End If
        End If
    End Sub

    'Sub to clear all text fields after the report for this month is generated
    Private Sub clearFields()
        txtEmployeeBonuses.Text = ""
        txtEmployeeSalaries.Text = ""
        txtEnergy.Text = ""
        txtGameCost.Text = ""
        txtGameSales.Text = ""
        txtIncome.Text = ""
        txtInternet.Text = ""
        txtMovieCost.Text = ""
        txtMovieRents.Text = ""
        txtMovieSales.Text = ""
    End Sub
End Class

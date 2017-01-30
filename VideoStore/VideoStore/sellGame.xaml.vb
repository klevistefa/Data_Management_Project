Imports System.Data

Class sellGame

    Private db As DBTransaction
    Private dtableGames As DataTable
    Private dtableMembers As DataTable
    Private dr1 As DataRowView

    'Sub that fills two data grids when the grid is loaded. One with the games in the database and the other with the members in the database
    Private Sub Grid_Loaded_1(sender As Object, e As RoutedEventArgs)
        db = New DBTransaction()
        dtableGames = New DataTable()
        dr1 = Nothing

        dtableGames = db.getAllGames()
        dgGames.ItemsSource = dtableGames.DefaultView

        dtableMembers = New DataTable()
        dtableMembers = db.getAllMembers()
        dgMembers.ItemsSource = dtableMembers.DefaultView

        lblError2.Content = ""
        lblSuccess.Content = ""
    End Sub

    'Sub that filters the list depending on what you write in the textbox
    Private Sub txtGameFilter_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtGameFilter.TextChanged
        dtableGames.DefaultView.RowFilter = "game_title LIKE '*" & txtGameFilter.Text & "*'"
    End Sub

    'Sub that filters the list depending on what you write in the textbox
    Private Sub txtMemberFilter_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtMemberFilter.TextChanged
        dtableMembers.DefaultView.RowFilter = "member_first_name LIKE '*" & txtMemberFilter.Text & "*'"
    End Sub

    'Sub that fills the fields for the games with the selected game
    Private Sub dgGames_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dgGames.MouseDoubleClick
        dr1 = dgGames.SelectedItem
        txtGameID.Text = dr1.Item("game_id")
        txtStockN.Text = dr1.Item("game_stock_new")
        txtStockUsed.Text = dr1.Item("game_stock_used")
        txtPrice.Text = dr1.Item("game_price")
        txtCost.Text = dr1.Item("game_cost")

        lblError2.Content = ""
        lblSuccess.Content = ""

        rbtNew.IsEnabled = True
        rbtUsed.IsEnabled = True

        If txtMemberId.Text.Length <> 0 Then

            Dim gamesBought As Integer = CType(txtGamesBought.Text, Integer)
            If gamesBought Mod 10 = 0 And gamesBought > 0 Then
                txtPrice.Text = "0"
            ElseIf CType(txtDiscount.Text.ToString, Integer) <> 0 And txtPrice.Text.Length <> 0 Then
                Dim pr As Double = CType(txtPrice.Text.ToString, Double)
                pr = pr * 0.9
                txtPrice.Text = pr.ToString
            Else
                txtPrice.Text = dr1.Item("game_price")
            End If

        End If

        
    End Sub

    'Sub that fills the fields for the members with the selected member
    Private Sub dgMembers_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dgMembers.MouseDoubleClick
        Dim dr As DataRowView = dgMembers.SelectedItem
        txtMemberId.Text = dr.Item("member_id")
        txtRegistrationDate.Text = dr.Item("member_registration_date")
        txtGamesBought.Text = dr.Item("member_games_bought")
        txtDiscount.Text = dr.Item("member_discount")

        If txtMemberId.Text.Length <> 0 Then

            Dim gamesBought As Integer = CType(txtGamesBought.Text, Integer)
            If gamesBought Mod 10 = 0 And gamesBought > 0 Then
                txtPrice.Text = "0"
            ElseIf CType(txtDiscount.Text.ToString, Integer) <> 0 And txtPrice.Text.Length <> 0 Then
                Dim pr As Double = getRealPrice()
                pr = pr * 0.9
                txtPrice.Text = pr.ToString
            ElseIf rbtUsed.IsChecked = False Then
                txtPrice.Text = getRealPrice().ToString
            End If

        End If

        lblError2.Content = ""
        lblSuccess.Content = ""
    End Sub

    'Sub that sets the price to zero if the game is used
    Private Sub rbtUsed_Checked(sender As Object, e As RoutedEventArgs) Handles rbtUsed.Checked
        Dim pr As Double = getRealPrice()
        Dim cs As Double = getRealCost()
        If pr <> 0 Then
            pr = pr - pr * 0.3
            txtPrice.Text = pr.ToString

            cs = cs - cs * 0.3
            txtCost.Text = cs.ToString
        End If
    End Sub

    'Sub that sets the normal price if the game is new
    Private Sub rbtNew_Checked(sender As Object, e As RoutedEventArgs) Handles rbtNew.Checked
        Dim pr As Double = txtPrice.Text.ToString()
        txtPrice.Text = pr
    End Sub

    'Sub that clears all the fields
    Private Sub clearFields()
        txtCost.Text = ""
        txtDiscount.Text = ""
        txtGameID.Text = ""
        txtGamesBought.Text = ""
        txtMemberId.Text = ""
        txtPrice.Text = ""
        txtRegistrationDate.Text = ""
        txtStockN.Text = ""
        txtStockUsed.Text = ""
        rbtNew.IsEnabled = False
        rbtUsed.IsEnabled = False
    End Sub

    'Button that clears all the fields
    Private Sub Button1_Click(sender As Object, e As RoutedEventArgs) Handles Button1.Click
        clearFields()
    End Sub

    'Button that sells game
    Private Sub btnSellGame_Click(sender As Object, e As RoutedEventArgs) Handles btnSellGame.Click
        If txtGameID.Text.Length <> 0 Then
            If rbtNew.IsChecked = False And rbtUsed.IsChecked = False Then
                lblError2.Content = ""
                lblError2.Content = "Select New/Used"
            Else
                Dim st As Boolean = True

                If rbtNew.IsChecked Then
                    st = True
                Else
                    st = False
                End If

                If st = True Then
                    If CType(txtStockN.Text.ToString, Integer) = 0 Then
                        lblError2.Content = "No Stock"
                        lblSuccess.Content = ""
                    Else
                        Dim mid As Integer = 0
                        If txtMemberId.Text.Length <> 0 Then
                            mid = CType(txtMemberId.Text, Integer)
                        End If
                        Dim gid As Integer = CType(txtGameID.Text, Integer)
                        Dim eid As Integer = MainWindow.getId()
                        Dim price As Double = CType(txtPrice.Text, Double)
                        Dim cost As Double = CType(txtCost.Text, Double)

                        Dim rs As Integer = db.sellGame(gid, eid, mid, price, cost, st)

                        If rs > 0 Then
                            lblSuccess.Content = "Sold"
                            lblError2.Content = ""
                            rbtUsed.IsChecked = False
                            rbtNew.IsChecked = False
                            clearFields()
                            dgGames.ItemsSource = db.getAllGames().DefaultView
                            dgMembers.ItemsSource = db.getAllMembers().DefaultView  
                        Else
                            lblError2.Content = "Error"
                            lblSuccess.Content = ""
                        End If
                    End If
                Else
                    If CType(txtStockUsed.Text.ToString, Integer) = 0 Then
                        lblError2.Content = "No Stock"
                        lblSuccess.Content = ""
                    Else
                        Dim mid As Integer = 0
                        If txtMemberId.Text.Length <> 0 Then
                            mid = CType(txtMemberId.Text, Integer)
                        End If
                        Dim gid As Integer = CType(txtGameID.Text, Integer)
                        Dim eid As Integer = MainWindow.getId()
                        Dim price As Double = CType(txtPrice.Text, Double)
                        Dim cost As Double = CType(txtCost.Text, Double)

                        Dim rs As Integer = db.sellGame(gid, eid, mid, price, cost, st)
                        If rs > 0 Then
                            lblSuccess.Content = "Sold"
                            lblError2.Content = ""
                            clearFields()
                            dgGames.ItemsSource = db.getAllGames().DefaultView
                            dgMembers.ItemsSource = db.getAllMembers().DefaultView
                        Else
                            lblError2.Content = "Error"
                            lblSuccess.Content = ""
                        End If

                    End If
                End If
            End If
        End If
    End Sub

    'Button that checks for discount for a member
    Private Sub btnCheck_Click(sender As Object, e As RoutedEventArgs) Handles btnCheck.Click
        If txtGamesBought.Text.Length <> 0 Then
            Dim gamesBought As Integer = CType(txtGamesBought.Text, Integer)

            If gamesBought Mod 10 = 0 And gamesBought > 0 Then
                lblSuccess.Content = "Success"
                lblError2.Content = ""
                txtPrice.Text = "0"
            Else
                lblError2.Content = "No discount"
                lblSuccess.Content = ""
            End If
        Else
            lblError2.Content = "Select a member"
            lblSuccess.Content = ""
        End If
    End Sub

    'Gets the game real price
    Private Function getRealPrice() As Double
        Dim drt As DataRowView = dgGames.SelectedItem
        Dim price As Double = drt.Item("game_price")

        Return price
    End Function

    'Gets the game real cost
    Private Function getRealCost() As Double
        Dim drt As DataRowView = dgGames.SelectedItem
        Dim price As Double = drt.Item("game_cost")

        Return price
    End Function

    Private Sub rbtNew_Click(sender As Object, e As RoutedEventArgs) Handles rbtNew.Click
        If rbtNew.IsChecked Then
            txtPrice.Text = getRealPrice()
            txtCost.Text = getRealCost()
        End If
    End Sub
End Class

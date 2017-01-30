Imports FirstFloor.ModernUI.Windows.Controls
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class MainWindow
    Inherits ModernWindow

    'Private variable of this class
    Private dbT As DBTransaction
    Private dataTable As DataTable

    'This ID is used to store who is logged in and identify them in any time
    Public Shared id As Integer

    'Class constructor
    Public Sub New(ByVal eId As Integer)
        id = eId
        dbT = New DBTransaction()
        dataTable = New DataTable()

        dbT.checkMembers()
    End Sub

    'Function to get the logged in user id
    Public Shared Function getId() As Integer
        Return id
    End Function
End Class

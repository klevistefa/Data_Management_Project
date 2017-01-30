Imports System.Data.SqlClient
Imports System.IO

'Class that provides utility functions
Public Class Utility

    'Function that is fired when the application launches and controls if needed database exists
    'If not it creates it
    Public Shared Function CheckDatabaseExists(ByVal Server As String, ByVal Database As String) As Boolean
        Dim connString As String = ("Data Source=" + (Server + ";Initial Catalog=Master;Integrated Security=True;"))
        Dim cmdText As String = ("SELECT * FROM master.dbo.sysdatabases WHERE NAME='" _
                    + (Database + "'"))
        Dim bRet As Boolean = False
        Using sqlConnection As SqlConnection = New SqlConnection(connString)
            sqlConnection.Open()
            Using sqlCmd As SqlCommand = New SqlCommand(cmdText, sqlConnection)
                Using reader As SqlDataReader = sqlCmd.ExecuteReader
                    bRet = reader.HasRows
                End Using
            End Using
        End Using
        Return bRet
    End Function

    'Function to retrieve the database connection string
    Public Shared Function GetConnectionString() As String
        Dim filePath As String = ""

        If Environment.Is64BitOperatingSystem Then
            filePath = getDrive() & ":\Program Files (x86)\Video Store\VideoStoreManagement\connS.txt"
        Else
            filePath = getDrive() & ":\Program Files\Video Store\VideoStoreManagement\connS.txt"
        End If

        Dim file As FileInfo = New FileInfo(filePath)

        Dim cString As String = file.OpenText().ReadToEnd()

        Return cString
    End Function

    'Function to get the drive where the application is installed
    Public Shared Function getDrive() As String
        Dim path As String = Directory.GetCurrentDirectory()
        path = path.Substring(0, 1)

        Return path
    End Function
End Class

'Class used for to get all the data from the inputs of the administrator when adding an employee and put it to database
Public Class Employees
    Private cFname As String
    Private cLname As String
    Private cGender As Char
    Private cAge As String
    Private cDateofEmployment As Date
    Private cSalary As Double
    Private cUsername As String
    Private cPass As String

    Public Sub New(ByVal fname As String, ByVal lname As String, ByVal gender As Char, ByVal age As String,
                    ByVal dateofEmployment As Date, ByVal salary As Double, ByVal username As String, ByVal pass As String)

        cFname = fname
        cLname = lname
        cGender = gender
        cAge = age
        cDateofEmployment = dateofEmployment
        cSalary = salary
        cUsername = username
        cPass = pass

    End Sub

    Public Function getFname() As String
        Return cFname
    End Function

    Public Function getLname() As String
        Return cLname
    End Function

    Public Function getGender() As Char
        Return cGender
    End Function

    Public Function getAge() As String
        Return cAge
    End Function

    Public Function getDateofEmployment() As String
        Return cDateofEmployment
    End Function

    Public Function getSalary() As Double
        Return cSalary
    End Function

    Public Function getUsername() As String
        Return cUsername
    End Function

    Public Function getPass() As String
        Return cPass
    End Function

End Class

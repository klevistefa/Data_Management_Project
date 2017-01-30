'Class used for to get all the data from the inputs of the administrator or employee when adding a member and put it to database
Public Class Member
    Private cFname As String
    Private cLname As String
    Private cGender As String
    Private cAge As String
    Private cRegistrationDate As Date
    Private cEmail As String
    Private cPhone As String
    Private cDiscount

    Public Sub New(ByVal fname As String, ByVal lname As String, ByVal gender As String, ByVal age As String,
                    ByVal registrationDate As Date, ByVal email As String, ByVal phone As String, ByVal dicount As Double)

        cFname = fname
        cLname = lname
        cGender = gender
        cAge = age
        cRegistrationDate = registrationDate
        cEmail = email
        cPhone = phone
        cDiscount = dicount

    End Sub

    Public Function getFname() As String
        Return cFname
    End Function

    Public Function getLname() As String
        Return cLname
    End Function

    Public Function getGender() As String
        Return cGender
    End Function

    Public Function getAge() As String
        Return cAge
    End Function

    Public Function getRegistrationDate() As String
        Return cRegistrationDate
    End Function

    Public Function getEmail() As String
        Return cEmail
    End Function

    Public Function getPhone() As String
        Return cPhone
    End Function

    Public Function getDiscount() As Double
        Return cDiscount
    End Function
End Class

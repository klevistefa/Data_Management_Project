'Class used for to get all the data from the inputs of the administrator or employee when adding a game and put it to database
Public Class Games

    Private cName As String
    Private cCategory As String
    Private cReleaseDate As Date
    Private cProducer As String
    Private cRating As Double
    Private cDescription As String
    Private cStock As Integer
    Private cStockUsed As Integer
    Private cPrice As Double
    Private cCost As Double
    Private cImage() As Byte
    Private cCompatibility As Integer

    Public Sub New(ByVal name As String, ByVal category As String, ByVal releaseDate As Date, ByVal producer As String, ByVal rating As Double,
                   ByVal description As String, ByVal compatibility As Integer, ByVal stock As Integer, ByVal stockUsed As Integer, ByVal price As Double, ByVal cost As Double, ByVal img() As Byte)

        cCompatibility = compatibility
        cName = name
        cCategory = category
        cReleaseDate = releaseDate
        cProducer = producer
        cRating = rating
        cDescription = description
        cStock = stock
        cPrice = price
        cCost = cost
        cImage = img
        cStockUsed = stockUsed

    End Sub

    Public Function getName() As String
        Return cName
    End Function

    Public Function getCategory() As String
        Return cCategory
    End Function

    Public Function getReleaseDate() As Date
        Return cReleaseDate
    End Function

    Public Function getProducer() As String
        Return cProducer
    End Function

    Public Function getRating() As Double
        Return cRating
    End Function

    Public Function getDescription() As String
        Return cDescription
    End Function

    Public Function getStock() As String
        Return cStock
    End Function

    Public Function getPrice() As String
        Return cPrice
    End Function

    Public Function getCost() As String
        Return cCost
    End Function

    Public Function getImage() As Byte()
        Return cImage
    End Function

    Public Function getCompatibility()
        Return cCompatibility
    End Function

    Public Function getStockUsed()
        Return cStockUsed
    End Function
End Class

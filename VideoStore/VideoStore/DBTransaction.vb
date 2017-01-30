Imports System.Data
Imports System.Data.SqlClient

'Main class for handling database transactions

Public Class DBTransaction

    'Private variable which are used in this class
    Private objConnection As SqlConnection
    Private objCommand As SqlCommand
    Private objCommand1 As SqlCommand
    Private objAdapter As SqlDataAdapter
    Private objDataTable As DataTable
    Private objDataReader As SqlDataReader

    'Constructor for the class
    Public Sub New()
        Dim connString As String = Utility.GetConnectionString()
        objConnection = New SqlConnection(connString)
        objCommand = New SqlCommand()
        objCommand1 = New SqlCommand()
        objCommand.Connection = objConnection
        objCommand1.Connection = objConnection
        objAdapter = New SqlDataAdapter()
        objAdapter.SelectCommand = New SqlCommand()
        objAdapter.SelectCommand.Connection = objConnection
        objAdapter.SelectCommand.CommandType = CommandType.Text
        objDataTable = New DataTable()

    End Sub

    'Function to add a new movie in database
    Public Function addMovie(ByVal newMovie As Movies) As Integer
        objCommand.CommandText = "INSERT INTO Films(film_title, film_categories, film_release_date " &
           ",film_runtime, film_rating, film_description, film_times_sold, film_stock, film_price, film_cost, film_image)" &
           "VALUES(@title, @category, @releasedate, @runtime, @rating, @description, @times, @stock, @price, @cost, @image)"

        objCommand.Parameters.AddWithValue("@title", newMovie.getName())
        objCommand.Parameters.AddWithValue("@category", newMovie.getCategory())
        objCommand.Parameters.AddWithValue("@releasedate", newMovie.getReleaseDate())
        objCommand.Parameters.AddWithValue("@runtime", newMovie.getRuntime())
        objCommand.Parameters.AddWithValue("@rating", newMovie.getRating())
        objCommand.Parameters.AddWithValue("@description", newMovie.getDescription())
        objCommand.Parameters.AddWithValue("@times", 0)
        objCommand.Parameters.AddWithValue("@stock", newMovie.getStock())
        objCommand.Parameters.AddWithValue("@price", newMovie.getPrice())
        objCommand.Parameters.AddWithValue("@cost", newMovie.getCost())
        objCommand.Parameters.AddWithValue("@image", newMovie.getImage())

        objConnection.Open()
        Dim result As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        Return result
    End Function

    'Function to add a new game in database
    Public Function addGame(ByVal newGame As Games) As Integer
        objCommand.CommandText = "INSERT INTO Games(game_title, game_categories, game_release_date " &
           ",game_producer, game_rating, game_description, game_compatibility_id, game_times_sold, game_stock_new, game_stock_used, game_price, game_cost, game_image)" &
           "VALUES(@title, @category, @releasedate, @producer, @rating, @description, @com_id, @times, @stock, @stock_u, @price, @cost, @image)"

        objCommand.Parameters.AddWithValue("@title", newGame.getName())
        objCommand.Parameters.AddWithValue("@category", newGame.getCategory())
        objCommand.Parameters.AddWithValue("@releasedate", newGame.getReleaseDate())
        objCommand.Parameters.AddWithValue("@producer", newGame.getProducer())
        objCommand.Parameters.AddWithValue("@rating", newGame.getRating())
        objCommand.Parameters.AddWithValue("@cost", newGame.getCost())
        objCommand.Parameters.AddWithValue("@price", newGame.getPrice())
        objCommand.Parameters.AddWithValue("@description", newGame.getDescription())
        objCommand.Parameters.AddWithValue("@times", 0)
        objCommand.Parameters.AddWithValue("@stock", newGame.getStock())
        objCommand.Parameters.AddWithValue("@stock_u", newGame.getStockUsed())
        objCommand.Parameters.AddWithValue("@image", newGame.getImage())
        objCommand.Parameters.AddWithValue("@com_id", newGame.getCompatibility())

        objConnection.Open()
        Dim result As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return result
    End Function

    'Function to get all movies from database
    Public Function getAllMovies() As DataTable
        Dim moviesTable As DataTable = New DataTable()
        objAdapter.SelectCommand.CommandText = "SELECT film_id, film_title, film_categories, film_release_date, film_runtime," &
                                                "film_rating, film_description, film_stock, film_price, film_cost, film_image FROM Films"
        objConnection.Open()
        objAdapter.Fill(moviesTable)
        objConnection.Close()

        Return moviesTable

    End Function

    'Function to edit a movie in database
    Public Function editMovie(ByVal id As Integer, ByVal name As String, ByVal category As String, ByVal releaseDate As Date,
                              ByVal runtime As String, ByVal rating As Double, ByVal description As String,
                              ByVal stock As Integer, ByVal price As Double, ByVal img() As Byte)
        objCommand.CommandText = "UPDATE Films SET film_title = @title, film_categories = @category, film_release_date = @releasedate," &
                                 "film_runtime = @runtime, film_rating = @rating, film_description = @description, film_stock = @stock," &
                                 "film_price = @price, film_image=@img WHERE film_id = @id"

        objCommand.Parameters.AddWithValue("@title", name)
        objCommand.Parameters.AddWithValue("@category", category)
        objCommand.Parameters.AddWithValue("@releasedate", releaseDate)
        objCommand.Parameters.AddWithValue("@runtime", runtime)
        objCommand.Parameters.AddWithValue("@rating", rating)
        objCommand.Parameters.AddWithValue("@description", description)
        objCommand.Parameters.AddWithValue("@stock", stock)
        objCommand.Parameters.AddWithValue("@price", price)
        objCommand.Parameters.AddWithValue("@id", id)
        objCommand.Parameters.AddWithValue("@img", img)

        objConnection.Open()
        Dim result As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return result
    End Function

    'Function to add a new employee in database
    Public Function addEmployee(ByVal newEmployee As Employees) As Integer

        objCommand.CommandText = "INSERT INTO Employees (employee_first_name, employee_last_name, employee_gender, employee_age," &
            "employee_date_employment, employee_salary, employee_username, employee_password, employee_films_sold, employee_games_sold, employee_bonus) VALUES (@fname, @lname, @gender, @age" &
            ", @dateEmployment, @salary, @username, @password, @fs, @gs, @bonus)"

        objCommand.Parameters.AddWithValue("@fname", newEmployee.getFname)
        objCommand.Parameters.AddWithValue("@lname", newEmployee.getLname)
        objCommand.Parameters.AddWithValue("@gender", newEmployee.getGender)
        objCommand.Parameters.AddWithValue("@age", newEmployee.getAge)
        objCommand.Parameters.AddWithValue("@dateEmployment", newEmployee.getDateofEmployment)
        objCommand.Parameters.AddWithValue("@salary", newEmployee.getSalary)
        objCommand.Parameters.AddWithValue("@username", newEmployee.getUsername)
        objCommand.Parameters.AddWithValue("@password", newEmployee.getPass)
        objCommand.Parameters.AddWithValue("@fs", 0)
        objCommand.Parameters.AddWithValue("@gs", 0)
        objCommand.Parameters.AddWithValue("@bonus", 0)

        objConnection.Open()
        Dim result As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        Return result
    End Function

    'Function to add a new member in database
    Public Function addMember(ByVal newMember As Member) As Integer

        objCommand.CommandText = "INSERT INTO Members (member_first_name, member_last_name, member_gender, member_age," &
            "member_registration_date, member_email, member_phone_number, member_films_bought, member_games_bought, member_discount) VALUES" &
            "(@fname, @lname, @gender, @age, @registrationDate, @email, @phone, @films_bought, @games_bought, @dis)"

        objCommand.Parameters.AddWithValue("@fname", newMember.getFname)
        objCommand.Parameters.AddWithValue("@lname", newMember.getLname)
        objCommand.Parameters.AddWithValue("@gender", newMember.getGender)
        objCommand.Parameters.AddWithValue("@age", newMember.getAge)
        objCommand.Parameters.AddWithValue("@registrationDate", newMember.getRegistrationDate)
        objCommand.Parameters.AddWithValue("@email", newMember.getEmail)
        objCommand.Parameters.AddWithValue("@phone", newMember.getPhone)
        objCommand.Parameters.AddWithValue("@films_bought", 0)
        objCommand.Parameters.AddWithValue("@games_bought", 0)
        objCommand.Parameters.AddWithValue("@dis", newMember.getDiscount)

        objConnection.Open()
        Dim result As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        Return result

    End Function

    'Function to get all games from database
    Public Function getAllGames() As DataTable
        Dim gamesTable As DataTable = New DataTable()
        objAdapter.SelectCommand.CommandText = "SELECT game_id, game_title, game_cost, game_categories, game_release_date, game_producer," &
                                                "game_rating, game_description, game_stock_new, game_stock_used, game_price, game_image, compatibility_id, compatibility_name FROM Games " &
                                                "JOIN compatibilities ON game_compatibility_id=compatibility_id"
        objConnection.Open()
        objAdapter.Fill(gamesTable)
        objConnection.Close()

        Return gamesTable

    End Function

    'Function to edit a game in database
    Public Function editGame(ByVal id As Integer, ByVal name As String, ByVal category As String, ByVal releaseDate As Date,
                             ByVal producer As String, ByVal rating As Double, ByVal description As String,
                             ByVal stock As Integer, ByVal stockUsed As Integer, ByVal price As Double, ByVal comp As Integer, ByVal img() As Byte)

        objCommand.CommandText = "UPDATE Games SET game_title = @title, game_categories = @category, game_release_date = @releasedate," &
                                 "game_producer = @producer, game_rating = @rating, game_description = @description, game_stock_new = @stock," &
                                 "game_stock_used=@stockU, game_price = @price, game_compatibility_id=@com, game_image=@img WHERE game_id = @id"

        objCommand.Parameters.AddWithValue("@title", name)
        objCommand.Parameters.AddWithValue("@category", category)
        objCommand.Parameters.AddWithValue("@releasedate", releaseDate)
        objCommand.Parameters.AddWithValue("@producer", producer)
        objCommand.Parameters.AddWithValue("@rating", rating)
        objCommand.Parameters.AddWithValue("@description", description)
        objCommand.Parameters.AddWithValue("@stock", stock)
        objCommand.Parameters.AddWithValue("@stockU", stockUsed)
        objCommand.Parameters.AddWithValue("@price", price)
        objCommand.Parameters.AddWithValue("@id", id)
        objCommand.Parameters.AddWithValue("@com", comp)
        objCommand.Parameters.AddWithValue("@img", img)

        objConnection.Open()
        Dim result As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return result
    End Function

    'Function to get all employee from database
    Public Function getAllEmployees() As DataTable

        Dim employeesTable As DataTable = New DataTable()
        objAdapter.SelectCommand.CommandText = "SELECT employee_id, employee_first_name, employee_last_name," &
            "employee_gender, employee_age, employee_date_employment, employee_salary, employee_username, employee_password, employee_bonus FROM Employees"

        objConnection.Open()
        objAdapter.Fill(employeesTable)
        objConnection.Close()

        Return employeesTable

    End Function

    'Function to edit an employee in database
    Public Function editEmployee(ByVal id As Integer, ByVal fname As String, ByVal lname As String, ByVal gender As Char,
                                 ByVal age As String, ByVal employmentDate As Date, ByVal salary As Double, ByVal username As String,
                                 ByVal pass As String)
        objCommand.CommandText = "UPDATE Employees SET employee_first_name = @fname, employee_last_name = @lname," &
            "employee_gender = @gender, employee_age = @age, employee_date_employment = @employmentDate," &
            "employee_salary = @salary, employee_username = @username, employee_password = @pass WHERE employee_id = @id"

        objCommand.Parameters.AddWithValue("@id", id)
        objCommand.Parameters.AddWithValue("@fname", fname)
        objCommand.Parameters.AddWithValue("@lname", lname)
        objCommand.Parameters.AddWithValue("@gender", gender)
        objCommand.Parameters.AddWithValue("@age", age)
        objCommand.Parameters.AddWithValue("@employmentDate", employmentDate)
        objCommand.Parameters.AddWithValue("@salary", salary)
        objCommand.Parameters.AddWithValue("@username", username)
        objCommand.Parameters.AddWithValue("@pass", pass)

        objConnection.Open()
        Dim result As Integer = objCommand.ExecuteNonQuery
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return result

    End Function

    'Function to get 10 new movies from database
    Public Function getNewMoviesTable() As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT TOP 10 film_id, film_title, film_categories, film_release_date," &
            "film_runtime, film_rating, film_description, film_image FROM Films ORDER BY film_release_date DESC"
        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        Return objDataTable
    End Function

    'Function to get 10 most rated movies from database
    Public Function getMostRateMoview() As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT TOP 10 film_id, film_title, film_categories, film_release_date," &
            "film_runtime, film_rating, film_description, film_image FROM films ORDER BY film_rating DESC"
        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        Return objDataTable
    End Function

    'Function to get all members from database
    Public Function getAllMembers() As DataTable
        Dim membersTable As DataTable = New DataTable()
        objAdapter.SelectCommand.CommandText = "SELECT * FROM Members"

        objConnection.Open()
        objAdapter.Fill(membersTable)
        objConnection.Close()

        Return membersTable

    End Function

    'Function to edit a member in database
    Public Function editMember(ByVal id As Integer, ByVal fname As String, ByVal lname As String, ByVal gender As Char,
                                ByVal age As String, ByVal registrationDate As Date, ByVal email As String, ByVal phone As String)
        objCommand.CommandText = "UPDATE Members SET member_first_name = @fname, member_last_name = @lname," &
            "member_gender = @gender, member_age = @age, member_registration_date = @registrationDate," &
            "member_email = @email, member_phone_number = @phone WHERE member_id = @id"

        objCommand.Parameters.AddWithValue("@id", id)
        objCommand.Parameters.AddWithValue("@fname", fname)
        objCommand.Parameters.AddWithValue("@lname", lname)
        objCommand.Parameters.AddWithValue("@gender", gender)
        objCommand.Parameters.AddWithValue("@age", age)
        objCommand.Parameters.AddWithValue("@registrationDate", registrationDate)
        objCommand.Parameters.AddWithValue("@phone", phone)
        objCommand.Parameters.AddWithValue("@email", email)

        objConnection.Open()
        Dim result As Integer = objCommand.ExecuteNonQuery
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return result

    End Function

    'Function to get 10 new games from database
    Public Function getNewGamesTable() As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT TOP 10 game_id, game_title, game_categories, game_release_date," &
            "game_producer, game_rating, game_description, game_image FROM Games ORDER BY game_release_date DESC"
        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        Return objDataTable
    End Function

    'Function to get 10 most rated games from database
    Public Function getMostRateGame() As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT TOP 10 game_id, game_title, game_categories, game_release_date," &
            "game_producer, game_rating, game_description, game_image FROM Games ORDER BY game_rating DESC"
        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        Return objDataTable
    End Function

    'Function to delete a movie
    Public Function deleteMovie(ByVal id As Integer) As Integer

        objCommand.CommandText = "DELETE FROM Films WHERE film_id = @id"
        objCommand.Parameters.AddWithValue("@id", id)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to sell a movie
    Public Function sellMovie(ByVal employeeId As Integer, ByVal movieId As Integer, ByVal movieSellDate As Date, ByVal moviePrice As Double,
                              ByVal movieCost As Double, ByVal memberId As Integer) As Integer

        objCommand.CommandText = "INSERT INTO Film_sales(employee_sold_id, film_sold_id, film_sale_date, film_price, film_cost, member_bought_id)" &
            "VALUES (@employeeId, @movieId, @movieSellDate, @moviePrice, @movieCost, @memberId)"

        objCommand.Parameters.AddWithValue("@employeeId", employeeId)
        objCommand.Parameters.AddWithValue("@movieId", movieId)
        objCommand.Parameters.AddWithValue("@movieSellDate", movieSellDate)
        objCommand.Parameters.AddWithValue("@moviePrice", moviePrice)
        objCommand.Parameters.AddWithValue("@movieCost", movieCost)
        objCommand.Parameters.AddWithValue("@memberId", memberId)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objCommand.Parameters.Clear()

        Dim rs1 As Integer = 1
        Dim rs2 As Integer = 1

        If employeeId <> 0 Then
            objCommand.CommandText = "UPDATE Employees SET employee_films_sold = employee_films_sold + 1 WHERE employee_id = @employeeId"
            objCommand.Parameters.AddWithValue("@employeeId", employeeId)

            rs2 = objCommand.ExecuteNonQuery()

            objCommand.Parameters.Clear()

            Dim bonus As Double = checkBonus(employeeId)

            objCommand.CommandText = "UPDATE employees SET employee_bonus = @bonus WHERE employee_id=@eid"
            objCommand.Parameters.AddWithValue("@bonus", bonus)
            objCommand.Parameters.AddWithValue("@eid", employeeId)

            objCommand.ExecuteNonQuery()

            objCommand.Parameters.Clear()

        End If
        If memberId <> 0 Then

            objCommand.CommandText = "UPDATE members SET member_films_bought = member_films_bought + 1 WHERE member_id = @memberId"
            objCommand.Parameters.AddWithValue("@memberId", memberId)

            rs1 = objCommand.ExecuteNonQuery()
        End If
        objConnection.Close()

        objCommand.Parameters.Clear()

        objCommand.CommandText = "UPDATE films SET film_times_sold = film_times_sold + 1 WHERE film_id=@id"
        objCommand.Parameters.AddWithValue("@id", movieId)

        objConnection.Open()
        objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs + rs1 + rs2

    End Function

    'Function to decrement film stock
    Public Function decrementFilmStock(ByVal movieId As Integer) As Integer
        objCommand.CommandText = "UPDATE Films SET film_stock = film_stock - 1 WHERE film_id = @movieId"
        objCommand.Parameters.AddWithValue("@movieId", movieId)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to get all film sales between two dates
    Public Function getAllmovieSalesTable(ByVal date1 As Date, ByVal date2 As Date) As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT film_title, film_id, Film_sales.film_price, Film_sales.film_cost, film_sale_date FROM Films JOIN " &
            "Film_sales ON film_id = film_sold_id WHERE film_sale_date <= @edate AND film_sale_date >= @bdate ORDER BY film_sale_date DESC"
        objAdapter.SelectCommand.Parameters.AddWithValue("@bdate", date1)
        objAdapter.SelectCommand.Parameters.AddWithValue("@edate", date2)

        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        objAdapter.SelectCommand.Parameters.Clear()


        Return objDataTable
    End Function

    'Function to get total income from film sales between two dates
    Public Function getTotalMovieSalesbyDate(ByVal date1 As Date, ByVal date2 As Date) As Double
        objCommand.CommandText = "SELECT SUM(film_price) FROM Film_sales WHERE film_sale_date <= @edate AND film_sale_date >= @bdate"
        objCommand.Parameters.AddWithValue("@bdate", date1)
        objCommand.Parameters.AddWithValue("@edate", date2)

        objConnection.Open()
        objDataReader = objCommand.ExecuteReader()
        objDataReader.Read()

        Dim total As Double = 0.0

        If objDataReader.HasRows Then
            total = objDataReader.Item(0)
        End If
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return total

    End Function

    'Get number of rented movies from a member of the store
    Public Function getRentedMoviesFromMemberId(ByVal id As Integer) As Integer
        objCommand.CommandText = "SELECT COUNT(member_rent_id) FROM film_rent WHERE member_rent_id=@id AND rent_status=1"
        objCommand.Parameters.AddWithValue("@id", id)

        Dim t As Integer = 0

        objConnection.Open()
        objDataReader = objCommand.ExecuteReader()
        objDataReader.Read()
        If objDataReader.HasRows Then
            t = objDataReader.Item(0)
        End If
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return t
    End Function

    'Function to add a new rent entry in database
    Public Function rentMovie(ByVal fID As Integer, ByVal mID As Integer, ByVal price As Double) As Integer
        objCommand.CommandText = "INSERT INTO film_rent(film_rent_id, member_rent_id, rent_date, return_date, film_rent_price, rent_status) " &
            "VALUES(@fid, @mid, @rd, @red, @pr, @st)"
        objCommand.Parameters.AddWithValue("@fid", fID)
        objCommand.Parameters.AddWithValue("@mid", mID)
        objCommand.Parameters.AddWithValue("@rd", Date.Now())
        objCommand.Parameters.AddWithValue("@red", Date.Now.AddDays(3))
        objCommand.Parameters.AddWithValue("@pr", price)
        objCommand.Parameters.AddWithValue("@st", True)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to get all rented movies from database
    Public Function getAllRentedMovies() As DataTable
        objDataTable.Dispose()
        objAdapter.SelectCommand.CommandText = "SELECT film_id, film_title, film_stock, member_id, member_first_name, member_last_name, rent_date, return_date FROM " &
            "films JOIN film_rent ON film_id=film_rent_id JOIN members ON member_id=member_rent_id WHERE rent_status=1"

        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        Return objDataTable
    End Function

    'Function to return a rented movie in database
    Public Function returnMovie(ByVal fID As Integer, ByVal mID As Integer, ByVal rDate As Date) As Integer
        objCommand.CommandText = "UPDATE film_rent SET rent_status=0 WHERE film_rent_id=@fid AND member_rent_id=@mid AND rent_date=@rd"
        objCommand.Parameters.AddWithValue("@fid", fID)
        objCommand.Parameters.AddWithValue("@mid", mID)
        objCommand.Parameters.AddWithValue("@rd", rDate)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to postpone a rent
    Public Function postponeRent(ByVal fID As Integer, ByVal mID As Integer, ByVal rDate As Date) As Integer
        objCommand.CommandText = "UPDATE film_rent SET return_date=DATEADD(day, 1, return_date), film_rent_price=film_rent_price+50 WHERE film_rent_id=@fid AND member_rent_id=@mid AND rent_date=@rd"
        objCommand.Parameters.AddWithValue("@fid", fID)
        objCommand.Parameters.AddWithValue("@mid", mID)
        objCommand.Parameters.AddWithValue("@rd", rDate)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to increment movie stock
    Public Function incrementMovieStock(ByVal fID As Integer) As Integer
        objCommand.CommandText = "UPDATE Films SET film_stock = film_stock + 1 WHERE film_id = @movieId"
        objCommand.Parameters.AddWithValue("@movieId", fID)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to get all game compatibilities from database
    Public Function getCompatibilities() As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT * FROM compatibilities"

        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        Return objDataTable
    End Function

    'Function to add a new compatibility in database
    Public Function insertCompatibility(ByVal name As String) As Integer
        objCommand.CommandText = "INSERT INTO compatibilities(compatibility_name) VALUES(@name)"
        objCommand.Parameters.AddWithValue("@name", name)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to update a compatibility in database
    Public Function updateCompatibility(ByVal id As Integer, ByVal name As String) As Integer
        objCommand.CommandText = "UPDATE compatibilities SET compatibility_name=@name WHERE compatibility_id=@id"
        objCommand.Parameters.AddWithValue("@name", name)
        objCommand.Parameters.AddWithValue("@id", id)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to delete a member from database
    Public Function deleteMember(ByVal id As Integer) As Integer

        objCommand.CommandText = "DELETE FROM Members WHERE member_id = @id"
        objCommand.Parameters.AddWithValue("@id", id)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to delete a game from database
    Public Function deleteGame(ByVal id As Integer) As Integer

        objCommand.CommandText = "DELETE FROM Games WHERE game_id = @id"
        objCommand.Parameters.AddWithValue("@id", id)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to delete an employee from database
    Public Function deleteEmployee(ByVal id As Integer) As Integer

        objCommand.CommandText = "DELETE FROM Employees WHERE employee_id = @id"
        objCommand.Parameters.AddWithValue("@id", id)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Subroutine to check if members have more than one year
    'registered in our video store and sets their discount
    Public Sub checkMembers()
        objAdapter.SelectCommand.CommandText = "SELECT member_id, member_registration_date FROM members"
        objAdapter.Fill(objDataTable)

        Dim index As Integer = 0

        While index < objDataTable.Rows.Count
            Dim rDate As Date = objDataTable.Rows(index).Item("member_registration_date")
            Dim diff As Integer = DateDiff(DateInterval.Day, rDate, Date.Now)

            If diff >= 365 Then
                updateDiscount(objDataTable.Rows(index).Item("member_id"))
            End If

            index += 1
        End While
    End Sub

    'Function to update member's discount
    Private Sub updateDiscount(ByVal member_id As Integer)
        objCommand1.CommandText = "UPDATE members SET member_discount=10 WHERE member_id=@id"
        objCommand1.Parameters.AddWithValue("@id", member_id)

        objConnection.Open()
        objCommand1.ExecuteNonQuery()
        objConnection.Close()

        objCommand1.Parameters.Clear()
    End Sub

    'Function to get film sales of an employee
    Public Function getMyFilmSales(ByVal id As Integer) As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT film_sale_date, film_sales.film_price, film_title FROM films JOIN film_sales ON " &
            "film_id=film_sold_id WHERE employee_sold_id=@id AND YEAR([film_sale_date])=YEAR(GETDATE()) AND MONTH([film_sale_date])=MONTH(GETDATE())" &
            "AND DAY([film_sale_date])<=DAY(GETDATE())"
        objAdapter.SelectCommand.Parameters.AddWithValue("@id", id)

        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        Return objDataTable
    End Function

    'Function to get game sales of an employee
    Public Function getMyGameSales(ByVal id As Integer) As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT game_sale_date, games_sales.game_price, game_title FROM games JOIN games_sales ON " &
            "game_id=game_sold_id WHERE employee_sold_id=@id AND YEAR([game_sale_date])=YEAR(GETDATE()) AND MONTH([game_sale_date])=MONTH(GETDATE())" &
            "AND DAY([game_sale_date])<=DAY(GETDATE())"
        objAdapter.SelectCommand.Parameters.AddWithValue("@id", id)

        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        Return objDataTable
    End Function

    'Function to get all game sales between two dates
    Public Function getAllGameSalesTable(ByVal d1 As Date, ByVal d2 As Date) As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT game_title, game_id, games_sales.game_price, games_sales.game_cost, game_sale_date FROM games JOIN " &
            "games_sales ON game_id = game_sold_id WHERE game_sale_date <= @edate AND game_sale_date >= @bdate ORDER BY game_sale_date DESC"
        objAdapter.SelectCommand.Parameters.AddWithValue("@bdate", d1)
        objAdapter.SelectCommand.Parameters.AddWithValue("@edate", d2)

        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        objAdapter.SelectCommand.Parameters.Clear()


        Return objDataTable
    End Function

    'Function to sell a game
    Public Function sellGame(ByVal gId As Integer, ByVal eId As Integer, ByVal mID As Integer, ByVal price As Double, ByVal cost As Double, ByVal gNew As Boolean) As Integer
        objCommand.CommandText = "INSERT INTO games_sales(employee_sold_id, game_sold_id, game_sale_date, game_price, game_cost, member_bought_id) " &
            "VALUES(@eid, @gid, @dt, @pr, @co, @mid)"
        objCommand.Parameters.AddWithValue("@eid", eId)
        objCommand.Parameters.AddWithValue("@gid", gId)
        objCommand.Parameters.AddWithValue("@dt", Date.Now())
        objCommand.Parameters.AddWithValue("@pr", price)
        objCommand.Parameters.AddWithValue("@co", cost)
        objCommand.Parameters.AddWithValue("@mid", mID)

        objConnection.Open()
        Dim rs As Integer = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Dim status As String = ""

        If gNew = True Then
            status = "game_stock_new"
        Else
            status = "game_stock_used"
        End If

        objCommand.CommandText = "UPDATE games SET " & status & "=" & status & "-1 WHERE game_id=@gid"
        objCommand.Parameters.AddWithValue("@gid", gId)

        objConnection.Open()
        objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        objCommand.CommandText = "UPDATE games SET game_times_sold = game_times_sold + 1 WHERE game_id=@id"
        objCommand.Parameters.AddWithValue("@id", gId)

        objConnection.Open()
        objCommand.ExecuteNonQuery()
        objConnection.Close()

        If mID <> 0 Then

            objCommand.CommandText = "UPDATE members SET member_games_bought = member_games_bought + 1 WHERE member_id = @memberId"
            objCommand.Parameters.AddWithValue("@memberId", mID)

            objConnection.Open()
            objCommand.ExecuteNonQuery()
            objConnection.Close()
        End If

        If eId <> 0 Then
            objConnection.Open()

            objCommand.CommandText = "UPDATE Employees SET employee_games_sold = employee_games_sold + 1 WHERE employee_id = @employeeId"
            objCommand.Parameters.AddWithValue("@employeeId", eId)

            objCommand.ExecuteNonQuery()

            objCommand.Parameters.Clear()

            Dim bonus As Double = checkBonus(eId)

            objCommand.CommandText = "UPDATE employees SET employee_bonus = @bonus WHERE employee_id=@eid"
            objCommand.Parameters.AddWithValue("@bonus", bonus)
            objCommand.Parameters.AddWithValue("@eid", eId)

            objCommand.ExecuteNonQuery()

            objConnection.Close()
        End If

        objCommand.Parameters.Clear()
        Return rs
    End Function

    'Function to get all rents which have passed their return dates
    Public Function getPassedRents() As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT member_first_name, member_last_name, member_email, film_title FROM members JOIN film_rent " &
            "ON member_id=member_rent_id JOIN films ON film_id=film_rent_id WHERE return_date < GETDATE() AND rent_status=1"

        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        Return objDataTable
    End Function

    'Function to modify administrator password
    Public Function modifyAdminPassword(ByVal password As String) As Integer
        objCommand.CommandText = "UPDATE administrator SET admin_password=@pas WHERE admin_id=1"
        objCommand.Parameters.AddWithValue("@pas", password)

        Dim rs As Integer

        objConnection.Open()
        rs = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to modify employee password
    Public Function modifyEmployeePassword(ByVal password As String, ByVal id As Integer) As Integer
        objCommand.CommandText = "UPDATE employees SET employee_password=@pas WHERE employee_id=@id"
        objCommand.Parameters.AddWithValue("@pas", password)
        objCommand.Parameters.AddWithValue("@id", id)

        Dim rs As Integer

        objConnection.Open()
        rs = objCommand.ExecuteNonQuery()
        objConnection.Close()

        objCommand.Parameters.Clear()

        Return rs
    End Function

    'Function to get most sold games
    Public Function getMostSoldGames() As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT TOP 10 * FROM games ORDER BY game_times_sold DESC"
        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        Return objDataTable
    End Function

    'Function to get most sold movies
    Public Function getMostSoldMoview() As DataTable
        objAdapter.SelectCommand.CommandText = "SELECT TOP 10 * FROM films ORDER BY film_times_sold DESC"
        objConnection.Open()
        objAdapter.Fill(objDataTable)
        objConnection.Close()

        Return objDataTable
    End Function

    'Function to check employee bonuses
    Public Function checkBonus(ByVal eId As Integer) As Double
        objAdapter.SelectCommand.CommandText = "SELECT employee_films_sold, employee_games_sold, employee_salary FROM employees " &
            "WHERE employee_id=@eid"
        objAdapter.SelectCommand.Parameters.AddWithValue("@eid", eId)

        Dim bonus As Double = 0.0
        Dim sum As Integer = 0

        objAdapter.Fill(objDataTable)

        sum = objDataTable.Rows(0).Item("employee_films_sold") + objDataTable.Rows(0).Item("employee_games_sold")

        If sum Mod 200 = 0 Then
            bonus = objDataTable.Rows(0).Item(2) * 0.1
        End If

        objAdapter.SelectCommand.Parameters.Clear()

        Return bonus
    End Function

    'Function to get total movie sales income for current month
    Public Function getTotalMovieSales(ByVal currentDate As Date) As Double
        objAdapter.SelectCommand.CommandText = "SELECT SUM(film_price) as total FROM film_sales WHERE " &
            "YEAR([film_sale_date])=YEAR(GETDATE()) AND MONTH([film_sale_date])=MONTH(GETDATE())" &
            "AND DAY([film_sale_date])<=DAY(GETDATE())"

        objAdapter.Fill(objDataTable)

        Dim total As Double = 0

        If Not (IsDBNull(objDataTable.Rows(0).Item(0))) Then
            total = objDataTable.Rows(0).Item(0)
        End If

        objDataTable.Clear()

        Return total
    End Function

    'Function to get total game sales income for current month
    Public Function getTotalGamesSales(ByVal currentDate As Date) As Double
        objAdapter.SelectCommand.CommandText = "SELECT SUM(game_price) as total FROM games_sales WHERE " &
           "YEAR([game_sale_date])=YEAR(GETDATE()) AND MONTH([game_sale_date])=MONTH(GETDATE())" &
           "AND DAY([game_sale_date])<=DAY(GETDATE())"
        objAdapter.Fill(objDataTable)

        Dim total As Double = 0

        If Not (IsDBNull(objDataTable.Rows(0).Item(0))) Then
            total = objDataTable.Rows(0).Item(0)
        End If
        objDataTable.Clear()

        Return total
    End Function

    'Function to get total game costs for current month
    Public Function getTotalGamesCost(ByVal currentDate As Date) As Double
        objAdapter.SelectCommand.CommandText = "SELECT SUM(game_cost) as total FROM games_sales WHERE " &
            "YEAR([game_sale_date])=YEAR(GETDATE()) AND MONTH([game_sale_date])=MONTH(GETDATE())" &
            "AND DAY([game_sale_date])<=DAY(GETDATE())"

        objAdapter.Fill(objDataTable)

        Dim total As Double = 0

        If Not (IsDBNull(objDataTable.Rows(0).Item(0))) Then
            total = objDataTable.Rows(0).Item(0)
        End If
        objDataTable.Clear()

        Return total
    End Function

    'Function to get total movie costs for current month
    Public Function getTotalMoviesCost(ByVal currentDate As Date) As Double
        objAdapter.SelectCommand.CommandText = "SELECT SUM(film_cost) as total FROM film_sales WHERE " &
            "YEAR([film_sale_date])=YEAR(GETDATE()) AND MONTH([film_sale_date])=MONTH(GETDATE())" &
            "AND DAY([film_sale_date])<=DAY(GETDATE())"

        objAdapter.Fill(objDataTable)

        Dim total As Double = 0

        If Not (IsDBNull(objDataTable.Rows(0).Item(0))) Then
            total = objDataTable.Rows(0).Item(0)
        End If
        objDataTable.Clear()

        Return total
    End Function

    'Function to get total movie rent income for current month
    Public Function getTotalMovieRents(ByVal currentDate As Date) As Double
        objAdapter.SelectCommand.CommandText = "SELECT SUM(film_rent_price) as total FROM film_rent WHERE " &
            "YEAR([rent_date])=YEAR(GETDATE()) AND MONTH([rent_date])=MONTH(GETDATE())" &
            "AND DAY([rent_date])<=DAY(GETDATE())"

        objAdapter.Fill(objDataTable)

        Dim total As Double = 0

        If Not (IsDBNull(objDataTable.Rows(0).Item(0))) Then
            total = objDataTable.Rows(0).Item(0)
        End If
        objDataTable.Clear()

        Return total
    End Function

    'Subroutine to reset employee bonus
    Public Sub zeroBonuses()
        objCommand.CommandText = "UPDATE employees SET employee_bonus=0"
        objConnection.Open()
        objCommand.ExecuteNonQuery()
        objConnection.Close()
    End Sub
End Class
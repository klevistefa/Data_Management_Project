use [CambridgeProject]

CREATE TABLE [dbo].[administrator](
	[admin_id] [int] IDENTITY(1,1) NOT NULL,
	[admin_username] [varchar](25) NOT NULL,
	[admin_password] [varchar](25) NOT NULL,
 CONSTRAINT [PK_administrator] PRIMARY KEY CLUSTERED 
(
	[admin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[compatibilities](
	[compatibility_id] [int] IDENTITY(1,1) NOT NULL,
	[compatibility_name] [varchar](25) NOT NULL,
 CONSTRAINT [PK_compatibilities] PRIMARY KEY CLUSTERED 
(
	[compatibility_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[film_rent](
	[film_rent_id] [int] NOT NULL,
	[member_rent_id] [int] NOT NULL,
	[rent_date] [date] NOT NULL,
	[return_date] [date] NOT NULL,
	[film_rent_price] [float] NOT NULL,
	[rent_status] [bit] NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[film_sales](
	[employee_sold_id] [int] NOT NULL,
	[film_sold_id] [int] NOT NULL,
	[film_sale_date] [date] NOT NULL,
	[film_price] [float] NOT NULL,
	[film_cost] [float] NOT NULL,
	[member_bought_id] [int] NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[films](
	[film_id] [int] IDENTITY(1,1) NOT NULL,
	[film_title] [varchar](100) NOT NULL,
	[film_categories] [varchar](60) NOT NULL,
	[film_release_date] [date] NOT NULL,
	[film_runtime] [varchar](10) NOT NULL,
	[film_rating] [varchar](4) NOT NULL,
	[film_description] [text] NOT NULL,
	[film_times_sold] [int] NOT NULL,
	[film_stock] [int] NOT NULL,
	[film_price] [float] NOT NULL,
	[film_cost] [float] NOT NULL,
	[film_image] [image] NOT NULL,
 CONSTRAINT [PK_films] PRIMARY KEY CLUSTERED 
(
	[film_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[games](
	[game_id] [int] IDENTITY(1,1) NOT NULL,
	[game_title] [varchar](100) NOT NULL,
	[game_categories] [varchar](60) NOT NULL,
	[game_release_date] [date] NOT NULL,
	[game_producer] [varchar](100) NULL,
	[game_rating] [varchar](4) NOT NULL,
	[game_description] [text] NOT NULL,
	[game_stock_new] [int] NULL,
	[game_stock_used] [int] NULL,
	[game_compatibility_id] [int] NOT NULL,
	[game_times_sold] [int] NOT NULL,
	[game_price] [float] NOT NULL,
	[game_cost] [float] NOT NULL,
	[game_image] [image] NOT NULL,
 CONSTRAINT [PK_games] PRIMARY KEY CLUSTERED 
(
	[game_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[games_sales](
	[employee_sold_id] [int] NOT NULL,
	[game_sold_id] [int] NOT NULL,
	[game_sale_date] [date] NOT NULL,
	[game_price] [float] NOT NULL,
	[game_cost] [float] NOT NULL,
	[member_bought_id] [int] NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[members](
	[member_id] [int] IDENTITY(1,1) NOT NULL,
	[member_first_name] [varchar](20) NOT NULL,
	[member_last_name] [varchar](20) NOT NULL,
	[member_age] [int] NOT NULL,
	[member_gender] [char](1) NOT NULL,
	[member_registration_date] [date] NOT NULL,
	[member_films_bought] [int] NOT NULL,
	[member_games_bought] [int] NOT NULL,
	[member_phone_number] [varchar](15) NOT NULL,
	[member_email] [varchar](50) NOT NULL,
	[member_discount] [float] NOT NULL,
 CONSTRAINT [PK_members] PRIMARY KEY CLUSTERED 
(
	[member_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[employees](
	[employee_id] [int] IDENTITY(1,1) NOT NULL,
	[employee_first_name] [varchar](20) NOT NULL,
	[employee_last_name] [varchar](20) NOT NULL,
	[employee_username] [varchar](25) NOT NULL,
	[employee_password] [varchar](25) NOT NULL,
	[employee_gender] [char](1) NOT NULL,
	[employee_date_employment] [date] NOT NULL,
	[employee_age] [int] NOT NULL,
	[employee_salary] [float] NOT NULL,
	[employee_bonus] [float] NULL,
	[employee_films_sold] [int] NULL,
	[employee_games_sold] [int] NULL,
 CONSTRAINT [PK_employees] PRIMARY KEY CLUSTERED 
(
	[employee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO administrator(admin_username, admin_password) VALUES('admin', 'admin')
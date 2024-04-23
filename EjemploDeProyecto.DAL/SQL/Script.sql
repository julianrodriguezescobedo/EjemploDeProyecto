USE [master]
GO
/****** Object:  Database [IS]    Script Date: 23/4/2024 18:11:00 ******/
CREATE DATABASE [IS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\IS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\IS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [IS] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IS] SET ARITHABORT OFF 
GO
ALTER DATABASE [IS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [IS] SET  MULTI_USER 
GO
ALTER DATABASE [IS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [IS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [IS] SET QUERY_STORE = OFF
GO
USE [IS]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 23/4/2024 18:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Tipo] [int] NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Mensaje] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BitacoraTipo]    Script Date: 23/4/2024 18:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BitacoraTipo](
	[Code] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BitacoraTipo] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23/4/2024 18:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_BitacoraTipo] FOREIGN KEY([Tipo])
REFERENCES [dbo].[BitacoraTipo] ([Code])
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_BitacoraTipo]
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_User] FOREIGN KEY([Usuario])
REFERENCES [dbo].[User] ([Username])
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_User]
GO
/****** Object:  StoredProcedure [dbo].[Bitacora_Add]    Script Date: 23/4/2024 18:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc [dbo].[Bitacora_Add]	@tipo int, @usuario varchar(50), @mensaje varchar(max) as

insert Bitacora values (getdate(), @tipo, @usuario, @mensaje)
GO
/****** Object:  StoredProcedure [dbo].[Bitacora_GetAllFiltered]    Script Date: 23/4/2024 18:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Bitacora_GetAllFiltered] @total bit, @page int, @perPage int, @usuario nvarchar(50), @desde datetime, @hasta datetime, @tipo int, @like nvarchar(255) as
begin
	if @desde is null select @desde = getdate()
	if @hasta is null select @hasta = getdate()
	
	select @like = '%' + isnull(@like,'') + '%'

	select @page = @page -1

	if @total = 1
	begin
		select	'Page' = @page + 1,
				'PerPage' = @perPage,
				'Total' = count(*),
				'TotalPages' = ceiling(convert(float,count(*)) / convert(float,@perPage))
		from	Bitacora a 
		where	(a.Usuario = @usuario or @usuario is null)
				and convert(date,a.Fecha) between convert(date,@desde) and convert(date,@hasta)
				and (a.Tipo = @tipo or @tipo = 3 or @tipo is null)
				and 
				(
					a.Usuario like @like
					or a.Mensaje like @like
				)
	end
	else
	begin
		select	*
		from
		(
			select	ROW_NUMBER() OVER (order by a.Fecha) as [Index], 
					a.Id,
					a.Usuario,
					a.Fecha,
					a.Tipo,
					a.Mensaje
			from	Bitacora a 
			where	(a.Usuario = @usuario or @usuario is null)
					and convert(date,a.Fecha) between convert(date,@desde) and convert(date,@hasta)
					and (a.Tipo = @tipo or @tipo = 3 or @tipo is null)
					and 
					(
						a.Usuario like @like
						or a.Mensaje like @like
					)
		) z
		where z.[Index] between (@perPage * @page) + 1 AND (@perPage * @page) + @perPage
	end
end
GO
USE [master]
GO
ALTER DATABASE [IS] SET  READ_WRITE 
GO

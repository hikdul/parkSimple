USE [master]
GO

/****** Object:  Database [BDParkSimple]    Script Date: 17/11/2020 18:12:01 ******/
CREATE DATABASE [BDParkSimple]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BDParkSimple', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BDParkSimple.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BDParkSimple_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BDParkSimple_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BDParkSimple].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [BDParkSimple] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [BDParkSimple] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [BDParkSimple] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [BDParkSimple] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [BDParkSimple] SET ARITHABORT OFF 
GO

ALTER DATABASE [BDParkSimple] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [BDParkSimple] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [BDParkSimple] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [BDParkSimple] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [BDParkSimple] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [BDParkSimple] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [BDParkSimple] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [BDParkSimple] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [BDParkSimple] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [BDParkSimple] SET  DISABLE_BROKER 
GO

ALTER DATABASE [BDParkSimple] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [BDParkSimple] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [BDParkSimple] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [BDParkSimple] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [BDParkSimple] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [BDParkSimple] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [BDParkSimple] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [BDParkSimple] SET RECOVERY FULL 
GO

ALTER DATABASE [BDParkSimple] SET  MULTI_USER 
GO

ALTER DATABASE [BDParkSimple] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [BDParkSimple] SET DB_CHAINING OFF 
GO

ALTER DATABASE [BDParkSimple] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [BDParkSimple] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [BDParkSimple] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [BDParkSimple] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [BDParkSimple] SET QUERY_STORE = OFF
GO

ALTER DATABASE [BDParkSimple] SET  READ_WRITE 
GO



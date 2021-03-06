USE [master]
GO
/****** Object:  Database [Bank]    Script Date: 14-03-2022 02:08:39 ******/
CREATE DATABASE [Bank]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Bank', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Bank.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Bank_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Bank_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Bank] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Bank].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Bank] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Bank] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Bank] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Bank] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Bank] SET ARITHABORT OFF 
GO
ALTER DATABASE [Bank] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Bank] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Bank] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Bank] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Bank] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Bank] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Bank] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Bank] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Bank] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Bank] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Bank] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Bank] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Bank] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Bank] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Bank] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Bank] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Bank] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Bank] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Bank] SET  MULTI_USER 
GO
ALTER DATABASE [Bank] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Bank] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Bank] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Bank] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Bank] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Bank]
GO
/****** Object:  Table [dbo].[Beneficiary]    Script Date: 14-03-2022 02:08:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Beneficiary](
	[Holder_Account_Number] [bigint] NOT NULL,
	[Payee_Account_Number] [bigint] NOT NULL,
	[Nickname] [varchar](30) NOT NULL,
	[Branch_Name] [varchar](10) NOT NULL,
	[IFSC] [varchar](10) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer_Info]    Script Date: 14-03-2022 02:08:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer_Info](
	[AccNumber] [bigint] NOT NULL,
	[CRN] [int] NULL,
	[IBPassword] [varchar](20) NULL,
	[TxnPassword] [varchar](10) NULL,
	[BranchName] [varchar](10) NULL,
	[IFSC] [varchar](10) NULL,
	[Name] [varchar](30) NULL,
	[Address] [varchar](50) NULL,
	[MoblieNumber] [numeric](10, 0) NULL,
	[EmailID] [varchar](max) NULL,
	[Status] [varchar](10) NULL,
	[DOB] [date] NULL,
	[Balance] [numeric](15, 2) NULL,
 CONSTRAINT [PK_Customer_Info] PRIMARY KEY CLUSTERED 
(
	[AccNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 14-03-2022 02:08:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Sender] [bigint] NOT NULL,
	[Receiver] [bigint] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Amount] [numeric](15, 2) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  UserDefinedFunction [dbo].[showall_Beneficiary]    Script Date: 14-03-2022 02:08:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[showall_Beneficiary]
(	
@Holder_Account_Number bigint
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT *  from Beneficiary where Holder_Account_Number=@Holder_Account_Number
)

GO
/****** Object:  StoredProcedure [dbo].[delete_Beneficiary]    Script Date: 14-03-2022 02:08:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[delete_Beneficiary]
	-- Add the parameters for the stored procedure here
	@AccountNo bigint,@Payee_Account_Number bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE from Beneficiary where  Holder_Account_Number= @AccountNo and Payee_Account_Number =@Payee_Account_Number
END

GO
/****** Object:  StoredProcedure [dbo].[insert_Beneficiary]    Script Date: 14-03-2022 02:08:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[insert_Beneficiary](
	-- Add the parameters for the stored procedure here
	@Holder_Account_Number bigint,
	@Payee_Account_Number bigint,
	@Nickname varchar(30),
	@Branch_Name varchar(10),
	@IFSC varchar(10)

	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into Beneficiary values(@Holder_Account_Number,@Payee_Account_Number,@Nickname,@Branch_Name,@IFSC)
END

GO
/****** Object:  StoredProcedure [dbo].[update_Beneficiary]    Script Date: 14-03-2022 02:08:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[update_Beneficiary]
(
	-- Add the parameters for the stored procedure here
	@Holder_Account_Number bigint,
	@Payee_Account_Number bigint,
	@Nickname varchar(30),
	@Branch_Name varchar(10),
	@IFSC varchar(10)

	)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Beneficiary set Nickname=@Nickname,Branch_Name=@Branch_Name,IFSC=@IFSC where Holder_Account_Number=@Holder_Account_Number and Payee_Account_Number = @Payee_Account_Number
END

GO
USE [master]
GO
ALTER DATABASE [Bank] SET  READ_WRITE 
GO

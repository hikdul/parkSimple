USE [BDParkSimple]
GO
/****** Object:  Table [dbo].[costo]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[costo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[activo] [bit] NOT NULL,
	[hora] [float] NOT NULL,
	[f30] [float] NOT NULL,
	[f15] [float] NOT NULL,
	[f5] [float] NOT NULL,
	[nocturno] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[park]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[park](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[activo] [bit] NOT NULL,
	[costo] [int] NOT NULL,
	[fechaI] [varchar](10) NOT NULL,
	[horaI] [varchar](5) NOT NULL,
	[fechaO] [varchar](10) NULL,
	[horaO] [varchar](5) NULL,
	[foto] [image] NULL,
 CONSTRAINT [PK_park] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usr]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usr](
	[nombre] [varchar](50) NOT NULL,
	[psw] [varchar](50) NOT NULL,
	[rol] [int] NOT NULL,
	[activo] [bit] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_usr] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[deleteCosto]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[deleteCosto]
@id AS int	
AS
BEGIN
	update Costo set activo = 0 where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[deletePark]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deletePark]
@id as int
AS
BEGIN
	update park set activo=0 where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[deleteUsr]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteUsr]
@id  as int
AS
BEGIN
	UPDATE usr set activo = 0 where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[getCosto]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getCosto]
AS 
BEGIN
	select * from Costo where activo = 1 ;
END
GO
/****** Object:  StoredProcedure [dbo].[getCostoId]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getCostoId]
@id as int
AS 
BEGIN
	select * from Costo where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[getPark]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getPark]
AS
BEGIN
	select * from park where activo = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[getParkId]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getParkId]
@id as int
AS
BEGIN
	select * from park where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[getParkTodo]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getParkTodo]
AS
BEGIN
	select * from park;
END
GO
/****** Object:  StoredProcedure [dbo].[getUsr]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getUsr]
	@nombre as varchar(50)
AS
BEGIN
	--if exists (select 1 from usr where nombre = @nombre)
		select * from usr where nombre = @nombre and activo = 1
END
GO
/****** Object:  StoredProcedure [dbo].[postCosto]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[postCosto]
	@hora as float,
	@f30 as float,
	@f15 as float,
	@f5 as float,
	@nocturno as float,
	@nombre as varchar(50)
AS
BEGIN
	insert into Costo (nombre,hora,f30,f15,f5,nocturno,activo) values (@nombre,@hora,@f30,@f15,@f5,@nocturno,1);
END
GO
/****** Object:  StoredProcedure [dbo].[postGetPark]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[postGetPark] 
	@costo as int,
	@fechaI as varchar(10),
	@horaI as varchar(5)
AS
BEGIN
	insert into park(costo,fechaI,horaI,activo) values(@costo,@fechaI,@horaI,1)
	declare @band as int
	set @band = SCOPE_IDENTITY()
	select * from park where id = @band
END
GO
/****** Object:  StoredProcedure [dbo].[postPark]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[postPark] 
	@costo as int,
	@fechaI as varchar(10),
	@horaI as varchar(5)
AS
BEGIN
	insert into park(costo,fechaI,horaI,activo) values(@costo,@fechaI,@horaI,1)
END
GO
/****** Object:  StoredProcedure [dbo].[postUsr]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[postUsr]
	@nombre as varchar(50),
	@psw as varchar(300),
	@rol as int
AS
BEGIN
	if not exists (select 1 from usr where nombre = @nombre)
		insert into usr (nombre,psw,rol,activo) values(@nombre,@psw,@rol,1)

END
GO
/****** Object:  StoredProcedure [dbo].[putCosto]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[putCosto]
	@id as int,
	@hora as float,
	@f30 as float,
	@f15 as float,
	@f5 as float,
	@nocturno as float,
	@nombre as varchar(50)
AS
BEGIN
	if not exists (select 1 from Costo where id = 1)
		begin
			insert into Costo (nombre,hora,f30,f15,f5,nocturno) values (@nombre,@hora,@f30,@f15,@f5,@nocturno)
		end
	else
	begin
		update Costo set  nombre=@nombre,  hora =@hora,f30 = @f30,f15=@f15,f5=@f5,nocturno=@nocturno,activo=1;
	end
END
GO
/****** Object:  StoredProcedure [dbo].[PutSalidaPark]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PutSalidaPark] 
	@id as int,
	@fechaO as varchar(10),
	@horaO as varchar(5)
AS
BEGIN
	update park set fechaO = @fechaO, horaO = @horaO, activo= 0 where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[putUsr]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[putUsr]
@id  as int,
@nombre as varchar(50),
	@psw as varchar(300),
	@rol as int

	AS
	BEGIN
		update usr set nombre = @nombre,psw= @psw,rol = @rol,activo = 1 where id = @id;
	END
GO
/****** Object:  StoredProcedure [dbo].[SalidaVehiculo]    Script Date: 22/11/2020 19:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE[dbo].[SalidaVehiculo]
	@id as int,
	@fechaO as varchar(10),
	@horaO as varchar(5)
AS
BEGIN
	if exists( select 1 from park where id = @id and activo = 0)
		begin
			select * from park where id = @id
		end
	else
		begin
			update park set fechaO = @fechaO, horaO = @horaO, activo= 0 where id = @id;

			select * from park where id = @id;
		end
END
GO

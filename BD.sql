USE [LogBelatrix]
GO
/****** Object:  StoredProcedure [dbo].[LogInsert]    Script Date: 08/05/2017 0:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LogInsert]
(
	@Message varchar(max),
	@Type int
)
AS
INSERT INTO Log VALUES (@Message, @Type)

GO
/****** Object:  Table [dbo].[Log]    Script Date: 08/05/2017 0:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Log](
	[Message] [varchar](max) NOT NULL,
	[Type] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

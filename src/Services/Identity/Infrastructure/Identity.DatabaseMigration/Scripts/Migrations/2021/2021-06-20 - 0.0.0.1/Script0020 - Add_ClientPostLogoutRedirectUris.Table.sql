USE [chinwag]
GO
/****** Object:  Table [dbo].[ClientPostLogoutRedirectUris]    Script Date: 20/06/2021 07:23:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientPostLogoutRedirectUris](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostLogoutRedirectUri] [nvarchar](2000) NOT NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_ClientPostLogoutRedirectUris] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClientPostLogoutRedirectUris]  WITH CHECK ADD  CONSTRAINT [FK_ClientPostLogoutRedirectUris_Clients_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClientPostLogoutRedirectUris] CHECK CONSTRAINT [FK_ClientPostLogoutRedirectUris_Clients_ClientId]
GO

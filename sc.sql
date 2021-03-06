/****** Object:  StoredProcedure [dbo].[usp_UpdateComment]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_UpdateComment]
GO
/****** Object:  StoredProcedure [dbo].[usp_SaveSocialConfig]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_SaveSocialConfig]
GO
/****** Object:  StoredProcedure [dbo].[usp_SaveFanPageConfigure]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_SaveFanPageConfigure]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetSocialConfig]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_GetSocialConfig]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetSetting]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_GetSetting]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetPostInfo]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_GetPostInfo]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetFanpageConfig]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_GetFanpageConfig]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCommentNegative]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_GetCommentNegative]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCommentByCommentId]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_GetCommentByCommentId]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteFanPageOfAgnet]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_DeleteFanPageOfAgnet]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteFanPageOfAgent]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_DeleteFanPageOfAgent]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddSetting]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_AddSetting]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddDatasets]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_AddDatasets]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddComment]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP PROCEDURE [dbo].[usp_AddComment]
/****** Object:  Table [dbo].[SocialConfig]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP TABLE [dbo].[SocialConfig]
GO
/****** Object:  Table [dbo].[Setting]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP TABLE [dbo].[Setting]
GO
/****** Object:  Table [dbo].[FanpageConfig]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP TABLE [dbo].[FanpageConfig]
GO
/****** Object:  Table [dbo].[Datasets]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP TABLE [dbo].[Datasets]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2019-12-17 8:40:14 PM ******/
DROP TABLE [dbo].[Comment]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[PageId] [varchar](255) NULL,
	[PostId] [varchar](255) NULL,
	[Message] [text] NULL,
	[ParentId] [varchar](255) NULL,
	[FromId] [varchar](255) NULL,
	[CommentId] [varchar](255) NULL,
	[Score] [tinyint] NULL,
	[IsNegative] [bit] NULL,
	[AgentId] [varchar](255) NULL,
	[Lock] [bit] NULL,
	[DateSend] [datetime] NULL,
	[DateReceived] [datetime] NULL,
	[IsTrain] [bit] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Link] [varchar](255) NULL,
	[FromUser] [varchar](255) NULL,
	[ContactId] [int] NULL,
	[FromName] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Datasets]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Datasets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[ReplyComment] [nvarchar](max) NULL,
 CONSTRAINT [PK_Datasets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FanpageConfig]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FanpageConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PageId] [varchar](255) NOT NULL,
	[PageTitle] [varchar](255) NULL,
	[CommentConfig] [text] NULL,
	[Active] [bit] NULL,
	[DateModified] [datetime] NULL,
	[SocialConfigId] [int] NULL,
	[AgentId] [varchar](255) NULL,
	[Deleted] [bit] NULL,
	[AgentName] [varchar](255) NULL,
	[PageAccessToken] [varchar](255) NULL,
 CONSTRAINT [PK_FanpageConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[PageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Setting]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting](
	[Key] [varchar](255) NOT NULL,
	[Value] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SocialConfig]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocialConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppId] [varchar](255) NULL,
	[AppSecret] [varchar](255) NULL,
	[AppType] [tinyint] NULL,
	[DateModified] [datetime] NULL,
	[Deleted] [bit] NULL,
	[AppName] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[FanpageConfig] ADD  CONSTRAINT [DF_FanpageConfig_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[SocialConfig] ADD  CONSTRAINT [DF_SocialConfig_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddComment]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AddComment]
     @Message TEXT=null,
	@PageId VARCHAR(255)=null,
	@PostId VARCHAR(255),
	@ParentId VARCHAR(255)=null,
	@FromId VARCHAR(255)=null,
	@CommentId VARCHAR(255)=null,
	@Score TINYINT=null,
	@AgentId VARCHAR(255)=null,
	@Link VARCHAR(255)=null,
	@IsNegative BIT=null,
	@Lock BIT=null,
	@IsTrain BIT=null
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT OFF;
 
    -- Insert statements for procedure here
	INSERT dbo.Comment
	        ( 
	          PageId ,
	          PostId ,
	          Message ,
	          ParentId ,
	          FromId ,
	          CommentId ,
	          Score ,
	          IsNegative ,
	          AgentId ,
	          Lock ,
			  DateSend,
	          DateReceived ,
	          IsTrain,
			  Link
	        )
	VALUES  ( 
	          @PageId , -- PageId - varchar(255)
	          @PostId, -- PostId - varchar(255)
	          @Message, -- Message - text
	          @ParentId, -- ParentId - varchar(255)
	          @FromId, -- FromId - varchar(255)
	          @CommentId, -- CommentId - varchar(255)
	          @Score, -- Score - tinyint
	          @IsNegative , -- IsNegative - bit
	          @AgentId, -- AgentId - varchar(255)
	          @Lock, -- Lock - bit
	          NULL, -- DateSend - datetime
	          GETDATE() , -- DateReceived - datetime
	          @IsTrain, -- IsTrain - bit
			  @Link
	        )


    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[usp_AddDatasets]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE
 PROCEDURE [dbo].[usp_AddDatasets]
   @Comment VARCHAR(max),
   @ReplyComment VARCHAR(max)
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT off;
 
    -- Insert statements for procedure here
	INSERT dbo.Datasets
	        ( Comment, ReplyComment )
	VALUES  ( @Comment, -- Comment - varchar(max)
	          @ReplyComment  -- ReplyComment - varchar(max)
	          )

    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[usp_AddSetting]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_AddSetting]
    @Key VARCHAR(255),
    @Value TEXT
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT OFF;
 
    -- Insert statements for procedure here
	IF EXISTS(SELECT 1 FROM dbo.Setting WHERE [Key]=@Key)
	BEGIN
		UPDATE dbo.Setting SET Value=@Value
		WHERE [Key]=@Key    
	END
	ELSE
	BEGIN
	    INSERT dbo.Setting
	            ( [Key], Value )
	    VALUES  ( @Key, -- Key - varchar(255)
	              @Value  -- Value - text
	              )
	END

    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteFanPageOfAgent]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DeleteFanPageOfAgent]
   @AgentId VARCHAR(255) =NULL
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT ON;
 
    -- Insert statements for procedure here
	UPDATE dbo.FanpageConfig
	SET Deleted=1
	WHERE AgentId=@AgentId

    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteFanPageOfAgnet]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DeleteFanPageOfAgnet]
   @AgentId VARCHAR(255) =NULL
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT ON;
 
    -- Insert statements for procedure here
	UPDATE dbo.FanpageConfig
	SET Deleted=1
	WHERE AgentId=@AgentId

    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCommentByCommentId]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetCommentByCommentId]
   @CommentId VARCHAR(255) = NULL
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT ON;
 
    -- Insert statements for procedure here
	SELECT * 
	FROM dbo.Comment
    WHERE CommentId=@CommentId

	END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetCommentNegative]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetCommentNegative]
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT ON;
 
    -- Insert statements for procedure here
	SELECT TOP(10) *
	INTO #temp 
	FROM dbo.Comment
	WHERE IsNegative=1 AND Lock=0 AND DateSend IS NULL
	ORDER BY DateReceived DESC

	UPDATE #temp SET Lock=1

	SELECT * FROM #temp
	
	DROP TABLE #temp

    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[usp_GetFanpageConfig]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetFanpageConfig]
    @Id INT=NULL,
    @PageId VARCHAR(255)=NULL,
	@SocialConfigId INT=-1,
	@Active BIT=NULL,
	@Deleted BIT=NULL
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT ON;
 
    -- Insert statements for procedure here
	SELECT * FROM dbo.FanpageConfig
	WHERE (@Id IS NULL OR (@Id IS NOT NULL AND Id=@Id)) AND
	(@PageId IS NULL OR (@PageId IS NOT NULL AND PageId=@PageId)) AND
	(@Active IS NULL OR (@Active IS NOT NULL AND Active=@Active))AND
	(@SocialConfigId =-1 OR (@SocialConfigId <>-1 AND SocialConfigId=@SocialConfigId))AND
    (@Deleted IS NULL OR (@Deleted IS NOT NULL AND Deleted=@Deleted))
    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[usp_GetPostInfo]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetPostInfo]
   @PageId VARCHAR(255)=NULL
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT ON;
 
    -- Insert statements for procedure here
	SELECT PostId,AVG(Score)AS AvgScore,SUM(CAST(IsNegative AS INT)) AS NumberNegative,COUNT(*) AS NumberComment
	FROM dbo.Comment
	WHERE (@PageId='-1' OR (@PageId<>'-1' AND PageId=@PageId))
	GROUP BY PostId


    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetSetting]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetSetting]
    @Key VARCHAR(255)=NULL
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT ON;
 
    -- Insert statements for procedure here
	IF	@Key IS NOT NULL
	BEGIN

	SELECT *
	FROM [Setting]
	WHERE [Key]=@Key

	END
    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
     
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetSocialConfig]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetSocialConfig]
    @AppType TINYINT=NULL,
    @AppSecret VARCHAR(255)=NULL,
	@AppId VARCHAR(255)=NULL,
	@Id INT=NULL,
	@Deleted BIT =NULL 
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT ON;
 
    -- Insert statements for procedure here
	SELECT * FROM dbo.SocialConfig
	WHERE (@AppType IS NULL OR (@AppType IS NOT NULL AND AppType=@AppType)) AND
		(@AppSecret IS NULL OR (@AppSecret IS NOT NULL AND AppSecret=@AppSecret)) AND
		(@AppId IS NULL OR (@AppId IS NOT NULL AND AppId=@AppId)) AND
		(@Id IS NULL OR (@Id IS NOT NULL AND Id=@Id)) AND
		(@Deleted IS NULL OR (@Deleted IS NOT NULL AND Deleted=@Deleted))

    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[usp_SaveFanPageConfigure]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_SaveFanPageConfigure]
   @PageId VARCHAR(255)=NULL,
    @CommentConfig TEXT=NULL,
	 @AgentName VARCHAR(255)=NULL,
	 @AgentId VARCHAR(255)=NULL,
	 @PageTitle VARCHAR(255)=NULL,
	 @SocialConfigId INT,
	 @Deleted BIT =NULL,	
	 @Active BIT =NULL,
	 @PageAccessToken VARCHAR(255)=NULL,
	 @Id INT =NULL
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT OFF;
 
    -- Insert statements for procedure here
	IF @Id=0 or @Id IS NULL
    BEGIN
        INSERT dbo.FanpageConfig
                ( PageId ,
                  PageTitle ,
                  CommentConfig ,
                  Active ,
                  DateModified ,
                  SocialConfigId ,
                  AgentId ,
                  Deleted ,
                  AgentName,
				  PageAccessToken
                )
        VALUES  ( @PageId , -- PageId - varchar(255)
                  @PageTitle, -- PageTitle - varchar(255)
                  @CommentConfig , -- CommentConfig - text
                  @Active, -- Active - bit
                  GETDATE() , -- DateModified - datetime
                  @SocialConfigId, -- SocialConfigId - int
                  @AgentId, -- AgentId - varchar(255)
                  @Deleted, -- Deleted - bit
                  @AgentName,-- AgentName - varbinary(255)
				  @PageAccessToken
                )
    END
	ELSE
    BEGIN
		UPDATE [dbo].[FanpageConfig]
		   SET [PageId] = @PageId
			  ,[PageTitle] = @PageTitle
			  ,[CommentConfig] = @CommentConfig
			  ,[Active] = @Active
			  ,[DateModified] = GETDATE()
			  ,[SocialConfigId] =@SocialConfigId
			  ,[AgentId] =@AgentId
			  ,[Deleted] = @Deleted
			  ,[AgentName] = @AgentName
			  ,[PageAccessToken]=@PageAccessToken
		 WHERE Id=@Id
    END
    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END	

GO
/****** Object:  StoredProcedure [dbo].[usp_SaveSocialConfig]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_SaveSocialConfig]
   @AppType TINYINT =NULL,
   @AppSecret VARCHAR(255) =NULL,
   @AppId VARCHAR(255) =NULL,
   @Token VARCHAR(255) =NULL,
   @Id INT =NULL,
   @AppName VARCHAR(255)=NULL,
   @Deleted BIT =NULL
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT off;
 
    -- Insert statements for procedure here
	IF @Id=0 or @Id IS null
	BEGIN
	    INSERT dbo.SocialConfig
	        ( AppId ,
	          AppSecret ,
	          Token ,
	          AppType ,
			  AppName,
	          DateModified
	        )
		VALUES  (  @AppId, -- AppId - varchar(255)
				   @AppSecret , -- AppSecret - varchar(255)
				   @Token , -- Token - varchar(255)
				   @AppType, -- AppType - tinyint,
				   @AppName,
				  GETDATE()  -- DateModified - datetime
				)
	END
	ELSE
    BEGIN
		UPDATE [dbo].[SocialConfig]
		   SET [AppId] = @AppId
			  ,[AppSecret] = @AppSecret
			  ,[Token] = @Token
			  ,[AppName]=@AppName
			  ,[AppType] = @AppType
			  ,[DateModified] = GETDATE()
			  ,Deleted=@Deleted
		 WHERE Id=@Id
		 UPDATE dbo.FanpageConfig SET Deleted=@Deleted
		 WHERE SocialConfigId=@Id
    END
    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateComment]    Script Date: 2019-12-17 8:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Gia NGuyen
-- Create date: <Create Date>
-- Description: <Description>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdateComment]
   @Message TEXT,
   @PageId VARCHAR(255),
   @PostId VARCHAR(255),
   @ParentId VARCHAR(255),
   @FromId VARCHAR(255),
   @CommentId VARCHAR(255),
   @Score TINYINT,
   @AgentId VARCHAR(255),
   @IsNegative BIT,
   @Lock BIT,
   @DateSend DATETIME,
   @IsTrain BIT,
   @Id INT
AS
BEGIN
    BEGIN TRY
    SET NOCOUNT ON;
 
    -- Insert statements for procedure here

		UPDATE [dbo].[Comment]
		   SET 
			   [PageId] = @PageId
			  ,[PostId] = @PostId
			  ,[Message] = @Message
			  ,[ParentId] = @ParentId
			  ,[FromId] = @FromId
			  ,[CommentId] = @CommentId
			  ,[Score] = @Score
			  ,[IsNegative] = @IsNegative
			  ,[AgentId] = @AgentId
			  ,[Lock] = @Lock
			  ,[DateSend] = @DateSend
			  ,[DateReceived] = GETDATE()
			  ,[IsTrain] = @IsTrain
		 WHERE Id=@Id




    END TRY
    BEGIN CATCH
        DECLARE @XACT_STATE as smallint
        SET @XACT_STATE = XACT_STATE()
      
        IF @XACT_STATE <> 0
        BEGIN
            ROLLBACK TRANSACTION
        END
      
       
    END CATCH
END

GO

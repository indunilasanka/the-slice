USE [TheSlice]
GO
/****** Object:  Table [dbo].[tblCart]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCart](
	[cartId] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NULL,
	[UserId] [int] NULL,
	[IsTem] [bit] NULL,
	[Qty] [int] NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblOrder]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrder](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Id] [int] NULL,
	[Qty] [int] NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblProduct]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ImageUrl] [nvarchar](500) NULL,
	[AlternateText] [nvarchar](500) NULL,
	[Title] [nvarchar](500) NULL,
	[Sku] [nvarchar](500) NULL,
	[ShortDescription] [nvarchar](4000) NULL,
	[StockAvailability] [nvarchar](500) NULL,
	[Price] [money] NULL,
	[isDeleted] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[FullName] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblWishList]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWishList](
	[wishId] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NULL,
	[UserId] [int] NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[slice_addcartItems]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

-- =============================================
CREATE PROCEDURE [dbo].[slice_addcartItems] 
	@UserId int,
    @Id int,
    @Qty int

AS
DECLARE @CartId INT
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO tblCart(
		[UserId],
		[Id],
		[Qty],
		[IsTem],
		[IsDeleted]
	)

	VALUES(
		@UserId,
		@Id,
		@Qty,
		1,
		0
	)

	Select top 1 CartId from tblCart where UserId=@UserId and Id = @Id and IsTem = 1 and IsDeleted = 0 
END

GO
/****** Object:  StoredProcedure [dbo].[slice_AddProduct]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

-- =============================================
CREATE PROCEDURE [dbo].[slice_AddProduct] 
	@Name nvarchar(50),
	@Title nvarchar(500),
	@Sku nvarchar(500),
	@AlternateText nvarchar(500),
	@ShortDescription nvarchar(4000),
	@StockAvailability nvarchar(500),
	@Price money

AS
DECLARE @Id INT
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO tblProduct(
		[Name],
		[Title],
		[Sku],
		[AlternateText],
		[ShortDescription],
		[StockAvailability],
		[Price],
		[IsDeleted]
	)

	VALUES(
		@Name,
		@Title,
		@Sku,
		@AlternateText,
		@ShortDescription,
		@StockAvailability,
		@Price,
		0
	)

	SET @Id = @@IDENTITY
	RETURN @Id 
END

GO
/****** Object:  StoredProcedure [dbo].[slice_addwishlistItems]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

-- =============================================
Create PROCEDURE [dbo].[slice_addwishlistItems] 
	@UserId int,
    @Id int

AS
DECLARE @wishId INT
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO tblWishList(
		[UserId],
		[Id],
		[IsDeleted]
	)

	VALUES(
		@UserId,
		@Id,
		0
	)

	Select top 1 wishId from tblWishList where UserId=@UserId and Id = @Id and IsDeleted = 0 
END

GO
/****** Object:  StoredProcedure [dbo].[slice_allOrders]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[slice_allOrders]
	
	
AS
BEGIN
	
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from tblOrder o left outer join tblProduct p on(o.Id=p.Id) left outer join tblUser u on(o.UserId=u.UserId)

	END

GO
/****** Object:  StoredProcedure [dbo].[slice_CheckMail]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec slice_CheckMail 'wai@gmail.com'
-- =============================================
CREATE PROCEDURE [dbo].[slice_CheckMail] 
    @Email nvarchar(50)

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Select UserId from tblUser where Email = @Email and IsDeleted = 0
    
END


GO
/****** Object:  StoredProcedure [dbo].[slice_Checkout]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

-- =============================================
CREATE PROCEDURE [dbo].[slice_Checkout] 
	@UserId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO tblOrder(
		[UserId],[Id],[Qty],[IsDeleted]
	)

	Select UserId,Id,Qty,IsDeleted from tblCart where UserId = @UserId and IsTem = 1 and IsDeleted = 0

	update tblCart
	set IsTem = 0
	where UserId = @UserId and IsTem = 1 and IsDeleted = 0

	RETURN @UserId 
END

GO
/****** Object:  StoredProcedure [dbo].[slice_DeleteCart]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

-- =============================================
Create PROCEDURE [dbo].[slice_DeleteCart]
	@Id int,
	@UserId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update tblCart 
	set 
		[isDeleted] = 1
	where Id=@Id and UserId = @UserId and IsTem = 1

	RETURN @Id 
END

GO
/****** Object:  StoredProcedure [dbo].[slice_DeleteProduct]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

-- =============================================
CREATE PROCEDURE [dbo].[slice_DeleteProduct]
	@Id int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update tblProduct 
	set 
		[isDeleted] = 1
	where Id=@Id

	RETURN @Id 
END

GO
/****** Object:  StoredProcedure [dbo].[slice_DeleteWish]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

-- =============================================
Create PROCEDURE [dbo].[slice_DeleteWish]
	@Id int,
	@UserId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update tblWishList 
	set 
		[isDeleted] = 1
	where Id=@Id and UserId = @UserId

	RETURN @Id 
END

GO
/****** Object:  StoredProcedure [dbo].[slice_editcartItems]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

-- =============================================
Create PROCEDURE [dbo].[slice_editcartItems] 
	@UserId int,
    @Id int,
    @Qty int

AS
DECLARE @Qty2 INT = 0
Select @Qty2 = Qty from tblCart where UserId=@UserId and Id=@Id and IsTem = 1 and IsDeleted=0

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update tblCart 
	set Qty = @Qty2+@Qty where UserId=@UserId and Id=@Id and IsTem = 1 and IsDeleted=0

	Select CartId from tblCart where UserId=@UserId and Id = @Id and IsTem = 1 and IsDeleted = 0 
END

GO
/****** Object:  StoredProcedure [dbo].[slice_EditProduct]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

-- =============================================
CREATE PROCEDURE [dbo].[slice_EditProduct]
	@Id int, 
	@Name nvarchar(50),
	@Title nvarchar(500),
	@Sku nvarchar(500),
	@AlternateText nvarchar(500),
	@ShortDescription nvarchar(4000),
	@StockAvailability nvarchar(500),
	@Price money

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update tblProduct 
	set 
		[Name] = @Name,
		[Title] = @Title,
		[Sku] = @Sku,
		[AlternateText] = @AlternateText,
		[ShortDescription] = @ShortDescription,
		[StockAvailability] = @StockAvailability,
		[Price] = @Price
	where Id=@Id

	RETURN @Id 
END

GO
/****** Object:  StoredProcedure [dbo].[slice_GetCartItems]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec slice_GetCartItems 1
-- =============================================
CREATE PROCEDURE [dbo].[slice_GetCartItems] 
	-- Add the parameters for the stored procedure here
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from tblProduct p left outer join tblCart c on (p.Id=c.Id)
	where c.UserId=@UserId and c.IsTem = 1 and c.IsDeleted = 0
END

GO
/****** Object:  StoredProcedure [dbo].[slice_GetProduct]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec slice_GetCartItems 1
-- =============================================
Create PROCEDURE [dbo].[slice_GetProduct] 
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from tblProduct where Id=@Id
END

GO
/****** Object:  StoredProcedure [dbo].[slice_GetWishListItems]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec slice_GetWishListItems 1
-- =============================================
Create PROCEDURE [dbo].[slice_GetWishListItems] 
	-- Add the parameters for the stored procedure here
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from tblProduct p left outer join tblWishList w on (p.Id=w.Id)
	where w.UserId=@UserId and w.IsDeleted = 0
END

GO
/****** Object:  StoredProcedure [dbo].[slice_Image]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec slice_Image 'hjhj'
-- =============================================
CREATE PROCEDURE [dbo].[slice_Image]
	@Image nvarchar(500)

AS
Declare @ImageUrl nvarchar(500) 
Set @ImageUrl = 'http://localhost:6051/content/images/thumbs/' + @Image;
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update tblProduct
	set 
		[ImageUrl] = @ImageUrl
	where (([ImageUrl] IS NULL) or ([ImageUrl] = ''))

	RETURN 1 
END

GO
/****** Object:  StoredProcedure [dbo].[slice_loginUser]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec slice_loginUser wai@gmail.com,1234
-- =============================================
CREATE PROCEDURE [dbo].[slice_loginUser]
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50),
	@Password varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from tblUser where Email = @Email and Password = @Password
END

GO
/****** Object:  StoredProcedure [dbo].[slice_Register]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec slice_Register '1234','wao1o@gmail.com','asas','sdsd'
-- =============================================
CREATE PROCEDURE [dbo].[slice_Register] 
	@Password nvarchar(50),
    @Email nvarchar(50),
    @FullName nvarchar(500),
    @Address nvarchar(500)
AS
DECLARE @UserId INT
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO tblUser(
		[Password],
		[Email],
		[FullName],
		[Address],
		[IsDeleted]
	)

	VALUES(
		@Password,
		@Email,
		@FullName,
		@Address,
		0
	)

	Select UserId from tblUser where Email=@Email and Password = @Password
END

GO
/****** Object:  StoredProcedure [dbo].[slice_SearchAllProducts]    Script Date: 15-May-17 04:22:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[slice_SearchAllProducts]
	
	
AS
BEGIN
	
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from tblProduct where isDeleted = 0

	END

GO

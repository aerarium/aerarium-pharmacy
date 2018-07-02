CREATE TABLE [DeletedCategories] (
  [Id] bigint NOT NULL,
  [Description] nvarchar(max) NULL,
  [Name] nvarchar(max) NOT NULL,  
  [DeleteAt] dateTime,
  [User] nvarchar(max)
);
GO

CREATE TRIGGER [Categories_DeleteTrigger] ON [Categories]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedCategories]
   ([Id], [Description], [Name], [DeleteAt], [User]) 
   SELECT [Id], [Description], [Name], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedPaymentModes] (
    [Id] smallint NOT NULL,
    [Description] nvarchar(max) NULL,
    [DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [Paymentmodes_DeleteTrigger] ON [PaymentModes]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedPaymentModes]
   ([Id], [Description], [DeleteAt], [User]) 
   SELECT [Id], [Description], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedRoles] (
    [Id] bigint NOT NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [Roles_DeleteTrigger] ON [Roles]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedRoles]
   ([Id], [ConcurrencyStamp], [Name], [NormalizedName], [DeleteAt], [User]) 
   SELECT [Id], [ConcurrencyStamp], [Name], [NormalizedName], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedStateProvinces] (
    [Id] bigint NOT NULL,
    [Abbreviation] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [StateProvinces_DeleteTrigger] ON [StateProvinces]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedStateProvinces]
   ([Id], [Abbreviation], [Name], [DeleteAt], [User]) 
   SELECT [Id], [Abbreviation], [Name], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedSuppliers] (
    [Id] bigint NOT NULL,
    [Address] nvarchar(100) NULL,
    [Cnpj] nvarchar(18) NULL,
    [Email] nvarchar(50) NULL,
    [Name] nvarchar(50) NOT NULL,
    [Phone] nvarchar(20) NULL,
    [PostCode] nvarchar(9) NULL,
    [DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [Suppliers_DeleteTrigger] ON [Suppliers]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedSuppliers]
   ([Id], [Address], [Cnpj], [Email], [Name], [Phone], [PostCode], [DeleteAt], [User]) 
   SELECT [Id], [Address], [Cnpj], [Email], [Name], [Phone], [PostCode], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedUsers] (
    [Id] bigint NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [Address] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Cpf] nvarchar(max) NULL,
    [Email] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [Name] nvarchar(max) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [PasswordHash] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [PostalCode] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256) NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [Users_DeleteTrigger] ON [Users]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedUsers]
   ([Id], [AccessFailedCount], [Address], [ConcurrencyStamp], [Cpf], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [Name], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [PostalCode], [SecurityStamp], [TwoFactorEnabled], [UserName], [DeleteAt], [User]) 
   SELECT [Id], [AccessFailedCount], [Address], [ConcurrencyStamp], [Cpf], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [Name], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [PostalCode], [SecurityStamp], [TwoFactorEnabled], [UserName], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedPayments] (
    [SaleId] bigint NOT NULL,
    [DateOfExpiration] datetime2 NOT NULL,
    [PaymentModeId] smallint NOT NULL,
    [Value] decimal(18, 2) NOT NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [Payments_DeleteTrigger] ON [Payments]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedPayments]
   ([SaleId], [DateOfExpiration], [PaymentModeId], [Value], [DeleteAt], [User]) 
   SELECT [SaleId], [DateOfExpiration], [PaymentModeId], [Value], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedRoleClaims] (
    [Id] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [RoleId] bigint NOT NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [RoleClaims_DeleteTrigger] ON [RoleClaims]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedRoleClaims]
   ([Id], [ClaimType], [ClaimValue], [RoleId], [DeleteAt], [User]) 
   SELECT [Id], [ClaimType], [ClaimValue], [RoleId], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedShippingRates] (
    [Id] bigint NOT NULL,
    [Price] decimal(18, 2) NOT NULL,
    [StateProvinceId] bigint NOT NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [ShippingRates_DeleteTrigger] ON [ShippingRates]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedShippingRates]
   ([Id], [Price], [StateProvinceId], [DeleteAt], [User]) 
   SELECT [Id], [Price], [StateProvinceId], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedProducts] (
    [Id] bigint NOT NULL,
    [Description] nvarchar(max) NULL,
    [Name] nvarchar(50) NOT NULL,
    [PathToPicture] nvarchar(max) NULL,
    [Price] decimal(18, 2) NOT NULL,
    [PriceWithDiscount] decimal(18, 2) NULL,
    [SerialCode] nvarchar(450) NULL,
    [SupplierId] bigint NOT NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [Products_DeleteTrigger] ON [Products]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedProducts]
   ([Id], [Description], [Name], [PathToPicture], [Price], [PriceWithDiscount], [SerialCode], [SupplierId], [DeleteAt], [User]) 
   SELECT [Id], [Description], [Name], [PathToPicture], [Price], [PriceWithDiscount], [SerialCode], [SupplierId], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedUserClaims] (
    [Id] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [UserId] bigint NOT NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [UserClaims_DeleteTrigger] ON [UserClaims]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedUserClaims]
   ([Id], [ClaimType], [ClaimValue], [UserId], [DeleteAt], [User]) 
   SELECT [Id], [ClaimType], [ClaimValue], [UserId], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] bigint NOT NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [UserLogins_DeleteTrigger] ON [UserLogins]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedUserLogins]
   ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId], [DeleteAt], [User]) 
   SELECT [LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedUserRoles] (
    [UserId] bigint NOT NULL,
    [RoleId] bigint NOT NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [UserRoles_DeleteTrigger] ON [UserRoles]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedUserRoles]
   ([UserId], [RoleId], [DeleteAt], [User]) 
   SELECT [UserId], [RoleId], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedUserTokens] (
    [UserId] bigint NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [UserTokens_DeleteTrigger] ON [UserTokens]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedUserTokens]
   ([UserId], [LoginProvider], [Name], [Value], [DeleteAt], [User]) 
   SELECT [UserId], [LoginProvider], [Name], [Value], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedSales] (
    [Id] bigint NOT NULL,
    [CustomerId] bigint NOT NULL,
    [IssuedAt] datetime2 NOT NULL,
    [PaymentId] bigint NOT NULL,
    [SellerId] bigint NULL,
    [ShippingRateId] bigint NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [Sales_DeleteTrigger] ON [Sales]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedSales]
   ([Id], [CustomerId], [IssuedAt], [PaymentId], [SellerId], [ShippingRateId], [DeleteAt], [User]) 
   SELECT [Id], [CustomerId], [IssuedAt], [PaymentId], [SellerId], [ShippingRateId], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedBatches] (
    [Id] bigint NOT NULL,
    [DateOfExpiration] datetime2 NOT NULL,
    [DateOfFabrication] datetime2 NOT NULL,
    [ProductId] bigint NOT NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [Batches_DeleteTrigger] ON [Batches]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedBatches]
   ([Id], [DateOfExpiration], [DateOfFabrication], [ProductId], [DeleteAt], [User]) 
   SELECT [Id], [DateOfExpiration], [DateOfFabrication], [ProductId], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedProductCategories] (
    [ProductId] bigint NOT NULL,
    [CategoryId] bigint NOT NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [ProductCategories_DeleteTrigger] ON [ProductCategories]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedProductCategories]
   ([ProductId], [CategoryId], [DeleteAt], [User]) 
   SELECT [ProductId], [CategoryId], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedSaleInvoices] (
    [SaleId] bigint NOT NULL,
    [ProductId] bigint NOT NULL,
    [Quantity] int NOT NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [SaleInvoices_DeleteTrigger] ON [SaleInvoices]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedSaleInvoices]
   ([SaleId], [ProductId], [Quantity], [DeleteAt], [User]) 
   SELECT [SaleId], [ProductId], [Quantity], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

CREATE TABLE [DeletedStocks] (
    [Id] bigint NOT NULL,
    [BatchId] bigint NOT NULL,
    [MovementType] int NOT NULL,
    [Quantity] int NOT NULL,
	[DeleteAt] dateTime,
    [User] nvarchar(max)
);
GO

CREATE TRIGGER [Stocks_DeleteTrigger] ON [Stocks]
FOR DELETE
AS
BEGIN
   INSERT INTO [DeletedStocks]
   ([Id], [BatchId], [MovementType], [Quantity], [DeleteAt], [User]) 
   SELECT [Id], [BatchId], [MovementType], [Quantity], SYSDATETIME(), SYSTEM_USER
   FROM deleted;
END;

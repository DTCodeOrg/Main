USE [IdentityDatabase]

GO

INSERT [dbo].[Tenants] ([TenantId], [TenantName], [HostType], [Host], [SecretKey], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) 
VALUES (N'00000001-0000-0000-0000-000000000000', N'Tenant 1', 0, N'tenant1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL)

GO

INSERT [dbo].[Tenants] ([TenantId], [TenantName], [HostType], [Host], [SecretKey], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) 
VALUES (N'00000002-0000-0000-0000-000000000000', N'Tenant 2', 0, N'tenant2', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL)

GO

INSERT [dbo].[Tenants] ([TenantId], [TenantName], [HostType], [Host], [SecretKey], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) 
VALUES (N'00000003-0000-0000-0000-000000000000', N'finearts', 0, N'finearts', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL)

GO

INSERT [dbo].[Tenants] ([TenantId], [TenantName], [HostType], [Host], [SecretKey], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) 
VALUES (N'00000004-0000-0000-0000-000000000000', N'lifestyles', 0, N'lifestyles', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL)

GO

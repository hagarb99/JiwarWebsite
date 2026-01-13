-- 1. التأكد من وجود الرول
IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'InteriorDesigner')
BEGIN
    INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
    VALUES (NEWID(), 'InteriorDesigner', 'INTERIORDESIGNER', NEWID());
    PRINT 'Created InteriorDesigner Role';
END

-- 2. تحويل المستخدم الحالي (Ahmed) إلى InteriorDesigner
DECLARE @UserId NVARCHAR(450);
DECLARE @RoleId NVARCHAR(450);

-- هات ID بتاع المستخدم أحمد (أو أي مستخدم عايز تخليه ديزاينر)
SELECT TOP 1 @UserId = Id FROM AspNetUsers WHERE Email LIKE '%ahmed%'; -- تأكد من الإيميل

-- هات ID بتاع الرول
SELECT @RoleId = Id FROM AspNetRoles WHERE Name = 'InteriorDesigner';

-- لو المستخدم موجود والرول موجودة، اربطهم ببعض
IF @UserId IS NOT NULL AND @RoleId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @RoleId)
    BEGIN
        INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (@UserId, @RoleId);
        PRINT 'User added to InteriorDesigner role';
    END
    ELSE
    BEGIN
        PRINT 'User is already an InteriorDesigner';
    END
END
ELSE
BEGIN
    PRINT 'User or Role not found';
END

-- 3. عرض كل الديزاينرز للتأكد
SELECT u.Email, r.Name as Role 
FROM AspNetUsers u 
JOIN AspNetUserRoles ur ON u.Id = ur.UserId 
JOIN AspNetRoles r ON ur.RoleId = r.Id
WHERE r.Name = 'InteriorDesigner';

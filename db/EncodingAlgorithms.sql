CREATE TABLE EncodingAlgorithms
(
    Id int primary key not null identity(1,1),
    DisplayName varchar(256) not null,
    ClassName varchar(256) not null,
    Category varchar(256) NOT NULL,    -- 'Encoding'
    Family varchar(256) NOT NULL,
    FolderName varchar(100) NOT NULL,
    IsActive bit not null default 1,
    SortOrder int not null default 0,
    CreatedOn datetime not null default getdate()
)

GO 

merge into EncodingAlgorithms as target
using (values

-- ==========================================
    -- BASE ENCODINGS (Data-to-String)
    -- ==========================================
    ('Base64', 'Base64Encoder', 'Encoding', 'Base Encoding', 'Base', 1, 100),
    ('Base64Url', 'Base64UrlEncoder', 'Encoding', 'Base Encoding', 'Base', 1, 101),
    ('Base32', 'Base32Encoder', 'Encoding', 'Base Encoding', 'Base', 1, 102),
    ('Base16 (Hex)', 'Base16Encoder', 'Encoding', 'Base Encoding', 'Base', 1, 103),
    ('Base58 (Bitcoin)', 'Base58Encoder', 'Encoding', 'Base Encoding', 'Base', 1, 104),
    ('Base85 (Ascii85)', 'Base85Encoder', 'Encoding', 'Base Encoding', 'Base', 1, 105),

    -- ==========================================
    -- WEB & TEXT ENCODINGS
    -- ==========================================
    ('URL Encoding', 'UrlEncoder', 'Encoding', 'Web', 'Web', 1, 200),
    ('HTML Entity', 'HtmlEncoder', 'Encoding', 'Web', 'Web', 1, 201),
    ('JWT Decode', 'JwtDecoder', 'Encoding', 'Web', 'Web', 1, 202),

    -- ==========================================
    -- MATHEMATICAL & OBFUSCATION
    -- ==========================================
    ('ROT13', 'Rot13Encoder', 'Encoding', 'Obfuscation', 'Obfuscation', 1, 300),
    ('Binary', 'BinaryEncoder', 'Encoding', 'Math', 'Math', 1, 400),
    ('ASCII', 'AsciiEncoder', 'Encoding', 'Text', 'Text', 1, 500),
    ('UTF-8', 'Utf8Encoder', 'Encoding', 'Text', 'Text', 1, 501)

) as source (DisplayName, ClassName, Category, Family, FolderName, IsActive, SortOrder)
on target.Displayname = source.displayName
when not matched then 
    INSERT (DisplayName, ClassName, Category, Family, FolderName, IsActive, SortOrder)
    VALUES (source.DisplayName, source.ClassName, source.Category, source.Family, source.FolderName, 
    source.IsActive, source.SortOrder);
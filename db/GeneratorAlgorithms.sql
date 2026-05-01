CREATE TABLE GeneratorAlgorithms
(
    Id int primary key not null identity(1,1),
    DisplayName varchar(256) not null,
    ClassName varchar(256) not null,
    Category varchar(256) NOT NULL,    -- 'Generator'
    Family varchar(256) NOT NULL,
    FolderName varchar(100) NOT NULL,  -- For dynamic C# Reflection!
    IsActive bit not null default 1,
    SortOrder int not null default 0,
    CreatedOn datetime not null default getdate()
);

go

Merge into GeneratorAlgorithms as target
using(values

-- ==========================================
    -- PASSWORD & PASSPHRASE GENERATORS
    -- ==========================================
    ('Secure Password', 'SecurePasswordGenerator', 'Generator', 'Passwords', 'Passwords', 1, 10),
    ('Memorable Passphrase', 'PassphraseGenerator', 'Generator', 'Passwords', 'Passwords', 1, 20),
    ('PIN Code (Numeric)', 'PinCodeGenerator', 'Generator', 'Tokens', 'Tokens', 1, 30),

    -- ==========================================
    -- UNIQUE IDENTIFIERS
    -- ==========================================
    ('GUID / UUID v4 (Random)', 'GuidV4Generator', 'Generator', 'Identifiers', 'Identifiers', 1, 40),
    ('GUID / UUID v7 (Time-based)', 'GuidV7Generator', 'Generator', 'Identifiers', 'Identifiers', 1, 41),
    ('Nano ID', 'NanoIdGenerator', 'Generator', 'Identifiers', 'Identifiers', 1, 42),

    -- ==========================================
    -- CRYPTOGRAPHIC KEYS
    -- ==========================================
    ('Hex Key (WEP/WPA/AES)', 'HexKeyGenerator', 'Generator', 'Keys', 'Keys', 1, 50),
    ('API Key (Base64Url)', 'ApiKeyGenerator', 'Generator', 'Keys', 'Keys', 1, 51),
    ('BIP39 Mnemonic (Crypto Seed)', 'Bip39MnemonicGenerator', 'Generator', 'Keys', 'Keys', 1, 52)

) as Source (DisplayName, ClassName, Category, Family, FolderName, IsActive, SortOrder)
on target.DisplayName = Source.DisplayName
when not Matched Then
    insert (DisplayName, ClassName, Category, Family, FolderName, IsActive, SortOrder)
    values(source.DisplayName, source.ClassName, source.Category, source.Family, 
    source.FolderName, source.IsActive, source.SortOrder);


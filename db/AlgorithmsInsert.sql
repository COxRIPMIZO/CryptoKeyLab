--select * from HashAlgorithms


-- 1. The NIST SHA-3 Finalists (The Heavyweights)
INSERT INTO HashAlgorithms (DisplayName, ClassName, Category, RequiresKey, RequiresSalt, RequiresIterations, IsSecure, SortOrder)
VALUES 
('Skein (512-bit)', 'SkeinAlgorithm', 'Cryptographic', 0, 0, 0, 1, 30),
('Grøstl', 'GroestlAlgorithm', 'Cryptographic', 0, 0, 0, 1, 31),
('JH', 'JhAlgorithm', 'Cryptographic', 0, 0, 0, 1, 32);

-- 2. Regional & National Standards
INSERT INTO HashAlgorithms (DisplayName, ClassName, Category, RequiresKey, RequiresSalt, RequiresIterations, IsSecure, SortOrder)
VALUES 
('GOST R 34.11-94', 'Gost94Algorithm', 'Cryptographic', 0, 0, 0, 1, 40),
('KangarooTwelve', 'KangarooTwelveAlgorithm', 'Cryptographic', 0, 0, 0, 1, 41);

-- 3. Historical / Legacy (The "Museum" Collection)
INSERT INTO HashAlgorithms (DisplayName, ClassName, Category, RequiresKey, RequiresSalt, RequiresIterations, IsSecure, SortOrder)
VALUES 
('RIPEMD-160', 'Ripemd160Algorithm', 'Cryptographic', 0, 0, 0, 1, 50),
('Tiger', 'TigerAlgorithm', 'Cryptographic', 0, 0, 0, 1, 51),
('MD4', 'Md4Algorithm', 'Deprecated', 0, 0, 0, 0, 92),
('MD2', 'Md2Algorithm', 'Deprecated', 0, 0, 0, 0, 93);



-- update

-- 1. Add the new column (allow nulls temporarily so we can update)
ALTER TABLE HashAlgorithms ADD Family VARCHAR(100) NULL;
GO

-- 2. Update the existing rows with their respective families
UPDATE HashAlgorithms SET Family = 'SHA-2' WHERE DisplayName IN ('SHA-256', 'SHA-384', 'SHA-512');
UPDATE HashAlgorithms SET Family = 'SHA-3 (Keccak)' WHERE DisplayName IN ('SHA3_256', 'SHA3_384', 'SHA3_512', 'KangarooTwelve');
UPDATE HashAlgorithms SET Family = 'HMAC' WHERE DisplayName LIKE 'HMAC-%';
UPDATE HashAlgorithms SET Family = 'BLAKE' WHERE DisplayName = 'BLAKE3';
UPDATE HashAlgorithms SET Family = 'MD (Message Digest)' WHERE DisplayName IN ('MD2', 'MD4', 'MD5');
UPDATE HashAlgorithms SET Family = 'SHA-1' WHERE DisplayName = 'SHA-1';
UPDATE HashAlgorithms SET Family = 'NIST SHA-3 Finalists' WHERE DisplayName IN ('Skein (512-bit)', 'Grøstl', 'JH');
UPDATE HashAlgorithms SET Family = 'GOST (Russian Standards)' WHERE DisplayName IN ('GOST R 34.11-94', 'Streebog (Russian Standard)');
UPDATE HashAlgorithms SET Family = 'OSCCA (Chinese Standards)' WHERE DisplayName = 'SM3 (Chinese Standard)';
UPDATE HashAlgorithms SET Family = 'RIPEMD' WHERE DisplayName = 'RIPEMD-160';
UPDATE HashAlgorithms SET Family = 'Square / AES-based' WHERE DisplayName = 'Whirlpool';
UPDATE HashAlgorithms SET Family = 'Tiger' WHERE DisplayName = 'Tiger';
UPDATE HashAlgorithms SET Family = 'NIST Lightweight' WHERE DisplayName = 'Ascon (Lightweight)';
GO

-- 3. Now make the column NOT NULL so future algorithms are forced to have a family
ALTER TABLE HashAlgorithms ALTER COLUMN Family VARCHAR(100) NOT NULL;
GO



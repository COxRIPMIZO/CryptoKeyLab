select * from HashAlgorithms where Family = 'BLAKE'


-- 1. Add the column
ALTER TABLE HashAlgorithms ADD FolderName VARCHAR(100) NULL;
GO

-- 2. Update with clean, exact folder names (No spaces, no weird symbols!)
UPDATE HashAlgorithms SET FolderName = 'Sha2' WHERE Family = 'SHA-2';
UPDATE HashAlgorithms SET FolderName = 'Sha3' WHERE Family = 'SHA-3 (Keccak)';
UPDATE HashAlgorithms SET FolderName = 'Hmac' WHERE Family = 'HMAC';
UPDATE HashAlgorithms SET FolderName = 'Blake' WHERE Family = 'BLAKE';
UPDATE HashAlgorithms SET FolderName = 'MessageDigest' WHERE Family = 'MD (Message Digest)';
UPDATE HashAlgorithms SET FolderName = 'Sha1' WHERE Family = 'SHA-1';
UPDATE HashAlgorithms SET FolderName = 'NistFinalists' WHERE Family = 'NIST SHA-3 Finalists';
UPDATE HashAlgorithms SET FolderName = 'Gost' WHERE Family = 'GOST (Russian Standards)';
UPDATE HashAlgorithms SET FolderName = 'Oscca' WHERE Family = 'OSCCA (Chinese Standards)';
UPDATE HashAlgorithms SET FolderName = 'Ripemd' WHERE Family = 'RIPEMD';
UPDATE HashAlgorithms SET FolderName = 'Square' WHERE Family = 'Square / AES-based';
UPDATE HashAlgorithms SET FolderName = 'Tiger' WHERE Family = 'Tiger';
UPDATE HashAlgorithms SET FolderName = 'Lightweight' WHERE Family = 'NIST Lightweight';
GO

-- 3. Make it required so we never forget it for future algorithms
ALTER TABLE HashAlgorithms ALTER COLUMN FolderName VARCHAR(100) NOT NULL;
GO
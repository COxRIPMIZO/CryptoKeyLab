<div align="center">

# 🔐 CryptoKeyLab
**The Ultimate Cryptography-as-a-Service (CaaS) Platform**

[![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20(Onion)-00E5FF?style=for-the-badge)](#-architecture)
[![Database](https://img.shields.io/badge/Database-Dapper%20%7C%20SQL-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://github.com/DapperLib/Dapper)
[![Cache](https://img.shields.io/badge/Cache-Redis%20%7C%20Memory-DC382D?style=for-the-badge&logo=redis&logoColor=white)](#-high-performance-caching)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)

*A high-performance, stateless, and metadata-driven cryptographic engine supporting 115+ algorithms. Built for developers who demand uncompromising security, extreme low latency, and enterprise-grade architecture.*

[Explore API Docs](#) · [Report Bug](issues) · [Request Feature](issues)

</div>

---

## ⚡ Why CryptoKeyLab?

Modern applications require complex cryptography, but implementing it securely is hard. **CryptoKeyLab** abstracts the complexity of Symmetric/Asymmetric encryption, Hashing, Encoding, and secure Data Generation into a blazing-fast, RESTful API.

Whether you need to hash a password using `Argon2id`, encrypt a payload using hardware-accelerated `AES-256-GCM`, or generate a Time-based `UUIDv7`, CryptoKeyLab handles it with sub-millisecond latency.

### ✨ Key Features
- **🌐 115+ Algorithms:** Supports industry standards (SHA-3, AES-GCM), high-speed hashes (BLAKE3), KDFs (Argon2, bcrypt), and exotic standards (SM3, GOST).
- **🛡️ Zero-Trust Security:** Built-in API Key Gatekeeper. We never store plain-text keys. All API keys are hashed using SHA-256 before persistence.
- **🚀 Cache-Aside Architecture:** Agnostic caching layer supporting both **Redis** and **In-Memory Cache** for sub-millisecond rate-limiting and metadata fetching.
- **⚙️ Dynamic Reflection Engine:** Algorithms are loaded dynamically via database metadata. No hardcoded `switch` statements. Completely Open/Closed Principle (OCP) compliant.
- **⏱️ Atomic Rate Limiting:** Self-healing background workers (`LimitResetWorker`) automatically manage API quotas and key expirations without database deadlocks.

---

## 🏛️ Enterprise Architecture

CryptoKeyLab is built using a strict **Clean Architecture (Onion Architecture)** inside a .NET 9 Monorepo.

```mermaid
graph TD;
    API[CryptoKeyLab.API] --> Core[CryptoKeyLab.Core];
    API --> Infrastructure[CryptoKeyLab.Infrastructure];
    Core --> Domain[CryptoKeyLab.Domain];
    Infrastructure --> Domain;
    Core --> Cryptography[CryptoKeyLab.Cryptography];
    Cryptography --> Domain;
Domain: Pure C# contracts, Interfaces, and Immutable Records (DTOs). Zero dependencies.
Cryptography: The isolated Math Engine. Contains pure cryptographic implementations utilizing standard libraries and high-performance packages (BouncyCastle, BLAKE3).
Infrastructure: High-throughput Data Access using Dapper, Stored Procedures, and Redis.
Core: Business logic, Validation Services, and the Reflection-based Factory pattern.
API: The HTTP Gateway, featuring Global Exception Handling, Swagger/Scalar Documentation, and Action Filters.
🧮 Supported Algorithms
CryptoKeyLab maps algorithms dynamically. Expand the sections below to see the full list of currently supported algorithms.
<details>
<summary><b>🔒 Hashing & Passwords (KDFs)</b></summary>
<br>
- <b>Cryptographic:</b> SHA-256, SHA-512, SHA-3 (Keccak), BLAKE3, Whirlpool, RIPEMD-160 <br>
- <b>Password (KDF):</b> Argon2id, bcrypt, scrypt, PBKDF2 <br>
- <b>Auth (MAC):</b> HMAC-SHA256, HMAC-BLAKE3, KMAC256 <br>
- <b>XOF / Fast:</b> SHAKE256, xxHash3, MurmurHash3 <br>
- <b>Regional:</b> SM3 (China), Streebog (Russia), Kupyna (Ukraine)
</details>
<details>
<summary><b>🛡️ Symmetric & Asymmetric Encryption</b></summary>
<br>
- <b>Symmetric:</b> AES-256-GCM, AES-256-CBC, ChaCha20-Poly1305, Camellia, Twofish <br>
- <b>Asymmetric:</b> RSA-4096, ECIES (Secp256r1), SM2 <br>
- <b>Post-Quantum:</b> CRYSTALS-Kyber-768
</details>
<details>
<summary><b>🔤 Encoding & Generators</b></summary>
<br>
- <b>Encoding:</b> Base64, Base64Url, Base32, Base58 (Bitcoin), Hex (Base16) <br>
- <b>Generators:</b> Secure Passwords, UUIDv4, UUIDv7 (Time-based), NanoID, BIP39 Mnemonics
</details>
💻 Quick Start
Prerequisites
.NET 9.0 SDK
SQL Server (LocalDB or Docker)
Redis (Optional, falls back to InMemory Cache)
1. Clone & Setup
code
Bash
git clone https://github.com/COxRIPMIZO/CryptoKeyLab.git
cd CryptoKeyLab/src
2. Database Setup
Execute the provided SQL scripts located in the /db folder to generate the ApiKeys and AlgorithmMetadata tables. Update your appsettings.json in the CryptoKeyLab.API project:
code
JSON
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CryptoKeyLabDb;Trusted_Connection=True;"
},
"CacheSettings": {
    "Provider": "InMemory" 
}
3. Run the API
code
Bash
cd CryptoKeyLab.API
dotnet run
Navigate to https://localhost:7036/scalar/v1 to view the interactive API documentation.
📡 API Usage Example
1. Generate an API Key
code
Http
POST /api/Access/generate-temp-key
Response:
code
JSON
{
  "apiKey": "ckl_tmp_g3X4V8YwN6Z7L...",
  "expiresAt": "2026-06-28T12:00:00Z",
  "rateLimitPerMinute": 30
}
2. Compute a Hash
code
Http
POST /api/Hash/compute-hash?algorithmName=BLAKE3
X-API-KEY: ckl_tmp_g3X4V8YwN6Z7L...

{
  "input": "MySecureData"
}
Response:
code
JSON
{
  "algorithmUsed": "BLAKE3",
  "output": "d66d6d4c67a6f5542bca021...",
  "timeTakenMs": 0.0014
}
🤝 Contributing
Contributions make the open-source community an amazing place to learn, inspire, and create. Any contributions you make are greatly appreciated.
Fork the Project
Create your Feature Branch (git checkout -b feature/AmazingFeature)
Commit your Changes (git commit -m 'feat: Add some AmazingFeature')
Push to the Branch (git push origin feature/AmazingFeature)
Open a Pull Request
📝 License
Distributed under the MIT License. See LICENSE for more information.
<div align="center">
<b>Built with 💻 by <a href="https://github.com/COxRIPMIZO">Vishal Yadav</a></b>
</div>

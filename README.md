# 🔐 CryptoKeyLab

**CryptoKeyLab** is a modern, high-performance web-based laboratory for cryptographic operations. Built with **.NET 8 Blazor**, it provides an intuitive interface for key generation, encryption, and decryption. 

Most importantly, CryptoKeyLab is designed with a **strict stateless architecture**—meaning no generated keys, passwords, or encrypted data are ever saved, logged, or stored on the server. What happens in your session, stays in your session.

---

### 🚀 Key Features

*   **🛡️ 100% Stateless & Zero-Logging:** The application operates entirely in memory. It does not use a database to store user keys. Once you close the page or export your keys, the data is completely wiped.
*   **🔑 RSA Key Generation:** Securely generate standard and password-protected RSA key pairs with industry-standard padding (OAEP SHA256).
*   **🧪 Crypto Playground:** A live, interactive testing environment to perform encryption and decryption operations using your freshly generated or imported keys.
*   **🚦 Intelligent Rate Limiting:** Built-in server-side and UI-level rate limiting protects the system from CPU-exhaustion and Denial of Service (DoS) attacks.
*   **💬 Integrated User Feedback:** A built-in feedback window allows users to easily report issues, suggest features, and interact with the developer directly from the app.
*   **📱 Responsive UI:** Clean, modern, and fully functional on both desktop and mobile devices.

---

### 🏗️ Tech Stack & Architecture

*   **Framework:** .NET 8 / Blazor Web App
*   **Languages:** C#, HTML5, CSS3, JavaScript
*   **Security Core:** `System.Security.Cryptography`
*   **Infrastructure:** Custom Middleware, `IMemoryCache` for transient rate-limiting, entirely stateless data flow.

---

### 🔐 Security Architecture

1.  **Zero-Knowledge Execution:** All cryptographic generation and cryptographic math are processed on the fly. No private keys are ever written to a physical disk or database.
2.  **Abuse Prevention:** MemoryCache-based IP/Session tracking strictly limits how many CPU-intensive RSA generation requests a user can make, automatically banning abusers for a 30-minute cooldown window.
3.  **Safe Asynchronous UI:** Utilizes secure CancellationTokens to manage background tasks and UI states without memory leaks or race conditions.

---

### 🏁 Getting Started

#### Prerequisites
*[.NET 8.0 SDK](https://dotnet.microsoft.com/download) or higher.
*   Visual Studio 2022 or VS Code with C# Dev Kit.

#### Installation & Run
1.**Clone the repository:**
   git clone https://github.com/COxRIPMIZO/CryptoKeyLab.git

2.**Navigate to the project directory:**
  cd CryptoKeyLab

3.**Run the application:**
  dotnet run
  
4.**Access the app**
  Open your browser and navigate to http://localhost:5191.
  
(**Note**: To test on a local mobile device, ensure applicationUrl in launchSettings.json is set to 0.0.0.0 instead of localhost and connect via your IPv4 address).


🗺️ **Roadmap**
Add support for ECC (Elliptic Curve Cryptography).
Implementation of file-based encryption/decryption (AES-256).
Dark mode/Light mode toggle.
WebAssembly (WASM) migration for fully offline client-side execution.


🤝 **Contributing**
Contributions make the open-source community an amazing place to learn, inspire, and create. Any contributions you make are greatly appreciated.
Fork the Project.
Create your Feature Branch (git checkout -b feature/AmazingFeature).
Commit your Changes (git commit -m 'Add some AmazingFeature').
Push to the Branch (git push origin feature/AmazingFeature).
Open a Pull Request.


⚖️ **Disclaimer**
This tool is intended for educational, testing, and secure generation purposes. While the application is stateless and uses standard .NET cryptographic libraries, users should always handle and store their exported private keys with extreme care. The developer assumes no liability for lost keys or compromised data.


                                                              Developed with 💻 and 🔒 by **None**.

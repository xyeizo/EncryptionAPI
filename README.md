
<a href="Ω"><img src="http://readme-typing-svg.herokuapp.com?font=VT323&size=90&duration=2000&pause=1000&color=39008a&center=true&random=false&width=1100&height=140&lines=%E2%98%A6++EncryptionAPI++%E2%98%A6;%E2%98%A6++By+xyeizo++%E2%98%A6;" alt="Ω" /></a>
 

EncryptionAPI is a straightforward implementation of AES encryption for securely managing string data in .NET applications. The API is designed to simplify the encryption and decryption of strings using a password, salt, and AES algorithm specifics. It also includes functionalities for hashing passwords using SHA256.

## Features

- **Secure String Encryption and Decryption**: Provides methods to encrypt and decrypt strings, ensuring data security in applications.
- **Password Hashing**: Allows for secure SHA256 hashing of passwords.
- **Salt Generation**: Automatically generates a secure random salt for use in the encryption process.
- **Simple Usage**: The API is designed for easy integration and use in any .NET project requiring data encryption.

## Implementation Details

The EncryptionAPI utilizes AES (Advanced Encryption Standard) with a Cipher Feedback (CFB) mode and PKCS7 padding to ensure that plaintext of varying lengths can be encrypted securely. Salt used in the encryption process helps to prevent rainbow table attacks, enhancing security further.

Note: This API does not provide features beyond basic encryption, decryption, and password hashing. It is suitable for projects where simple and direct encryption is needed without extended cryptographic features.

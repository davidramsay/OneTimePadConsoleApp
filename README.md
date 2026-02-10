** OTP CONSOLE APP **
---
- This is a simple console application that encrypts and decrypts messages using one time pads, and generates keys for one time pads based on message length.
- The application is written in C# and uses .NET 10.0.
- The application has three main functionalities:
  1. Encrypting a message using a one time pad.
  2. Decrypting a message using a one time pad.
  3. Generating a key for a one time pad based on the length of the message.

- The application uses a simple command-line interface.

Feel free to use and modify the code as needed. If you have any questions or suggestions, please let me know!

For more information on one time pads, and an explanation on what makes them unbreakable, read this article: https://www.tutorialspoint.com/cryptography/cryptography_one_time_pad_cipher.htm.

** HOW OTP works **
---
Imagine Alice wants to send a private message to Bob. Alice writes down "HELLO" and creates a unique key, "QWERT" and shares that with Bob "out of band", via dead drop or encrypted messenger like Meshtastic for example.
- To keep the message secret, Alice adds the numerical value of each letter of "HELLO" with the corresponding numerical value of each letter in "QWERT". For example, the plaintext letter is H with a numerical value of 7, and the corresponding key letter is Q with a value of 16. 7 + 16 = 23. The letter in the 23rd place of the alphabet (using an index of 0 where A=0) is X.

Plain - H /
Key - Q /
Cipher - X

Plain - E /
Key - W /
Cipher - A

Plain - L /
Key - E /
Cipher - P

Plain - L /
Key - R /
Cipher - C

Plain - O /
Key - T /
Cipher - H

- If we follow this logic, Alice's encrypted message becomes "XAPCH"
- Bob receives the encrypted message and uses the same "QWERT" key to decode it.
- He reverses the rule: X(23) - Q(16) = H(7)
- Bob recovers the original message, "HELLO"

I wanted to include numbers, special characters, and lower case letters, so I changed this to use a string containing a full list of the characters we want to allow for use in the process.
In this case, during the encryption/decryption process, each of the newly added characters is assigned a value just like A, B, and C are in the normal all-caps/alphabet-only examples of one-time pad commonly used. Normally A=0, Z=25,etc. while in this case we also have a=26, z=51, 0=52, etc.

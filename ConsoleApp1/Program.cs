using System;

public class OTP
{
    // Define the character set to include letters, numbers, and common special characters
    private const string CHARSET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 !@#$%^&*()_+-=[]{}|;:',.<>?/\"\\";
    private static readonly int CHARSET_SIZE = CHARSET.Length;

    // Method 3
    // Main driver method
    static void Main()
    {
        //dashboard: user selects whether to:
        ////1.) input a plaintext and generate a key to generate ciphertext, or 
        ////2.) input a plaintext and a key to generate ciphertext, or 
        ////3.) input a ciphertext and a key  to generate plaintext
        do
        {
            Console.Clear();
            Console.WriteLine("Vernam Cipher Algorithm\nSelect your option:\n" +
                "1.) input a plaintext and generate a key to generate ciphertext, or \n" +
                "2.) input a plaintext and a key to generate ciphertext, or \n" +
                "3.) input a ciphertext and a key  to generate plaintext");

            string selector = Console.ReadLine();
            //validate whether the input is an int, and if so if it is either 1, 2, or 3

            int selectorInt = int.Parse(selector);
            switch (selectorInt)
            {
                case 1:
                    InputPlaintextGenerateKey();
                    break;
                case 2:
                    InputPlaintextInputKey();
                    break;
                case 3:
                    InputCiphertextInputKey();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                    break;
            }

            Console.WriteLine("Do you want to perform another operation? (y/n)");
        } while (Console.ReadLine()?.Trim().ToLower() == "y");
    }

    private static void InputCiphertextInputKey()
    {
        Console.WriteLine("input ciphertext");
        String encryptedText = Console.ReadLine();
        Console.WriteLine("input key");
        String key = Console.ReadLine();

        // Validate that key and ciphertext have the same length
        if (encryptedText.Length != key.Length)
        {
            Console.WriteLine($"Error: Key length ({key.Length}) must match ciphertext length ({encryptedText.Length})");
            return;
        }

        // Calling above method to stringDecryption
        // with encryptedText and key as parameters
        Console.WriteLine(
            "Message - "
            + stringDecryption(encryptedText, key));
        return;
    }

    private static void InputPlaintextInputKey()
    {
        String plainText, key;
        Console.WriteLine("input plaintext");
        plainText = Console.ReadLine();

        Console.WriteLine("input key");
        key = Console.ReadLine();

        // Validate that key and plaintext have the same length
        if (plainText.Length != key.Length)
        {
            Console.WriteLine($"Error: Key length ({key.Length}) must match plaintext length ({plainText.Length})");
            return;
        }

        Console.WriteLine("Key - " + key);

        // function call to stringEncryption
        // with plainText and key as parameters
        String encryptedText = stringEncryption(plainText, key);

        // Printing cipher Text
        Console.WriteLine("Cipher Text - " + encryptedText);

        return;
    }

    public static void InputPlaintextGenerateKey()
    {
        String plainText, key;
        Console.WriteLine("input plaintext");
        plainText = Console.ReadLine();

        key = generateKey(plainText);

        Console.WriteLine("Key - " + key);

        // function call to stringEncryption
        // with plainText and key as parameters
        String encryptedText = stringEncryption(plainText, key);

        // Printing cipher Text
        Console.WriteLine("Cipher Text - " + encryptedText);

        return;
    }

    public static String stringEncryption(String text, String key)
    {
        // Validate input lengths
        if (text.Length != key.Length)
        {
            throw new ArgumentException($"Text length ({text.Length}) must match key length ({key.Length})");
        }

        // Initializing cipherText
        String cipherText = "";

        // Initialize cipher array that stores the sum of corresponding character indices
        int[] cipher = new int[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            int textIndex = GetCharIndex(text[i]);
            int keyIndex = GetCharIndex(key[i]);

            if (textIndex == -1 || keyIndex == -1)
            {
                throw new ArgumentException($"Unsupported character at position {i}");
            }

            cipher[i] = textIndex + keyIndex;
        }

        // Apply modulo to wrap around the character set
        for (int i = 0; i < text.Length; i++)
        {
            if (cipher[i] >= CHARSET_SIZE)
            {
                cipher[i] = cipher[i] % CHARSET_SIZE;
            }
        }

        // Convert indices back to characters
        for (int i = 0; i < text.Length; i++)
        {
            cipherText += CHARSET[cipher[i]];
        }

        // Returning the cipherText
        return cipherText;
    }


    // Returning plain text
    public static String stringDecryption(String s, String key)
    {
        // Validate input lengths
        if (s.Length != key.Length)
        {
            throw new ArgumentException($"Ciphertext length ({s.Length}) must match key length ({key.Length})");
        }

        // Initializing plain text
        String plainText = "";

        // Initializing integer array that stores difference of corresponding character indices
        int[] plain = new int[s.Length];

        // Running for loop for each character subtracting and storing in the array
        for (int i = 0; i < s.Length; i++)
        {
            int cipherIndex = GetCharIndex(s[i]);
            int keyIndex = GetCharIndex(key[i]);

            if (cipherIndex == -1 || keyIndex == -1)
            {
                throw new ArgumentException($"Unsupported character at position {i}");
            }

            plain[i] = cipherIndex - keyIndex;
        }

        // If the difference is less than 0, add CHARSET_SIZE to wrap around
        for (int i = 0; i < s.Length; i++)
        {
            if (plain[i] < 0)
            {
                plain[i] = plain[i] + CHARSET_SIZE;
            }
        }

        // Converting indices to corresponding characters
        for (int i = 0; i < s.Length; i++)
        {
            plainText += CHARSET[plain[i]];
        }

        // Returning plainText
        return plainText;
    }

    // Helper method to get the index of a character in the CHARSET
    private static int GetCharIndex(char c)
    {
        return CHARSET.IndexOf(c);
    }

    //method that generates a random key based on the length of the plaintext input
    public static string generateKey(String inputText)
    {
        //get length of string input
        int length = inputText.Length;

        //generate random key of the same length as the input string
        string key = GenerateRandomKey(length);
        return key;
    }

    // Generate a random key using characters from CHARSET
    static string GenerateRandomKey(int length)
    {
        Random random = new Random();
        char[] keyChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            keyChars[i] = CHARSET[random.Next(0, CHARSET_SIZE)];
        }

        return new string(keyChars);
    }
}

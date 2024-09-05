using UnityEngine;

public class PasswordGenerator
{
    string alphabet = "abcdefghijklmopqrstuvwxyz0123456789!_@";

    string letters = "abcdefghijklmopqrstuvwxyz";
    string numbers = "0123456789";
    string specialSymbols = "!_@";

    public string GeneratePassword(int len)
    {
        string password = "";
        for(int i = 0; i < len; i++)
        {
            password += alphabet[Random.Range(0, alphabet.Length)];
        }
        return password;
    }

    public char GetRandomSymbol()
    {
        return alphabet[Random.Range(0, alphabet.Length)];
    }

    public string RandomisePassword(string password, int changeSymbolsAmount)
    {
        string temporaryPassword = password;
        for(int i = 0; i < changeSymbolsAmount; i++)
        {
            int changedSymbol = Random.Range(0, temporaryPassword.Length);
            temporaryPassword = temporaryPassword.Substring(0, changeSymbolsAmount) +
                alphabet[Random.Range(0, alphabet.Length)] + temporaryPassword.Substring(changedSymbol + 1); 
        }
        return temporaryPassword;
    }

}

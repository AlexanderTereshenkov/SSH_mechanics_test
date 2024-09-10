using System.Text.RegularExpressions;
using UnityEngine;

public class PasswordGenerator
{

    private string _alphabet;

    public string IncludedLetters
    {
        get;
        set;
    }

    public string IncludedNumbers
    {
        get;
        set;
    }

    public string IncludedSpecialSymbols
    {
        get;
        set;
    }


    public string GeneratePassword(int passwordLength, int symbolsCount)
    {
        IncludedLetters = GetSubstring(IncludedLetters, symbolsCount);
        IncludedNumbers = GetSubstring(IncludedNumbers, symbolsCount);
        IncludedSpecialSymbols = GetSubstring(IncludedSpecialSymbols, symbolsCount);
        
        _alphabet = IncludedLetters + IncludedNumbers + IncludedSpecialSymbols;

        string password = "";

        for(int i = 0; i < passwordLength; i++)
        {
            password += _alphabet[Random.Range(0, _alphabet.Length)];
        }

        var letters = new Regex(@"[a-zA-Z]");
        var digits = new Regex(@"[0-9]");
        var symbols = new Regex(@"[!_@+$#]");
        
        if (IncludedLetters != null && !letters.IsMatch(password))
        {
            password = RandomisePassword(password, IncludedLetters);
        }
        if (IncludedNumbers != null && !digits.IsMatch(password))
        {
            password = RandomisePassword(password, IncludedNumbers);
        }
        if (IncludedSpecialSymbols != null && !symbols.IsMatch(password))
        {
            password = RandomisePassword(password, IncludedSpecialSymbols);
        }

        return password;
    }

    public string GetFormattedString(DecompileBlock.BlockType type)
    {
        string text = "";
        switch (type)
        {
            case DecompileBlock.BlockType.Letters:
                text = IncludedLetters;
                break;
            case DecompileBlock.BlockType.Numbers:
                text = IncludedNumbers;
                break;
            case DecompileBlock.BlockType.SpecialSymbols:
                return IncludedSpecialSymbols;
        }
        return "#" + text[0] + "-" + text[text.Length - 1];
    }

    public void AddSymbols(string symbols, DecompileBlock.BlockType type)
    {
        switch (type)
        {
            case DecompileBlock.BlockType.Letters:
                IncludedLetters = symbols;
                break;
            case DecompileBlock.BlockType.Numbers:
                IncludedNumbers = symbols;
                break;
            case DecompileBlock.BlockType.SpecialSymbols:
                IncludedSpecialSymbols = symbols;
                break;
        }
    }

    private string RandomisePassword(string password, string alphabet)
    {
        string temporaryPassword = password;
        int randomSymbolIndex = Random.Range(0, temporaryPassword.Length);

        temporaryPassword = temporaryPassword.Substring(0, randomSymbolIndex) + alphabet[Random.Range(0, alphabet.Length)];

        if (randomSymbolIndex + 1 < password.Length)
        {
            temporaryPassword += password.Substring(randomSymbolIndex + 1);
        }

        return temporaryPassword;
    }

    private string GetSubstring(string currentString, int length)
    {
        if(currentString == null)
        {
            return null;
        }
        Debug.Log(currentString);
        int firstIndex = currentString.IndexOf(currentString[Random.Range(0, currentString.Length / 2)]);
        if(firstIndex + length > currentString.Length)
        {
            length = currentString.Length - firstIndex;
        }
        return currentString.Substring(firstIndex, length);
    }

}

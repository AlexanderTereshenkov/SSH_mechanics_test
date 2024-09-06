using System.Text.RegularExpressions;
using UnityEngine;

public class PasswordGenerator
{

    private readonly string _letters = "abcdefghijklmopqrstuvwxyz";
    private readonly string _numbers = "0123456789";
    private readonly string _specialSymbols = "!_@+$#";

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
        get
        {
            return _specialSymbols;
        }
    }


    public string GeneratePassword(int len)
    {
        IncludedLetters = _letters.Substring(_letters.IndexOf(_letters[Random.Range(0, _letters.Length / 2)]), 4);
        IncludedNumbers = _numbers.Substring(_numbers.IndexOf(_numbers[Random.Range(0, _numbers.Length / 2)]), 4);

        _alphabet = IncludedNumbers + IncludedLetters;

        string password = "";

        for(int i = 0; i < len; i++)
        {
            password += _alphabet[Random.Range(0, _alphabet.Length)];
        }

        var digits = new Regex(@"[0-9]");
        var symbols = new Regex(@"[!_@+$#]");

        if (!digits.IsMatch(password))
        {
            password = RandomisePassword(password, IncludedNumbers);
        }

        return password;
    }

    public string GetFormattedString(string text)
    {
        return "#" + text[0] + "-" + text[text.Length - 1];
    }

    private string RandomisePassword(string password, string alphabet)
    {
        string temporaryPassword = password;
        int randomSymbolIndex = Random.Range(0, temporaryPassword.Length);
        temporaryPassword = temporaryPassword.Substring(0, randomSymbolIndex) +
            alphabet[Random.Range(0, alphabet.Length)];
        if (randomSymbolIndex + 1 < password.Length)
        {
            temporaryPassword += password.Substring(randomSymbolIndex + 1);
        }
        return temporaryPassword;
    }

}

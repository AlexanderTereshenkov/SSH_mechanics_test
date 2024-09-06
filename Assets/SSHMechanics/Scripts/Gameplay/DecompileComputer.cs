using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

public class DecompileComputer : MonoBehaviour, IInteractible
{
    [Range(5, 8)]
    [SerializeField] private int passwordLen;
    [SerializeField] private ComputerUI computerUI;

    private string _correctPassword;
    private int _position = 0;
    private PasswordGenerator _passwordGenerator;
    private SymbolCheckingBlock _checkingBlock;

    private void Start()
    {
        _passwordGenerator = new PasswordGenerator();
        _checkingBlock = new();
        _correctPassword = _passwordGenerator.GeneratePassword(passwordLen);

        var letters = new Regex(@"^[a-z]");
        var digits = new Regex(@"^[0-9]");

        if (letters.IsMatch(_correctPassword))
        {
            computerUI.LettersBlock = computerUI.SetFirstBlock(
                _passwordGenerator.GetFormattedString(_passwordGenerator.IncludedLetters)
                );
            computerUI.NumbersBlock = computerUI.SetSecondBlock(
                _passwordGenerator.GetFormattedString(_passwordGenerator.IncludedNumbers)
                );

        }
        else if ((digits.IsMatch(_correctPassword)))
        {
            computerUI.NumbersBlock = 
                computerUI.SetFirstBlock(_passwordGenerator.GetFormattedString(_passwordGenerator.IncludedNumbers));
            computerUI.LettersBlock = 
                computerUI.SetSecondBlock(_passwordGenerator.GetFormattedString(_passwordGenerator.IncludedLetters));
        }

    }

    public void Interact()
    {
        Debug.Log(_correctPassword);
    }

    public void CheckPassword()
    {
        var inputString = computerUI.InputString;
        StartCoroutine(CheckPasswordCoroutine(inputString));
    }

    private IEnumerator CheckPasswordCoroutine(string password)
    {
        yield return new WaitForSeconds(1);
        while (_position < password.Length)
        {
            ChangeUI(_correctPassword, true);
            yield return new WaitForSeconds(0.5f);
            if (!_checkingBlock.CheckPassword(password, _correctPassword, _position))
            {
                ChangeUI(_correctPassword, false);
                yield return new WaitForSeconds(0.5f);
                Debug.Log("WROOOONG   " + password[_position]);
                _position = 0;
                yield break;
            }
            ChangeUI(_correctPassword, false);
            yield return new WaitForSeconds(0.5f);
            _position++;
            yield return new WaitForSeconds(1);
            
        }
        _position = 0;
    }

    private bool CheckIfDigit(char elem)
    {
        var digits = new Regex(@"^[0-9]");
        return digits.IsMatch(elem.ToString());
    }

    private bool CheckIfLeter(char elem)
    {
        var letter = new Regex(@"^[a-z]");
        return letter.IsMatch(elem.ToString());
    }

    private void ChangeUI(string password, bool state)
    {
        if (CheckIfDigit(password[_position]))
        {
            computerUI.NumbersBlock.CheckState(state);
        }
        else
        {
            computerUI.LettersBlock.CheckState(state);
        }
    }

}

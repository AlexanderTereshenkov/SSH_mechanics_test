using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

public class DecompileComputer : MonoBehaviour, IInteractible
{
    [Range(5, 8)]
    [SerializeField] private int passwordLen;
    [SerializeField] private float waitingTime;

    [SerializeField] private ComputerUI computerUI;

    private string _correctPassword;
    private int _position = 0;
    private PasswordGenerator _passwordGenerator;
    private SymbolCheckingBlock _checkingBlock;
    private Regex _letters = new Regex(@"^[a-z]");
    private Regex _digits = new Regex(@"^[0-9]");

    private void Start()
    {
        _passwordGenerator = new PasswordGenerator();
        _checkingBlock = new();
        _correctPassword = _passwordGenerator.GeneratePassword(passwordLen);

        if (_letters.IsMatch(_correctPassword))
        {
            computerUI.LettersBlock = computerUI.SetFirstBlock(
                _passwordGenerator.GetFormattedString(_passwordGenerator.IncludedLetters)
                );
            computerUI.NumbersBlock = computerUI.SetSecondBlock(
                _passwordGenerator.GetFormattedString(_passwordGenerator.IncludedNumbers)
                );

        }
        else if ((_digits.IsMatch(_correctPassword)))
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
        while (_position < password.Length)
        {
            if(_position >= _correctPassword.Length)
            {
                computerUI.SetCurrentSymbol("OUT OF BOUNDS");
                yield break;
            }

            computerUI.SetCurrentSymbol(password[_position].ToString());
            ChangeUI(_correctPassword, 0);
            yield return new WaitForSeconds(waitingTime);

            if (!_checkingBlock.CheckPassword(password, _correctPassword, _position))
            {
                ChangeUI(_correctPassword, 2);
                yield return new WaitForSeconds(waitingTime);
                ChangeUI(_correctPassword, 1);
                _position = 0;
                yield break;
            }

            ChangeUI(_correctPassword, 3);
            yield return new WaitForSeconds(waitingTime);

            ChangeUI(_correctPassword, 1);
            yield return new WaitForSeconds(waitingTime);

            _position++;
            computerUI.SetCurrentSymbol(string.Empty);
            
        }

        _position = 0;
        computerUI.SetCurrentSymbol(string.Empty);
    }

    private bool CheckIfDigit(char elem)
    {
        return _digits.IsMatch(elem.ToString());
    }

    private bool CheckIfLeter(char elem)
    {
        return _letters.IsMatch(elem.ToString());
    }

    private void ChangeUI(string password, int state)
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

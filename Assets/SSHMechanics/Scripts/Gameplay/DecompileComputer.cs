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
    private bool _isPasswordCorrect;
    private CheckingBlockUI _activeBlock;

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

        _activeBlock = GetActiveBlock(_correctPassword, _position);

    }

    public void Interact()
    {
        Debug.Log(_correctPassword);
    }

    public void CheckPassword()
    {
        if ((_isPasswordCorrect))
        {
            return;
        }
        var inputString = computerUI.InputString;
        
        StartCoroutine(CheckPasswordCoroutine(inputString));
    }

    private IEnumerator CheckPasswordCoroutine(string password)
    {
        
        ChangeBlockState(_activeBlock, CheckingBlockUI.CheckingStates.Standart);
        yield return new WaitForSeconds(waitingTime);

        while (_position < password.Length)
        {
            if(_position >= _correctPassword.Length)
            {
                computerUI.SetCurrentSymbol("OUT OF BOUNDS");
                yield break;
            }

            computerUI.SetCurrentSymbol(password[_position].ToString());

            _activeBlock = GetActiveBlock(_correctPassword, _position);

            ChangeBlockState(_activeBlock, CheckingBlockUI.CheckingStates.Checking);
            yield return new WaitForSeconds(waitingTime);

            if (!_checkingBlock.CheckPassword(password, _correctPassword, _position))
            {
                ChangeBlockState(_activeBlock, CheckingBlockUI.CheckingStates.Wrong);
                yield return new WaitForSeconds(waitingTime);
                _position = 0;
                yield break;
            }

            ChangeBlockState(_activeBlock, CheckingBlockUI.CheckingStates.Right);
            yield return new WaitForSeconds(waitingTime);

            ChangeBlockState(_activeBlock, CheckingBlockUI.CheckingStates.Standart);
            yield return new WaitForSeconds(waitingTime);

            _position++;
            computerUI.SetCurrentSymbol(string.Empty);
            
        }

        _position = 0;
        computerUI.SetCurrentSymbol(string.Empty);
        if(password.Length == _correctPassword.Length)
        {
            _isPasswordCorrect = true;
            computerUI.SetCorrectPassword();
        }

    }

    private bool CheckIfDigit(char elem)
    {
        return _digits.IsMatch(elem.ToString());
    }

    private bool CheckIfLeter(char elem)
    {
        return _letters.IsMatch(elem.ToString());
    }

    private void ChangeBlockState(CheckingBlockUI activeBlock, CheckingBlockUI.CheckingStates state)
    {
        activeBlock.SetCheckingState(state);
    }

    private CheckingBlockUI GetActiveBlock(string password, int position)
    {
        CheckingBlockUI activeBlock = default;
        if (CheckIfDigit(password[position]))
        {
            activeBlock = computerUI.NumbersBlock;
        }
        else
        {
            activeBlock = computerUI.LettersBlock;
        }
        return activeBlock;
    }

}

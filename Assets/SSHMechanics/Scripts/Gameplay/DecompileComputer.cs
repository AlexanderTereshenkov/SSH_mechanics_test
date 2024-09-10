using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

public class DecompileComputer : MonoBehaviour, IInteractible
{

    [SerializeField] private float waitingTime;

    [SerializeField] private ComputerUI computerUI;
    [SerializeField] private DecompileProcessManager decompileProcessManager;

    private string _correctPassword;
    private int _position = 0;
    private SymbolCheckingBlock _checkingBlock;
    private bool _isPasswordCorrect;
    private DecompileBlock _activeBlock;

    private void Start()
    {
        _correctPassword = decompileProcessManager.CorrectPassword;
        _activeBlock = GetActiveBlock(_correctPassword, _position);
        _checkingBlock = new();
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
        
        ChangeBlockState(_activeBlock, DecompileBlockUI.CheckingStates.Standart);
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

            ChangeBlockState(_activeBlock, DecompileBlockUI.CheckingStates.Checking);
            yield return new WaitForSeconds(waitingTime);

            if (!_checkingBlock.CheckPassword(password, _correctPassword, _position))
            {
                ChangeBlockState(_activeBlock, DecompileBlockUI.CheckingStates.Wrong);
                yield return new WaitForSeconds(waitingTime);
                _position = 0;
                yield break;
            }

            ChangeBlockState(_activeBlock, DecompileBlockUI.CheckingStates.Right);
            yield return new WaitForSeconds(waitingTime);

            ChangeBlockState(_activeBlock, DecompileBlockUI.CheckingStates.Standart);
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

    private void ChangeBlockState(DecompileBlock activeBlock, DecompileBlockUI.CheckingStates state)
    {
        activeBlock.BlockUI.ChangeCheckingState(state);
    }

    private DecompileBlock GetActiveBlock(string password, int position)
    {
        var letters = new Regex(@"[a-zA-Z]");
        var digits = new Regex(@"[0-9]");
        var symbols = new Regex(@"[!_@+$#]");

        DecompileBlock.BlockType blockType = default;

        if (letters.IsMatch(password[position].ToString()))
        {
            blockType = DecompileBlock.BlockType.Letters;
        }
        else if (digits.IsMatch(password[position].ToString()))
        {
            blockType = DecompileBlock.BlockType.Numbers;
        }
        else
        {
            blockType = DecompileBlock.BlockType.SpecialSymbols;
        }

        return decompileProcessManager.GetDecompileBlock(blockType);
    }

}

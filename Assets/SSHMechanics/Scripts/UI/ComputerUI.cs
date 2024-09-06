using TMPro;
using UnityEngine;

public class ComputerUI : MonoBehaviour
{
    [SerializeField] private CheckingBlockUI firstBlock;
    [SerializeField] protected CheckingBlockUI secondBlock;
    [SerializeField] private TMP_InputField inputField;

    public CheckingBlockUI LettersBlock
    {
        get;
        set;
    }

    public CheckingBlockUI NumbersBlock
    {
        get;
        set;
    }

    public string InputString
    {
        get
        {
            return inputField.text;
        }
    }

    public CheckingBlockUI SetFirstBlock(string text)
    {
        var block = firstBlock.SetCommentsText(text);
        return block;
    }

    public CheckingBlockUI SetSecondBlock(string text)
    {
        var block = secondBlock.SetCommentsText(text);
        return block;
    }

}

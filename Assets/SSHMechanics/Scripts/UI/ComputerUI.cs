using TMPro;
using UnityEngine;

public class ComputerUI : MonoBehaviour
{
    [SerializeField] private CheckingBlockUI firstBlock;
    [SerializeField] protected CheckingBlockUI secondBlock;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI currentSymbol;

    [Header("Input Field field")]
    [SerializeField] private Color correctPasswordColor;
    [SerializeField] private TextMeshProUGUI inputFieldText;

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

    public void SetCurrentSymbol(string symbol)
    {
        currentSymbol.text = "Current symbol: " + symbol;
    }

    public void SetCorrectPassword()
    {
        inputField.readOnly = true;
        inputFieldText.color = correctPasswordColor;
    }

}

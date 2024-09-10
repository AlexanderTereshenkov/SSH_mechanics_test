using TMPro;
using UnityEngine;

public class ComputerUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI currentSymbol;

    [Header("Input Field field")]
    [SerializeField] private Color correctPasswordColor;
    [SerializeField] private TextMeshProUGUI inputFieldText;

    public string InputString
    {
        get
        {
            return inputField.text;
        }
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

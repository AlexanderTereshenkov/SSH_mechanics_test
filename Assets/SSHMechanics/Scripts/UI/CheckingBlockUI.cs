using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckingBlockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI commentsText;
    [SerializeField] private Image background;
    [SerializeField] private Color checkColor;
    [SerializeField] private Color wrongColor;
    [SerializeField] private Color rightColor;

    private Color _standartColor;

    private void Awake()
    {
        _standartColor = background.color;
    }

    public CheckingBlockUI SetCommentsText(string text)
    {
        commentsText.text = text;
        return this;
    }

    public void SetCheckingState(CheckingStates state)
    {
        switch (state)
        {
            case CheckingStates.Checking:
                background.color = checkColor;
                break;
            case CheckingStates.Standart:
                background.color = _standartColor;
                break;
            case CheckingStates.Wrong:
                background.color = wrongColor;
                break;
            case CheckingStates.Right:
                background.color = rightColor;
                break;
        }
    }

    public enum CheckingStates
    {
        Checking,
        Standart,
        Right,
        Wrong
    }

}

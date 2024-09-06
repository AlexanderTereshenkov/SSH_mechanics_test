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

    //0 - checking
    //1 - standart
    //2 - wrong
    //3 - right
    public void CheckState(int state)
    {
        switch (state)
        {
            case 0:
                background.color = checkColor;
                break;
            case 1:
                background.color = _standartColor;
                break;
            case 2:
                background.color = wrongColor;
                break;
            case 3:
                background.color = rightColor;
                break;
        }
    }

}

using TMPro;
using UnityEngine;

public class CheckingBlockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI commentsText;
    [SerializeField] private Color checkColor;
    [SerializeField] private Color wrongColor;
    [SerializeField] private Color rightColor;

    private Color _standartColor;

    private void Awake()
    {
        _standartColor = commentsText.color;
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
                commentsText.color = checkColor;
                break;
            case 1:
                commentsText.color = _standartColor;
                break;
            case 2: 
                commentsText.color = wrongColor;
                break;
            case 3:
                commentsText.color = rightColor;
                break;
        }
    }

}

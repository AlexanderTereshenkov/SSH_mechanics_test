using TMPro;
using UnityEngine;

public class CheckingBlockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI commentsText;
    [SerializeField] private Color checkColor;

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

    public void CheckState(bool isChecking)
    {
        if (isChecking)
        {
            commentsText.color = checkColor;
        }
        else
        {
            commentsText.color = _standartColor;
        }
    }

}

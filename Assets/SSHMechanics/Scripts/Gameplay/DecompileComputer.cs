using UnityEngine;
using System.Text.RegularExpressions;

public class DecompileComputer : MonoBehaviour, IInteractible
{
    [Range(5, 8)]
    [SerializeField] private int passwordLen;

    private string _correctPassword;
    private int _position = 0;



    private void Start()
    {
        var passwordGenerator = new PasswordGenerator();
        _correctPassword = passwordGenerator.GeneratePassword(passwordLen);

        var letters = new Regex(@"^[a-z]");
        var digits = new Regex(@"^[0-9]");
        if (letters.IsMatch(_correctPassword))
        {
            Debug.Log("It starts with letter");
        }
        else if ((digits.IsMatch(_correctPassword)))
        {
            Debug.Log("It starts with digit");
        }
        else
        {
            Debug.Log("It starts with AAAAAAAAAAAAAA");
        }


    }

    public void Interact()
    {
        Debug.Log(_correctPassword);
    }

}

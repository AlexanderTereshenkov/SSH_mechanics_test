using UnityEngine;

public class DecompileComputer : MonoBehaviour, IInteractible
{
    [Range(5, 8)]
    [SerializeField] private int passwordLen;

    private string _correctPassword;

    private void Start()
    {
        var passwordGenerator = new PasswordGenerator();
        _correctPassword = passwordGenerator.GeneratePassword(passwordLen);
    }

    public void Interact()
    {
        Debug.Log(_correctPassword);
    }

}

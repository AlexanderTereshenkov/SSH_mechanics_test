using UnityEngine;

public class LettersBlock : MonoBehaviour, ICheckBlock
{

    public bool CheckPassword(string password, string correctPassword)
    {
        if(password.Length == 0 || password.Length != correctPassword.Length)
        {
            return false;
        }

        return true;
    }

}

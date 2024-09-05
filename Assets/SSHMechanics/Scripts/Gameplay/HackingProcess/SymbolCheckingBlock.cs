using UnityEngine;

public class SymbolCheckingBlock : MonoBehaviour, ICheckBlock
{

    public bool CheckPassword(string password, string correctPassword, int position)
    {

        if(password.Length == 0 || password.Length != correctPassword.Length)
        {
            return false;
        }

        return password[position] == correctPassword[position];
    }

}

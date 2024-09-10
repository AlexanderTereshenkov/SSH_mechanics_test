using System.Collections.Generic;
using UnityEngine;

public class DecompileProcessManager : MonoBehaviour
{

    [Header("Password settings")]
    [SerializeField] private int passwordLength;
    [SerializeField] private int passwordSymbolsInterval;

    [SerializeField] private DecompileBlock[] blocks;

    private PasswordGenerator _passwordGenerator;
    private Dictionary<DecompileBlock.BlockType, DecompileBlock> _blocks = new();

    public string CorrectPassword
    {
        get;
        set;
    }


    private void Awake()
    {
        _passwordGenerator = new();

        foreach (var block in blocks)
        {
            _passwordGenerator.AddSymbols(block.BlockSymbols, block.DecompileBlockType);
            _blocks.Add(block.DecompileBlockType, block);
            block.BlockUI.SetCommentsText(_passwordGenerator.GetFormattedString(block.DecompileBlockType));
        }

        CorrectPassword = _passwordGenerator.GeneratePassword(passwordLength, passwordSymbolsInterval);
        Debug.Log(CorrectPassword);

    }

    public DecompileBlock GetDecompileBlock(DecompileBlock.BlockType blockType)
    {
        return _blocks[blockType];
    }

}



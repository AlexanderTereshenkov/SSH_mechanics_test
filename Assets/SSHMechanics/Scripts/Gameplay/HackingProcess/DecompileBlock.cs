using UnityEngine;

[RequireComponent(typeof(DecompileBlockUI))]
public class DecompileBlock : MonoBehaviour
{

    [SerializeField] private string symbols;
    [SerializeField] private BlockType decompileBlockType;
    [SerializeField] private DecompileBlockUI blockUI;

    public BlockType DecompileBlockType
    {
        get
        {
            return decompileBlockType;
        }
    }

    public string BlockSymbols
    {
        get
        {
            return symbols;
        }
    }

    public DecompileBlockUI BlockUI
    {
        get
        {
            return blockUI;
        }
    }


    public enum BlockType
    {
        Letters,
        Numbers,
        SpecialSymbols
    }
}

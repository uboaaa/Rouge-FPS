using UnityEngine;

public class Mosaic : CustomImageEffect
{
    #region Fields

    [SerializeField]
    [Range(1, 100)]
    private float m_Size;

    #endregion

    #region Properties

    public override string ShaderName
    {
        get { return "Custom/Mosaic"; }
    }

    #endregion

    #region Methods

    protected override void UpdateMaterial()
    {
        Material.SetFloat("_Size", m_Size);
    }

    #endregion

}
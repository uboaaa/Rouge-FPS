using UnityEngine;

/// <summary>
/// Transformの拡張クラス
/// </summary>
public static class TransformExtension
{

    //=================================================================================
    //初期化
    //=================================================================================

    /// <summary>
    /// position、rotation、scaleをリセットする
    /// </summary>
    public static void Reset(this Transform transform, bool isLocal = false)
    {
        if (isLocal)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(
              transform.localScale.x / transform.lossyScale.x,
              transform.localScale.y / transform.lossyScale.y,
              transform.localScale.z / transform.lossyScale.z
            );
        }
    }

}

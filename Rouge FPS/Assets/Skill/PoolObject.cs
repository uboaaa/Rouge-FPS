using UnityEngine;
using System.Collections.Generic;

public abstract class PoolObject<T> : MonoBehaviour
{
    private static GameObject mOriginal;
    private static Stack<T> mObjPool = new Stack<T>();

    public static void SetOriginal(GameObject origin)
    {
        mOriginal = origin;
    }

    public static T Create()
    {
        T obj;
        if (mObjPool.Count > 0)
        {
            obj = Pop();
        }
        else
        {
            var go = Instantiate(mOriginal);
            obj = go.GetComponent<T>();
        }
        (obj as PoolObject<T>).Init();
        return obj;
    }

    private static T Pop()
    {
        var ret = mObjPool.Pop();
        return ret;
    }

    public static void Pool(T obj)
    {
        (obj as PoolObject<T>).Sleep();
        mObjPool.Push(obj);
    }

    public static void Clear()
    {
        mObjPool.Clear();
    }

    public abstract void Init();
    public abstract void Sleep();

}
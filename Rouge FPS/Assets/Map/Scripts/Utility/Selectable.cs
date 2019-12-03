using System;

//選択中の値を管理するクラス
public class Selectable<T>
{
    private T m_Value;       //選択中の値

    public T Value
    {
        get { return m_Value; }
        set
        {
            m_Value = value;
            OnChanged(m_Value);
        }
    }

    //値が変更された時に呼ぶ
    #pragma warning disable 0067
    public Action<T> mChanged;
    #pragma warning restore 0067

    //------------------
    //コンストラクタ
    //------------------
    public Selectable()
    {
        m_Value = default(T);
    }

    public Selectable(T value)
    {
        m_Value = value;
    }

    //-------------------
    //値を設定する
    //-------------------
    //設定後にイベントは呼ばない
    public void SetValueWithoutCallback(T value)
    {
        m_Value = value;
    }

    //変更された場合のみイベント呼び出す
    public void SetValueIfNotEqual(T value)
    {
        if (m_Value.Equals(value))
        {
            return;
        }
        m_Value = value;
        OnChanged(m_Value);
    }

    //----------------------------
    //値が変更された時に呼び出す
    //----------------------------
    private void OnChanged(T value)
    {
        var onChanged = mChanged;
        if(onChanged==null)
        {
            return;
        }
        onChanged(value);
    }

}

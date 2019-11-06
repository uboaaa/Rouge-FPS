using System.Collections.Generic;

static class Expantion
{
    //KeyValuePairで追加するメソッド
    public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> source, KeyValuePair<TKey, TValue> addPair)
    => source.Add(addPair.Key, addPair.Value);

    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> addPairs)
    {
        foreach (var kv in addPairs)
        {
            source.Add(kv);
        }
    }
}

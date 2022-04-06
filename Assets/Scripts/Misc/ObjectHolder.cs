using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder<T> : MonoBehaviour where T : MonoBehaviour
{
    List<T> _list = new List<T>();

    public void PopulateHolder(List<T> templates, int num)
    {
        Clear();
        for (var i = num; i > 0; i--)
        {
            var obj = Instantiate(templates[Random.Range(0, templates.Count)], gameObject.transform);
            obj.gameObject.SetActive(false);
            _list.Add(obj);
        }
    }

    public T GetObject()
    {
        var obj = _list.Find(o => !o.gameObject.activeInHierarchy);
        _list.Remove(obj);
        obj.gameObject.SetActive(true);
        return obj;
    }

    void Clear()
    {
        foreach (var obj in _list)
        {
            Destroy(obj.gameObject);
        }
        _list.Clear();
    }
}

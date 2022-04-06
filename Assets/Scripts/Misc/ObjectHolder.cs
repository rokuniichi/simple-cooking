using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder<T> : MonoBehaviour where T : MonoBehaviour
{
    List<T> _list;

    public void PopulateHolder(List<T> templates, int num)
    {
        _list = new List<T>();
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
}

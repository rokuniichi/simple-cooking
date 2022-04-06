using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    List<T> _list;

    public void PopulatePool(List<T> templates, int num)
    {
        _list = new List<T>();
        for (var i = num; i > 0; i--)
        {
            var obj = Instantiate(templates[Random.Range(0, templates.Count)], gameObject.transform);
            obj.gameObject.SetActive(false);
            _list.Add(obj);
        }
    }

    public T GetPooledObject()
    {
        return _list.Find(o => !o.gameObject.activeInHierarchy);    
    }
}

using UnityEngine;

public abstract class BasePlace : MonoBehaviour
{
    protected PlaceableObject _placeableObject;
    
    public bool IsFree => _placeableObject == null;
    
    public virtual void Engage(PlaceableObject placeableObject)
    {
        _placeableObject = placeableObject;
        _placeableObject.Init();
        _placeableObject.transform.SetParent(gameObject.transform);
        _placeableObject.transform.localPosition = Vector3.zero;
    }

    public virtual void Free()
    {
        if (_placeableObject)
        {
            Destroy(_placeableObject.gameObject);
            _placeableObject = null;
        }
    }
}

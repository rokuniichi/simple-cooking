using UnityEngine;

public abstract class PlaceableObject : MonoBehaviour
{
    Place _place;

    public virtual void Place(Place place)
    {
        _place = place;
        _place.Engage();
        transform.SetParent(_place.transform);
        transform.localPosition = Vector3.zero;
    }

    public void Return()
    {
        _place.Free();
        _place = null;
        Destroy(gameObject);
    }
}

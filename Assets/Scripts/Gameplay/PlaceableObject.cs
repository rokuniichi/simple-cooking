using UnityEngine;

public abstract class PlaceableObject : MonoBehaviour
{
    Place _place;

    public virtual void Place(Place place)
    {
        _place = place;
        _place.Engage();
    }

    public void Return()
    {
        _place.Free();
        _place = null;
    }
}

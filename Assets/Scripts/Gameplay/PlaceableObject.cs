using UnityEngine;

public abstract class PlaceableObject : MonoBehaviour
{
    public virtual void Init()
    {
        gameObject.SetActive(true);
    }
}

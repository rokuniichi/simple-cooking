using UnityEngine;

public class Place : MonoBehaviour
{
    public bool IsFree { get; private set; }
    
    public virtual void Engage()
    {
        gameObject.SetActive(true);
        IsFree = false;
    }

    public void Free()
    {
        gameObject.SetActive(false);
        IsFree = true;
    }
}

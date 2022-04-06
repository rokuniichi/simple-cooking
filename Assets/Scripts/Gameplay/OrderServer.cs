using UnityEngine;

public class OrderServer : MonoBehaviour
{
    public string Name;

    public void TryServeOrder()
    {
        CustomerController.Instance.TryServeOrder(Name);
    }
}

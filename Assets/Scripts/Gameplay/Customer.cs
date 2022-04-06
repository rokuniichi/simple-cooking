using System.Collections.Generic;

public class Customer : PlaceableObject
{
    public List<OrderPlace> OrderPlaces;

    List<Order> _orders;

    public override void Init()
    {
        base.Init();
        _orders = new List<Order>();
    }

    public void CreateOrder(Order order)
    {
        _orders.Add(order);
        OrderPlaces.Find(x => x.IsFree).Engage(order);
    }
 }

using System.Collections.Generic;

public class Customer : PlaceableObject
{
    public List<Place> OrderPlaces;
    
    List<Order> _orders;
    
    public override void Place(Place place)
    {
        base.Place(place);
        foreach (var orderPlace in OrderPlaces)
        {
            orderPlace.Free();
        }

        _orders = new List<Order>();
    }

    public void CreateOrder(Order order)
    {
        var place = OrderPlaces.Find(x => x.IsFree);
        order.Place(place);
        _orders.Add(order);
    }

    public bool TryServeOrder(string order)
    {
        var o = _orders.Find(x => x.Name == order);
        if (o)
        {
            o.Return();
            _orders.Remove(o);
            if (_orders.Count == 0)
            {
                Return();
            }
            
            return true;
        }

        return false;
    }
}

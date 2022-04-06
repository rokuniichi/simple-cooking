using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Customer : PlaceableObject
{
    public List<Place> OrderPlaces;
    public GameObject  Bubble;

    Sequence    _sequence;
    List<Order> _orders;
    
    public event Action OrderServed;
    
    public bool IsServable()
    {
        return HasOrders() && Bubble.activeSelf;
    }

    public bool HasOrders()
    {
        return _orders.Count > 0;
    }
    
    public bool TryServeOrder(string order)
    {
        var o = _orders.Find(x => x.Name == order);
        if (o)
        {
            ServeOrder(o);
            _orders.Remove(o);
            return true;
        }

        return false;
    }

    public void UseBooster()
    {
        foreach (var order in _orders)
        {
            ServeOrder(order);
        }
        
        _orders.Clear();
    }

    public void AnimateArrival(Transform background, Transform from, Place place, List<Order> orders)
    {
        if (_sequence != null && _sequence.IsActive()) return;
        _orders = orders;
        CreateOrders();
        transform.position = from.position;
        Bubble.SetActive(false);
        transform.SetParent(background);
        Place(place);
        RefreshSequence();
        _sequence.Append(transform.DOMove(place.transform.position, 3f));
        _sequence.AppendCallback(() =>
        {
            transform.SetParent(place.transform);
            Bubble.SetActive(true);
        });
    }

    public void AnimateDeparture(Transform background, Transform to)
    {
        if (_sequence != null && _sequence.IsActive() && !HasOrders()) return;
        ClearOrders();
        Bubble.SetActive(false);
        transform.SetParent(background);
        Return();
        RefreshSequence();
        _sequence.Append(transform.DOMove(to.position, 3f));
        _sequence.AppendCallback(() => Destroy(gameObject));
    }

    void CreateOrders()
    {
        foreach (var orderPlace in OrderPlaces)
        {
            orderPlace.Free();
        }

        foreach (var order in _orders)
        {
            var place = OrderPlaces.Find(x => x.IsFree);
            order.Place(place);
        }
    }

    void ClearOrders()
    {
        foreach (var order in _orders)
        {
            order.Return();
            Destroy(order.gameObject);
        }
        
        _orders.Clear();
    }

    void ServeOrder(Order order)
    {
        order.Return();
        OrderServed?.Invoke();
    }

    void RefreshSequence()
    {
        if (_sequence != null)
        {
            _sequence.SetAutoKill(false);
            _sequence.Kill();
        }

        _sequence = DOTween.Sequence();
    }
}
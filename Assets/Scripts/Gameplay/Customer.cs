using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Customer : PlaceableObject
{
    public List<Place> OrderPlaces;
    public GameObject  Bubble;

    Sequence    _sequence;
    List<Order> _orders;

    public bool TryServeOrder(string order)
    {
        var o = _orders.Find(x => x.Name == order);
        if (o)
        {
            o.Return();
            _orders.Remove(o);
            return true;
        }

        return false;
    }

    public bool HasOrders()
    {
        return _orders.Count > 0 && Bubble.activeSelf;
    }

    public void AnimateArrival(Transform background, Transform from, Place place, List<Order> orders)
    {
        _orders = orders;
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
            CreateOrders();
        });
    }

    public void AnimateDeparture(Transform background, Transform to)
    {
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
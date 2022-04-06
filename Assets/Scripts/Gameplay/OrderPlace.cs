using UnityEngine;

public class OrderPlace : BasePlace
{
    public override void Engage(PlaceableObject placeableObject)
    {
        base.Engage(placeableObject);
        var rt = placeableObject.GetComponent<RectTransform>();
        rt.anchorMin        = Vector2.zero;
        rt.anchorMax        = Vector2.one;
        rt.sizeDelta        = Vector2.zero;
        rt.anchoredPosition = Vector2.zero;
    }
}

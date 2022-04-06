using UnityEngine;
using UnityEngine.UI;

public class BoosterWindow : MonoBehaviour
{
    public Button BuyButton;
    public Button CloseButton;

    public void Show()
    {
        BuyButton.onClick.AddListener(Buy);
        CloseButton.onClick.AddListener(Hide);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        CloseButton.onClick.RemoveAllListeners();
        BuyButton.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
        GameController.Instance.Continue();
    }

    public void Buy()
    {
        CustomerController.Instance.BuyBooster();
    }
}

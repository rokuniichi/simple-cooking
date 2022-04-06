using UnityEngine;
using UnityEngine.UI;

public class RestartWindow : MonoBehaviour
{
    public Button RestartButton;

    public void Show()
    {
        RestartButton.onClick.AddListener(Restart);
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        RestartButton.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
        GameController.Instance.Restart();
    }
}

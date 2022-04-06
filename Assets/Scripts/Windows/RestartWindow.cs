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
        GameController.Instance.Restart();
        RestartButton.onClick.RemoveListener(Restart);
        gameObject.SetActive(false);

    }
}

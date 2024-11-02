using UnityEngine;
using UnityEngine.UI;

public class MenuCtrl : MonoBehaviour
{
    public Button startButton;

    private void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();

            startButton.onClick.AddListener(() => GameManager.Instance.StartGame());
        }
        else
        {
            Debug.LogError("StartButton is not assigned to the MenuCtrl");
        }
    }
}

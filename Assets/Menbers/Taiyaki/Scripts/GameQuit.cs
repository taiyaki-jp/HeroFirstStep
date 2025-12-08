using UnityEngine;
using UnityEngine.UI;

public class GameQuit : MonoBehaviour
{
    private Button _quitButton;
    private void Awake()
    {
        _quitButton = GetComponent<Button>();
    }

    private void Start()
    {
        _quitButton.onClick.AddListener(Quit);
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }
}

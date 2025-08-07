using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject mainMenu;
    private void OnEnable()
    {
        backButton.onClick.AddListener(ClosePanel);
    }

    private void ClosePanel()
    {
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
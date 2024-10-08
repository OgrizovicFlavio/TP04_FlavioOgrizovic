using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOptions : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] Slider volumeSlider;
    [SerializeField] TextMeshProUGUI volumeCounter;
    [Header("UI")]
    [SerializeField] private Button backButtonOptions;
    [SerializeField] private GameObject mainMenuPanel;

    private void Awake()
    {
        //volumeSlider.onValueChanged.AddListener(OnValueChangedVolumeSlider);
        backButtonOptions.onClick.AddListener(OnBackButtonOptionsClicked);
    }

    private void OnDestroy()
    {
        //volumeSlider.onValueChanged.RemoveListener(OnValueChangedVolumeSlider);
        backButtonOptions.onClick.RemoveListener(OnBackButtonOptionsClicked);
    }

    private void OnValueChangedVolumeSlider()
    {

    }

    private void OnBackButtonOptionsClicked()
    {
        gameObject.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}

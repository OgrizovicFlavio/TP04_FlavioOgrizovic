using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;

    private void Start()
    {
        Time.timeScale = 1;
        mainMenuPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !optionsPanel.activeSelf)
        {
            if (!mainMenuPanel.activeSelf)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        mainMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        mainMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}

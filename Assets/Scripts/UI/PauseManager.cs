using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool isPause;

    private void Awake()
    {
        pausePanel.SetActive(false);
        isPause = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // && Time.timeScale != 0f
        {
            resumeOnClick();
        }
    }



    //BUTTONS ON CLICKS
    public void resumeOnClick()
    {
        if (isPause)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            isPause = false;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPause = true;
        }
    }

    public void mainMenuOnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quitOnClick()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header ("GUI")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject achivmentsPanel;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private TextMeshProUGUI highScoreText;


    [Header ("Effects")]
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem[] particles;


    private void Awake()
    {
        Time.timeScale = 1f;

        //start all the aprticles
        for(int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
        }

        //Set the highscore for the player
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();
    }


    private IEnumerator LoadLevel()
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        int nextArena = Random.Range(1, 5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + nextArena);
        Time.timeScale = 1f;
    }

    //BUTTONS ON CLICKS
    public void playOnClick()
    {
        StartCoroutine(LoadLevel());
    }

    public void settingsOnClick()
    {
        settingsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void achivmentsOnClick()
    {
        achivmentsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }


    public void quitOnClick()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }
}

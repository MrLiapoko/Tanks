using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private Health playersHealth;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Animator anim;


    private ScoreManager scoreManager;

    private bool isDead = false;
    private float delay = 1f;
    private float delayTimer = 0;


    private void Awake()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        deathPanel.SetActive(false);
    }

    private void Update()
    {
        if(playersHealth.currentHealth <= 0)
        {
            if (delayTimer >= delay)
            {
                isDead = true;
                deathPanel.SetActive(true);
                Time.timeScale = 0f;

                //save high score
                float score = scoreManager.score;
                float HighScore = PlayerPrefs.GetFloat("HighScore", 0);
                if(score > HighScore)
                {
                    PlayerPrefs.SetFloat("HighScore", score);
                }

                updateUI(score);

                delayTimer = 0;
            }
            else
            {
                delayTimer += Time.deltaTime;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isDead)
            {
                Time.timeScale = 1f;
                StartCoroutine(LoadLevel());
            }
        }
    }

    private IEnumerator LoadLevel()
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        deathPanel.SetActive(false);
        isDead = false;

        int nextArena = Random.Range(1, 5);
        SceneManager.LoadScene(nextArena);
        Time.timeScale = 1f;
    }


    private void updateUI(float score)
    {
        scoreText.text = "SCORE: " + score;
        highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetFloat("HighScore", 0);
    }
}

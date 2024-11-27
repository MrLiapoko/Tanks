using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score = 0f;
    private Text scoreText;

    [Header("Score animation")]
    private Vector3 originalScale;

    [Header("Animations")]
    [SerializeField] private Animator anim;


    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void animatePop()
    {
        anim.SetTrigger("pop");
    }
}

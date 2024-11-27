using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class AchivmentManager : MonoBehaviour
{
    [SerializeField] GameObject[] medals = new GameObject[5];
    int[] achivments = { 100, 250, 500, 750, 1000 };

    private void Start()
    {
        float currenHighScore = PlayerPrefs.GetFloat("HighScore", 0);

        for (int i = 0; i < achivments.Length; i++)
        {
            if (achivments[i] <= currenHighScore)
            {
                medals[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}

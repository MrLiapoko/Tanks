using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    //constants
    private const string VolumePref = "volume";

    [Header("Panel")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject achivmentsPanel;
    [SerializeField] private GameObject mainPanel;

    [Header ("Volume")]
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey(VolumePref))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumePref);
            volumeSlider.value = savedVolume;
            MusicManager.instance.setVolme(savedVolume);
        }
        else
        {
            volumeSlider.value = 1f;
            MusicManager.instance.setVolme(1f);
        }

        //listener
        volumeSlider.onValueChanged.AddListener(setVolume);

    }

    public void setVolume(float volume)
    {
        MusicManager.instance.setVolme(volume);
        PlayerPrefs.SetFloat(VolumePref, volume);
        PlayerPrefs.Save();
    }


    //OnClick
    public void backOnClick()
    {
        settingsPanel.SetActive(false);
        achivmentsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}

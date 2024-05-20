using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject HowToPlayPanel;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject CreditsPanel;
    [SerializeField] private GameObject SoundButton;
    [SerializeField] private GameObject NoSoundButton;


    public AudioSource audioSource;
    private bool isPaused = false;
    private float pauseTime;

    

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenuControl()
    {
        SceneManager.LoadScene(0);
    }

    public void HowToPlay()
    {
        MainMenuPanel.SetActive(false);

        if (HowToPlayPanel != null)
        {
            bool isActive = HowToPlayPanel.activeSelf;
            HowToPlayPanel.SetActive(!isActive);
        }


        if (HowToPlayPanel != null)
        {
            HowToPlayPanel.SetActive(true);
        }

    }

    public void Credits()
    {
        MainMenuPanel.SetActive(false);

        if (CreditsPanel != null)
        {
            bool isActive = CreditsPanel.activeSelf;
            CreditsPanel.SetActive(!isActive);
        }


        if (CreditsPanel != null)
        {
            CreditsPanel.SetActive(true);
        }

    }

    public void SoundControl()
    {

        SoundButton.SetActive(false);
        NoSoundButton.SetActive(true);

        if (audioSource.isPlaying)
        {
            pauseTime = audioSource.time;
            audioSource.Pause();
            isPaused = true;
        }
        else if (isPaused)
        {
            NoSoundButton.SetActive(false);
            SoundButton.SetActive(true);
            audioSource.time = pauseTime;
            audioSource.Play();
            isPaused = false;
        }
        else
        {
           
            audioSource.Play();
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}

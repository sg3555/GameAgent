using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    GameObject PauseUI;
    Slider volumeSlider;
    float volume;

    bool paused = false;

    // Start is called before the first frame update
    void Awake()
    {
        PauseUI = GameObject.Find("Canvas").transform.Find("PauseUI").gameObject;
        volumeSlider = PauseUI.transform.Find("Slider").GetComponent<Slider>();
        PauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
            paused = !paused;

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
        volume = volumeSlider.value;
        AudioListener.volume = volume;
    }

    public void ResumeBtn()
    {
        paused = false;
    }
}

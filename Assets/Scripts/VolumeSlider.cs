using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider masterVolume;
    private float masterVol = 1f;

    void Start()
    {
        masterVolume.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        masterVol = masterVolume.value;
        AudioListener.volume = masterVol;
    }
}

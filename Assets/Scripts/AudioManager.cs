using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jason;


public class AudioManager : MonoBehaviour
{
    AudioSource m_AudioSource;
    Slider m_Slider;

    private void Awake()
    {
        Loadcomponent();
    }



    private void Update()
    {
        m_AudioSource.volume = m_Slider.value;
    }

    void Loadcomponent()
    {
        Transform parent = AssetAssist.FindObject("UICanvas");
        m_AudioSource = AssetAssist.FindComponent<AudioSource>("InGameBGM");
        m_Slider = AssetAssist.FindComponent<Slider>("BGMVolumeSlider", parent);

        m_Slider.value = 0.2f;
    }
}

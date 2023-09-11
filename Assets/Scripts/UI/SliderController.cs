using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private const int SLIDER_MIN_VALUE = 0;
    private const int SLIDER_MAX_VALUE = 3;

    [SerializeField] private Slider m_Slider;

    [Header("Audio")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_MoveSliderSound;
    [SerializeField] private AudioClip m_BlockedSound;

    public void AddValue()
    {
        if ((int)m_Slider.value >= SLIDER_MAX_VALUE)
        {
            m_AudioSource.PlayOneShot(m_BlockedSound);
            return;
        }

        m_AudioSource.PlayOneShot(m_MoveSliderSound);
        m_Slider.value += 1;
    }

    public void SubtractValue()
    {
        if ((int)m_Slider.value <= SLIDER_MIN_VALUE)
        {
            m_AudioSource.PlayOneShot(m_BlockedSound);
            return;
        }

        m_AudioSource.PlayOneShot(m_MoveSliderSound);
        m_Slider.value -= 1;
    }
}

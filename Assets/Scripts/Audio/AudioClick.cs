using UnityEngine;

public class AudioClick : MonoBehaviour
{
    public void ClickSFX()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Click_Button);
    }
}

using UnityEngine;

public class GuitarPlay : MonoBehaviour
{
    public AudioSource guitarAudio;

    public void PlayGuitar()
    {
        guitarAudio?.Play();
    }
}
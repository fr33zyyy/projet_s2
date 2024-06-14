using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSound : MonoBehaviour 
{
    public static AudioSound instance; // Instance statique pour Singleton
    public AudioSource clickSound; // AudioSource pour le son de clic
    public AudioSource dash;
    public AudioSource ambiance;
    public AudioSource night;

    void Awake()
    {
        // Mettre en place le Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        // Assurez-vous que le GameObject du gestionnaire de sons persiste entre les scènes
        DontDestroyOnLoad(gameObject);
    }

    public void PlayClickSound()
    {
        if (clickSound != null && clickSound.clip != null)
        {
            clickSound.Play(); // Jouer le son de clic
        }
        else
        {
            Debug.LogWarning("AudioSource de clic ou clip audio non défini.");
        }
    }

    public void PlayDashSound(){
        dash.Play();
    }
}

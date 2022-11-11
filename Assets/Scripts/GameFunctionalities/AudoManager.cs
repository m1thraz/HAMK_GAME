using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudoManager : MonoBehaviour
{
    public Sound[] sounds;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        foreach( Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {

        Debug.Log("Playing sound");
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        Debug.Log("Playing done");
    }
}
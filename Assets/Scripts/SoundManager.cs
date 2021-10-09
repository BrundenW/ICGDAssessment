using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip IntroMusic;
    public AudioClip RegularBackgroundMusic;
    public AudioClip ScaredBackgroundMusic;
    public AudioClip DeadBackgroundMusic;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
        StartCoroutine(playMusic());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator playMusic()
    {
        source.clip = IntroMusic;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        source.clip = RegularBackgroundMusic;
        source.Play();
    }
}

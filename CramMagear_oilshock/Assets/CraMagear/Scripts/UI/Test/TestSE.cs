using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSE : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 左
        if (Input.GetKey(KeyCode.W))
        {
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
        }
        if(Input.GetKey(KeyCode.A))
        {
            audioSource.PlayOneShot(sound2);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound3);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound4);
        }
    }
}

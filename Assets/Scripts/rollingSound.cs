using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rollingSound : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
      audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      if (audioSource.isPlaying) audioSource.pitch = Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude / 2, -1, 1.09f);
      else if (Time.timeScale == 1 && SceneManager.GetActiveScene().name != "levelSelection") {
        audioSource.clip = GameObject.Find("soundsReference").GetComponent<soundsReference>().rollingSound;
        audioSource.mute = false;
        audioSource.Play();
      }
    }
}

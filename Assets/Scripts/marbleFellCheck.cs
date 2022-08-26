using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class marbleFellCheck : MonoBehaviour
{
    public liveManager liveManager;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
      liveManager = GameObject.Find("liveManager").GetComponent<liveManager>();
      audioSource = GameObject.Find("fallingMarbleSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      if (SceneManager.GetActiveScene().name == "levelSelection") return;
      if (gameObject.transform.position.y < -3.15) {
        Debug.Log("Marble fell down!");
        audioSource.Play();
        liveManager.takeDamage(false);
        gameObject.transform.position = new Vector3(0, 10, 0);
      }
    }
}

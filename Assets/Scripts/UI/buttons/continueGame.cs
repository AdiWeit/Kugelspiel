using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continueGame : MonoBehaviour
{
    private movePlane movePlane;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void continueGameF()
    {
      movePlane = GameObject.Find("movingCube").GetComponent<movePlane>();
      pauseMenu.SetActive(false);
      movePlane.waitForMousePosition = true;
      if (Input.gyro.enabled) {
        Time.timeScale = 1f;
      }
    }
}

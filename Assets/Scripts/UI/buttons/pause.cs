using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pause : MonoBehaviour
{
    // Start is called before the first frame update
    private levelManager levelManager;
    public GameObject pauseMenu;
    public continueGame continueGame; 
    private movePlane movePlane;
    public GameObject sensitivityWarning;
    void Start()
    {
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown("p")) {
        movePlane = GameObject.Find("movingCube").GetComponent<movePlane>();
        movePlane.sensitivityBefore = movePlane.sensitivity;
        if (levelManager.gameStarted || movePlane.waitForMousePosition) pauseGame();
        else continueGame.continueGameF();
      }
    }
    public void pauseGame()
    {
      levelManager.sceneBefore = SceneManager.GetActiveScene().name;
      levelManager.gameStarted = false;
      if (pauseMenu.activeInHierarchy) {
        continueGame.continueGameF();
      } 
      else {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        sensitivityWarning.SetActive(Input.gyro.enabled);
      }
    }
}

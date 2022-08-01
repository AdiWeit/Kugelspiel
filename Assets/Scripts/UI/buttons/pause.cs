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
    void Start()
    {
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown("p") && levelManager.gameStarted) pauseGame();
    }
    public void pauseGame()
    {
      levelManager.sceneBefore = SceneManager.GetActiveScene().name;
      levelManager.gameStarted = false;
      SceneManager.LoadScene("settings");
    }
}

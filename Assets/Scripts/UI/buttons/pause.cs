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
      levelManager = GameObject.Find("goal_hitbox").GetComponent<levelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pauseGame()
    {
      levelManager.gameStarted = false;
      SceneManager.LoadScene("settings");
    }
}

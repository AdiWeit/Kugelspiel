using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class liveManager : MonoBehaviour
{
    private int lives = 3;
    public levelManager levelManager;
    public Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage()
    {
      lives--;
      if (lives > 0) levelManager.startLevel(-1, "random");
      else {
        levelManager.startLevel(0, "random");
        lives = 3;
      }
      livesText.text = lives + " lives";
    }
}

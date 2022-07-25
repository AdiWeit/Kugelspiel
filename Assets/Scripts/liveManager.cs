using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class liveManager : MonoBehaviour
{
    private int lives = 5;
    public levelManager levelManager;
    public Text livesText;
    // Start is called before the first frame update
    void Start()
    {
      levelManager = GameObject.Find("goal_hitbox").GetComponent<levelManager>();
      livesText = GameObject.Find("livesText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(bool byStuckOne)
    {
      if (!levelManager.waitBlockedDisapears || byStuckOne) {
      lives--;
      if (lives > 0) levelManager.startLevel(-1, "random");
      else {
        levelManager.startLevel(0, "random");
        lives = 5;
      }
      livesText.text = lives + " lives";
      }
    }
    public void getLive()
    {
      lives++;
      livesText.text = lives + " lives";
    }
}

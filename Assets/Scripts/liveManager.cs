using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class liveManager : MonoBehaviour
{
    private int lives = 5;
    public levelManager levelManager;
    public Text livesText;
    private loadLevel levelLoader;
    // Start is called before the first frame update
    void Start()
    {
      levelManager = GameObject.Find("goal_hitbox").GetComponent<levelManager>();
      if (GameObject.Find("livesText")) livesText = GameObject.Find("livesText").GetComponent<Text>();
      levelLoader = GameObject.Find("levelLoader").GetComponent<loadLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(bool byStuckOne)
    {
      if ((!levelManager.waitBlockedDisapears || byStuckOne) && levelLoader.random) {
      lives--;
      if (lives > 0) levelManager.startLevel(0);
      else {
        levelManager.startLevel(-1);
        lives = 5;
      }
      if (livesText) livesText.text = lives + " lives";
      }
      if (!levelLoader.random) levelManager.levelLoader.LoadLevel(levelManager.currentLevel);
    }
    public void getLive()
    {
      lives++;
      if (livesText) livesText.text = lives + " lives";
    }
}

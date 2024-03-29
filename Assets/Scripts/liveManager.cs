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
    public GameObject triesManager;
    // Start is called before the first frame update
    void Start()
    {
      if (GameObject.Find("livesText")) livesText = GameObject.Find("livesText").GetComponent<Text>();
      levelLoader = GameObject.Find("levelLoader").GetComponent<loadLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(bool byStuckOne)
    {
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
      if (!levelManager.random) triesManager.GetComponent<triesManager>().increaceTries();
      if ((!levelManager.waitBlockedDisapears || byStuckOne) && levelManager.random) {
      lives--;
      if (lives > 0) levelManager.startLevel(0, true);
      else {
        levelManager.startLevel(-1, true);
        lives = 5;
      }
      if (livesText) livesText.text = lives + " lives";
      }
      if (!levelManager.random) levelManager.levelLoader.LoadLevel(levelManager.currentLevel, true, true);
    }
    public void getLive()
    {
      lives++;
      if (livesText) livesText.text = lives + " lives";
    }
}

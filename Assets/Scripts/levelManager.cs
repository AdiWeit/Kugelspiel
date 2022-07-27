using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    // public int sphereCount = 8;
    public int sphereCount = 0;
    public int inGoalCount = 0;
    // private int currentLevel = 7;
    private int currentLevel = 0;
    public bool waitBlockedDisapears = false;
    public scoreSystem scoreObj;
    public gameInstructions instructionsText;
    public objectManager objManager;
    public liveManager liveManager;
    public loadLevel levelLoader;
    public Text levelNrText;
    public borderObj borderObj;
    public bool random = true;
    private Vector3 borderStartingPosition;

    // Start is called before the first frame update
    public bool gameStarted = false;
    void Start()
    {
      scoreObj = GameObject.Find("scoreText").GetComponent<scoreSystem>();
      instructionsText = GameObject.Find("gameInstructions").GetComponent<gameInstructions>();
      objManager = GameObject.Find("objectManager").GetComponent<objectManager>();
      liveManager = GameObject.Find("liveManager").GetComponent<liveManager>();
      levelNrText = GameObject.Find("levelNrText").GetComponent<Text>();
      borderObj = GameObject.Find("border").GetComponent<borderObj>();
      levelLoader = GameObject.Find("levelLoader").GetComponent<loadLevel>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void goalReached(GameObject marble) {
      if (marble.GetComponent<marbleParams>().type != "blocker" || (marble.GetComponent<marbleParams>().type == "blocker" && GameObject.FindGameObjectsWithTag("marble").Where(o => o.GetComponent<marbleParams>().type != "blocker" && !o.GetComponent<marbleParams>().type.Contains("Bounce")).ToArray().Length == 0))
      {
        Destroy(marble, 0.25f);
        inGoalCount++;
        if (GameObject.FindGameObjectsWithTag("marble").Where(o => o.GetComponent<marbleParams>().type != "blocker" && !o.GetComponent<marbleParams>().type.Contains("Bounce")).ToArray().Length == 0) {
          inGoalCount = sphereCount;
        }
        Debug.Log(inGoalCount + "/" + sphereCount + " marbles reached the goal!");
        if (inGoalCount == sphereCount) {
          Debug.Log("random levels: " + random);
          if (random) {
           sphereCount++;
           scoreObj.goalReached();
           currentLevel++;
           liveManager.getLive();
           Debug.Log((currentLevel + 1) + ". Level reached!");
           instructionsText.levelDone(currentLevel);
           startLevel(currentLevel, "random");
          } 
          else {
            Debug.Log("Trying to load level Nr. " + (int.Parse(SceneManager.GetActiveScene().name.Split("_")[1]) + 1));
            levelLoader.LoadLevel(int.Parse(SceneManager.GetActiveScene().name.Split("_")[1]) + 1);
          }
        }
      }
      else if (!waitBlockedDisapears) {
        waitBlockedDisapears = true;
        StartCoroutine("marbleBlocked");
      }
    }
    IEnumerator marbleBlocked()
    {
      Debug.Log("restart Level because of blocked marble");
      yield return new WaitForSeconds(2);
      liveManager.takeDamage(true);
      waitBlockedDisapears = false;
    }
    private void OnTriggerEnter(Collider other)
    {
      goalReached(other.gameObject);
    }
    public void startLevel(int number, string type) 
    {
      if (number == -1) number = currentLevel;
      if (number == 0) {
        Debug.Log("restart with first level");
        if (currentLevel > 0) {
          currentLevel--;
          sphereCount--;
          borderObj.transform.localPosition = new Vector3(borderObj.transform.localPosition.x, borderObj.transform.localPosition.y + 0.001f, borderObj.transform.localPosition.z);
        }
      }
      // reset level
      else if (borderObj.transform.position.y > 0.38) borderObj.transform.localPosition = new Vector3(borderObj.transform.localPosition.x, borderObj.transform.localPosition.y - 0.001f, borderObj.transform.localPosition.z);
      levelNrText.text = (currentLevel + 1 + ". level");
      foreach (GameObject obj in GameObject.FindGameObjectsWithTag("marble"))
      {
        Destroy(obj);
      }
      inGoalCount = 0;
      if (type == "random") {
        string[] marbleDistribution = new string [sphereCount];
        // manage marble type distribution (Verteilung)
        if (currentLevel < 4 && currentLevel > 0) marbleDistribution[0] = "enemy";
        if (Mathf.Floor(currentLevel / 4) <= 4) {
          for (int i = 0; i < Mathf.Floor(currentLevel / 4); i++)
          {
            marbleDistribution[i] = "blocker";
          }
        }
        if (Mathf.Floor((currentLevel - 4) / 2) <= 4 && currentLevel > 3) {
          int mediumSpeedCounter = 0;
          for (int i = 0; mediumSpeedCounter < Mathf.Floor((currentLevel - 3) / 2) && i < marbleDistribution.Length; i++)
          {
            if (marbleDistribution[i] == null) {
              marbleDistribution[i] = "mediumSpeed";
              mediumSpeedCounter++;
            }
          }
        }
        if (Mathf.Floor((currentLevel - 7) / 3) <= 4 && currentLevel > 7) {
          int mediumSpeedCounter = 0;
          for (int i = 0; mediumSpeedCounter < Mathf.Floor((currentLevel - 7) / 3) && i < marbleDistribution.Length; i++)
          {
            if (marbleDistribution[i] == null) {
              marbleDistribution[i] = "heighSpeed";
              mediumSpeedCounter++;
            }
          }
        }
        if (currentLevel > 5 && currentLevel <= 9) marbleDistribution[sphereCount - 1] = "littleBounce";
        if (currentLevel > 9 && currentLevel <= 13) marbleDistribution[sphereCount - 2] = "mediumBounce";
        if (currentLevel > 13) marbleDistribution[sphereCount - 3] = "muchBounce";
        for (int i = 0; i < marbleDistribution.Length; i++) {
          if (marbleDistribution[i] == null) marbleDistribution[i] = "normal";
        }
        objManager.spawnSphere(0, 0, marbleDistribution);
      }
    }
}

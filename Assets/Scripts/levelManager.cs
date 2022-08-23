using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Linq.Expressions;
using System.Xml;

public class levelManager : MonoBehaviour
{
    public int sphereCount = 1;
    public int inGoalCount = 0;
    // private int currentLevel = 7;
    public int currentLevel = 0;
    
    public bool waitBlockedDisapears = false;
    public scoreSystem scoreObj;
    public gameInstructions instructionsText;
    public objectManager objManager;
    public liveManager liveManager;
    public loadLevel levelLoader;
    public movePlane movePlane;
    public Text levelNrText;
    public borderObj borderObj;
    private Vector3 borderStartingPosition;
    // Start is called before the first frame update
    public bool gameStarted = false;
    public bool random = false;
    public string sceneBefore;
    public bool[] highscores = new bool[1];
    public GameObject randomSettingsManager;
    public float spawnHeight = 3.8f;
    void Start()
    {
      levelLoader = GameObject.Find("levelLoader")?.GetComponent<loadLevel>();
      if (GameObject.FindObjectsOfType<levelManager>().Length > 1) Destroy(gameObject);
      // DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void goalReached(GameObject marble) {
      movePlane = GameObject.Find("movingCube").GetComponent<movePlane>();
      scoreObj = GameObject.Find("scoreText")?.GetComponent<scoreSystem>();
      instructionsText = GameObject.Find("gameInstructions")?.GetComponent<gameInstructions>();
      liveManager = GameObject.Find("liveManager")?.GetComponent<liveManager>();
      levelLoader = GameObject.Find("levelLoader")?.GetComponent<loadLevel>();
      if (marble.GetComponent<marbleParams>().type != "blocker" || (marble.GetComponent<marbleParams>().type == "blocker" && GameObject.FindGameObjectsWithTag("marble").Where(o => o.GetComponent<marbleParams>().type != "blocker" && !o.GetComponent<marbleParams>().type.Contains("Bounce")).ToArray().Length == 0))
      {
        Destroy(marble, 0.25f);
        inGoalCount++;
        if ((GameObject.FindGameObjectsWithTag("marble").Where(o => o.GetComponent<marbleParams>().type == "blocker").ToArray().Length == 0 && GameObject.FindGameObjectsWithTag("marble").Where(o => !o.GetComponent<marbleParams>().type.Contains("Bounce")).ToArray().Length <= 1) || GameObject.FindGameObjectsWithTag("marble").Where(o => o.GetComponent<marbleParams>().type != "blocker" && !o.GetComponent<marbleParams>().type.Contains("Bounce")).ToArray().Length == 0) {
          inGoalCount = sphereCount;
        }
        Debug.Log(inGoalCount + "/" + sphereCount + " marbles reached the goal!");
        if (inGoalCount == sphereCount) {
          currentLevel++;
          if (random) {
           sphereCount++;
           scoreObj.goalReached();
           liveManager.getLive();
           Debug.Log((currentLevel + 1) + ". Level reached!");
           instructionsText.levelDone(currentLevel, random);
           if (GameObject.Find("settingsManager").GetComponent<settingsManager>().resetBoxPosition) {
            gameStarted = false;
            // GameObject.Find("movingCube").GetComponent<movePlane>().waitForMousePosition = true;
            GameObject.Find("movingCube").transform.eulerAngles = new Vector3(0, 0, 0);
            if (Input.gyro.enabled) {
              GameObject.Find("playBReference").GetComponent<playBReference>().playB.GetComponent<continueGame>().continueGameF();
              movePlane.instructionsText.showText("tilt your device like it is lying on a table, so the red indication border has to be in the same angle the box is. Alternatively, click two times (doubleclick possible) to start the level with your current tilt. You can also turn this off in the settings.");
            }
            else {
              movePlane.waitForClick = true;
              movePlane.startPosition = new Vector3(0, 0, 0);
              movePlane.instructionsText.showText("Click to place a new joystick. Move your mouse/finger to use the joystick. ");
            }
           }
           /*else */startLevel(currentLevel, false);
          } 
          else {
            // highscores[currentLevel] = true;
            levelLoader.loadHighscores(false);
            highscores = setArrayIndexValue(true, currentLevel - 1);
            PlayerPrefs.SetString("levelHighscores", convertArrayToString(highscores));
            PlayerPrefs.Save();
            if (GameObject.Find("settingsManager").GetComponent<settingsManager>().continueLevel) {
              levelLoader.LoadLevel(currentLevel, true, false);
              movePlane.instructionsText.showText("Click to place a new joystick. Move your mouse/finger to use the joystick. ");
            }
            else {
              levelLoader.openLevelSelection();
            }
          }
        }
      }
      else if (!waitBlockedDisapears) {
        waitBlockedDisapears = true;
        StartCoroutine("marbleBlocked");
      }
    }
    public bool[] setArrayIndexValue(bool value, int index) {
      if (highscores.Length <= index) {
        bool[] newArray = new bool[index + 1];
        for (int i = 0; i < newArray.Length; i++)
        {
          newArray[i] = false;
        }
        for (int i = 0; i < highscores.Length; i++)
        {
          newArray[i] = highscores[i];
        }
        newArray[index] = value;
        return newArray;
      }
      else {
        highscores[index] = value;
        return highscores;
      }
    }
    public string convertArrayToString (bool [] array) {
      string pString = "[";
      for (int i = 0; i < array.Length; i++)
      {
        pString += array[i].ToString();
        if (i + 1 != array.Length) pString += ",";
      }
      pString += "]";
      return pString;
    }
    IEnumerator marbleBlocked()
    {
      Debug.Log("restart Level because of blocked marble");
      yield return new WaitForSeconds(2);
      liveManager.takeDamage(true);
      waitBlockedDisapears = false;
    }
    public void startLevel(int number, bool restartLevel) 
    {
      objManager = GameObject.Find("objectManager")?.GetComponent<objectManager>();
      levelNrText = GameObject.Find("levelNrText")?.GetComponent<Text>();
      borderObj = GameObject.Find("border")?.GetComponent<borderObj>();
      movePlane = GameObject.Find("movingCube").GetComponent<movePlane>();
      inGoalCount = 0;
      if (restartLevel && GameObject.Find("settingsManager").GetComponent<settingsManager>().resetBoxPosition) {
        movePlane.transform.eulerAngles = new Vector3(0, 0, 0);
        GameObject.Find("levelManager").GetComponent<levelManager>().gameStarted = false;
        if (Input.gyro.enabled) {
          GameObject.Find("playBReference").GetComponent<playBReference>().playB.GetComponent<continueGame>().continueGameF();
          movePlane.instructionsText.showText("tilt your device like it is lying on a table, so the red indication border has to be in the same angle the box is. ");
        } 
        else {
          movePlane.startPosition = new Vector3(0, 0, 0);
          movePlane.instructionsText.showText("Click to place a new joystick. Move your mouse/finger to use the joystick. ");
        }
      }
      if (random) {
        Debug.Log("endless runner selected!");
      if (number == 0) {
        number = currentLevel;
        if (borderObj.transform.position.y > 0.38) borderObj.transform.localPosition = new Vector3(borderObj.transform.localPosition.x, borderObj.transform.localPosition.y - 0.001f, borderObj.transform.localPosition.z);
      }
      if (number == -1) {
        Debug.Log("restart with level before");
        if (currentLevel > 0) {
          currentLevel--;
          sphereCount--;
          borderObj.transform.localPosition = new Vector3(borderObj.transform.localPosition.x, borderObj.transform.localPosition.y + 0.001f, borderObj.transform.localPosition.z);
        }
      }
      // reset level
      levelNrText.text = (currentLevel + 1 + ". level");
      foreach (GameObject obj in GameObject.FindGameObjectsWithTag("marble"))
      {
        Destroy(obj);
      }
        string[] marbleDistribution = new string [sphereCount];
        // manage marble type distribution (Verteilung)
        if (currentLevel < 4 && currentLevel > 0 && randomSettingsManager.GetComponent<endlessRunnerSettingsManager>().redMarbles > 0) marbleDistribution[0] = "enemy";
        if (Mathf.Floor(currentLevel / 4) <= randomSettingsManager.GetComponent<endlessRunnerSettingsManager>().grayMarbles) {
          for (int i = 0; i < Mathf.Floor(currentLevel / 4); i++)
          {
            marbleDistribution[i] = "blocker";
          }
        }
        if (Mathf.Floor((currentLevel - 4) / 2) + Mathf.Floor((currentLevel - 7) / 3) <= randomSettingsManager.GetComponent<endlessRunnerSettingsManager>().blueMarbles && currentLevel > 3) {
          int mediumSpeedCounter = 0;
          for (int i = 0; mediumSpeedCounter < Mathf.Floor((currentLevel - 3) / 2) && i < marbleDistribution.Length; i++)
          {
            if (marbleDistribution[i] == null) {
              marbleDistribution[i] = "mediumSpeed";
              mediumSpeedCounter++;
            }
          }
        }
        if (Mathf.Floor((currentLevel - 4) / 2) + Mathf.Floor((currentLevel - 7) / 3) <= randomSettingsManager.GetComponent<endlessRunnerSettingsManager>().blueMarbles && currentLevel > 7) {
          int mediumSpeedCounter = 0;
          for (int i = 0; mediumSpeedCounter < Mathf.Floor((currentLevel - 7) / 3) && i < marbleDistribution.Length; i++)
          {
            if (marbleDistribution[i] == null) {
              marbleDistribution[i] = "highSpeed";
              mediumSpeedCounter++;
            }
          }
        }
        if (randomSettingsManager.GetComponent<endlessRunnerSettingsManager>().orangeMarbles > 0) {
          if (currentLevel > 5 && currentLevel <= 9) marbleDistribution[sphereCount - 1] = "littleBounce";
          if (currentLevel > 9 && currentLevel <= 13) marbleDistribution[sphereCount - 2] = "mediumBounce";
          if (currentLevel > 13) marbleDistribution[sphereCount - 3] = "muchBounce";
        }
        for (int i = 0; i < Mathf.Floor((currentLevel - 6) / 4) && i < randomSettingsManager.GetComponent<endlessRunnerSettingsManager>().orangeMarbles - 1; i++)
          {
            if (marbleDistribution[i] == null) {
              marbleDistribution[i] = "littleBounce";
            }
          }
        for (int i = 0; i < marbleDistribution.Length; i++) {
          if (marbleDistribution[i] == null) marbleDistribution[i] = "normal";
        }
        objManager.spawnSphere(0, 0, marbleDistribution);
        if (!GameObject.Find("settingsManager").GetComponent<settingsManager>().resetBoxPosition) {
          foreach (GameObject marble in GameObject.FindGameObjectsWithTag("marble"))
          {
            StartCoroutine(marble.GetComponent<marbleParams>().setGravity(true));
          }
        }
      }
      else {
        Debug.Log(GameObject.FindGameObjectsWithTag("marble").Length + " marbles in this level!");
        sphereCount = GameObject.FindGameObjectsWithTag("marble").Length;
        levelNrText.text = (number + ". level");
      }
    }
}

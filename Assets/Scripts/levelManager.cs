using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UI;
public class levelManager : MonoBehaviour
{
    public int sphereCount = 1;
    public int inGoalCount = 0;
    private int currentLevel = 0;
    public scoreSystem scoreObj;
    public gameInstructions instructionsText;
    public objectManager objManager;
    public liveManager liveManager;
    public Text levelNrText;

    // Start is called before the first frame update
    public bool gameStarted = false;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void goalReached(GameObject marble) {
      // TODO: multiple blocked --> make them disappear or just leave the other ones on the field?
      if (marble.GetComponent<marbleParams>().type != "blocker" || (marble.GetComponent<marbleParams>().type == "blocker" && inGoalCount + 1 == sphereCount))
      {
        Destroy(marble, 0.25f);
        inGoalCount++;
        Debug.Log(inGoalCount + "/" + sphereCount + " marbles reached the goal!");
        if (inGoalCount == sphereCount) {
          sphereCount++;
          scoreObj.goalReached();
          currentLevel++;
          Debug.Log((currentLevel + 1) + ". Level reached!");
          instructionsText.levelDone(currentLevel);
          inGoalCount = 0;
          startLevel(currentLevel, "random");
        }
      }
      else StartCoroutine("marbleBlocked");
    }
    IEnumerator marbleBlocked()
    {
      Debug.Log("restart Level because of blocked marble");
      yield return new WaitForSeconds(2);
      liveManager.takeDamage();
    }
    private void OnTriggerEnter(Collider other)
    {
      goalReached(other.gameObject);
    }
    public void startLevel(int number, string type) 
    {
      if (number == -1) number = currentLevel;
      if (number == 0) {
        currentLevel = 0;
        sphereCount = 1;
      }
      levelNrText.text = (currentLevel + 1 + ". level");
      // reset level
      foreach (GameObject obj in GameObject.FindGameObjectsWithTag("marble"))
      {
        Destroy(obj);
      }
      inGoalCount = 0;
      if (type == "random") {
          // manage marble type distribution (Verteilung)
          string[] marbleDistribution = new string [sphereCount];
          if (currentLevel < 4 && currentLevel > 0) marbleDistribution[0] = "enemy";
          if (currentLevel < 20 && Mathf.Floor(currentLevel / 4) <= 4) {
            for (int i = 0; i < Mathf.Floor(currentLevel / 4); i++)
            {
              marbleDistribution[i] = "blocker";
            }
          }
          for (int i = 0; i < marbleDistribution.Length; i++) {
            if (marbleDistribution[i] == null) marbleDistribution[i] = "normal";
          }
          objManager.spawnSphere(0, 0, marbleDistribution);
      }
    }
}

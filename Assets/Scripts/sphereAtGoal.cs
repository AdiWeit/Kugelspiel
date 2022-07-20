using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class sphereAtGoal : MonoBehaviour
{
    public int sphereCount = 1;
    public int inGoalCount = 0;
    private int currentLevel = 0;
    public scoreSystem scoreObj;
    public manageGameInstructions instructionsText;
    public objectManager objManager;

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
      if (marble.GetComponent<customSphereParam>().type != "blocker" || (marble.GetComponent<customSphereParam>().type == "blocker" && inGoalCount + 1 == sphereCount))
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
          // manage marble type distribution (Verteilung)
          string[] marbleDistribution = new string [sphereCount];
          if (currentLevel < 4) marbleDistribution[0] = "enemy";
          // Debug.Log(currentLevel % 4);
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
      else
      {
        Debug.Log("restart Level because of blocked marble");
      }
    }
    private void OnTriggerEnter(Collider other)
    {
      goalReached(other.gameObject);
    }
}

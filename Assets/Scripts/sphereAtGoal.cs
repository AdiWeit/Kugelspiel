using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

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
    private void OnTriggerEnter(Collider other)
    {
      other.gameObject.GetComponent<customSphereParam>().reachedGoal = true;
      Debug.Log("sphere reached goal!");
      Destroy(other.gameObject, 0.25f);
      inGoalCount++;
      if (inGoalCount == sphereCount) {
        sphereCount++;
        scoreObj.goalReached();
        currentLevel++;
        instructionsText.levelDone(currentLevel);
        inGoalCount = 0;
        objManager.spawnSphere(sphereCount, null);
      }
    }
}

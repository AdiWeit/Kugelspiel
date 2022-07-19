using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlane : MonoBehaviour
{
    public sphereAtGoal goalManager;
    public manageGameInstructions instructionsText;
    Vector3 startPosition; 
    Vector3 rotationPosition;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetButtonDown("Fire1")) {
        startPosition = Input.mousePosition;
        Debug.Log("Set start position");
      }
      rotationPosition = Input.mousePosition - startPosition;
      if (!goalManager.gameStarted && !(startPosition.x == 0 && startPosition.y == 0) && (rotationPosition.x != 0 || rotationPosition.z != 0)) {
        goalManager.spawnSphere(1, null);
        goalManager.gameStarted = true;
        instructionsText.gameBegins();
      }
      if (goalManager.gameStarted) {
      transform.transform.eulerAngles = new Vector3(
        rotationPosition.y,
        0,
        -rotationPosition.x
);
      }
    }
}

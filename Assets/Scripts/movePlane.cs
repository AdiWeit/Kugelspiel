using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movePlane : MonoBehaviour
{
    public levelManager levelManager;
    public gameInstructions instructionsText;
    public objectManager objManager;
    // public float motionSpeed = 90*4.5f;
    private float motionSpeed = 80;
    // public float motionSpeed = 180f;
    Vector3 startPosition; 
    Vector3 rotationPosition;
    // Start is called before the first frame update
    void Start()
    {
      levelManager = GameObject.Find("goal_hitbox").GetComponent<levelManager>();
      instructionsText = GameObject.Find("gameInstructions").GetComponent<gameInstructions>();
      objManager = GameObject.Find("objectManager").GetComponent<objectManager>();
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetButtonDown("Fire1")) {
        startPosition = Input.mousePosition;
        Debug.Log("Set start position");
      }
      rotationPosition = Input.mousePosition - startPosition;
      // if (Input.acceleration.x != 0 || Input.acceleration.y != 0) {
      if (Input.acceleration.x != 0 || Input.acceleration.y != 0) {
        // rotationPosition += Input.acceleration * motionSpeed;
        // rotationPosition += new Vector3(Input.acceleration.x*motionSpeed, Input.acceleration.y*motionSpeed, 0); // Input.acceleration * motionSpeed;
        rotationPosition = new Vector3(Input.acceleration.x*motionSpeed, Input.acceleration.y*motionSpeed, 0); // Input.acceleration * motionSpeed;
        // instructionsText.showText(Input.acceleration.x + " - " + Input.acceleration.y + " *rotaitonSpeed:  " + (Input.acceleration.x * motionSpeed) + " - " + (Input.acceleration.y * motionSpeed) + " position: " + rotationPosition.x + " - " + rotationPosition.y);
        // instructionsText.showText(Input.acceleration.x + "*" + motionSpeed + " = " + Input.acceleration.x*motionSpeed + ", " + Input.acceleration.y + "*" + motionSpeed + " = " + Input.acceleration.y*motionSpeed);
        // instructionsText.showText(Input.acceleration.x + " - " + Input.acceleration.y + " *rotaitonSpeed:  " + (Input.acceleration * motionSpeed).x + " - " + (Input.acceleration * motionSpeed).y + " position: " + rotationPosition.x + " - " + rotationPosition.y);
      }
      if (!levelManager.gameStarted && !(startPosition.x == 0 && startPosition.y == 0) && (rotationPosition.x != 0 || rotationPosition.z != 0)) {
        if (!SceneManager.GetActiveScene().name.Contains("level")) objManager.spawnSphere(0, 0, "normal");
        else {
          foreach (GameObject marble in GameObject.FindGameObjectsWithTag("marble"))
          {
            marble.GetComponent<Rigidbody>().useGravity = true;
          }
        } 
        levelManager.gameStarted = true;
        instructionsText.levelDone(0);
      }
      if (levelManager.gameStarted || Input.acceleration.x != 0 || Input.acceleration.y != 0) {
      transform.transform.eulerAngles = new Vector3(
        rotationPosition.y,
        0,
        -rotationPosition.x
);
      }
    }
}

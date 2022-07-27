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
    private float motionSpeed = 80f;
    public float smoothness = 1.5f;
    Vector3 startPosition; 
    Vector3 rotationPosition;
    // Start is called before the first frame update
    void Start()
    {
      levelManager = GameObject.Find("goal_hitbox").GetComponent<levelManager>();
      instructionsText = GameObject.Find("gameInstructions").GetComponent<gameInstructions>();
      objManager = GameObject.Find("objectManager").GetComponent<objectManager>();
      instructionsText.levelDone(0);
      Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetButtonDown("Fire1")) {
        startPosition = Input.mousePosition;
        Debug.Log("Set start position");
        if (SystemInfo.deviceType == DeviceType.Handheld && !levelManager.gameStarted) beginGame();
      }
      if (SystemInfo.deviceType == DeviceType.Handheld && Input.gyro.enabled) {
        // instructionsText.showText(Input.gyro.gravity.ToString());
        rotationPosition = Input.gyro.gravity*motionSpeed;
      }
      else rotationPosition = Input.mousePosition - startPosition;
      if (!levelManager.gameStarted && !(startPosition.x == 0 && startPosition.y == 0) && (rotationPosition.x != 0 || rotationPosition.z != 0)) {
        beginGame();
      }
      if (levelManager.gameStarted/* && SystemInfo.deviceType != DeviceType.Handheld*/) {
      transform.transform.eulerAngles = new Vector3(
        rotationPosition.y,
        0,
        -rotationPosition.x
);
      // instructionsText.showText("new transformed angles: " + transform.transform.eulerAngles.ToString());
      }
    }
    private void beginGame()
    {
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
}

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
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetButtonDown("Fire1")) {
        startPosition = Input.mousePosition;
        Debug.Log("Set start position");
        if (SystemInfo.deviceType == DeviceType.Handheld && !levelManager.gameStarted) beginGame();
      }
      if (SystemInfo.deviceType == DeviceType.Handheld/* && gyr.enabled*/) {
        // // round float with x decimals: (float) System.Math.Round(val, x);
        float newX = rotationPosition.x;
        float newY = rotationPosition.y;
        if (Math.Abs(rotationPosition.x - Mathf.Round(Input.acceleration.x*motionSpeed)) > 2/*1*/) newX = Mathf.Round(Input.acceleration.x*(motionSpeed));
        if (Math.Abs(rotationPosition.y - Mathf.Round(Input.acceleration.y*motionSpeed)) > 2/*1*/) newY = Mathf.Round(Input.acceleration.y*(motionSpeed));
        instructionsText.showText(Input.acceleration.ToString());
        rotationPosition = Vector3.MoveTowards(rotationPosition, new Vector3(newX, newY, 0), smoothness);
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
      instructionsText.showText("new transformed angles: " + transform.transform.eulerAngles.ToString());
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

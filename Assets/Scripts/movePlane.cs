using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movePlane : MonoBehaviour
{
    public levelManager levelManager;
    public gameInstructions instructionsText;
    public objectManager objManager;
    private float motionSpeed = 80f;
    public float smoothness = 1.5f;
    public Vector3 startPosition; 
    public Vector3 rotationPosition;
    // Start is called before the first frame update
    void Start()
    {
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
      instructionsText = GameObject.Find("gameInstructions").GetComponent<gameInstructions>();
      objManager = GameObject.Find("objectManager").GetComponent<objectManager>();
      // instructionsText.levelDone(0);
      if (SystemInfo.supportsGyroscope) {
        Input.gyro.enabled = true;
        Destroy(GameObject.Find("joystickStick"));
      }
      else Input.gyro.enabled = false;
      if (!SceneManager.GetActiveScene().name.Contains("level")) objManager.spawnSphere(0, 0, "normal");
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetButtonDown("Fire1")) {
        startPosition = Input.mousePosition;
        Debug.Log("Set start position");
        if (SystemInfo.deviceType == DeviceType.Handheld && !levelManager.gameStarted) beginGame();
      }
      if (Input.gyro.enabled) {
        // instructionsText.showText(Input.gyro.gravity.ToString());
        rotationPosition = Input.gyro.gravity*motionSpeed;
      }
      else {
        rotationPosition = Input.mousePosition - startPosition;
        if (startPosition != new Vector3(0, 0, 0)) 
        {
          GameObject.Find("joystickStick").GetComponent<Transform>().localPosition = getMousePosition(startPosition);
          LineRenderer lr = gameObject.GetComponent<LineRenderer>();
          lr.SetPosition(0, /*new Vector3(startPosition.x, startPosition.y, 0.03f)*/getMousePosition(startPosition));
          lr.SetPosition(1, /*new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.03f)*/getMousePosition(Input.mousePosition));
        }
      }
      if (!levelManager.gameStarted && !(startPosition.x == 0 && startPosition.y == 0) && (rotationPosition.x != 0 || rotationPosition.z != 0)) {
        beginGame();
      }
      if (levelManager.gameStarted/* && SystemInfo.deviceType != DeviceType.Handheld*/) {
      transform.transform.eulerAngles = new Vector3(
        rotationPosition.y,
        0,
        -rotationPosition.x
);
      }
    }
    public Vector3 getMousePosition(Vector3 position)
    {
      position.z = 10;//+= Camera.main.nearClipPlane;
      Vector3 mousePosition = Camera.main.ScreenToWorldPoint(position);
      return mousePosition;
    }
    private void beginGame()
    {
        foreach (GameObject marble in GameObject.FindGameObjectsWithTag("marble"))
        {
          marble.GetComponent<Rigidbody>().useGravity = true;
        }
      levelManager.gameStarted = true;
      instructionsText.levelDone(0);
    }
}

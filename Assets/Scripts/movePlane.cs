using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class movePlane : MonoBehaviour
{
    public levelManager levelManager;
    public gameInstructions instructionsText;
    public objectManager objManager;
    public GameObject mouseBackCircle;
    private float motionSpeed = 80f;
    public float smoothness = 1.5f;
    public Vector3 startPosition; 
    public Vector3 rotationPosition;
    private Vector3 mousePositionBefore;
    public GameObject pauseMenu;
    public bool waitForMousePosition = false;
    // Start is called before the first frame update
    void Start()
    {
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
      instructionsText = GameObject.Find("gameInstructions").GetComponent<gameInstructions>();
      objManager = GameObject.Find("objectManager").GetComponent<objectManager>();
      pauseMenu = GameObject.Find("pauseMenuReference").GetComponent<pauseMenuReference>().pauseMenu;
      // instructionsText.levelDone(0);
      if (SystemInfo.supportsGyroscope) {
        Input.gyro.enabled = true;
        Destroy(GameObject.Find("joystickStick"));
      }
      else {
        Input.gyro.enabled = false;
        GameObject.Find("motionControl").SetActive(false);
      }
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetButtonDown("Fire1") && !pauseMenu.activeInHierarchy) {
        if (waitForMousePosition) {
          Time.timeScale = 1;
          waitForMousePosition = false;
          GameObject.Find("mouseBackCircle").transform.position = new Vector3(0, 0, -100);
        }
        else {
          startPosition = Input.mousePosition;
          Debug.Log("Set start position");
          if (SystemInfo.deviceType == DeviceType.Handheld && !levelManager.gameStarted) beginGame();
        }
      }
      if (Input.gyro.enabled) {
        // instructionsText.showText(Input.gyro.gravity.ToString());
        rotationPosition = Input.gyro.gravity*motionSpeed;
      }
      else {
        if (GameObject.FindGameObjectsWithTag("joystickStick") == null) Instantiate(GameObject.Find("joystickStick"), new Vector3(-0.3891516f, -10.36f, 1302357f), GameObject.Find("joystickStick").transform.rotation);
        rotationPosition = Input.mousePosition - startPosition;
        if (startPosition != new Vector3(0, 0, 0)) 
        {
          GameObject.Find("joystickStick").GetComponent<Transform>().localPosition = getMousePosition(startPosition);
          if (levelManager.gameStarted)
          {
            LineRenderer lr = gameObject.GetComponent<LineRenderer>();
            lr.SetPosition(0, getMousePosition(startPosition));
            lr.SetPosition(1, getMousePosition(Input.mousePosition));
            mousePositionBefore = Input.mousePosition;
          }
        }
          if (waitForMousePosition) {
            mouseBackCircle = GameObject.Find("mouseBackCircle");
            mouseBackCircle.transform.localPosition = getMousePosition(mousePositionBefore);
          }
      }
      Debug.Log(pauseMenu);
      if (!pauseMenu.activeInHierarchy && !waitForMousePosition && !levelManager.gameStarted && !(startPosition.x == 0 && startPosition.y == 0) && (rotationPosition.x != 0 || rotationPosition.z != 0)) {
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

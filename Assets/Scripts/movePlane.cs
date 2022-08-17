using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class movePlane : MonoBehaviour
{
    public levelManager levelManager;
    public gameInstructions instructionsText;
    public objectManager objManager;
    public GameObject mouseBackCircle;
    public loadLevel levelLoader;
    public float sensitivity = 1;
    public float sensitivityBefore = 1;
    public Vector3 startPosition; 
    public Vector3 rotationPosition;
    private Vector3 mousePositionBefore;
    public GameObject pauseMenu;
    public GameObject joystick;
    public GameObject referencePlane;
    public bool waitForMousePosition = false;
    // Start is called before the first frame update
    void Start()
    {
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
      instructionsText = GameObject.Find("gameInstructions").GetComponent<gameInstructions>();
      objManager = GameObject.Find("objectManager").GetComponent<objectManager>();
      pauseMenu = GameObject.Find("pauseMenuReference").GetComponent<pauseMenuReference>().pauseMenu;
      joystick = GameObject.Find("joystickReference").GetComponent<joystickReference>().joyStick;
      levelLoader = GameObject.Find("levelLoader").GetComponent<loadLevel>();
      // instructionsText.levelDone(0);
      if (!levelLoader.levelSelected) {
        Debug.Log("preset settings");
        if (SystemInfo.supportsGyroscope) {
          Input.gyro.enabled = true;
          joystick.SetActive(false);
        }
        else {
          Input.gyro.enabled = false;
          joystick.SetActive(true);
          GameObject.Find("motionControlCheckReference").GetComponent<motionControlCheckReference>().motionControlRef.SetActive(false);
        }
        levelLoader.levelSelected = true;
      }
    }

    // Update is called once per frame
    void Update()
    {
      if (SceneManager.GetActiveScene().name == "levelSelection") return;
      if (Input.GetButtonDown("Fire1") && !pauseMenu.activeInHierarchy) {
        if (waitForMousePosition) {
          Time.timeScale = 1;
          waitForMousePosition = false;
          GameObject.Find("mouseBackCircle").transform.position = new Vector3(0, 0, -100);
          if (referencePlane != null) Destroy(referencePlane);
        }
        else {
          startPosition = Input.mousePosition;
          Debug.Log("Set start position");
          if (SystemInfo.deviceType == DeviceType.Handheld && !levelManager.gameStarted) beginGame();
        }
      }
      if (Input.gyro.enabled) {
        // instructionsText.showText(Input.gyro.gravity.ToString());
        rotationPosition = Input.gyro.gravity*(sensitivity*90);
      }
      else {
        rotationPosition = (Input.mousePosition - startPosition)*sensitivity;
        if (startPosition != new Vector3(0, 0, 0)) 
        {
          joystick.GetComponent<Transform>().localPosition = getMousePosition(startPosition);
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
            mouseBackCircle.transform.localPosition = getMousePosition(startPosition + ((mousePositionBefore - startPosition)*sensitivityBefore)/sensitivity);
          }
      }
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
      if (referencePlane != null) {
        referencePlane.transform.eulerAngles = new Vector3(
        rotationPosition.y,
        0,
        -rotationPosition.x
       );
       float xDiv = Math.Abs(rotationPosition.y - gameObject.transform.eulerAngles.x);
       float yDiv = Math.Abs(-rotationPosition.x - gameObject.transform.eulerAngles.z);
       if (xDiv > 360) xDiv -= 360;
       if (yDiv > 360) yDiv -= 360;
       if (xDiv + yDiv < 7f) {
        Time.timeScale = 1;
        waitForMousePosition = false;
        GameObject.Find("mouseBackCircle").transform.position = new Vector3(0, 0, -100);
        if (referencePlane != null) Destroy(referencePlane);
       }
      }
    }
    public Vector3 getMousePosition(Vector3 position)
    {
      position.z = 5;//+= Camera.main.nearClipPlane;
      Vector3 mousePosition = Camera.main.ScreenToWorldPoint(position);
      return mousePosition;
    }
    public void sensitivityChanged(toggleSensibility slider) {
      sensitivity = slider.gameObject.GetComponent<Slider>().value;
      if (Input.gyro.enabled) PlayerPrefs.SetFloat("gyroSensitivity", sensitivity);
      else PlayerPrefs.SetFloat("joystickSensitivity", sensitivity);
      PlayerPrefs.Save();
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

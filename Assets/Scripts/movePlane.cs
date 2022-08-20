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
    public GameObject pauseMenu;
    public GameObject joystick;
    public GameObject referencePlane;
    public GameObject borderReference;
    public bool waitForMousePosition = false;
    public bool waitForClick = false;
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
    }

    // Update is called once per frame
    void Update()
    {
      if (SceneManager.GetActiveScene().name == "levelSelection") return;
      GameObject pauseB = GameObject.Find("pause_B");
      if (Input.GetButtonDown("Fire1") && !pauseMenu.activeInHierarchy && (Input.mousePosition.x < pauseB.transform.position.x - pauseB.GetComponent<RectTransform>().rect.width/2 || Input.mousePosition.y < pauseB.transform.position.y - pauseB.GetComponent<RectTransform>().rect.height/2)) {
        if (waitForMousePosition) {
          Time.timeScale = 1;
          waitForMousePosition = false;
          GameObject.Find("mouseBackCircle").transform.position = new Vector3(0, 0, -100);
          if (referencePlane != null) referencePlane.transform.position = new Vector3(-500, referencePlane.transform.position.y, referencePlane.transform.position.z); // Destroy(referencePlane);
        }
        else {
          if (!Input.gyro.enabled) startPosition = Input.mousePosition;
          Debug.Log("Set start position");
          if ((SystemInfo.deviceType == DeviceType.Handheld) && !levelManager.gameStarted) beginGame();
        }
      }
      if (Input.gyro.enabled && !pauseMenu.activeInHierarchy) {
        // instructionsText.showText(Input.gyro.gravity.ToString());
        rotationPosition = Input.gyro.gravity*(sensitivity*90);
      }
      else if (Input.mousePosition.x < pauseB.transform.position.x - pauseB.GetComponent<RectTransform>().rect.width/2 || Input.mousePosition.y < pauseB.transform.position.y - pauseB.GetComponent<RectTransform>().rect.height/2) {
        if (!pauseMenu.activeInHierarchy && !waitForMousePosition) rotationPosition = (Input.mousePosition - startPosition)*sensitivity;
        if (startPosition != new Vector3(0, 0, 0)) 
        {
          joystick.GetComponent<Transform>().localPosition = getMousePosition(startPosition);
          if (levelManager.gameStarted)
          {
            LineRenderer lr = gameObject.GetComponent<LineRenderer>();
            lr.SetPosition(0, getMousePosition(startPosition));
            lr.SetPosition(1, getMousePosition(Input.mousePosition));
          }
        }
      }
      if (!pauseMenu.activeInHierarchy && !waitForMousePosition && !levelManager.gameStarted && !(startPosition.x == 0 && startPosition.y == 0) && (rotationPosition.x != 0 || rotationPosition.z != 0)) {
        beginGame();
        waitForClick = false;
      }
      if (levelManager.gameStarted/* && SystemInfo.deviceType != DeviceType.Handheld*/) {
      transform.transform.eulerAngles = new Vector3(
        rotationPosition.y,
        0,
        -rotationPosition.x
      );
      }
      if (referencePlane != null && referencePlane.transform.position.x != -500) {
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
        if (referencePlane != null && referencePlane.transform.position.x != -500) {
          // Destroy(referencePlane);
          referencePlane.transform.position = new Vector3(-500, referencePlane.transform.position.y, referencePlane.transform.position.z);
          if (!levelManager.gameStarted) {
            beginGame();
          }
        }
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

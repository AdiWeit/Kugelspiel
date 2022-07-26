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
    // Gyroscope gyr;
    // Quaternion referenceRotation;
    private float motionSpeed = 80f;
    // private float speed = 2;
    public float smoothness = 1.5f;
    Vector3 startPosition; 
    Vector3 rotationPosition;
    // Start is called before the first frame update
    void Start()
    {
      // Input.simulateMouseWithTouches = true;
      levelManager = GameObject.Find("goal_hitbox").GetComponent<levelManager>();
      instructionsText = GameObject.Find("gameInstructions").GetComponent<gameInstructions>();
      objManager = GameObject.Find("objectManager").GetComponent<objectManager>();
      // gyr = Input.gyro;
      instructionsText.levelDone(0);
      // if (SystemInfo.supportsGyroscope)
      // {
      //     gyr.enabled = true;
      //     referenceRotation = Quaternion.Inverse( gyr.attitude );
      // }
    }

    // Update is called once per frame
    void Update()
    {
      // if( Input.GetMouseButtonDown( 0 ) ) {
      //     // re-orient to current position on touch to prove that it can always be relative to current screen orientation
      //     referenceRotation = Quaternion.Inverse( Input.gyro.attitude );
      // }
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
        // rotationPosition = new Vector3(newX, newY, 0);
        // Vector3 direction = new Vector3(Input.acceleration.y*motionSpeed, 0, -Input.acceleration.x*motionSpeed);
        // direction *= Time.deltaTime;
        // direction *= speed;
        // transform.transform.eulerAngles = direction;

        // rotationPosition = Vector3.Lerp(transform.position, new Vector3(newX, newY, 0), 0.5f);
        // rotationPosition = Vector3.MoveTowards(rotationPosition, new Vector3(newX, newY, 0), 2.5f);
        rotationPosition = Vector3.MoveTowards(rotationPosition, new Vector3(newX, newY, 0), smoothness);
        // // instructionsText.showText(Input.mousePosition + " - " + Input.mousePosition.y);
        // // instructionsText.levelDone(0);

        // instructionsText.showText(gyr.attitude.ToString());
        // instructionsText.showText(gyr.attitude.y + " - 0 " + gyr.attitude.x);
        // instructionsText.showText(gyr.rotationRate.ToString());
        // transform.rotation = gyr.attitude;
        // transform.transform.eulerAngles = new Vector3(gyr.attitude.y, 0, gyr.attitude.x);

        // transform.rotation = Quaternion.Euler(new Vector3(gyr.rotationRate.y, 0, gyr.rotationRate.x));

        // transform.rotation = Quaternion.Euler(new Vector3(gyr.attitude.eulerAngles.y, 0, -gyr.attitude.eulerAngles.x));
        // transform.rotation = Quaternion.Euler(new Vector3(gyr.attitude.eulerAngles.y, gyr.attitude.eulerAngles.x, - ));

        // up + down working
        // transform.rotation = Quaternion.Euler(new Vector3(gyr.attitude.eulerAngles.x, 0, 0));

        // left + right working
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, gyr.attitude.eulerAngles.y));
        
        // combined
        // transform.rotation = Quaternion.Euler(new Vector3(gyr.attitude.eulerAngles.x, 0, gyr.attitude.eulerAngles.y));
     
        // without orientation
        // transform.localRotation = Quaternion.Euler(new Vector3(gyr.attitude.eulerAngles.x, 0, gyr.attitude.eulerAngles.y));
     
        // transform.localRotation = Quaternion.AngleAxis(90, Vector3.right) * transform.localRotation;

        // Vector3 angles = transform.rotation.eulerAngles;// gyr.attitude.eulerAngles;
 
        // Quaternion undoY = Quaternion.Euler(0, -angles.y, 0);
        // transform.rotation = undoY * transform.rotation;

        // transform.rotation = Quaternion.Euler(new Vector3(gyr.attitude.eulerAngles.x, gyr.attitude.eulerAngles.y, gyr.attitude.eulerAngles.z));
        // transform.localRotation = gyr.attitude * new Quaternion (0, 0, 1, 0); // Quaternion.Euler(new Vector3(gyr.attitude.eulerAngles.x, gyr.attitude.eulerAngles.y, gyr.attitude.eulerAngles.z));

        // transform.rotation = gyr.attitude * new Vector3(0, 90, 0);

        // rotationPosition += gyr.userAcceleration*10;
        // transform.transform.eulerAngles = new Vector3(
        //   -rotationPosition.y,
        //   0,
        //   rotationPosition.x
        // );
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
    // void FixedUpdate()
    // {
    //   if (SystemInfo.deviceType == DeviceType.Handheld) {
    //     Debug.Log("Simulates Handheld");
    //     // round float with x decimals: (float) System.Math.Round(val, x);
    //     // float newX = rotationPosition.x;
    //     // float newY = rotationPosition.y;
    //     // /*if (Math.Abs(rotationPosition.x - Mathf.Round(Input.acceleration.x*motionSpeed)) > 1)*/ newX = /*Mathf.Round(*/Input.acceleration.x*motionSpeed/*)*/;
    //     // /*if (Math.Abs(rotationPosition.y - Mathf.Round(Input.acceleration.y*motionSpeed)) > 1)*/ newY = /*Mathf.Round(*/Input.acceleration.y*motionSpeed/*)*/;
    //     // rotationPosition = new Vector3(newX, newY, 0);
    //     // instructionsText.showText(rotationPosition.x + " - " + rotationPosition.y);
    //     // instructionsText.levelDone(0);
    //     // Vector3 tilt = Input.acceleration;
    //     // tilt = Quaternion.Euler(90, 0, 0) * tilt;
    //     // rotationPosition += tilt;
    //     rotationPosition = Input.gyro.attitude * new Vector3(1, 1, 1);
    //     transform.transform.eulerAngles = new Vector3(
    //       rotationPosition.y,
    //       0,
    //       -rotationPosition.x
    //     );
    //   }
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsManager : MonoBehaviour
{
  private movePlane movePlane;
  public GameObject gyroCheck;
  public GameObject sensitivitySlider;
  // Start is called before the first frame update
  void Start()
  {
    
  }
  public void getSettings() {
    movePlane = GameObject.Find("movingCube").GetComponent<movePlane>();
    if (Input.gyro.enabled && PlayerPrefs.HasKey("gyroSensitivity")) movePlane.sensitivity = PlayerPrefs.GetFloat("gyroSensitivity");
    if (!Input.gyro.enabled && PlayerPrefs.HasKey("joystickSensitivity")) movePlane.sensitivity = PlayerPrefs.GetFloat("joystickSensitivity");
    if (PlayerPrefs.HasKey("useGyro")) Input.gyro.enabled = PlayerPrefs.GetInt("useGyro") == 1 ? true : false;
    else Input.gyro.enabled = true;
    // change settings canvas
    GameObject motionControl = GameObject.Find("motionControlCheckReference").GetComponent<motionControlCheckReference>().motionControlRef;
    if (!SystemInfo.supportsGyroscope) {
      motionControl.SetActive(false);
      Input.gyro.enabled = false;
    }
    motionControl.GetComponent<Toggle>().isOn = Input.gyro.enabled;
    motionControl.GetComponent<toggleMotionControl>().toggleMotionControlF();
    sensitivitySlider.GetComponent<Slider>().value = movePlane.sensitivity;
  }
}

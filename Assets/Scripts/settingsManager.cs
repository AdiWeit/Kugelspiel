using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsManager : MonoBehaviour
{
  private movePlane movePlane;
  public GameObject gyroCheck;
  public GameObject sensitivitySlider;
  public GameObject resetBoxPositionCheck;
  public GameObject resetJoystick;
  public bool resetBoxPosition = true;
  // Start is called before the first frame update
  void Start()
  {
    
  }
  public void setResetBoxPosition(bool pChecked) {
    resetBoxPosition = pChecked;
    if (Input.gyro.enabled) PlayerPrefs.SetInt("resetBoxPositionGyro", pChecked ? 1 : 0);
    PlayerPrefs.Save();
  }
  public void setResetJoystick(bool pChecked) {
    movePlane = GameObject.Find("movingCube").GetComponent<movePlane>();
    movePlane.resetJoystick = pChecked;
    PlayerPrefs.SetInt("resetJoystick", pChecked ? 1 : 0);
    PlayerPrefs.Save();
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
    if (PlayerPrefs.HasKey("resetJoystick")) movePlane.resetJoystick = PlayerPrefs.GetInt("resetJoystick") == 1 ? true : false;
    resetJoystick.GetComponent<Toggle>().isOn = movePlane.resetJoystick;
    motionControl.GetComponent<toggleMotionControl>().toggleMotionControlF();
    if (Input.gyro.enabled && PlayerPrefs.HasKey("resetBoxPositionGyro")) resetBoxPosition = PlayerPrefs.GetInt("resetBoxPositionGyro") == 1 ? true : false;
    resetBoxPositionCheck.GetComponent<Toggle>().isOn = resetBoxPosition;
    sensitivitySlider.GetComponent<Slider>().value = movePlane.sensitivity;
  }
}

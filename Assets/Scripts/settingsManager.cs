using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class settingsManager : MonoBehaviour
{
  private movePlane movePlane;
  public GameObject gyroCheck;
  public GameObject levelManager;
  public GameObject sensitivitySlider;
  public GameObject resetBoxPositionCheck;
  public GameObject resetJoystickCheck;
  public GameObject skipLevelCheck;
  public GameObject continueLevelCheck;
  public GameObject muteB;
  public GameObject fallingMarbleSound;
  public bool resetBoxPosition = true;
  public bool continueLevel = true;
  public bool skipLevels = true;
  public bool isMuted = false;
  // Start is called before the first frame update
  void Start()
  {
    
  }
  public void setContinueLevel(bool pChecked) {
    continueLevel = pChecked;
    PlayerPrefs.SetInt("continueLevel", pChecked ? 1 : 0);
    settingChanged();
  }
  public void setResetBoxPosition(bool pChecked) {
    resetBoxPosition = pChecked;
    if (Input.gyro.enabled) PlayerPrefs.SetInt("resetBoxPositionGyro", pChecked ? 1 : 0);
    else PlayerPrefs.SetInt("resetBoxPositionJoystick", pChecked ? 1 : 0);
    settingChanged();
  }
  public void setResetJoystick(bool pChecked) {
    movePlane = GameObject.Find("movingCube").GetComponent<movePlane>();
    movePlane.resetJoystick = pChecked;
    PlayerPrefs.SetInt("resetJoystick", pChecked ? 1 : 0);
    settingChanged();
  }
  public void setSkipLevels(bool pChecked) {
    skipLevels = pChecked;
    PlayerPrefs.SetInt("skipLevels", pChecked ? 1 : 0);
    settingChanged();
  }
  public void setMuted(bool pMuted) {
    isMuted = pMuted;
    PlayerPrefs.SetInt("muted", pMuted ? 1 : 0);
    PlayerPrefs.Save();
    getSettings(false);
  }
  public void settingChanged() {
    PlayerPrefs.Save();
    resetJoystickCheck.SetActive(!Input.gyro.enabled);
    continueLevelCheck.SetActive(!levelManager.GetComponent<levelManager>().random);
    skipLevelCheck.SetActive(continueLevelCheck.GetComponent<Toggle>().isOn);
    if (levelManager.GetComponent<levelManager>().random) skipLevelCheck.SetActive(false);
  }
  public void getSettings(bool calledByMotionControlToggle) {
    movePlane = GameObject.Find("movingCube").GetComponent<movePlane>();
    // sensitivity
    if (Input.gyro.enabled && PlayerPrefs.HasKey("gyroSensitivity")) movePlane.sensitivity = PlayerPrefs.GetFloat("gyroSensitivity");
    if (!Input.gyro.enabled && PlayerPrefs.HasKey("joystickSensitivity")) movePlane.sensitivity = PlayerPrefs.GetFloat("joystickSensitivity");
    sensitivitySlider.GetComponent<Slider>().value = movePlane.sensitivity;
    // useGyro
    if (PlayerPrefs.HasKey("useGyro")) Input.gyro.enabled = PlayerPrefs.GetInt("useGyro") == 1 ? true : false;
    else Input.gyro.enabled = true;
    GameObject motionControl = GameObject.Find("motionControlCheckReference").GetComponent<motionControlCheckReference>().motionControlRef;
    if (!SystemInfo.supportsGyroscope) {
      motionControl.SetActive(false);
      Input.gyro.enabled = false;
    }
    motionControl.GetComponent<Toggle>().isOn = Input.gyro.enabled;
    if (!calledByMotionControlToggle) motionControl.GetComponent<toggleMotionControl>().toggleMotionControlF();
    // resetJoystick
    if (PlayerPrefs.HasKey("resetJoystick")) movePlane.resetJoystick = PlayerPrefs.GetInt("resetJoystick") == 1 ? true : false;
    resetJoystickCheck.GetComponent<Toggle>().isOn = movePlane.resetJoystick;
    // continueLevel
    continueLevelCheck.GetComponent<Toggle>().isOn = continueLevel;
    if (PlayerPrefs.HasKey("continueLevel")) continueLevel = PlayerPrefs.GetInt("continueLevel") == 1 ? true : false;
    // resetBoxPosition
    if (Input.gyro.enabled && PlayerPrefs.HasKey("resetBoxPositionGyro")) resetBoxPosition = PlayerPrefs.GetInt("resetBoxPositionGyro") == 1 ? true : false;
    if (!Input.gyro.enabled && PlayerPrefs.HasKey("resetBoxPositionJoystick")) resetBoxPosition = PlayerPrefs.GetInt("resetBoxPositionJoystick") == 1 ? true : false;
    resetBoxPositionCheck.GetComponent<Toggle>().isOn = resetBoxPosition;
    // skipLevels
    if (PlayerPrefs.HasKey("skipLevels")) skipLevels = PlayerPrefs.GetInt("skipLevels") == 1 ? true : false;
    skipLevelCheck.GetComponent<Toggle>().isOn = skipLevels;
    // get muted
    if (PlayerPrefs.HasKey("muted") && isMuted != (PlayerPrefs.GetInt("muted") == 1 ? true : false)) {
      muteB.GetComponent<toggleMute>().toggleMuteF();
    }
    if (isMuted) AudioListener.volume = 0;
    else AudioListener.volume = 1;
    settingChanged();
  }
}

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
    Debug.Log(PlayerPrefs.GetString("useGyro"));
    if (PlayerPrefs.HasKey("sensitivity")) movePlane.sensitivity = PlayerPrefs.GetFloat("sensitivity");
    if (PlayerPrefs.HasKey("useGyro")) Input.gyro.enabled = PlayerPrefs.GetInt("useGyro") == 1 ? true : false;
    // change settings canvas
    gyroCheck.GetComponent<Toggle>().isOn = Input.gyro.enabled;
    if (!SystemInfo.supportsGyroscope) GameObject.Find("motionControlCheckReference").GetComponent<motionControlCheckReference>().motionControlRef.SetActive(false);
    sensitivitySlider.GetComponent<Slider>().value = movePlane.sensitivity;
  }
}

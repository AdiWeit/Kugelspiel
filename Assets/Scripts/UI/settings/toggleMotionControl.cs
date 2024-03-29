using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleMotionControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sensitivityWarning;
    public GameObject sensitivityToggle;
    public GameObject settingsManager;
    public void toggleMotionControlF()
    {
      Input.gyro.enabled = GetComponent<Toggle>().isOn;
      sensitivityWarning.SetActive(Input.gyro.enabled); 
      GameObject.Find("joystickReference").GetComponent<joystickReference>().joyStick.SetActive(!Input.gyro.enabled);
      PlayerPrefs.SetInt("useGyro", Input.gyro.enabled ? 1 : 0);
      PlayerPrefs.Save();
      if (Input.gyro.enabled) {
        sensitivityToggle.GetComponent<Slider>().value = PlayerPrefs.GetFloat("gyroSensitivity");
      }
      else {
        sensitivityToggle.GetComponent<Slider>().value = PlayerPrefs.GetFloat("joystickSensitivity");
      }
      GameObject.Find("movingCube").GetComponent<LineRenderer>().enabled = !Input.gyro.enabled;
      settingsManager.GetComponent<settingsManager>().getSettings(true);
      settingsManager.GetComponent<settingsManager>().settingChanged();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleResetJoystick : MonoBehaviour
{
  public GameObject settingsManager;
  public void toggleResetJoystickSetting() {
    settingsManager.GetComponent<settingsManager>().setResetJoystick(this.GetComponent<Toggle>().isOn);
  }
}

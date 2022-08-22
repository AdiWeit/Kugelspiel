using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleContinueLevel : MonoBehaviour
{
  public GameObject settingsManager;
  public void toggleContinueLevelF() {
    settingsManager.GetComponent<settingsManager>().setContinueLevel(gameObject.GetComponent<Toggle>().isOn);
  }
}

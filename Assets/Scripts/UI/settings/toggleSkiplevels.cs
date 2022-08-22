using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleSkiplevels : MonoBehaviour
{
  public GameObject settingsManager;
  public void toggleSkiplevelsF() {
    settingsManager.GetComponent<settingsManager>().setSkipLevels(gameObject.GetComponent<Toggle>().isOn);
  }
}

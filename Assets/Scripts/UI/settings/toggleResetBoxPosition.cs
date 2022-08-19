using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleResetBoxPosition : MonoBehaviour
{
  public GameObject settingsManager;
  public void ResetBoxPositionChanged() {
    settingsManager.GetComponent<settingsManager>().setResetBoxPosition(gameObject.GetComponent<Toggle>().isOn);
  }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class endlessRunnerSettingsManager : MonoBehaviour
{
  public int redMarbles = -1;
  public int grayMarbles = -1;
  public int blueMarbles = -1;
  public int orangeMarbles = -1;

  public GameObject redMarblesObj;
  public GameObject grayMarblesObj;
  public GameObject blueMarblesObj;
  public GameObject orangeMarblesObj;

  public void getSettings() {
    redMarbles = int.Parse(redMarblesObj.GetComponent<TMP_InputField>().text);
    grayMarbles = int.Parse(grayMarblesObj.GetComponent<TMP_InputField>().text);
    orangeMarbles = int.Parse(orangeMarblesObj.GetComponent<TMP_InputField>().text);
    blueMarbles = int.Parse(blueMarblesObj.GetComponent<TMP_InputField>().text);
  }
}

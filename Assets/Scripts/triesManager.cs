using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class triesManager : MonoBehaviour
{
  private int tries = 1;
  public void increaceTries() {
    tries++;
    GameObject.Find("triesText").GetComponent<Text>().text = "tries: " + tries;
  }
  public void resetTries() {
    tries = 1;
    GameObject.Find("triesText").GetComponent<Text>().text = "tries: " + tries;
  }
}

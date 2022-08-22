using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beginEndlessRun : MonoBehaviour
{
  public void startEndlessRunner() {
    GameObject.Find("levelLoader").GetComponent<loadLevel>().beginEndlessRun();
  }
}

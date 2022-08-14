using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleSensibility : MonoBehaviour
{
  public void sensitivityChanged() {
    GameObject.Find("movingCube").GetComponent<movePlane>().sensitivityChanged(this);
  }
}

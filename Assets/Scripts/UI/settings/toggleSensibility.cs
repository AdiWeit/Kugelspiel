using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleSensibility : MonoBehaviour
{
  public GameObject sensText;
  public void sensitivityChanged() {
    GameObject.Find("movingCube").GetComponent<movePlane>().sensitivityChanged(this);
    sensText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = ((Mathf.Round((this.GetComponent<Slider>().value) * 100)) / 100.0).ToString();
  }
}

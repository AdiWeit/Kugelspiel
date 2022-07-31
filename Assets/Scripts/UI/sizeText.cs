using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sizeText : MonoBehaviour
{
  private float _oldWidth;
  private float _oldHeight;
  private float _fontSize;
  private float presetFontSize = 14;
  public float Ratio = 10;
  // private bool positionUpdated = false;
    // Start is called before the first frame update
    void Start()
    {
      Debug.Log(Screen.width + " - " + Screen.height);
    }

    // Update is called once per frame
 
 void Update() {
    if (_oldWidth != Screen.width || _oldHeight != Screen.height) {
        _oldWidth = Screen.width;
        _oldHeight = Screen.height;
        // _fontSize = presetFontSize / (Math.Min(_oldWidth, _oldHeight) / Ratio);
        _fontSize = presetFontSize * (_oldWidth / 956);
    }
}
void OnGUI() {
  // Debug.Log(_oldWidth);
  // Debug.Log(_fontSize);
    // GUI.skin.textField.fontSize = (int) _fontSize;
    // Debug.Log(GUI.skin.textField.fontSize);
    // your GUI.TextField here
    // GUI.skin.textField.fontSize = 50;
    gameObject.GetComponent<Text>().fontSize = (int) _fontSize;
    // if (!positionUpdated) {
    //   gameObject.GetComponent<RectTransform>().position += new Vector3 ((int) _fontSize + Ratio, 0, 0);
    //   positionUpdated = true;
    // }
}
}

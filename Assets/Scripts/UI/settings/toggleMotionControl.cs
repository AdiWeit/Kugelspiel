using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleMotionControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
  //     gameObject.onValueChanged.AddListener((value) =>
  //   {
  //       MyListener(value);
  //  });//Do this in Start() for example
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void toggleMotionControlF()
    {
      Input.gyro.enabled = GetComponent<Toggle>().isOn;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customSphereParam : MonoBehaviour
{
    public bool reachedGround = false;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine("setReachedGround");
    }
    // Update is called once per frame
    void Update()
    {
      
    }
    IEnumerator setReachedGround()
    {
      yield return new WaitForSeconds(5);
      reachedGround = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class checkGlitchThrough : MonoBehaviour
{
    public sphereAtGoal goalManager;
    // public bool glitchFixed = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
      if (!other.gameObject.GetComponent<customSphereParam>().reachedGoal) {
        // Debug.Break();
        Debug.Log("sphere glitched!");
        goalManager.spawnSphere(1, other.gameObject);
        Destroy(other.gameObject);
      }

      // glitchFixed = false;
      // other.gameObject.GetComponent<Rigidbody>().useGravity = false;
      // StartCoroutine("fixGlitch", other.gameObject);
    }

    // IEnumerator fixGlitch(GameObject other)
    // {
    //   other.transform.Translate(0, 5f, 0);
    //   Debug.Log("push up sphere");
    //   yield return new WaitForSeconds(0.15f);
    //   if (!glitchFixed) StartCoroutine("fixGlitch", other.gameObject);
    // }
}

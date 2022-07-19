// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class checkGlichFixFinished : MonoBehaviour
// {
//     public checkGlitchThrough glichCheckObj;
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
//     private void OnTriggerEnter(Collider other)
//     {
//       if (other.gameObject.GetComponent<customSphereParam>().reachedGround) {
//         glichCheckObj.glitchFixed = true;
//         other.gameObject.GetComponent<customSphereParam>().reachedGround = false;
//         Debug.Log("glitch fixed!");
//         glichCheckObj.gameObject.GetComponent<Rigidbody>().useGravity = true;
//         Debug.Break();
//       }
//     }
// }

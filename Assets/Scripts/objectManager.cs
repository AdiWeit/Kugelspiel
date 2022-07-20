using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectManager : MonoBehaviour
{
    public GameObject sphereReference;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawnSphere(int counter, GameObject referenceGameObj)
    {
      Quaternion rotation = new Quaternion(0, 0, 0, 0);
      for (int i = 0; i < counter; i++)
      {
        Vector3 position = new Vector3(Random.Range(0, 2.91f), 8.88f, Random.Range(-2.75f, 2.75f));
        if (referenceGameObj != null) {
          position = referenceGameObj.transform.position;
          position.y = referenceGameObj.transform.position.y + 4f;// + 0.645f;
          rotation = referenceGameObj.transform.rotation;
        }
        GameObject newSphere = Instantiate(sphereReference, position, rotation);
      }
    }
}

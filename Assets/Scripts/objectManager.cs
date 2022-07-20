using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectManager : MonoBehaviour
{
    public GameObject sphereReference;
    public movePlane movingCube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawnSphere(int counter, float x, float z)
    {
      Quaternion rotation = new Quaternion(0, 0, 0, 0);
      for (int i = 0; i < counter; i++)
      {
        Vector3 position = new Vector3(Random.Range(-4.84f + movingCube.transform.rotation.x, 3.8f + movingCube.transform.rotation.x), 3.88f, Random.Range(-4.46f + movingCube.transform.rotation.x, 4.36f + movingCube.transform.rotation.y));
        if (x != 0) position = new Vector3(x, 3.88f, z);
        GameObject newSphere = Instantiate(sphereReference, position, rotation);
      }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectManager : MonoBehaviour
{
    public Material enemyMaterial;
    public Material blockerMaterial;
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
    
    public void spawnSphere(float x, float z, params string[] type)
    {
      Quaternion rotation = new Quaternion(0, 0, 0, 0);
      for (int i = 0; i < type.Length; i++)
      {
        Vector3 position = new Vector3(Random.Range(-4.84f + movingCube.transform.rotation.x, 3.8f + movingCube.transform.rotation.x), 3.88f, Random.Range(-4.46f + movingCube.transform.rotation.x, 4.36f + movingCube.transform.rotation.y));
        if (x != 0) position = new Vector3(x, 3.88f, z);
        GameObject newSphere = Instantiate(sphereReference, position, rotation);
        newSphere.GetComponent<marbleParams>().type = type[i];
        if (type[i] == "enemy") newSphere.GetComponent<MeshRenderer>().material = enemyMaterial;
        if (type[i] == "blocker") {
          newSphere.GetComponent<MeshRenderer>().material = blockerMaterial;
          newSphere.GetComponent<MeshRenderer>().transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
      }
    }
}

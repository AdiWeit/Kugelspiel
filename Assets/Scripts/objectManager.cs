using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectManager : MonoBehaviour
{
    public Material enemyMaterial;
    public Material blockerMaterial;
    public Material mediumSpeedMaterial;
    public Material heightSpeedMaterial;
    public Material littleBounceMaterial;
    public Material mediumBounceMaterial;
    public Material muchBounceMaterial;
    public PhysicMaterial bouncePhysicMaterial;
    public GameObject sphereReference;
    public movePlane movingCube;
    public liveManager liveManager;
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
        if (type[i] == "littleBounce") position.y = 2.3f + movingCube.transform.rotation.y;
        if (type[i] == "mediumBounce") position.y = 2.9f + movingCube.transform.rotation.y;
        if (type[i] == "muchBounce") position.y = 3.5f + movingCube.transform.rotation.y;
        // if (type[i].Contains("Bounce")) Debug.Break();
        GameObject newSphere = Instantiate(sphereReference, position, rotation);
        newSphere.GetComponent<marbleKilledCheck>().liveManager = liveManager;
        newSphere.GetComponent<marbleFellCheck>().liveManager = liveManager;
        newSphere.GetComponent<marbleParams>().type = type[i];
        if (type[i] == "enemy") newSphere.GetComponent<MeshRenderer>().material = enemyMaterial;
        if (type[i] == "blocker") {
          newSphere.GetComponent<MeshRenderer>().material = blockerMaterial;
          newSphere.GetComponent<MeshRenderer>().transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
        if (type[i] == "mediumSpeed") {
          newSphere.GetComponent<MeshRenderer>().material = mediumSpeedMaterial;
          newSphere.GetComponent<Rigidbody>().maxAngularVelocity = 33;
        }
        if (type[i] == "heighSpeed") {
          newSphere.GetComponent<MeshRenderer>().material = heightSpeedMaterial;
          newSphere.GetComponent<Rigidbody>().maxAngularVelocity = 55;
        }
        if (type[i].Contains("Bounce")) newSphere.GetComponent<SphereCollider>().material = bouncePhysicMaterial;
        if (type[i] == "littleBounce") {
          newSphere.GetComponent<MeshRenderer>().material = littleBounceMaterial;
        }
        if (type[i] == "mediumBounce") {
          newSphere.GetComponent<MeshRenderer>().material = mediumBounceMaterial;
        }
        if (type[i] == "muchBounce") {
          newSphere.GetComponent<MeshRenderer>().material = muchBounceMaterial;
        }
      }
    }
}

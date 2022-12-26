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
    public Material white;
    public PhysicMaterial bouncePhysicMaterial;
    public GameObject sphereReference;
    public movePlane movingCube;
    public liveManager liveManager;
    public levelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public GameObject spawnSphere(float x, float z, params string[] type)
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
        // if (type[i].Contains("Bounce")) newSphere.GetComponent<Rigidbody>().drag = 0;
        newSphere.transform.SetParent(GameObject.Find("spawningSpheres").transform);
        // newSphere.GetComponent<marbleKilledCheck>().liveManager = liveManager;
        // newSphere.GetComponent<marbleFellCheck>().liveManager = liveManager;
        transformMarbleToType(newSphere, type[i]);
        if (type.Length == 1) return newSphere;
      }
      return new GameObject();
    }
    public void transformMarbleToType(GameObject marble, string type)
    {
      marble.GetComponent<marbleParams>().type = type;
      marble.GetComponent<SphereCollider>().material = null;
      marble.GetComponent<MeshRenderer>().transform.localScale = new Vector3(1f, 1f, 1f);
      if (type == "enemy") marble.GetComponent<MeshRenderer>().material = enemyMaterial;
      if (type == "blocker") {
        marble.GetComponent<MeshRenderer>().material = blockerMaterial;
        marble.GetComponent<MeshRenderer>().transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
      }
      if (type.Contains("Speed")) marble.gameObject.GetComponent<Rigidbody>().mass = 0.5f;
      if (type == "mediumSpeed") {
        marble.GetComponent<MeshRenderer>().material = mediumSpeedMaterial;
        marble.GetComponent<marbleParams>().speed = 10;
        // marble.GetComponent<Rigidbody>().drag = 1;
      }
      if (type == "highSpeed") {
        marble.GetComponent<MeshRenderer>().material = heightSpeedMaterial;
        // marble.GetComponent<Rigidbody>().drag = 0;
        marble.GetComponent<marbleParams>().speed = 25;
      }
      if (type.Contains("Bounce")) {
        marble.GetComponent<SphereCollider>().material = bouncePhysicMaterial;
      }
      if (type == "littleBounce") {
        marble.GetComponent<MeshRenderer>().material = littleBounceMaterial;
      }
      if (type == "mediumBounce") {
        marble.GetComponent<MeshRenderer>().material = mediumBounceMaterial;
      }
      if (type == "muchBounce") {
        marble.GetComponent<MeshRenderer>().material = muchBounceMaterial;
      }
      if (type == "normal") {
        marble.GetComponent<MeshRenderer>().material = white;
      }
    }
}

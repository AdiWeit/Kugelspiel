using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spray : MonoBehaviour
{
  public string type;
  public Material red;
  public Material blue;
  public Material orange;
  public Material white;
  public Material black;
  public GameObject body;
    // Start is called before the first frame update
    void Start()
    {
      if (type == "enemy") {
        body.gameObject.GetComponent<MeshRenderer>().material = red;
      }
      if (type.Contains("Speed")) {
        body.gameObject.GetComponent<MeshRenderer>().material = blue;
      }
      if (type.Contains("Bounce")) {
        body.gameObject.GetComponent<MeshRenderer>().material = orange;
      }
      if (type == "normal") {
        body.gameObject.GetComponent<MeshRenderer>().material = white;
      }
      if (type == "blocker") {
        body.gameObject.GetComponent<MeshRenderer>().material = black;
      }
    }

    // Update is called once per frame
    void Update()
    {
      this.gameObject.GetComponent<Transform>().transform.Rotate(new Vector3(35*Time.deltaTime, 0, 0));
    }
    private void OnCollisionEnter(Collision other)
    {
      Debug.Log("change marble style to " + type);
      GameObject.Find("objectManager").GetComponent<objectManager>().transformMarbleToType(other.gameObject, type);
      if (type.Contains("Bounce")) {
        other.gameObject.transform.position += new Vector3(0, 2, 0);
      }
      Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spray : MonoBehaviour
{
  public string[] types;
  public string type;
  public float interval = 3;
  private float typeChangeTimer;
  public bool random = false;
  private int type_index = 0;
  public Material red;
  public Material blue;
  public Material orange;
  public Material white;
  public Material black;
  public Material random_material;
  public GameObject body;
    // Start is called before the first frame update
    void Start()
    {
      typeChangeTimer = interval;
      displaySprayType();
    }

    // Update is called once per frame
    private void displaySprayType() {
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
      if (random) {
        body.gameObject.GetComponent<MeshRenderer>().material = random_material;
      }
    }
    void Update()
    {
      this.gameObject.GetComponent<Transform>().transform.Rotate(new Vector3(35*Time.deltaTime, 0, 0));
      if (types.Length > 1 && !random) {
        typeChangeTimer -= Time.deltaTime;
        if (typeChangeTimer <= 0) {
          typeChangeTimer = interval;
          if (type_index + 1 < types.Length) {
            type_index++;
          }
          else {
            type_index = 0;
          }
          type = types[type_index];
          displaySprayType();
        }
      }
    }
    private void OnCollisionEnter(Collision other)
    {
      Debug.Log("change marble style to " + type);
      if (random) {
        type = types[UnityEngine.Random.Range(0, types.Length)];
      }
      GameObject.Find("objectManager").GetComponent<objectManager>().transformMarbleToType(other.gameObject, type);
      if (type.Contains("Bounce")) {
        other.gameObject.transform.position += new Vector3(0, 2, 0);
      }
      Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class marbleParams : MonoBehaviour
{
    public string type = "normal";
    public int speed = 5;
    private Vector3 positionBefore;
    private int repositionMarbleCount;
    private levelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
      if (SceneManager.GetActiveScene().name.Contains("level")) {
        Debug.Log("set marble type");
        GameObject.Find("objectManager").GetComponent<objectManager>().transformMarbleToType(gameObject, false, type);
        if (speed != 33 && speed != 55) gameObject.GetComponent<Rigidbody>().maxAngularVelocity = speed;
      }
      gameObject.GetComponent<Rigidbody>().useGravity = false;
      repositionMarbleCount = 0;
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
    }
    // Update is called once per frame
    void Update()
    {
      positionBefore = gameObject.transform.position;
    }
    public IEnumerator setGravity(bool pUseGravity)
    {
      yield return new WaitForSeconds(0.5f);
      if (gameObject) {
        gameObject.GetComponent<SphereCollider>().isTrigger = false;
        gameObject.GetComponent<Rigidbody>().useGravity = pUseGravity;
      }
    }
    private void OnTriggerEnter(Collider other) {
      if (other.gameObject.tag == "marble" && !gameObject.GetComponent<Rigidbody>().useGravity && GameObject.Find("levelManager").GetComponent<levelManager>().random) {
        if (repositionMarbleCount < 4) {
          gameObject.transform.position = new Vector3(Random.Range(-4.84f, 3.8f), 3.88f, Random.Range(-4.46f, 4.36f));
          repositionMarbleCount++;
        }
        else {
          levelManager.spawnHeight += 1.9f;
          gameObject.transform.position = new Vector3(0, levelManager.spawnHeight, 0);
        }
      }
    }
}

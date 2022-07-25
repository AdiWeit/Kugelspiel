using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class marbleParams : MonoBehaviour
{
    public bool reachedGround = false;
    public string type = "normal";
    public int speed = 5;
    private Vector3 positionBefore;

    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine("setReachedGround");
      gameObject.GetComponent<Rigidbody>().maxAngularVelocity = speed;
      if (SceneManager.GetActiveScene().name.Contains("level")) {
        GameObject.Find("objectManager").GetComponent<objectManager>().transformMarbleToType(gameObject, false, type);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
      }
    }
    // Update is called once per frame
    void Update()
    {
      positionBefore = gameObject.transform.position;
    }
    IEnumerator setReachedGround()
    {
      yield return new WaitForSeconds(5);
      reachedGround = true;
    }
}

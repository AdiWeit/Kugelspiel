using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class marbleParams : MonoBehaviour
{
    public string type = "normal";
    public int speed = 5;
    private Vector3 positionBefore;

    // Start is called before the first frame update
    void Start()
    {
      gameObject.GetComponent<Rigidbody>().maxAngularVelocity = speed;
      if (SceneManager.GetActiveScene().name.Contains("level")) {
        GameObject.Find("objectManager").GetComponent<objectManager>().transformMarbleToType(gameObject, false, type);
      }
      gameObject.GetComponent<Rigidbody>().useGravity = false;
    }
    // Update is called once per frame
    void Update()
    {
      positionBefore = gameObject.transform.position;
    }
    public IEnumerator setGravity(bool pUseGravity)
    {
      yield return new WaitForSeconds(0.5f);
      if (gameObject) gameObject.GetComponent<Rigidbody>().useGravity = pUseGravity;
    }
}

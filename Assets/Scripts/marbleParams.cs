using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marbleParams : MonoBehaviour
{
    public bool reachedGround = false;
    public string type;
    public float speed;
    private Vector3 positionBefore;

    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine("setReachedGround");
    }
    // Update is called once per frame
    void Update()
    {
      speed = Vector3.Distance(positionBefore, gameObject.transform.position);
      positionBefore = gameObject.transform.position;
    }
    IEnumerator setReachedGround()
    {
      yield return new WaitForSeconds(5);
      reachedGround = true;
    }
}

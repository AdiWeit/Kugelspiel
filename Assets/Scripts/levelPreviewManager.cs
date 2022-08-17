using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelPreviewManager : MonoBehaviour
{
    private GameObject movingCube;
    public bool waitForMarblePosition;
    // Update is called once per frame
    void Update()
    {
      movingCube = GameObject.Find("movingCube");
      if (movingCube != null && SceneManager.GetActiveScene().name == "levelSelection" && !waitForMarblePosition) {
        movingCube.transform.localPosition = new UnityEngine.Vector3(0, 0, 0);
      }
    }
}

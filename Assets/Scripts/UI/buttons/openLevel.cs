using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class openLevel : MonoBehaviour
{
    public loadLevel levelLoader;
    private levelManager levelManager;
    private GameObject movingCube;
    private levelPreviewManager levelPreviewManager;
    // Start is called before the first frame update
    void Start()
    {
      gameObject.GetComponent<Button>().onClick.AddListener(clicked);
      levelLoader = GameObject.Find("levelLoader").GetComponent<loadLevel>();
      levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void clicked()
    {
      // levelLoader.LoadLevel(int.Parse(gameObject.name.Split(' ')[1]), false, false);
      if (levelManager.currentLevel != 0) GameObject.Find("level " + levelManager.currentLevel).transform.GetChild(0).GetComponent<Text>().text = "level " + levelManager.currentLevel;
      if (int.Parse(gameObject.name.Split(' ')[1]) == levelManager.currentLevel) {
        levelLoader.LoadLevel(int.Parse(gameObject.name.Split(' ')[1]), false, false);
        return;
      }
      gameObject.transform.GetChild(0).GetComponent<Text>().text = "start!";
      levelPreviewManager = GameObject.Find("levelPreviewManager").GetComponent<levelPreviewManager>();
      levelPreviewManager.waitForMarblePosition = true;
      Debug.Log("level manager level: " + levelManager.currentLevel);
      if (levelManager.currentLevel != -1) {
        Debug.Log("trying to destroy level " + levelManager.currentLevel + "...");
        Destroy(GameObject.Find("level_" + levelManager.currentLevel + "(Clone)"));
        movingCube = null;
      }
      levelManager.currentLevel = int.Parse(gameObject.name.Split(' ')[1]);
      GameObject.Find("levelSpawner").GetComponent<levelPreviewSpawner>().spawnlevel(levelLoader, levelManager.currentLevel);
      StartCoroutine(setPlanePosition());
    }
    IEnumerator setPlanePosition() {
      yield return new WaitForSeconds(0.0001f);
      movingCube = GameObject.Find("movingCube");
      foreach (GameObject marble in GameObject.FindGameObjectsWithTag("marble"))
      {
        Debug.Log(movingCube.transform.localPosition);
        marble.transform.localPosition -= movingCube.transform.localPosition;
      }
      levelPreviewManager.waitForMarblePosition = false;
    }
}

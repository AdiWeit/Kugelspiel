using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class marbleKilledCheck : MonoBehaviour
{
    public liveManager liveManager;
    public marbleParams marbleParams;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
      liveManager = GameObject.Find("liveManager").GetComponent<liveManager>();
      marbleParams = gameObject.GetComponent<marbleParams>();
      audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.tag == "marble") audioSource.clip = GameObject.Find("soundsReference").GetComponent<soundsReference>().marbleDied[Random.Range(0, GameObject.Find("soundsReference").GetComponent<soundsReference>().marbleDied.Length - 1)];
      audioSource.pitch = 1;
      audioSource.volume = 1;
      if (other.gameObject.tag == "marble") {
        if (other.gameObject.GetComponent<marbleParams>().type == "enemy" && (gameObject.GetComponent<marbleParams>().type != "enemy" || getMarbleIndex(gameObject) < getMarbleIndex(other.gameObject)))
        {
          Debug.Log("Restart level because enemy touched marble!");
          audioSource.clip = GameObject.Find("soundsReference").GetComponent<soundsReference>().marbleDied[Random.Range(0, GameObject.Find("soundsReference").GetComponent<soundsReference>().marbleDied.Length - 1)];
          // liveManager.takeDamage(false);
          StartCoroutine("reloadLevel");
        }
        else audioSource.volume = 0.25f;
      }
      if (other.gameObject.name.Contains("border")) {
        audioSource.clip = GameObject.Find("soundsReference").GetComponent<soundsReference>().border[Random.Range(0, GameObject.Find("soundsReference").GetComponent<soundsReference>().border.Length - 1)];
        audioSource.volume = Mathf.Clamp(gameObject.GetComponent<Rigidbody>().velocity.magnitude - 0.7f, 0.1f, 1);
      }
      if (gameObject.tag == "marble" && gameObject.GetComponent<marbleParams>().type.Contains("Bounce")) {
        audioSource.clip = GameObject.Find("soundsReference").GetComponent<soundsReference>().marbleBounce;
        audioSource.volume = Mathf.Clamp(gameObject.GetComponent<Rigidbody>().velocity.magnitude - 5f, 0.1f, 0.2f);
        audioSource.Play();
      }
      else if (other.gameObject.tag == "marble" || other.gameObject.name.Contains("border")) {
        audioSource.Play();
      }
    }
    public int getMarbleIndex(GameObject pGameObj) {
      for (int i = 0; i < GameObject.FindGameObjectsWithTag("marble").Length; i++)
      {
        GameObject marble = GameObject.FindGameObjectsWithTag("marble")[i];
        if (marble == pGameObj) return i;
      }
      Debug.Log("getMarbleIndex failed!");
      return -1;
    }
    public IEnumerator reloadLevel() {
      yield return new WaitForSeconds(0.5f);
      liveManager.takeDamage(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelTwo : MonoBehaviour
{

    public GameObject player;
    public GameObject exitpoint; 


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, exitpoint.transform.position) < 3f)
        {
            SceneManager.LoadScene("LevelThree");
            Debug.Log("EnterNextScene");
        }
    }






}

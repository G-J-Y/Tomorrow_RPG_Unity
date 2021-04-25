using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransferToNextScen : MonoBehaviour
{


    public GameObject player;
    public GameObject gemone;
    public GameObject gemtwo;
    public GameObject gemthree;
    public int gemcount;
    public bool enternextlevel;
    public GameObject exitpoint;

    
    // Update is called once per frame
    void Update()
    {
        if (gemone != null && Vector3.Distance(player.transform.position, gemone.transform.position) < 6f) 
        { 
            gemcount++; 
            Destroy(gemone);
        }

        if (gemtwo != null && Vector3.Distance(player.transform.position, gemtwo.transform.position) < 6f)
        {
            gemcount++;
            Destroy(gemtwo);
        }

        if (gemthree != null &&Vector3.Distance(player.transform.position, gemthree.transform.position) < 6f)
        {
            gemcount++;
            Destroy(gemthree);
        }



        if (gemcount >= 3)
        {
            enternextlevel = true;
            Debug.Log("ReadyToLeave");
        }


        if (enternextlevel==true && Vector3.Distance(player.transform.position, exitpoint.transform.position) < 5f)
        {
            SceneManager.LoadScene("LevelTwo");
            Debug.Log("EnterNextScene");
        }


    }
}

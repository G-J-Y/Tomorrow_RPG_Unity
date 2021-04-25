using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    [SerializeField] GameObject HelpPicture;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HelpPicture != null)
        {
            if (HelpPicture.active == true)
            {
                if (Input.anyKeyDown)
                {
                    HelpPicture.active = false;
                }
            }
        }
    }

    public void LoadSceneLevel1()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void HelpButton()
    {
        if (HelpPicture!=null)
        {
            HelpPicture.active = true;
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}

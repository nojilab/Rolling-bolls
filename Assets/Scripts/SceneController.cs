using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OnClickGameStartButton(string level)
    {
        switch(level){
            case "Game Start":
                SceneManager.LoadScene("MainScene");
                break;
            case "Rule":
                SceneManager.LoadScene("RuleScene");
                break;
            case "title":
                SceneManager.LoadScene("TitleScene");
                break;
            default:
                SceneManager.LoadScene("MainScene");
                break;
        }
    }

    public void OnClickToTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

// hello
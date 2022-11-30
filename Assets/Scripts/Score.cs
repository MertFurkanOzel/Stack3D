using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public Button bt1;
    public Button bt2;
   
    void Start()
    {
        int highscore;
        int score = PlayerPrefs.GetInt("Score");
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highscore = PlayerPrefs.GetInt("HighScore");
            if (score > highscore)
            {
                text2.text = "New High Score: " + score;
                text1.text = "Previous High Score: " + highscore;
                highscore = score;
                PlayerPrefs.SetInt("HighScore", highscore);
            }
            else
            {
                text1.text = "High Score: " + highscore;
                text2.text = "Score: " + score;
            }  
            
        }
       else
        {
            text2.text = "Score: " + PlayerPrefs.GetInt("Score");
            PlayerPrefs.SetInt("HighScore",score);
        }
    }
    public void buton1()
    {
        SceneManager.LoadScene(0);
    }
    public void buton2()
    {
        Application.Quit();
    }

}

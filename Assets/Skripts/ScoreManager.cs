using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{


    private int[] needKillsArray = new int[] { 8, 7 , 5 };




    public static ScoreManager instance;
    public TextMeshProUGUI text;
    public TextMeshProUGUI textKill;

    public int score;

    public int hereNeedKills;
    private Teleport teleport;
    private PausedMenu pausedMenu;
    private bool endGame=false;


    // Start is called before the first frame update
    void Start()
    {
        hereNeedKills = needKillsArray[0];
        pausedMenu = FindObjectOfType<PausedMenu>();
        teleport = FindObjectOfType<Teleport>();

        if (instance == null)
        {
            instance = this;
        }
        ChangeScoreKill();
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = "X" + score.ToString();
    }

    public void SwapKills(int i)
    {
        if (i == 2) endGame = true;

        hereNeedKills = needKillsArray[i];
    }

    public void ChangeScoreKill()
    {
        print(teleport.kills);
        if (teleport.kills >= hereNeedKills)
        {
            textKill.text = "The level is passed " + hereNeedKills.ToString() + "/"+ hereNeedKills.ToString();
        }
        else textKill.text = "Need to kill " + teleport.kills.ToString() + "/"+ hereNeedKills.ToString();

        if (endGame == true && teleport.kills >= hereNeedKills)
        { pausedMenu.endGame.SetActive(true);Time.timeScale = 0f; }
    }

}

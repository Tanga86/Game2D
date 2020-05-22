using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //public GameObject portal;
    // public float waitForSeconds=0.5f;
    // public int needScore;
    //ScoreManager scoreManager;

    public GameObject endLvl;
    public int kills;
    GameObject player;
    ScoreManager scoreManager;
    Teleport teleport;

    private int numberLvl=0;

    void Start()
    {
        teleport=FindObjectOfType<Teleport>();
        scoreManager = FindObjectOfType<ScoreManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        //scoreManager = FindObjectOfType<ScoreManager>();

    }


        private void OnTriggerEnter2D(Collider2D portal)
    {
        print(teleport.kills + "  " + scoreManager.hereNeedKills);
          if (portal.gameObject.tag=="Player" && teleport.kills>= scoreManager.hereNeedKills)
          {
            endLvl.SetActive(true);
          }
    }

    public IEnumerator Tel(GameObject portalll)
    {
        yield return new WaitForSeconds(0f);
        player.transform.position = new Vector2(portalll.transform.position.x, portalll.transform.position.y);
        numberLvl +=1;
        scoreManager.SwapKills(numberLvl);
        kills = 0;
        scoreManager.ChangeScoreKill();
    }
}
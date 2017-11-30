using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;


public class Game : MonoBehaviour {

    public GameObject vertexku;
    public GameObject edgeku;
    public Vector2 spawnvalue;
    public Quaternion spawnrotation;
    public GameObject[] tombolwarna;


    private GameObject[] vertexnya;
    private Color warnanya;
    private Button theButton;
    private float timeAmt = 10;
    private float time;




    public int level;
    public Text timertext;
    public Text leveltext;
   // private bool gameover;


	// Use this for initialization
	void Start () {
        level = 1;
        time = timeAmt;
        CreateColor();

        
        spawnvalue = new Vector2(0, 0);
        spawnrotation = new Quaternion(0, 0, 0, 0);

        float r = UnityEngine.Random.Range(0.0f, 1.0f);
        float g = UnityEngine.Random.Range(0.0f, 1.0f);
        float b = UnityEngine.Random.Range(0.0f, 1.0f);
        Color baru = new Color(r, g, b);
        warnanya= Color.white;
        vertexnya = new GameObject[16];


        vertexnya[0] = Instantiate(vertexku, spawnvalue, spawnrotation);
        vertexnya[0].SendMessage("SetColor",baru);

        UpdateLevel();
	}

    public void CreateColor()
    {
        for(int i=0; i<tombolwarna.Length; i++)
        {
            float r = UnityEngine.Random.Range(0.0f, 1.0f);
            float g = UnityEngine.Random.Range(0.0f, 1.0f);
            float b = UnityEngine.Random.Range(0.0f, 1.0f);
            Color baru = new Color(r, g, b);
            tombolwarna[i].GetComponent<Image>().color = baru;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateTime();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics2D.Raycast(ray.origin, ray.direction))
            {
                for (int i = 0; i < vertexnya.Length; i++)
                {
                    if (hit.collider.gameObject == vertexnya[i])
                    {
                        vertexnya[i].SendMessage("SetColor", warnanya);
                    }
                }
            }
                
        }
	}


    public void UpdateLevel()
    {
        leveltext.text = "Level: " + level;
    }

    public void UpdateTime()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            timertext.text = "Time " + time.ToString("F");
        }
        else SceneManager.LoadScene("Game Over");
    }

    public void Set_Warna(int diklik)
    {
        warnanya=tombolwarna[diklik].GetComponent<Image>().color;
    }


}

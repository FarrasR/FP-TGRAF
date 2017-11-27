using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Game : MonoBehaviour {

    public GameObject vertexku;
    public GameObject edgeku;
    public Vector2 spawnvalue;
    public Quaternion spawnrotation;
    public GameObject[] tombolwarna;
    private ColorBlock theColor;
    private Button theButton;

    public int score;
    public int level;
    public Text timertext;
    public Text leveltext;
    private bool gameover;



	// Use this for initialization
	void Start () {
        level = 1;
        score = 0;
        CreateColor();

        gameover = false;
        spawnvalue = new Vector2(0, 0);
        spawnrotation = new Quaternion(0, 0, 0, 0);

        vertexku.transform.Find("Numbering").GetComponent<TextMesh>().text = "9";
        Instantiate(vertexku, spawnvalue, spawnrotation);


        spawnvalue = new Vector2(2, 2);
        vertexku.transform.Find("Numbering").GetComponent<TextMesh>().text = "19";
        Instantiate(vertexku, spawnvalue, spawnrotation);

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
    void Update () {
		
	}


    public void UpdateLevel()
    {
        leveltext.text = "Level: " + level;
    }

}

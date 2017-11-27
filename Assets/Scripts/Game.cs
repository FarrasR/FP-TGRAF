using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Game : MonoBehaviour {

    public GameObject vertexku;
    public GameObject edgeku;
    public Vector2 spawnvalue;

    public int score;
    public int level;
    public Text scoretext;
    public Text leveltext;
    private bool gameover;



	// Use this for initialization
	void Start () {
        level = 1;
        score = 0;
        gameover = false;
        UpdateLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void UpdateLevel()
    {
        leveltext.text = "Level: " + level;
    }

}

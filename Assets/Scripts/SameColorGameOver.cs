using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SameColorGameOver : MonoBehaviour {

    public Text timertext;


    // Use this for initialization
    void Awake () {
        timertext.text ="Warna Vertex "+ Game.warnasama1 +" dan "+ Game.warnasama2+" Sama";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

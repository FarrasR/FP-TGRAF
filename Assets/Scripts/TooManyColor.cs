using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooManyColor : MonoBehaviour {


    public Text timertext;
    // Use this for initialization
    void Awake()
    {
        timertext.text = "Anda Menggunakan "+Game.warnaplayer+" Warna. Minimalnya "+Game.warnasistem+" Warna";
    }

    // Update is called once per frame
    void Update () {
		
	}
}

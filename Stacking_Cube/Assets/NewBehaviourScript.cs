using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    private float done = 50.0F;
    public Text TimeCount;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (done > 0F)
        {
            done -= Time.deltaTime;
            TimeCount.text = "Time : " + string.Format("{0:f1}",done) + " sec";
        }
        else
        {
            print("Time is Over");
        }

    }
}

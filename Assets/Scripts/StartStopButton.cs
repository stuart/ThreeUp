using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartStopButton : MonoBehaviour {
    bool Started;
    public GameObject ExperimentManager;

	// Use this for initialization
	void Start () {
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BtnOnClick() {
        Debug.Log("Click");
        Debug.Log(Started);
 
        if (!Started)
        {
            Debug.Log("Here");
            gameObject.GetComponentInChildren<Text>().text = "Stop";
            ExperimentManager.SendMessage("StartExperiment");
            Started = true;
        } else
        {
            ExperimentManager.SendMessage("StopExperiment");
            Reset();
        }
    }

    public void Reset()
    {
        Started = false;
        gameObject.GetComponentInChildren<Text>().text = "Start";
    }
}

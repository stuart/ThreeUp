using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSpawner : MonoBehaviour {
    public int NumCoins;
    public int NumBatches;
    public float batchDelay = 10.0f;
    public ThreeSidedCoin coin;
    public GameObject Statistics;
    public InputField NumBatchesInput;
    public InputField NumCoinsInput;
    public InputField RatioInput;
    public Button StartStopButton;

    public float Ratio = 1.0f;
    public float torque;
    public float force;

    int batchCount = 0;
    float timer = 0.0f;
    bool experimentRunning = false;

	// Use this for initialization
	void Start () {
        Debug.Log(NormalDistribution.Phi(0.0));
        Debug.Log(NormalDistribution.Phi(-1.0));
	}
	
	// Update is called once per frame
	void Update () {
        if (experimentRunning)
        {
            timer += Time.deltaTime;
        }
        if(timer > batchDelay && batchCount < NumBatches)
        {
            ThrowCoins();
            batchCount++;
            timer = 0.0f;
        }
        if(batchCount == NumBatches && experimentRunning)
        {
            StopExperiment();
        }

        if (Input.GetKey("escape"))
          Application.Quit();
    }

    public void ThrowCoins()
    {
        for (int i = 0; i < NumCoins; i++)
        {
            /* Spread coins out more if there are more coins */
            float spread = Mathf.Sqrt((float)NumCoins);

            ThreeSidedCoin new_coin = GameObject.Instantiate(coin, new Vector3(Random.Range(-spread, spread), Random.Range(3f, 6f), Random.Range(-spread, spread)), Random.rotationUniform);
            new_coin.Statistics = Statistics;

            /* The coin model actually has a radius of 1 and a depth of 1 so we multiply the ratio by two for diameter */
            new_coin.transform.localScale = new Vector3(1.0f, 2.0f / Ratio, 1.0f);

            Rigidbody body = new_coin.GetComponent<Rigidbody>();
            body.AddTorque(new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque)));
            body.AddForce(new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force)));
        }

    }

    public void StartExperiment()
    {
        Debug.Log("Start Experiment");
        int.TryParse(NumBatchesInput.text, out NumBatches);
        int.TryParse(NumCoinsInput.text, out NumCoins);
        float.TryParse(RatioInput.text, out Ratio);

        Statistics.SendMessage("ClearCounts");
        batchCount = 0;
        experimentRunning = true;
        timer = batchDelay - 0.1f;
    }

    public void StopExperiment()
    {
        Debug.Log("Stop Experiment");
        experimentRunning = false;
        timer = 0.0f;
        StartStopButton.SendMessage("Reset");

    }
}

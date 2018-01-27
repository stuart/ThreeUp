using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinStatistics : MonoBehaviour {
    public int numHeads;
    public int numTails;
    public int numSide;
    public int numUnknown;
    public Text headsCount;
    public Text tailsCount;
    public Text sideCount;
    public Text unknownCount;
    public Text totalCount;
    public Text PValueText;
    public Text ExpectationText;
    public Text VarianceText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Heads()
    {
        numHeads++;
        updateCounts();
    }

    void Tails()
    {
        numTails++;
        updateCounts();
    }

    void Side()
    {
        numSide++;
        updateCounts();
    }

    void Unknown()
    {
        numUnknown++;
        updateCounts();
    }

    void ClearCounts()
    {
        numHeads = numTails = numSide = numUnknown = 0;
        updateCounts();
    }

    int TotalCounted()
    {
        return numHeads + numTails + numSide;
    }

    float expectedMean()
    {
       return TotalCounted() / 3.0f;
    }

    float expectedVariance()
    {
        return TotalCounted() * (1.0f / 3.0f) * (1.0f - 1.0f / 3.0f);
    }

    float PValue()
    {
        float z;

        z = (numSide - expectedMean()) / Mathf.Sqrt(expectedVariance());
        
        return (float)NormalDistribution.Phi(-Mathf.Abs(z));
    }

    private void updateCounts()
    {
        headsCount.text = numHeads.ToString();
        tailsCount.text = numTails.ToString();
        sideCount.text = numSide.ToString();
        unknownCount.text = numUnknown.ToString();
        totalCount.text = TotalCounted().ToString();
        PValueText.text = PValue().ToString("E");
        ExpectationText.text = expectedMean().ToString();
        VarianceText.text = expectedVariance().ToString();
    }

}

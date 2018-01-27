using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeSidedCoin : MonoBehaviour {
    public float thresholdVelocity;
    public float angleAccuracy;
    public GameObject Statistics;

    bool hasHitTable;
    bool hasReportedPosition;
  
    Rigidbody body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        hasHitTable = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (hasHitTable && !hasReportedPosition && body.velocity.magnitude < thresholdVelocity)
        {
            float angle = Vector3.Angle(Vector3.up, transform.up);

            hasReportedPosition = true;

            if (compareAngle(angle, 0f))
            {
                Debug.Log("Heads");
                Statistics.SendMessage("Heads");
            }
            else if (compareAngle(angle, 180f))
            {
                Debug.Log("Tails");
                Statistics.SendMessage("Tails");

            }
            else if (compareAngle(angle, 90f))
            {
                Debug.Log("Side");
                Statistics.SendMessage("Side");

            }
            else
            {
                Debug.Log("Unknown");
                Statistics.SendMessage("Unknown");

            }

            Destroy(gameObject);
        }
    }

    void HitTable()
    {
        hasHitTable = true;
    }

    private bool compareAngle(float angle, float comparison)
    {
        return comparison - angleAccuracy < angle && comparison + angleAccuracy > angle;
    }

    void OutOfBounds()
    {
        Debug.Log("Unknown");
        Statistics.SendMessage("Unknown");
        Destroy(gameObject);
     }
}

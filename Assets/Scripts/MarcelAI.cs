using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcelAI : BaseAI

{
    public bool shipSpotted = false;
    public GameObject me = GameObject.Find("Ship1");
    public GameObject target;
    public string targetName;
    public PirateShipController script;
    public bool gamePlaying = true;

    void FixedUpdate()
    {
        script = me.GetComponent<PirateShipController>();
        Debug.Log(shipSpotted);
    }

    public override IEnumerator RunAI()
    {
        //yield return Ahead(400);
        yield return TurnRight(45);
        yield return TurnLookoutLeft(90);
        if (gamePlaying == true)
        {
            if (shipSpotted == true)
            {
                target = GameObject.Find(targetName);

                Vector3.MoveTowards(me.transform.position, target.transform.position, script.BoatSpeed);
            }
        }
        //for (int i = 0; i < 9999; i++)
        //{
            yield return Ahead(100);
            //yield return FireFront(1);
            //yield return FireLeft(1);
            //yield return FireRight(1);
            //yield return TurnLookoutLeft(90);
            //yield return TurnLeft(5);
            //yield return FireLeft(1);
            //yield return TurnLookoutRight(180);
        //}
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
        if (e.Name == "Ship0" || e.Name == "Ship2" || e.Name == "Ship3")
        {
            shipSpotted = true;
        }
        
        targetName = e.Name;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcelAI : BaseAI

{
    public bool shipSpotted = false;
    public GameObject me;
    public GameObject target;
    public string targetName;
    public PirateShipController script;
    public bool gamePlaying;

    
        
        
    

    public override IEnumerator RunAI()
    {
        //yield return Ahead(400);
        gamePlaying = true;
        me = GameObject.Find("Ship1");
        Debug.Log(me);
        script = me.GetComponent<PirateShipController>();
        yield return TurnRight(45);
        yield return TurnLookoutLeft(90);
        while (gamePlaying == true)
        {
            //Debug.Log("yes");
            yield return Ahead(5);
            if (shipSpotted == true)
            {
               // Debug.Log("yes");
                target = GameObject.Find(targetName);
                //Vector3.MoveTowards(me.transform.position, target.transform.position, script.BoatSpeed);
                yield return FireLeft(1);
                shipSpotted = false;
                yield return null;
            }
            else
            {
                yield return null;
            }
            if (script.HitGameWall == true)
            {
                yield return Back(30);
                yield return TurnLeft(90);
                
            }
        }
        //for (int i = 0; i < 9999; i++)
        //{
            //yield return Ahead(100);
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
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
        if (e.Name == "Ship0" || e.Name == "Ship2" || e.Name == "Ship3")
        {
            shipSpotted = true;
        }

        
        targetName = e.Name;
        //Debug.Log(targetName);
    }
}

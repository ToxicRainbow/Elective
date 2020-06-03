using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcelAI : BaseAI
{
    public override IEnumerator RunAI()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return Ahead(50);
            yield return FireFront(1);
            //yield return TurnLookoutLeft(90);
            yield return TurnLeft(360);
            //yield return FireLeft(1);
            //yield return TurnLookoutRight(180);
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}

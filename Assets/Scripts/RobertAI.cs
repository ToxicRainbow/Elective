using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobertAI : BaseAI
{
    public override IEnumerator RunAI()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return Ahead(200);
            yield return FireFront(1);
            yield return FireRight(1);
            yield return FireLeft(1);
            //when scan to enemy is true, instantiate one of different attack paterns after attack instantiate tactical retreat.
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        // Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
        //Add Scan to Enemy
    }
}

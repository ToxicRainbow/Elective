using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdoAI : BaseAI
{

    private bool ship_detected = false;

    public override IEnumerator RunAI()
    {
        while(true)
        {
            yield return Ahead(10);
            
            if(ship_detected == true)
            {
                yield return FireFront(1);
                yield return TurnLeft(90);
                yield return TurnRight(180);
                yield return FireFront(1);
                ship_detected = false;
            }
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        ship_detected = true;
        Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
        Debug.Log("Edo's Ship fired!");
    }
}
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IljaAI : BaseAI
{
    public PirateShipController script;
    public Color color;
    public GameObject me;

    public override IEnumerator RunAI() {

        me = GameObject.Find("IljaAI");
        script = me.GetComponent<PirateShipController>();
        color = new Color(0F, 0F, 0F, 1F);
        script.__SetColor(color);

        for (int i = 0; i < 10; i++)
        {
            yield return Ahead(200);
            yield return FireFront(1);
            yield return TurnLookoutLeft(90);
            yield return TurnLeft(360);
            yield return FireLeft(1);
            yield return TurnLookoutRight(180);
            yield return Back(200);
            yield return FireRight(1);
            yield return TurnLookoutLeft(90);
            yield return TurnRight(90);
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
       // Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}

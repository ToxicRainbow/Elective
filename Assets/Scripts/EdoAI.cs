using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdoAI : BaseAI
{

    private bool ShipDetected = false;
    private bool ShipDetected1 = false;
    private bool ShipDetected2 = false;

    public bool LookoutIsNowLeft = false;
    public bool LookoutIsNowRight = false;
    public bool LookoutIsNowFront = false;

    public PirateShipController psc;

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        if (e.Name == "Ship0")
        {
            ShipDetected = true;
        }
        else if (e.Name == "Ship1")
        {
            ShipDetected1 = true;
        }
        else if (e.Name == "Ship2")
        {
            ShipDetected2 = true;
        }
    }


    public override IEnumerator RunAI()
    {
        yield return TurnLeft(90);

        if (psc.HitGameWall == true)
        {
            Debug.Log("Hello");
        }

            while (true)
            {
                if(ShipDetected == false)
            {
                yield return Ahead(200);
            }
                // move set for the AI if there is a ship on the left side
                yield return TurnLookoutLeft(90); 
                LookoutIsNowLeft = true;
                if (ShipDetected == true && LookoutIsNowLeft == true)
                {
                    yield return FireLeft(1);
                    LookoutIsNowLeft = false;
                }
                else if (ShipDetected1 == true && LookoutIsNowLeft == true)
                {
                    yield return FireLeft(1);
                    LookoutIsNowLeft = false;
                }
                else if (ShipDetected2 == true && LookoutIsNowLeft == true)
                {
                    yield return FireLeft(1);
                    LookoutIsNowLeft = false;
                }

                // move set for the AI if there is a ship on the Right side
                yield return TurnLookoutLeft(180);
                LookoutIsNowRight = true;
                if (ShipDetected == true && LookoutIsNowRight == true)
                {
                    yield return FireRight(1);
                    LookoutIsNowRight = false;
                }
                else if (ShipDetected1 == true && LookoutIsNowRight == true)
                {
                    yield return FireRight(1);
                    LookoutIsNowRight = false;
                }
                else if (ShipDetected2 == true && LookoutIsNowRight == true)
                {
                    yield return FireRight(1);
                    LookoutIsNowRight = false;
                }

                // move set for the AI if there is a ship on the Front side
                yield return TurnLookoutLeft(90);
                LookoutIsNowFront = true;
                if (ShipDetected == true && LookoutIsNowFront == true)
                {
                    yield return FireFront(1);
                    LookoutIsNowFront = false;
                }
                else if (ShipDetected1 == true && LookoutIsNowFront == true)
                {
                    yield return FireFront(1);
                    LookoutIsNowFront = false;
                }
                else if (ShipDetected2 == true && LookoutIsNowFront == true)
                {
                    yield return FireFront(1);
                    LookoutIsNowFront = false;
                }          
            }
        }   
}
    
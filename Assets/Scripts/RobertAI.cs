using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobertAI : BaseAI
{
    private bool InRangeShip1 = false;
    private bool InRangeShip2 = false;
    private bool InRangeShip3 = false;

    private bool ShipDetected = false;
    int AIRandomisation = 0;

    public override IEnumerator RunAI()
    {
        while (true)
        {
            yield return Ahead(200);
            while (ShipDetected == false)
            {
                if (hitGameWall == true)
                {
                    yield return TurnRight(180);
                    hitGameWall = false;
                }
                AIRandomisation = Random.Range(1, 4);
                //Math.random to select random number
                //3 or more different actions
                switch (AIRandomisation)
                {
                    case 1:
                        yield return TurnLeft(90);
                        yield return Ahead(50);
                        yield return FireFront(1);
                        break;
                    case 2:
                        yield return TurnRight(90);
                        yield return Ahead(50);
                        yield return FireFront(1);
                        break;
                    case 3:
                        yield return Ahead(150);
                        yield return FireFront(1);
                        break;
                }

            }
            while (ShipDetected == true)
            {
                if (hitGameWall == true)
                {
                    yield return TurnRight(180);
                    hitGameWall = false;
                }
                //when scan to enemy is true, instantiate one of different attack paterns after attack instantiate tactical retreat.
                if (InRangeShip1 == true)
                {
                    yield return FireFront(1);
                    yield return FireRight(1);
                    yield return FireLeft(1);
                    InRangeShip1 = false;
                    ShipDetected = false;
                }
                if (InRangeShip2 == true)
                {
                    yield return FireFront(1);
                    yield return FireRight(1);
                    yield return FireLeft(1);
                    InRangeShip2 = false;
                    ShipDetected = false;
                }
                if (InRangeShip3 == true)
                {
                    yield return FireFront(1);
                    yield return FireRight(1);
                    yield return FireLeft(1);
                    InRangeShip3 = false;
                    ShipDetected = false;
                }

            }

        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        if (e.Name == "IljaAI")
        {
            InRangeShip1 = true;
            ShipDetected = true;
        }
        else if (e.Name == "MarcelAI")
        {
            InRangeShip2 = true;
            ShipDetected = true;
        }
        else if (e.Name == "EdoAI")
        {
            InRangeShip3 = true;
            ShipDetected = true;
        }
    }
}

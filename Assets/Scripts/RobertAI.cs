using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobertAI : BaseAI
{
    private bool ShipFound = false;
    private bool InRangeShip1 = false;
    private bool InRangeShip2 = false;
    private bool InRangeShip3 = false;

    int AIRandomiser = 0;

    public override IEnumerator RunAI()
    {
        while (true)
        {
            yield return Ahead(200);
            while (ShipFound == false)
            {
                {
                    AIRandomiser = Random.Range(1, 4);
                    //Math.random function
                    //Three different AI rotations.]
                    switch (AIRandomiser)
                    {
                        case 1:
                            yield return TurnLeft(90);
                            yield return Ahead(50);
                            yield return FireRight(1);
                            Debug.Log("1");
                            break;
                        case 2:
                            yield return TurnRight(90);
                            yield return Ahead(50);
                            yield return FireRight(1);
                            Debug.Log("2");
                            break;
                        case 3:
                            yield return Ahead(150);
                            yield return FireRight(1);
                            Debug.Log("3");
                            break;
                    }

                }
            }
            while (ShipFound == true)
            {
                {
                    //Check rotation on where ship was found.
                    //Rotate if ship is left or right from ship
                    if (InRangeShip1 == true)
                    {
                        yield return FireFront(1);
                        yield return FireRight(1);
                        yield return FireLeft(1);
                        InRangeShip1 = false;
                        ShipFound = false;
                        Debug.Log("ShipLost");
                    }
                    if (InRangeShip2 == true)
                    {
                        yield return FireFront(1);
                        yield return FireRight(1);
                        yield return FireLeft(1);
                        InRangeShip2 = false;
                        ShipFound = false;
                        Debug.Log("ShipLost");
                    }
                    if (InRangeShip3 == true)
                    {
                        yield return FireFront(1);
                        yield return FireRight(1);
                        yield return FireLeft(1);
                        InRangeShip3 = false;
                        ShipFound = false;
                        Debug.Log("ShipLost");
                    }
                    //when scan to enemy is true, instantiate one of different attack paterns after attack instantiate tactical retreat.
                }
            }
        }

    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        if (e.Name == "Ship0")
        {
            InRangeShip1 = true;
            ShipFound = true;
        }
        else if(e.Name == "Ship1")
        {
            InRangeShip2 = true;
            ShipFound = true;
        }
        else if (e.Name == "Ship3")
        {
            InRangeShip3 = true;
            ShipFound = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobertAI : BaseAI
{
    private bool InRangeShip1 = false;
    private bool InRangeShip2 = false;
    private bool InRangeShip3 = false;

    public override IEnumerator RunAI()
    {
        while (true)
        {
            {
                yield return Ahead(200);
                if (InRangeShip1 == true)
                {
                    yield return FireFront(1);
                    yield return FireRight(1);
                    yield return FireLeft(1);
                    InRangeShip1 = false;
                }
                if (InRangeShip2 == true)
                {
                    yield return FireFront(1);
                    yield return FireRight(1);
                    yield return FireLeft(1);
                    InRangeShip2 = false;
                }
                if (InRangeShip3 == true)
                {
                    yield return FireFront(1);
                    yield return FireRight(1);
                    yield return FireLeft(1);
                    InRangeShip3 = false;
                }

                //when scan to enemy is true, instantiate one of different attack paterns after attack instantiate tactical retreat.
            }
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        if (e.Name == "Ship0")
        {
            InRangeShip1 = true;
        }
        else if(e.Name == "Ship1")
        {
            InRangeShip2 = true;
        }
        else if (e.Name == "Ship3")
        {
            InRangeShip3 = true;
        }
    }
}

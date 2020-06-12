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

    public PirateShipController script;
    public Color color;
    public GameObject me;   

    public override IEnumerator RunAI()
    {
        me = GameObject.Find("RobertAI");
        script = me.GetComponent<PirateShipController>();
        color = new Color(0.8F, 0.8F, 0.1F, 1F);
        script.__SetColor(color);
        while (true)
        {
            yield return Ahead(200);
            //while no ship in range
            while (ShipDetected == false)
            {
                //On wall detection turn the ship away to avoid wall hugging.
                if (hitGameWall == true)
                {
                    yield return TurnRight(180);
                    yield return Ahead(100);
                    hitGameWall = false;
                }
                AIRandomisation = Random.Range(1, 4);
                //Random.Range to select random number
                //3 or more different actions
                switch (AIRandomisation)
                {
                    case 1:
                        yield return TurnLeft(45);
                        yield return Ahead(50);
                        break;
                    case 2:
                        yield return TurnRight(90);
                        yield return Ahead(50);
                        break;
                    case 3:
                        yield return Ahead(150);
                        break;
                }

            }
            //while a ship in range
            while (ShipDetected == true)
            {
                //On wall detection turn the ship away to avoid wall hugging.
                if (hitGameWall == true)
                {
                    yield return TurnRight(180);
                    hitGameWall = false;
                }
                //when scan to enemy is detected, instantiate one of different attacks.
                if (InRangeShip1 == true)
                {
                    yield return FireFront(1);
                    yield return FireRight(1);
                    yield return FireLeft(1);
                    yield return Back(20);
                    InRangeShip1 = false;
                    ShipDetected = false;
                }
                if (InRangeShip2 == true)
                {
                    yield return FireFront(1);
                    yield return FireRight(1);
                    yield return FireLeft(1);
                    yield return Ahead(20);
                    InRangeShip2 = false;
                    ShipDetected = false;
                }
                if (InRangeShip3 == true)
                {
                    yield return FireFront(1);
                    yield return FireRight(1);
                    yield return FireLeft(1);
                    yield return TurnLeft(90);
                    InRangeShip3 = false;
                    ShipDetected = false;
                }

            }

        }
    }

    //Scan for ships with different names
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

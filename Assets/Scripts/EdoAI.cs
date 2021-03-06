﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdoAI : BaseAI
{
    #region all variables
    private bool ShipDetected = false;
    private bool ShipDetected1 = false;
    private bool ShipDetected2 = false;

    public bool LookoutIsNowLeft = false;
    public bool LookoutIsNowRight = false;
    public bool LookoutIsNowFront = false;

    public PirateShipController script;
    public Color color;

    public GameObject me;

    public int randomNumber = Random.Range(0, 10);
    #endregion
    #region OnScannedRobot
    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        // here i give each ship a name to be later used in the code
        if (e.Name == "IljaAI")
        {
            ShipDetected = true;
        }
        else if (e.Name == "RobertAI")
        {
            ShipDetected1 = true;
        }
        else if (e.Name == "MarcelAI")
        {
            ShipDetected2 = true;
        }
    }
    #endregion

    public override IEnumerator RunAI()
    {
        // code to change the colour of my ship
        me = GameObject.Find("EdoAI");
        script = me.GetComponent<PirateShipController>();
        color = new Color(0.1F, 0.7F, 0.8F, 1F);
        script.__SetColor(color);

        #region Start, with random numbers 
        // with the start of the game it will choose a number between 0 and 10. 
        // and do the action acoring to that number.
        if (randomNumber == 0)// left random 
        {
            yield return TurnLeft(Random.Range(5, 45));
        }
        if (randomNumber == 1)// right random
        {
            yield return TurnRight(Random.Range(5, 45));
        }
        if (randomNumber == 2)// straight ahead random
        {
            yield return Ahead(Random.Range(50, 200));
        }
        if (randomNumber == 3)// first left then straight ahead 
        {
            yield return TurnLeft(Random.Range(5, 45));
            yield return Ahead(Random.Range(50, 100));
        }
        if (randomNumber == 4)// first right then straight ahead
        {
            yield return TurnRight(Random.Range(5, 45));
            yield return Ahead(Random.Range(50, 100));
        }
        if (randomNumber == 5)// first straight ahead then left
        {
            yield return Ahead(Random.Range(50, 100));
            yield return TurnLeft(Random.Range(5, 45));
        }
        if (randomNumber == 6)// first straight ahead then right
        {
            yield return Ahead(Random.Range(50, 100));
            yield return TurnRight(Random.Range(5, 45));
        }
        if (randomNumber == 7)// first turn left then ahead and then right again
        {
            yield return TurnLeft(Random.Range(5, 45));
            yield return Ahead(Random.Range(50, 100));
            yield return TurnRight(Random.Range(5, 90));
        }
        if (randomNumber == 8)// first turn right then aheaad and then left again
        {
            yield return TurnRight(Random.Range(5, 45));
            yield return Ahead(Random.Range(50, 100));
            yield return TurnLeft(Random.Range(5, 90));
        }
        if (randomNumber == 9)// first ahead then left and then right 
        {
            yield return Ahead(Random.Range(50, 100));
            yield return TurnLeft(Random.Range(5, 45));            
            yield return TurnRight(Random.Range(5, 90));
        }
        if (randomNumber == 10)// first ahead then left and then right
        {
            yield return Ahead(Random.Range(50, 100));
            yield return TurnRight(Random.Range(5, 45));
            yield return TurnLeft(Random.Range(5, 90));
        }
        #endregion
    #region While loop with all moves
        while (true){
            // if there are no ships detected it will go straight ahead for a random amount between
            // 50 and 200
            if (ShipDetected == false || ShipDetected1 == false || ShipDetected2 == false)
            {
                yield return Ahead(Random.Range(50, 200));
            }

            // move set for the AI if there is a ship on the left side
            // it will first turn the lookout left and then set the bool to true so that it can activate 
            // the following actions below.
            yield return TurnLookoutLeft(90);
            LookoutIsNowLeft = true;
            if (ShipDetected == true && LookoutIsNowLeft == true)
            {
                yield return FireLeft(1);
                yield return TurnLeft(90);
                ShipDetected = false;
                LookoutIsNowLeft = false;
            }
            else if (ShipDetected1 == true && LookoutIsNowLeft == true)
            {
                yield return FireLeft(1);
                yield return TurnLeft(90);
                LookoutIsNowLeft = false;
                ShipDetected1 = false;
            }
            else if (ShipDetected2 == true && LookoutIsNowLeft == true)
            {
                yield return FireLeft(1);
                yield return TurnLeft(90);
                LookoutIsNowLeft = false;
                ShipDetected2 = false;
            }

            // move set for the AI if there is a ship on the Right side
            yield return TurnLookoutRight(0);
            LookoutIsNowFront = true;
            if (ShipDetected == true && LookoutIsNowFront == true)
            {
                yield return FireFront(1);
                LookoutIsNowFront = false;
                ShipDetected = false;
            }
            else if (ShipDetected1 == true && LookoutIsNowFront == true)
            {
                yield return FireFront(1);
                LookoutIsNowFront = false;
                ShipDetected1 = false;
            }
            else if (ShipDetected2 == true && LookoutIsNowFront == true)
            {
                yield return FireFront(1);
                LookoutIsNowFront = false;
                ShipDetected2 = false;
            }

            // move set for the AI if there is a ship on the Right side
            yield return TurnLookoutRight(180);
            LookoutIsNowRight = true;
            if (ShipDetected == true && LookoutIsNowRight == true)
            {
                yield return FireRight(1);
                yield return TurnRight(90);
                LookoutIsNowRight = false;
                ShipDetected = false;
            }
            else if (ShipDetected1 == true && LookoutIsNowRight == true)
            {
                yield return FireRight(1);
                yield return TurnRight(90);
                LookoutIsNowRight = false;
                ShipDetected1 = false;
            }
            else if (ShipDetected2 == true && LookoutIsNowRight == true)
            {
                yield return FireRight(1);
                yield return TurnRight(90);
                LookoutIsNowRight = false;
                ShipDetected2 = false;
            }

            // move set for the AI if there is a ship on the Front side
            yield return TurnLookoutLeft(90);
            LookoutIsNowFront = true;
            if (ShipDetected == true && LookoutIsNowFront == true)
            {
                yield return FireFront(1);
                LookoutIsNowFront = false;
                ShipDetected = false;
            }
            else if (ShipDetected1 == true && LookoutIsNowFront == true)
            {
                yield return FireFront(1);
                LookoutIsNowFront = false;
                ShipDetected1 = false;
            }
            else if (ShipDetected2 == true && LookoutIsNowFront == true)
            {
                yield return FireFront(1);
                LookoutIsNowFront = false;
                ShipDetected2 = false;
            }

            // if the ship hits a wall it will go back and turn a random amount, between 90 and 180, 
            // degrees around
            if (hitGameWall == true)
            {
                yield return Back(15);
                yield return TurnRight(Random.Range(90, 180));
                hitGameWall = false;
            }
        }
    }
    #endregion
}

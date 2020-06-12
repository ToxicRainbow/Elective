using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcelAI : BaseAI

{
    public bool shipSpotted = false;
    public GameObject me;
    public GameObject target;
    public string targetName;
    public PirateShipController script;
    public bool gamePlaying;
    public bool healthSwap = false;
    public Color color;
    

    
        
        
    

    public override IEnumerator RunAI()
    {
        color = new Color(0.5F, 0F, 0.5F, 0);

        gamePlaying = true;
        me = GameObject.Find("MarcelAI");
        
        script = me.GetComponent<PirateShipController>();
        script.__SetColor(color);

        while (gamePlaying == true)
        {
            if (script.BoatHealth > 30)
            {
                yield return Ahead(100);
                if (shipSpotted == true)
                {
                    yield return FireFront(5);
                    shipSpotted = false;
                }
                else
                {
                    yield return TurnLookoutRight(90);
                    if (shipSpotted == true)
                    {
                        yield return TurnRight(90);
                        yield return TurnLookoutLeft(90);
                        yield return FireFront(5);
                        shipSpotted = false;
                    }
                    else
                    {
                        yield return TurnLookoutRight(90);
                        if (shipSpotted == true)
                        {
                            yield return TurnRight(180);
                            yield return TurnLookoutLeft(180);
                            yield return FireFront(5);
                            shipSpotted = false;
                        }
                        else
                        {
                            
                            yield return TurnLookoutRight(90);
                            if (shipSpotted == true)
                            {
                                yield return TurnRight(270);
                                yield return TurnLookoutLeft(270);
                                yield return FireFront(5);
                                shipSpotted = false;
                            }
                            else
                            {

                                yield return TurnLookoutRight(90);
                            }
                        }
                    }
                    
                }
                if (script.HitGameWall == true)
                {
                    yield return Back(30);
                    yield return TurnLeft(90);

                }
            }
            if (script.BoatHealth <= 30)
            {
                if (healthSwap == false)
                {
                    yield return TurnLeft(45);
                    yield return TurnLookoutLeft(90);
                    healthSwap = true;
                }
                yield return Ahead(10);
                if (shipSpotted == true)
                {
                    target = GameObject.Find(targetName);
                    yield return FireLeft(1);
                    shipSpotted = false;
                }
                if (script.HitGameWall == true)
                {
                    yield return Back(30);
                    yield return TurnLeft(90);

                }
            }
            yield return null;   
        }
        
    }

   

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
        if (e.Name == "IljaAI" || e.Name == "RobertAI" || e.Name == "EdoAI")
        {
            shipSpotted = true;
        }

        
        targetName = e.Name;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcelAI : BaseAI

{
    //variable to check if my ship spotted a ship
    public bool shipSpotted = false;

    //variables to indetify which ship I am and to find pirateship controller script
    public GameObject me;
    public GameObject target;
    public PirateShipController script;

    //variable to make sure my code keeps running
    public bool gamePlaying = true;

    //variable to make certain code only run once when I reach a certain threshhold
    public bool healthSwap = false;

    //variable to set specific color
    public Color color;
    

    
        
        
    

    public override IEnumerator RunAI()
    {
        //sets a specific color
        color = new Color(0.5F, 0F, 0.5F, 0);
        
        //find the pirate ship controller and set which ship I am
        me = GameObject.Find("MarcelAI");
        script = me.GetComponent<PirateShipController>();

        //set the color of my ship to purple
        script.__SetColor(color);

        //while statement to make sure my code keeps running
        while (gamePlaying == true)
        {
            //if statement to check how healthy I am and to swap between tactics, above 30 is agressive playstyle
            if (script.BoatHealth > 30)
            {
                //goes forward and checks for ship, if it found a ship, it will shoot, if not,
                //it will turn the lookout and check at the other sides of the boat till it has either gone a full circle with having found nothing
                //or it found something
                yield return Ahead(100);
                if (shipSpotted == true)
                {
                    yield return FireFront(5);
                    //after it has shot it will set shipSpotted to false to make sure the ship doesnt keep shooting
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
                //checks if it hit the game wall, and if so, makes it go backwards a bit, and turn away.
                if (script.HitGameWall == true)
                {
                    yield return Back(30);
                    yield return TurnLeft(90);

                }
            }
            //checks if it is time for deffensive strategy
            if (script.BoatHealth <= 30)
            {
                //if statement to make sure it only does this code once. this code makes the ship's rotation align with the game wall
                if (healthSwap == false)
                {
                    yield return TurnLeft(45);
                    yield return TurnLookoutLeft(90);
                    healthSwap = true;
                }
                //this code then makes it go ahead and check for ships, if it spots a ship it will stay still and start shooting.
                yield return Ahead(10);
                if (shipSpotted == true)
                {                    
                    yield return FireLeft(1);
                    //after it has shot it will set shipSpotted to false to make sure the ship doesnt keep shooting
                    shipSpotted = false;
                }
                //if it hits the wall it will turn so it follows along the next wall
                if (script.HitGameWall == true)
                {
                    yield return Back(30);
                    yield return TurnLeft(90);

                }
            }
            //this line of code is to make sure the game doesnt crash, if this line isnt here it will think its an infinite loop
            yield return null;   
        }
        
    }

   
    //this line of code scans if there are other ships nearby
    public override void OnScannedRobot(ScannedRobotEvent e)
    {       
        //if there are other ships nearby it will set this to true
            shipSpotted = true;        
    }
}

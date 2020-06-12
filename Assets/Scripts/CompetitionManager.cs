using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CompetitionManager : MonoBehaviour
{
    public GameObject PirateShipPrefab = null;
    public Transform[] SpawnPoints = null;

    // switchstate is here to hold the number of boats killed because we cant put in pirateShipController
    public int SwitchState = 0;

    private List<PirateShipController> pirateShips = new List<PirateShipController>();

    //variables for the speed up & down
    public float timeSpeed = 1;
    public Text speedTracker;

    string[] names;
    public bool GameBegin = false;

    public Text TextInfo;

    
    void Awake()
    {
        //to find the text objects that we want to change
        TextInfo = GameObject.Find("GameInfo").GetComponent<Text>();
        speedTracker = GameObject.Find("SpeedChecker").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        BaseAI[] aiArray = new BaseAI[] {
            new IljaAI(), 
            new MarcelAI(), 
            new RobertAI(),
            new EdoAI()
        };

        names = new string[]{"IljaAI", "MarcelAI", "RobertAI", "EdoAI"};

        for (int i = 0; i < names.Length; i++)
        {
            GameObject pirateShip = Instantiate(PirateShipPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
            PirateShipController pirateShipController = pirateShip.GetComponent<PirateShipController>();
            pirateShipController.SetAI(aiArray[i]);
            pirateShips.Add(pirateShipController);
            pirateShip.name = names[i];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //if statement to avoid being able to start the game whilst it is still running
        if(GameBegin == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (var pirateShip in pirateShips)
                {
                    pirateShip.StartBattle();
                }
                GameBegin = true;
                TextInfo.gameObject.SetActive(false);
                Debug.Log("GameStarted");
            }
        }

        // function to reload the game in case it is needed
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // functions to speed up the game
        if (Input.GetKeyDown(KeyCode.UpArrow) && timeSpeed <16)
        {
            timeSpeed = timeSpeed * 2;
            Time.timeScale = timeSpeed;
            speedTracker.text = "Speed: " + timeSpeed.ToString();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && timeSpeed >= 0.5)
        {
            timeSpeed = timeSpeed / 2;
            Time.timeScale = timeSpeed;
            speedTracker.text = "Speed: " + timeSpeed.ToString();
        }
    }
}

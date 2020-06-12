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

    public float timeSpeed = 1;
    string[] names;
    public bool GameBegin = false;

    public Text TextInfo;

    void Awake()
    {
        TextInfo = GameObject.Find("GameInfo").GetComponent<Text>();
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

        // function to speed up the game
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && timeSpeed <16)
        {
            timeSpeed = timeSpeed * 2;
            Time.timeScale = timeSpeed;
            Debug.Log(timeSpeed);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && timeSpeed >= 0.5)
        {
            timeSpeed = timeSpeed / 2;
            Time.timeScale = timeSpeed;
            Debug.Log(timeSpeed);
        }
    }
}

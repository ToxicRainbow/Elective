using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PirateShipController : MonoBehaviour
{
    public GameObject CannonBallPrefab = null;
    public Transform CannonFrontSpawnPoint = null;
    public Transform CannonLeftSpawnPoint = null;
    public Transform CannonRightSpawnPoint = null;
    public Transform targetForDirection = null;
    public GameObject Lookout = null;
    public GameObject[] sails = null;
    private BaseAI ai = null;

    public int BoatsDown = 0;
    public int BoatHealth = 100;
    public float BoatSpeed = 100.0f;
    private float SeaSize = 500.0f;
    private float RotationSpeed = 180.0f;

    public bool HitGameWall = false;

    public Slider healthBar;

    public string boatName;
    public GameObject CompManager;

    float timeBetweenShoot = 0.5f;


    public Text GameoverText;

    void Awake()
    {
        GameoverText = GameObject.Find("gameovertext").GetComponent<Text>();
    }

    // here we link our health bars with our boats
    void Start()
    {
        GameoverText.gameObject.SetActive(false);
        boatName = transform.root.name;
        Debug.Log(boatName);
        if (boatName == "IljaAI")
        {
            healthBar = GameObject.Find("Ilja health bar").GetComponent<Slider>();
        } else if ( boatName == "MarcelAI")
        {
            healthBar = GameObject.Find("Marcel health bar").GetComponent<Slider>();
        }
        else if (boatName == "RobertAI")
        {
            healthBar = GameObject.Find("Robert health bar ").GetComponent<Slider>();
        }
        else if (boatName == "EdoAI")
        {
            healthBar = GameObject.Find("Edo health bar").GetComponent<Slider>();
        }
    }

    // every time a boat dies switchstate wil go up
    public void BoatDamage()
    {
        if(BoatHealth == 0)
        {
            GameObject CompManager = GameObject.Find("CompetitionManager");
            CompetitionManager competitionmanager = CompManager.GetComponent<CompetitionManager>();
            competitionmanager.SwitchState++;
            Destroy(gameObject);            
        }
    }

    // when swhichstate hits 3 this function will trigger. 
    public void WinState()
    {       
        if (BoatsDown == 3)
        {
            GameoverText.text = this.gameObject.name + "WINS!";
            GameoverText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if a boat gets hit by a canon ball the boat health will take damage
        if (other.tag == "CannonBall")
        {
            BoatHealth = BoatHealth - 10;
            healthBar.value -= 0.1f;
            
        }
        if (other.tag == "GameWall")
        {
            HitGameWall = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GameWall")
        {
            HitGameWall = false;
        }
    }


    public void SetAI(BaseAI _ai) {
        ai = _ai;
        ai.Ship = this;
    }

    public void StartBattle() {
        Debug.Log("test");
        StartCoroutine(ai.RunAI());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BoatDamage();
        WinState();
        GameObject CompManager = GameObject.Find("CompetitionManager");
        CompetitionManager competitionmanager = CompManager.GetComponent<CompetitionManager>();
        BoatsDown = competitionmanager.SwitchState;
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Boat") {
            ScannedRobotEvent scannedRobotEvent = new ScannedRobotEvent();
            scannedRobotEvent.Distance = Vector3.Distance(transform.position, other.transform.position);
            scannedRobotEvent.Name = other.name;
            ai.OnScannedRobot(scannedRobotEvent);
        }
    }

    public IEnumerator __Ahead(float distance) {
        int numFrames = (int)(distance / (BoatSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Translate(new Vector3(0f, 0f, BoatSpeed * Time.fixedDeltaTime), Space.Self);
            Vector3 clampedPosition = Vector3.Max(Vector3.Min(transform.position, new Vector3(SeaSize, 0, SeaSize)), new Vector3(-SeaSize, 0, -SeaSize));
            transform.position = clampedPosition;

            yield return new WaitForFixedUpdate();            
        }
    }

    public IEnumerator __Back(float distance) {
        int numFrames = (int)(distance / (BoatSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Translate(new Vector3(0f, 0f, -BoatSpeed * Time.fixedDeltaTime), Space.Self);
            Vector3 clampedPosition = Vector3.Max(Vector3.Min(transform.position, new Vector3(SeaSize, 0, SeaSize)), new Vector3(-SeaSize, 0, -SeaSize));
            transform.position = clampedPosition;

            yield return new WaitForFixedUpdate();            
        }
    }

    public IEnumerator __TurnLeft(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Rotate(0f, -RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }

    public IEnumerator __TurnRight(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Rotate(0f, RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }

    public IEnumerator __DoNothing() {
        yield return new WaitForFixedUpdate();
    }


    public IEnumerator __FireFront(float power) {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonFrontSpawnPoint.position, CannonFrontSpawnPoint.rotation);
        yield return new WaitForSeconds(timeBetweenShoot);
    }

    public IEnumerator __FireLeft(float power) {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonLeftSpawnPoint.position, CannonLeftSpawnPoint.rotation);
        yield return new WaitForSeconds(timeBetweenShoot);
    }

    public IEnumerator __FireRight(float power) {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonRightSpawnPoint.position, CannonRightSpawnPoint.rotation);
        yield return new WaitForSeconds(timeBetweenShoot);
    }

    public void __SetColor(Color color) {
        foreach (GameObject sail in sails) {
            sail.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    public IEnumerator __TurnLookoutLeft(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            Lookout.transform.Rotate(0f, -RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }

    public IEnumerator __TurnLookoutRight(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            Lookout.transform.Rotate(0f, RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }



    
}

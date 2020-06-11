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

    public int BoatHealth = 100;
    public float BoatSpeed = 100.0f;
    private float SeaSize = 500.0f;
    private float RotationSpeed = 180.0f;

    public bool HitGameWall = false;

    public Slider healthBar;

    public string boatName;

    CompetitionManager cp;

    public Text GameoverText;



    // Start is called before the first frame update
    void Start()
    {
        boatName = transform.root.name;
        Debug.Log(boatName);
        if (boatName == "Ship0")
        {
            healthBar = GameObject.Find("Ilja health bar").GetComponent<Slider>();
        } else if ( boatName == "Ship1")
        {
            healthBar = GameObject.Find("Marcel health bar").GetComponent<Slider>();
        }
        else if (boatName == "Ship2")
        {
            healthBar = GameObject.Find("Robert health bar ").GetComponent<Slider>();
        }
        else if (boatName == "Ship3")
        {
            healthBar = GameObject.Find("Edo health bar").GetComponent<Slider>();
        }
    }
    public void BoatDamage()
    {
        if(BoatHealth == 0)
        {
            cp.SwitchState += 1;
            Destroy(gameObject);            
        }
    }

    public void WinState()
    {       
        if (cp.SwitchState == 3)
        {
            Debug.Log("Won");
           GameoverText = GameObject.Find("gameovertext").GetComponent<Text>();
           GameoverText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
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
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator __FireLeft(float power) {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonLeftSpawnPoint.position, CannonLeftSpawnPoint.rotation);
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator __FireRight(float power) {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonRightSpawnPoint.position, CannonRightSpawnPoint.rotation);
        yield return new WaitForFixedUpdate();
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

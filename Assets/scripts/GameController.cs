using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject stimPack;
    private float lastStimPackAppiar;

    public GameObject hpBar;
    public GameObject pointBar;
    public GameObject GameCanvas;
	private GameObject playerOneHpBar;
	private GameObject playerTwoHpBar;
	private GameObject playerOnePointBar;
	private GameObject playerTwoPointBar;
	private GameObject playerThreePointBar;
	private GameObject playerThreeHpBar;

    public GameObject playerPrefab;
	public Transform playerOneInitialSpawnPoint;
	public Transform playerTwoInitialSpawnPoint;
	public Transform playerThreeInitialSpawnPoint;
	public Transform[] spawnPoints; 

    private GameObject playerOne;
    private GameObject playerTwo;
    private GameObject playerThree;

    void Awake() {
		createPlayerOne(playerOneInitialSpawnPoint);
		createPlayerTwo(playerTwoInitialSpawnPoint);
		if (managerLevel.threePayer) {
			createPlayerTree(playerThreeInitialSpawnPoint);
		}
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		handleStimPack();
		handleHealthBars();
		controlPlayerOne();
		controlPlayerTwo();
		controlPlayerThree();
	}

	void handleStimPack() {
        float offset  = Random.Range(0f, 10f);
		if (Time.time - lastStimPackAppiar - offset >= Random.Range(30f, 50f)) {
		    int positionNum = Random.Range(0, spawnPoints.Length);
			GameObject stimPck = Instantiate(stimPack, spawnPoints[positionNum].position, spawnPoints[positionNum].rotation);
			lastStimPackAppiar = Time.time;
			Destroy(stimPck, 10f);
       }
    }

	void controlPlayerOne() {
	   if (playerOne) { 
			if (Input.GetKey(KeyCode.D)) {
				playerOne.GetComponent<PersonCont>().clickRight = true;
			} else {
				playerOne.GetComponent<PersonCont>().clickRight = false;
			}
			if (Input.GetKey(KeyCode.A)) {
				playerOne.GetComponent<PersonCont>().clickLeft = true;
			} else {
				playerOne.GetComponent<PersonCont>().clickLeft = false;
			}
			if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.JoystickButton4)) {
				playerOne.GetComponent<PersonCont>().isRuning = true;
			} else {
				playerOne.GetComponent<PersonCont>().isRuning = false;
			}
			if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton2)) {
				playerOne.GetComponent<PersonCont>().isJumping = true;
			} else {
				playerOne.GetComponent<PersonCont>().isJumping = false;
			}
			if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton1)) {
				playerOne.GetComponent<PersonCont>().isShooting = true;
			} else {
				playerOne.GetComponent<PersonCont>().isShooting = false;
			}
			if (Input.GetKey(KeyCode.W)) {
				playerOne.GetComponent<PersonCont>().clickUp = true;
			} else {
				playerOne.GetComponent<PersonCont>().clickUp = false;
			}
			if (Input.GetKey(KeyCode.S)) {
				playerOne.GetComponent<PersonCont>().clickDown = true;
			} else {
				playerOne.GetComponent<PersonCont>().clickDown = false;
			}
	   }
	}

	void controlPlayerTwo() {
	   if (playerTwo) { 
			if (Input.GetKey(KeyCode.K)) {
				playerTwo.GetComponent<PersonCont>().clickRight = true;
			} else {
				playerTwo.GetComponent<PersonCont>().clickRight = false;
			}
			if (Input.GetKey(KeyCode.H)) {
				playerTwo.GetComponent<PersonCont>().clickLeft = true;
			} else {
				playerTwo.GetComponent<PersonCont>().clickLeft = false;
			}
			if (Input.GetKey(KeyCode.I)) {
				playerTwo.GetComponent<PersonCont>().isRuning = true;
			} else {
				playerTwo.GetComponent<PersonCont>().isRuning = false;
			}
			if (Input.GetKeyDown(KeyCode.O)) {
				playerTwo.GetComponent<PersonCont>().isJumping = true;
			} else {
				playerTwo.GetComponent<PersonCont>().isJumping = false;
			}
			if (Input.GetKeyDown(KeyCode.L)) {
				playerTwo.GetComponent<PersonCont>().isShooting = true;
			} else {
				playerTwo.GetComponent<PersonCont>().isShooting = false;
			}
			if (Input.GetKey(KeyCode.U)) {
				playerTwo.GetComponent<PersonCont>().clickUp = true;
			} else {
				playerTwo.GetComponent<PersonCont>().clickUp = false;
			}
			if (Input.GetKey(KeyCode.J)) {
				playerTwo.GetComponent<PersonCont>().clickDown = true;
			} else {
				playerTwo.GetComponent<PersonCont>().clickDown = false;
			}
	   }
	}

	void controlPlayerThree() {
	   if (playerThree) { 
			if (Input.GetKey(KeyCode.RightArrow)) {
				playerThree.GetComponent<PersonCont>().clickRight = true;
			} else {
				playerThree.GetComponent<PersonCont>().clickRight = false;
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				playerThree.GetComponent<PersonCont>().clickLeft = true;
			} else {
				playerThree.GetComponent<PersonCont>().clickLeft = false;
			}
			if (Input.GetKey(KeyCode.Alpha0)) {
				playerThree.GetComponent<PersonCont>().isRuning = true;
			} else {
				playerThree.GetComponent<PersonCont>().isRuning = false;
			}
			if (Input.GetKeyDown(KeyCode.Alpha9)) {
				playerThree.GetComponent<PersonCont>().isJumping = true;
			} else {
				playerThree.GetComponent<PersonCont>().isJumping = false;
			}
			if (Input.GetKeyDown(KeyCode.Alpha8)) {
				playerThree.GetComponent<PersonCont>().isShooting = true;
			} else {
				playerThree.GetComponent<PersonCont>().isShooting = false;
			}
			if (Input.GetKey(KeyCode.UpArrow)) {
				playerThree.GetComponent<PersonCont>().clickUp = true;
			} else {
				playerThree.GetComponent<PersonCont>().clickUp = false;
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				playerThree.GetComponent<PersonCont>().clickDown = true;
			} else {
				playerThree.GetComponent<PersonCont>().clickDown = false;
			}
	   }
	}

	void handleHealthBars() {
	   if (playerOne) {
			playerOneHpBar.GetComponent<Image>().fillAmount = playerOne.GetComponent<PersonCont>().HP / 100f;
			playerOnePointBar.GetComponent<Text>().text = "Point: " + playerOne.GetComponent<PersonCont>().Point;
	   }
		if (playerTwo) {
			playerTwoHpBar.GetComponent<Image>().fillAmount = playerTwo.GetComponent<PersonCont>().HP / 100f;
			playerTwoPointBar.GetComponent<Text>().text = "Point: " + playerTwo.GetComponent<PersonCont>().Point;
	   }
		if (playerThree) {
			playerThreeHpBar.GetComponent<Image>().fillAmount = playerThree.GetComponent<PersonCont>().HP / 100f;
			playerThreePointBar.GetComponent<Text>().text = "Point: " + playerThree.GetComponent<PersonCont>().Point;
	   }
	}

	void createPlayerOne(Transform spawnPoint) {
		playerOneHpBar = Instantiate(hpBar) as GameObject;
		playerOneHpBar .transform.SetParent(GameCanvas.transform, false);
		playerOneHpBar.GetComponent<RectTransform>().localPosition = new Vector3(-790f, 426, 0);

		playerOnePointBar = Instantiate(pointBar) as GameObject;
		playerOnePointBar.transform.SetParent(GameCanvas.transform, false);
		playerOnePointBar.GetComponent<RectTransform>().localPosition = new Vector3(-500f, 420, 0);
		playerOnePointBar.GetComponent<Text>().color = new Color(1, 0, 0, 1);

		playerOne = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
		playerOne.transform.Find("arrow").GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
		playerOne.GetComponent<PersonCont>().facingRight = true;
		playerOne.GetComponent<PersonCont>().facingLeft = false;
	}

	void createPlayerTwo(Transform spawnPoint) {
		playerTwoHpBar = Instantiate(hpBar) as GameObject;
		playerTwoHpBar .transform.SetParent(GameCanvas.transform, false);
		playerTwoHpBar.GetComponent<RectTransform>().localPosition = new Vector3(790f, 426, 0);
		playerTwoHpBar.GetComponent<RectTransform>().rotation =  Quaternion.Euler(0, 180f, 0);

		playerTwoPointBar = Instantiate(pointBar) as GameObject;
		playerTwoPointBar.transform.SetParent(GameCanvas.transform, false);
		playerTwoPointBar.GetComponent<RectTransform>().localPosition = new Vector3(500f, 420, 0);
		playerTwoPointBar.GetComponent<Text>().alignment = TextAnchor.UpperRight;
		playerTwoPointBar.GetComponent<Text>().color = new Color(0, 0, 1, 1);

		playerTwo = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
		playerTwo.transform.Find("arrow").GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
		playerTwo.GetComponent<PersonCont>().facingRight = false;
		playerTwo.GetComponent<PersonCont>().facingLeft = true;
	}

	void createPlayerTree(Transform spawnPoint) {
		playerThreeHpBar = Instantiate(hpBar) as GameObject;
		playerThreeHpBar .transform.SetParent(GameCanvas.transform, false);
		playerThreeHpBar.GetComponent<RectTransform>().localPosition = new Vector3(-50f, 426, 0);
		playerThreeHpBar.GetComponent<RectTransform>().rotation =  Quaternion.Euler(0, 180f, 0);

		playerThreePointBar = Instantiate(pointBar) as GameObject;
		playerThreePointBar.transform.SetParent(GameCanvas.transform, false);
		playerThreePointBar.GetComponent<RectTransform>().localPosition = new Vector3(40f, 420, 0);
		playerThreePointBar.GetComponent<Text>().alignment = TextAnchor.UpperRight;
		playerThreePointBar.GetComponent<Text>().color = new Color(0, 1, 0, 1);

		playerThree = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
		playerThree.transform.Find("arrow").GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
		playerThree.GetComponent<PersonCont>().facingRight = false;
		playerThree.GetComponent<PersonCont>().facingLeft = true;
	}
}

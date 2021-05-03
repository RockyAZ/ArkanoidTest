using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public static GameController Instance { get; private set; }

	[SerializeField]
	BallScript ballScript;
	[SerializeField]
	BatController batController;

	bool gameStart;

	//ui vars
	[SerializeField]
	Text greenText;
	[SerializeField]
	Text redText;

	private int playerBonkCounter = 0;
	private int loseBonkCounter = 0;

	public int PlayerBonkCounter
	{
		get => playerBonkCounter;
		set
		{
			playerBonkCounter = value;
			greenText.text = playerBonkCounter.ToString();
		}
	}
	public int LoseBonkCounter
	{
		get => loseBonkCounter;
		set
		{
			loseBonkCounter = value;
			redText.text = loseBonkCounter.ToString();
		}
	}

	public Vector3 PlayerPos{ get => batController.transform.position; }
	public Vector3 PlayerScal{ get => batController.transform.localScale; }

	private void OnValidate()
	{
		if (ballScript == null)
			Debug.LogError("ballScript bad value");
		if (batController == null)
			Debug.LogError("batController bad value");
		if (greenText == null)
			Debug.LogError("greenText bad value");
		if (redText == null)
			Debug.LogError("redText bad value");
	}
	// Start is called before the first frame update
	void Awake()
	{
		if (Instance == null)
			Instance = this;
		greenText.text = "0";
		redText.text = "0";
		gameStart = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			StartGame();
	}

	void StartGame()
	{
		if (gameStart == false)
		{
			gameStart = true;
			batController.isGame = true;
			ballScript.StartBall();
		}
	}
}

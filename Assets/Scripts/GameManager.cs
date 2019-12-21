using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode {GM_WAIT, GM_GAME, GM_GAMEOVER}

public class GameManager : Singleton<GameManager>
{
    public Ball ball;

    public GameObject goCamera;
    public Transform cubesHolder;
    public Transform worldTransform;

    public Vector3 startPos;
    private Vector3 lastPos;

    public GameObject platformPref;
    public GameObject cubePref;
    public GameObject crystalPref;

    public Text txtScore;
    public Text txtTapPlay;
    public Text txtTapRestart;

    private bool isRight = true;
    private GameMode gameMode = GameMode.GM_WAIT;

    private const int MAX_CUBE_COUNT = 50;
    private int cubeCount;
    private int currentNumber;
    private const int START_NUMBER = 5;
    private const int MIN_NUMBER = 0;
    private const int MAX_NUMBER = 8;

    private int score;

    private Pool<Crystal> crystalPool;
    private Pool<Cube> cubePool;

    private List<Cube> cubes = new List<Cube>();
    private List<Crystal> crystals = new List<Crystal>();

    private Platform platform;



    private void Awake()
    {
        crystalPool = new Pool<Crystal>(crystalPref, 50);
        cubePool = new Pool<Cube>(cubePref, 50);

        GameObject go = Instantiate(platformPref, worldTransform);
        platform = go.GetComponent<Platform>();
    }



    void Start () {
        NewGame();
	}
	


	void Update () {
		if (gameMode == GameMode.GM_GAME)
        {
            float newPos = (ball.GetPosition().x + ball.GetPosition().z) * 0.5f;
            Vector3 newCameraPosition = new Vector3(newPos, 0, newPos);
            goCamera.transform.position = newCameraPosition;

            if (isRight)
            {
                ball.MoveRight();
            }
            else
            {
                ball.MoveForward();
            }

            if (cubeCount < MAX_CUBE_COUNT)
            {
                GenerateCube();
            }
        }
	}



    public void NewGame()
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            cubePool.Despawn(cubes[i]);
        }
        cubes.Clear();

        crystals.Clear();

        goCamera.transform.position = Vector3.zero;
        platform.SetStartPos();

        cubeCount = 0;
        score = 0;
        currentNumber = START_NUMBER;
        isRight = true;

        for (int i = 0; i < MAX_CUBE_COUNT; i++)
        {
            GenerateCube();
        }

        ball.SetStartPos();

        SetGameMode(GameMode.GM_WAIT);
        UpdateScoreText();
    }



    private void GenerateCube()
    {
        if (cubeCount == 0)
        {
            Cube cube = cubePool.Spawn(startPos, Quaternion.identity, cubesHolder);
            cubes.Add(cube);

            lastPos = startPos;
            cubeCount++;
        }
        else
        {
            int side = Random.Range(0, 2);

            if (currentNumber == MIN_NUMBER)
                side = 1;
            if (currentNumber == MAX_NUMBER)
                side = 0;

            Vector3 newPos = lastPos;
            if (side == 0)
            {
                newPos.z++;
                currentNumber--;
            }
            else
            {
                newPos.x++;
                currentNumber++;
            }

            Cube cube = cubePool.Spawn(newPos, Quaternion.identity, cubesHolder);
            cubes.Add(cube);

            int r = Random.Range(0, 100);
            if (r < 20)
            {
                Crystal crystal = crystalPool.Spawn();
                cube.AttachCrystal(crystal);
            }

            lastPos = newPos;
            cubeCount++;
        }
    }



    public void SetGameMode(GameMode newGameMode)
    {
        switch (newGameMode)
        {
            case GameMode.GM_WAIT:
                txtTapPlay.gameObject.SetActive(true);
                txtTapRestart.gameObject.SetActive(false);
                break;
            case GameMode.GM_GAMEOVER:
                txtTapPlay.gameObject.SetActive(false);
                txtTapRestart.gameObject.SetActive(true);
                break;
            default:
                txtTapPlay.gameObject.SetActive(false);
                txtTapRestart.gameObject.SetActive(false);
                break;
        }
        gameMode = newGameMode;
    }



    public void OnScreenTouch()
    {
        switch (gameMode)
        {
            case GameMode.GM_WAIT:
                SetGameMode(GameMode.GM_GAME);
                break;

            case GameMode.GM_GAME:
                isRight = !isRight;
                break;

            case GameMode.GM_GAMEOVER:
                NewGame();
                break;
        }
    }



    public void DeleteCube(Cube cube)
    {
        cubePool.Despawn(cube);
        cubes.Remove(cube);
        cubeCount--;
    }



    public void DeleteCrystal(Crystal crystal)
    {
        crystalPool.Despawn(crystal);
        crystals.Remove(crystal);
    }



    public void AddScore(int addedScore)
    {
        score += addedScore;
        UpdateScoreText();
    }



    private void UpdateScoreText()
    {
        txtScore.text = "" + score;
    }
}

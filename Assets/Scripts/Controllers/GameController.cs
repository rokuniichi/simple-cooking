using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    MainConfig _config;

    public float LevelTime { get; private set; }

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Setup();
    }

    void Start()
    {
        LevelTime = _config.LevelTime;
    }

    void Update()
    {
        LevelTime -= Time.deltaTime;
        if (LevelTime < 0)
        {
            LevelTime = 0f;
            Time.timeScale = 0f;
        }
    }

    void Setup()
    {
        var configHandler = new ConfigHandler();
        _config = configHandler.Load<MainConfig>(ConfigPaths.MAIN_CONFIG_PATH);
    }
}
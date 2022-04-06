using UnityEngine;

public class GameController : BaseController<GameController>
{
    MainConfig _config;

    public float LevelTime { get; private set; }

    void Awake()
    {
       base.Awake();
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
using UnityEngine;

public class GameController : BaseController<GameController>
{
    public MainConfig Config { get; private set; }
    public float LevelTime { get; private set; }

    void Start()
    {
        Init();
    }

    void Update()
    {
        LevelTime -= Time.deltaTime;
        if (LevelTime < 0)
        {
            Stop();
        }
    }

    protected override void PreInit()
    {
        var configHandler = new ConfigHandler();
        Config = configHandler.Load<MainConfig>(ConfigPaths.MAIN_CONFIG_PATH);
        LevelTime = Config.LevelTime;
    }

    void Init()
    {
        Time.timeScale = 1f;
    }

    public void Stop()
    {
        Time.timeScale = 0f;
    }
}
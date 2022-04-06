using UnityEngine;

public class GameController : BaseController<GameController>
{
    public MainConfig Config { get; private set; }
    public float LevelTime { get; private set; }

    void Start()
    {
        LevelTime = Config.LevelTime;
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

    protected override void PreInit()
    {
        var configHandler = new ConfigHandler();
        Config = configHandler.Load<MainConfig>(ConfigPaths.MAIN_CONFIG_PATH);
    }
}
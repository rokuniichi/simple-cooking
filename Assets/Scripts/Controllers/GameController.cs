using UnityEngine;

public class GameController : BaseController<GameController>
{
    public TopUI TopUI;
    public RestartWindow VictoryWindow;
    public RestartWindow DefeatWindow;
    
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
            Lose();
        }
    }

    protected override void PreInit()
    {
        var configHandler = new ConfigHandler();
        Config = configHandler.Load<MainConfig>(ConfigPaths.MAIN_CONFIG_PATH);
    }

    public void Restart()
    {
        TopUI.DeInit();
        Init();
    }

    public void Win()
    {
        Stop();
        VictoryWindow.Show();
    }

    void Lose()
    {
        Stop();
        DefeatWindow.Show();
    }
    
    void Stop()
    {
        Time.timeScale = 0f;
        LevelTime = 0f;
    }
    
    void Init()
    {
        CustomerController.Instance.Init();
        LevelTime = Config.LevelTime;
        TopUI.Init();
        Time.timeScale = 1f;
    }
}
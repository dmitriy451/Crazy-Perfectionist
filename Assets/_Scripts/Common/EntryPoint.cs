using UnityEngine;

public class EntryPoint : Singleton<EntryPoint>
{
    private void Start()
    {
        IncreaseTargetFrameRate();
        CreateSystems();
    }

    private static void IncreaseTargetFrameRate()
    {
        Application.targetFrameRate = 60;
    }

    private static void CreateSystems()
    {
        //GameLogic
        //new CreateLevelSystem();
        new MoneySystem();
        new LevelTimerSystem();
        new VibrationSystem();
        new CommendSystem();
        new LevelAccuracySystem();
        new LeanTouchSystem();

        //Alanytics
        new LevelFinishAnalyticsSender();
        new LevelFailAnalyticsSender();
    }

    protected override void Initialize()
    {
    }
}
using System.Collections;
using RH.Utilities.ComponentSystem;
using RH.Utilities.Coroutines;
using UnityEngine;

public class LevelTimerSystem : BaseSystem
{
    public override void Dispose()
    {
    }

    protected override void Init()
    {
        CoroutineLauncher.Start(LevelTimerUpdate());
    }

    private IEnumerator LevelTimerUpdate()
    {
        yield return new WaitForSeconds(1.0f);
        CoroutineLauncher.Start(LevelTimerUpdate());
    }
}
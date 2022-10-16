using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Level))]
public class JumpObjectsManager : MonoBehaviour
{
    private readonly List<LevelCompleteJumpObject> jumpingObjects = new();
    private Level level;

    private void Start()
    {
        level = GetComponent<Level>();
        jumpingObjects.AddRange(level.GetComponentsInChildren<LevelCompleteJumpObject>());
        level.OnLevelComplete.AddListener(ObjectJumps);
    }

    private void ObjectJumps()
    {
        for (var i = 0; i < jumpingObjects.Count; i++)
            jumpingObjects[i].Jump(i);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueTriggerInteraction : TriggerInteractionBase
{

    public enum StatueToSpawnAt
    {
        None,
        One,
        Two,
        Three,
        Four,
    }

    [Header("Spawn TO")]
    [SerializeField] private StatueToSpawnAt StatueToSpawnTo;
    [SerializeField] private SceneField _sceneToLoad;

    [Space(10f)]
    [Header("THIS Statue")]
    public StatueToSpawnAt CurrentStatuePosition;

    public override void Interact()
    {
        SceneSwapManager.SwapSceneFromStatueUse(_sceneToLoad, StatueToSpawnTo);
    }
}

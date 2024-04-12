using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager instance;

    private static bool _loadFromStatue;

    private GameObject _player;
    private Collider2D _playerColl;
    private Collider2D _statueColl;
    private Vector3 _playerSpawnPosition;

    private StatueTriggerInteraction.StatueToSpawnAt _statueToSpawnTo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerColl = _player.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void SwapSceneFromStatueUse(SceneField myScene, StatueTriggerInteraction.StatueToSpawnAt statueToSpawnAt)
    {
        _loadFromStatue = true;
        instance.StartCoroutine(instance.FadeOutThenChangeScene(myScene, statueToSpawnAt));
    }

    private IEnumerator FadeOutThenChangeScene(SceneField myScene, StatueTriggerInteraction.StatueToSpawnAt statueToSpawnAt = StatueTriggerInteraction.StatueToSpawnAt.None)
    {
        SceneFadeManager.instance.StartFadeOut();

        while(SceneFadeManager.instance.IsFadingOut)
        {
            yield return null;
        }

        _statueToSpawnTo = statueToSpawnAt;
        SceneManager.LoadScene(myScene);
    }

    //CALLED WHENEVER A NEW SCENE IS LOADED (INCLUDING THE START OF THE GAME
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneFadeManager.instance.StartFadeIn();

        if (_loadFromStatue)
        {
            FindStatue(_statueToSpawnTo);
            _player.transform.position = _playerSpawnPosition;
            _loadFromStatue = false;
        }
    }

    private void FindStatue(StatueTriggerInteraction.StatueToSpawnAt statueSpawnNumber)
    {
        StatueTriggerInteraction[] statues = FindObjectsOfType<StatueTriggerInteraction>();

        for (int i = 0; i < statues.Length; i++)
        {
            if (statues[i].CurrentStatuePosition == statueSpawnNumber)
            {
                _statueColl = statues[i].gameObject.GetComponent<Collider2D>();

                CalculateSpawnPosition();
                return;
            }
        }
    }

    private void CalculateSpawnPosition()
    {
        float colliderHeight = _playerColl.bounds.extents.y;
        _playerSpawnPosition = _statueColl.transform.position - new Vector3(0f, colliderHeight - .5f, 0f);
    }
}

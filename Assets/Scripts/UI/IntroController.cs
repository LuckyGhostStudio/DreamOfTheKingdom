using UnityEngine;
using UnityEngine.Playables;

public class IntroController : MonoBehaviour
{
    private PlayableDirector director;  // Timeline 播放器

    [Header("加载主菜单事件广播")]
    public ObjectEventSO loadMenuEvent;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += OnDirectorStopped;
    }

    private void Update()
    {
        // 正在播放
        if (Input.GetKeyDown(KeyCode.Space) && director.state == PlayState.Playing)
        {
            director.Stop();    // 停止播放
        }
    }

    /// <summary>
    /// Timeline 播放器停止时调用
    /// </summary>
    /// <param name="obj"></param>
    private void OnDirectorStopped(PlayableDirector obj)
    {
        loadMenuEvent.RaiseEvent(null, this);   // 触发主菜单加载事件
    }
}

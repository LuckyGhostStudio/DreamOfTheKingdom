using UnityEngine;
using UnityEngine.Playables;

public class IntroController : MonoBehaviour
{
    private PlayableDirector director;  // Timeline ������

    [Header("�������˵��¼��㲥")]
    public ObjectEventSO loadMenuEvent;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += OnDirectorStopped;
    }

    private void Update()
    {
        // ���ڲ���
        if (Input.GetKeyDown(KeyCode.Space) && director.state == PlayState.Playing)
        {
            director.Stop();    // ֹͣ����
        }
    }

    /// <summary>
    /// Timeline ������ֹͣʱ����
    /// </summary>
    /// <param name="obj"></param>
    private void OnDirectorStopped(PlayableDirector obj)
    {
        loadMenuEvent.RaiseEvent(null, this);   // �������˵������¼�
    }
}

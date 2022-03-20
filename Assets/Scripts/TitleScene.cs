using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector title_pd;
    [SerializeField] private PlayableDirector start_pd;
    [SerializeField] private GameObject titleUI;

    public void OnClickStartButton()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.PRESS_BUTTON);

        titleUI.SetActive(false);

        title_pd.Stop();
        start_pd.Play();
    }

    // start_pd 애니메이션에서 이벤트로 호출함
    public void MoveIngameScene()
    {
        SceneManager.LoadScene("InGame1");
    }

    public void OnClickRankButton()
    {
        SceneManager.LoadScene("Ranking");
    }

    public void OnClickExitButton()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.PRESS_BUTTON);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

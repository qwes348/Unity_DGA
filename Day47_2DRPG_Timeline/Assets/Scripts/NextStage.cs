using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using DG.Tweening;

public class NextStage : MonoBehaviour
{
    public string nextStage;
    public bool sceneLoadEnabled = true;
    public SceneTransition sceneTransition;
    public AudioSource music;

    PlayableDirector pd;

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && sceneLoadEnabled)
        {
            sceneLoadEnabled = false;
            GameObject player = GameFlow.instance.player;
            player.GetComponent<PlayerFSM>().controllable = false;
            player.transform.position = transform.position + Vector3.up * -1.5f;
            //player.GetComponent<PlayerFSM>().lookAtHere = Vector3.up;
            //yield return null;

            ////transform.GetChild(0).DOLocalMoveX(-1, 0.5f);    // 내부적으로 코루틴으로 되어있음 async함
            ////transform.GetChild(1).DOLocalMoveX(1, 0.5f);
            
            var timelineAsset = pd.playableAsset as TimelineAsset;
            if (timelineAsset == null)
                yield break;

            foreach (var track in timelineAsset.GetOutputTracks())
            {
                var animTrack = track as AnimationTrack;
                if (animTrack == null)
                    continue;
                print(animTrack.name);
                if (animTrack.name == "Player")
                {
                    animTrack.position = player.transform.position;
                    break;
                }
            }

            foreach (var track in timelineAsset.outputs)
            {
                if (track.streamName == "Player")
                    pd.SetGenericBinding(track.sourceObject, player);
                if(track.streamName == "Player Animation")
                    pd.SetGenericBinding(track.sourceObject, player.transform.Find("Model").GetComponent<Animator>());
            }

            pd.Play();
            yield return new WaitForSeconds(1f);

            //player.transform.DOMoveY(1.8f, 0.5f).SetRelative();  // SetRelative == Y값을 현재값에서 상대적으로 1.5f 이동하게만듦
            //player.transform.DOScale(0.5f, 0.5f);
            //yield return new WaitForSeconds(0.5f);

            player.transform.Find("Model").GetComponent<Renderer>().enabled = false;
            player.transform.localScale = Vector3.one;
            music?.DOFade(0f, 1f);
            yield return StartCoroutine(sceneTransition.FadeIn());
            player.transform.Find("Model").GetComponent<Renderer>().enabled = true;

            SceneMgr.instance.LoadScene(nextStage);
            UIController.instance.bag.Hide();
            sceneLoadEnabled = true;
        }
    }
}

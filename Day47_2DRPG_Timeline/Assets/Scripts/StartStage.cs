using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using DG.Tweening;

public class StartStage : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public AudioSource music;
    public PlayableAsset exitTimelineAsset;

    GameObject player;
    PlayableDirector pd;    

    private void Awake()
    {
        player = GameFlow.instance.player;
        if (player == null)
        {
            GameFlow.instance.InstantiatePlayer();
            player = GameFlow.instance.player;
        }
    }

    IEnumerator Start()
    {
        List<NextStage> entries = new List<NextStage>(FindObjectsOfType<NextStage>());
        var entry = entries.Find(o => o.nextStage == SceneMgr.instance.prevScene);     // predicate = return값이 bool인 Func()
        if(entry != null)
        {
            pd = entry.GetComponent<PlayableDirector>();
            PlayableAsset back = pd.playableAsset;
            pd.playableAsset = exitTimelineAsset;
            
            entry.sceneLoadEnabled = false;
            player.transform.position = entry.transform.position;
            StartCoroutine(sceneTransition.FadeOut());
            music?.DOFade(1f, 1f);
            player.transform.Find("Model").GetComponent<Renderer>().enabled = false;

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
                if (track.streamName == "Player Animation")
                    pd.SetGenericBinding(track.sourceObject, player.transform.Find("Model").GetComponent<Animator>());
            }

            //entry.transform.GetChild(0).DOLocalMoveX(-1, 0.5f);
            //entry.transform.GetChild(1).DOLocalMoveX(1, 0.5f)
            pd.Play();   // Timeline Play
            yield return new WaitForSeconds(0.5f);

            //player.transform.localScale *= 0.5f;
            player.transform.Find("Model").GetComponent<Renderer>().enabled = true;
            //player.GetComponent<PlayerFSM>().lookAtHere = Vector3.down;
            //player.transform.DOMoveY(-1.8f, 0.5f).SetRelative();
            //player.transform.DOScale(1f, 0.5f);
            yield return new WaitForSeconds(0.5f);

            
            UIController.instance.bag.Show();
            yield return new WaitForSeconds(0.6f);            
            entry.sceneLoadEnabled = true;
            
            pd.playableAsset = back;            
            player.GetComponent<PlayerFSM>().controllable = true;
            //entry.transform.GetChild(0).DOLocalMoveX(-0.48f, 0.5f);
            //entry.transform.GetChild(1).DOLocalMoveX(0.48f, 0.5f);
        }
        else  // Title => Scene1 Case
        {
            player.GetComponent<PlayerFSM>().controllable = true;
            player.transform.position = transform.position;
            UIController.instance.bag.Show();
            StartCoroutine(sceneTransition.FadeOut());
        }                
    }

}

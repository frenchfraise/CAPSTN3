using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
//using System.Threading.Tasks;
//using System.Threading;
public class Splashscreen : MonoBehaviour
{
    [SerializeField] private VideoPlayer splashcreenVideo;
    //CancellationTokenSource tokenSource;
    IEnumerator runningCoroutine;
    private void Start()
    {
        float waitTime = (float)splashcreenVideo.clip.length;
        //tokenSource = new CancellationTokenSource();
        InitializeSplashscreen(waitTime);
        // WaitForIntro(waitTime);
    }
    public void InitializeSplashscreen(float p_videoLength)
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
        runningCoroutine = Co_InitializeSplashscreen(p_videoLength);
        StartCoroutine(runningCoroutine);

        //await Task.Yield();
    }
    //public async void InitializeSplashscreen(float p_videoLength)
    //{
    //    Task fadeIn = Task.Run(() => 
    //    TransitionUI.onFadeTransition.Invoke(0,false),
    //    tokenSource.Token);
       
        
    //    await fadeIn;
    //    if (tokenSource.IsCancellationRequested)
    //    {
    //        tokenSource.Dispose();
    //        tokenSource = null;
    //    }
    //    splashcreenVideo.Play();
    //    Task waitVideo = Task.Delay((int)p_videoLength * 1000,cancellationToken: tokenSource.Token);
        
    //    await waitVideo;
    //    if (tokenSource.IsCancellationRequested)
    //    {
    //        tokenSource.Dispose();
    //        tokenSource = null;
    //    }
    //    Task fadeOut = Task.Run(() => 
    //    TransitionUI.onFadeTransition.Invoke(1),
    //    tokenSource.Token);
    //    await fadeOut;
    //    if (tokenSource.IsCancellationRequested)
    //    {
    //        tokenSource.Dispose();
    //        tokenSource = null;
    //    }
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    //    //await Task.Yield();
    //}
    IEnumerator Co_InitializeSplashscreen(float p_videoLength)
    {

        TransitionUI.onFadeTransition.Invoke(0, false);
        yield return new WaitForSeconds(0.5f);
        
        splashcreenVideo.Play();
        yield return new WaitForSeconds(p_videoLength);

        TransitionUI.onFadeTransition.Invoke(1);
        yield return new WaitForSeconds(0.5f);
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //public async void OnSkipButtonUIClicked()
    //{

    //    tokenSource.Cancel();
    //    Task fadeOut = TransitionUI.onFadeTransition.Invoke(1);
    //    await fadeOut;
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
    public void OnSkipButtonUIClicked()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
        runningCoroutine = Co_OnSkipButtonUIClicked();
        StartCoroutine(runningCoroutine);
    }

    IEnumerator Co_OnSkipButtonUIClicked()
    {
        TransitionUI.onFadeTransition.Invoke(1);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

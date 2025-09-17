using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject screenTitle = null;
    [SerializeField] private GameObject screenLevel = null;
    [SerializeField] private GameObject screenGamePlay = null;
    [SerializeField] private GameObject screenSetting = null;
    [SerializeField] private GameObject screenCredit = null;
    [SerializeField] private GameObject screenPause = null;
    [SerializeField] private ScreenFader fader = null;

    void Start()
    {
        ShowScreen(screenTitle);
    }

    void DeactivateAllBase()
    {
        if (screenTitle) screenTitle.SetActive(false);
        if (screenLevel) screenLevel.SetActive(false);
        if (screenGamePlay) screenGamePlay.SetActive(false);
        if (screenSetting) screenSetting.SetActive(false);
        if (screenCredit) screenCredit.SetActive(false);
        if (screenPause) screenPause.SetActive(false);
    }

    public void ShowPause(bool show)
    {
        if (screenPause) screenPause.SetActive(show);
    }

    public void ShowScreen(GameObject target)
    {
        if (!target) return;
        StartCoroutine(SwitchScreen(target));
    }

    IEnumerator SwitchScreen(GameObject next)
    {
        if (fader) yield return fader.FadeOut();
        DeactivateAllBase();
        next.SetActive(true);
        if (fader) yield return fader.FadeIn();
    }
}

using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject screenTitle = null;
    [SerializeField]
    private GameObject screenLevel = null;
    [SerializeField]
    private GameObject screenGamePlay = null;

    [SerializeField] private GameObject screenSetting = null;
    [SerializeField] private GameObject screenCredit = null;


    public void ClickPlay()
    {
        screenTitle.SetActive(false);
        screenLevel.SetActive(true);
    }

    public void ClickSetting()
    {
        screenTitle.SetActive(false);
        screenSetting.SetActive(true);
    }

    public void ClickCredit()
    {
        screenTitle.SetActive(false);
        screenCredit.SetActive(true);
    }

    public void BackLevelSelectToTitle()
    {
        screenLevel.SetActive(false);
        screenTitle.SetActive(true);
    }

    public void BackFromSetting()
    {
        screenSetting.SetActive(false);
        screenTitle.SetActive(true);
    }

    public void BackFromCredit()
    {
        screenCredit.SetActive(false);
        screenTitle.SetActive(true);
    }

    public void ClickLevel()
    {
        screenLevel.SetActive(false);
        screenGamePlay.SetActive(true);
    }

    public void BackFromScreenPlay()
    {
        screenGamePlay.SetActive(false);
        screenLevel.SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private int currentScenarioStep = 0;
    [SerializeField] private GameObject getEquipmentRoom;
    [SerializeField] private GameObject actionRoom;
    [SerializeField] private Image fadeImage;

    public void CompleteScenarioStep()
    {
        currentScenarioStep++;
        ShowScenarioStep();
    }

    private void ShowScenarioStep()
    {
        switch (currentScenarioStep)
        {
            case 1:
                StartCoroutine(FadeInOutWithCallbackCoroutine(delegate
                {
                    getEquipmentRoom.SetActive(false);
                    actionRoom.SetActive(true);
                }, 1.5f));
                break;
            default:
                break;
        }
    }

    private IEnumerator FadeInOutWithCallbackCoroutine(Action callback, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, timer));
            yield return null;
        }
        callback();
        timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, timer));
            yield return null;
        }

    }
}

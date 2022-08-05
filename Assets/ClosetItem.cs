using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetItem : MonoBehaviour
{
    [SerializeField] private Transform mySlotTransform;
    private bool isShown = false;
    public bool isWorn = false;


    public void Show()
    {
        if (isShown) return;
        if (GetComponent<Animator>()) GetComponent<Animator>().Play("ClosetItemShow");
        isShown = true;
    }
    public void Hide()
    {
        if (!isShown) return;
        if (GetComponent<Animator>()) GetComponent<Animator>().Play("ClosetItemHide");
        isShown = false;
    }

    public IEnumerator GoToSlotCoroutine()
    {
        if (isWorn || mySlotTransform==null) yield break;
        isWorn = true;
        GetComponent<Animator>().enabled = false;
        while (Vector3.Distance(transform.localPosition, Vector3.zero) > 0.05f )
        {
            transform.position = Vector3.Lerp(transform.position, mySlotTransform.position, Time.deltaTime * 5);
            yield return null;
        }
    }

    public IEnumerator GoToClosetCoroutine()
    {
        if (!isWorn) yield break;
        isWorn = false;
        while (Vector3.Distance(transform.position, mySlotTransform.position) > 0.05f)
        {
            transform.localPosition = Vector3.Lerp(transform.position, Vector3.zero, Time.deltaTime * 5);
            yield return null;
        }
        GetComponent<Animator>().enabled = true;
    }
}

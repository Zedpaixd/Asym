using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PressableButton : MonoBehaviour
{

    public UnityEvent onPress = new UnityEvent();
    float startYPos,
          startYParentPos;
    float endYPos,
          endYParentPos;
    public UnityEvent onRelease = new UnityEvent();
    bool isPressed = false;
    static int pressedButtons;

    private void Start()
    {
        pressedButtons = 0;
        startYPos = transform.localPosition.y;
        endYPos = -transform.parent.localScale.y;
        startYParentPos = transform.parent.localPosition.y;
        endYParentPos = transform.parent.localPosition.y + endYPos;

        /*onPress.AddListener(() =>
        {
            if (pressedButtons == 2)
            {
                Debug.Log("Shenanigans");
            }
        });*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == transform.parent || collision.CompareTag(Globals.GROUND_TAG))
        {
            return;
        }
        isPressed = true;
        StartCoroutine(smoothPress(0.1f));
        pressedButtons++;

        if (pressedButtons == 2)
        {
            Debug.Log("Shenanigans");
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == transform.parent || collision.CompareTag(Globals.GROUND_TAG))
        {
            return;
        }
        isPressed = false;
        StartCoroutine(smoothRelease(0.05f));
        pressedButtons--;
    }



    IEnumerator smoothPress(float delayT)
    {

        float timer = 0f;


        while (timer <= delayT)
        {
            if (!isPressed)
            {
                yield break;
            }
            timer += Time.deltaTime;
            transform.localPosition = new Vector2(transform.localPosition.x, Mathf.Lerp(startYPos, endYPos, timer / delayT));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        onPress?.Invoke();
        timer = 0f;
        while (timer < delayT)
        {
            if (!isPressed)
            {
                yield break;
            }
            timer += Time.deltaTime;
            transform.parent.localPosition = new Vector2(transform.parent.localPosition.x, Mathf.Lerp(startYParentPos, endYParentPos, timer / delayT));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    IEnumerator smoothRelease(float delayT)
    {

        float timer = 0f;
        while (timer < delayT)
        {
            if (isPressed)
            {
                yield break;
            }
            timer += Time.deltaTime;
            transform.parent.localPosition = new Vector2(transform.parent.localPosition.x, Mathf.Lerp(endYParentPos, startYParentPos, timer / delayT));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        onRelease?.Invoke();
        timer = 0f;
        while (timer <= delayT)
        {

            if (isPressed)
            {
                yield break;
            }

            timer += Time.deltaTime;
            transform.localPosition = new Vector2(transform.localPosition.x, Mathf.Lerp(endYPos, startYPos, timer / delayT));
            yield return new WaitForEndOfFrame();
        }



        yield return null;
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndOfGame : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private static UIEndOfGame singleton;

    public static UIEndOfGame Singleton
    {
        get { return singleton; }
        set
        {
            if (singleton != null)
            {
                Destroy(value.gameObject);
                Debug.Log("Destroying new setter of UIStartOfGame");
            }
            singleton = value;
        }
    }

    private void Awake()
    {
        Singleton = this;
        canvasGroup = GetComponent<CanvasGroup>();
        SetInVisibleAndUnInteractable();
    }

    public void SetVisibleAndInteractable()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = false;
    }

    public void SetInVisibleAndUnInteractable()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = true;
    }
}

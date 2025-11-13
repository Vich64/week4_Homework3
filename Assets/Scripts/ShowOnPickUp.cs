using UnityEngine;

public class ShowOnPickUp : MonoBehaviour
{
    [SerializeField]
    Renderer[] renderersToToggle;

    [SerializeField]
    bool hideOnStart = true;

    GameObject currentItem;
    bool hasBeenRevealed;

    void Awake()
    {
        if (renderersToToggle == null || renderersToToggle.Length == 0)
        {
            renderersToToggle = GetComponentsInChildren<Renderer>(includeInactive: true);
        }

        if (hideOnStart)
        {
            ResetVisibility();
        }
        else
        {
            hasBeenRevealed = renderersToToggle.Length > 0 && renderersToToggle[0].enabled;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentItem == null)
        {
            currentItem = other.gameObject;
        }
        Reveal();
    }

    void OnTriggerExit(Collider other)
    {
        if (hasBeenRevealed)
        {
            return;
        }

        if (other.gameObject == currentItem)
        {
            currentItem = null;
            SetVisible(false);
        }
    }

    void SetVisible(bool visible)
    {
        for (int i = 0; i < renderersToToggle.Length; i++)
        {
            if (renderersToToggle[i] != null)
            {
                renderersToToggle[i].enabled = visible;
            }
        }
    }

    public void ShowFor(GameObject candidate)
    {
        if (candidate == null)
        {
            return;
        }

        currentItem = candidate;
        Reveal();
    }

    public void HideFor(GameObject candidate)
    {
        if (hasBeenRevealed)
        {
            return;
        }

        if (candidate != null && candidate == currentItem)
        {
            currentItem = null;
            SetVisible(false);
        }
    }

    public void Show()
    {
        Reveal();
    }

    public void Hide()
    {
        if (hasBeenRevealed)
        {
            return;
        }

        currentItem = null;
        SetVisible(false);
    }

    void Reveal()
    {
        hasBeenRevealed = true;
        SetVisible(true);
    }

    public void ResetVisibility()
    {
        hasBeenRevealed = false;
        currentItem = null;
        SetVisible(false);
    }
}

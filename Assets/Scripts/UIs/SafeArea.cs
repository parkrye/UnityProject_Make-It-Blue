using UnityEngine;

public enum SafeAreaVector
{
    Up,
    Down,
    Both
}

public class SafeArea : MonoBehaviour
{
    public SafeAreaVector AreaVector;

    private void Awake()
    {
        var rect = GetComponent<RectTransform>();
        var safeArea = Screen.safeArea;

        var minAnchor = safeArea.position;
        var maxAnchor = minAnchor + safeArea.size;

        if (AreaVector == SafeAreaVector.Up || AreaVector == SafeAreaVector.Both)
        {
            maxAnchor.x /= Screen.width;
            maxAnchor.y /= Screen.height;
            rect.anchorMax = maxAnchor;
        }

        if (AreaVector == SafeAreaVector.Down || AreaVector == SafeAreaVector.Both)
        {
            minAnchor.x /= Screen.width;
            minAnchor.y /= Screen.height;
            rect.anchorMin = minAnchor;
        }
    }
}
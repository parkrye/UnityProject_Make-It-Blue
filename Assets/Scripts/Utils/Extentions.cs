using DG.Tweening;
using TMPro;
using UnityEngine;

public static class Extentions
{
    public static void DOText(this TMP_Text tmp, string text, float duration)
    {
        tmp.DOKill();
        tmp.maxVisibleCharacters = 0;
        tmp.text = text;
        DOTween.To(x => tmp.maxVisibleCharacters = (int)x, 0f, tmp.text.Length, duration);
    }

    public static void DoDisappear(this Component comp, float duration)
    {
        comp.DOKill();
        comp.gameObject.SetActive(true);
        DOTween.To(x => comp.gameObject.SetActive(x < 1f), 0f, 1f, duration);
    }

    public static void SetTransform(this Transform from, Transform to)
    {
        from.position = to.position;
        from.localEulerAngles = to.localEulerAngles;
    }
}

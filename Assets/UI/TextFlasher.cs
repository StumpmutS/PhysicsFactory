using System.Collections;
using TMPro;
using UnityEngine;

public class TextFlasher : MonoBehaviour
{
    [SerializeField] private TMP_Text prefab;
    [SerializeField] private RectTransform container;
    [SerializeField] private float flashTime;

    public void Flash(string value)
    {
        var text = Instantiate(prefab, container);
        text.text = value;
        StartCoroutine(DestroyFlashed(text.gameObject));
    }

    private IEnumerator DestroyFlashed(GameObject gameObj)
    {
        yield return new WaitForSeconds(flashTime);
        Destroy(gameObj);
    }
}

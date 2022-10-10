using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    
    [SerializeField] private RawImage animatedBackground;
    [SerializeField] private float x, y;

    void Update()
    {

        animatedBackground.uvRect = new Rect(animatedBackground.uvRect.position + new Vector2(x, y) * Time.deltaTime, animatedBackground.uvRect.size);

    }

}

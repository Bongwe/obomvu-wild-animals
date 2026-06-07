using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HotSpotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Image targetImage;
	private Color defaultColor;
	private const float DefaultAlpha = 0f;
	private const float HoverAlpha = 0.2f;

	private void Awake()
	{
		targetImage = GetComponent<Image>();
		defaultColor = targetImage.color;
		SetColorAndAlpha(defaultColor, DefaultAlpha);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		SetColorAndAlpha(Color.yellow, HoverAlpha);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		SetColorAndAlpha(defaultColor, DefaultAlpha);
	}

	private void SetColorAndAlpha(Color color, float alpha)
	{
		Color current = color;
		current.a = alpha;
		targetImage.color = current;
	}
}

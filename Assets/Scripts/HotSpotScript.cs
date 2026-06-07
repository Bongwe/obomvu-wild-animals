using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HotSpotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	private Image targetImage;
	private Color defaultColor;
	[SerializeField] private GameObject textBoxDogFacts;
	[SerializeField] private TMP_Text textBoxDogFactsText;
	[SerializeField] private UnityEvent onClickPointerEvent;
	private const float DefaultAlpha = 0f;
	private const float HoverAlpha = 0.2f;

	private static readonly Dictionary<string, string> DogFactsByPart = new Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase)
	{
		{ "HotSpotNeck", "Neck: A wild dog's strong neck helps it carry prey and work together during hunts." },
		{ "HotSpotEarLeft", "Ears: Their large, rounded ears help them hear distant sounds and keep cool in hot weather." },
		{ "HotSpotEarRight", "Ears: Their large, rounded ears help them hear distant sounds and keep cool in hot weather." },
		{ "HotSpotEyeLeft", "Eyes: Wild dogs have sharp eyesight that helps them spot prey while running across open grasslands." },
		{ "HotSpotEyeRight", "Eyes: Wild dogs have sharp eyesight that helps them spot prey while running across open grasslands." },
		{ "HotSpotNose", "Nose: Their powerful nose helps them track scents and find food over long distances." },
		{ "HotSpotTail", "Tail: The white-tipped tail helps wild dogs follow each other and communicate during hunts." },
		{ "HotSpotStomach", "Body: African wild dogs have lean, lightweight bodies and long legs built for speed, stamina, and teamwork." },
		{ "HotSpotRightLeg", "Legs: African wild dogs have long, lightweight legs built for endurance hunting. Unlike most dogs, they have only four toes per foot, which helps them run efficiently. Their powerful legs allow them to chase prey for kilometers at speeds reaching 60 km/h across the savanna." },
		{ "HotSpotBackLeg", "Legs: African wild dogs have long, lightweight legs built for endurance hunting. Unlike most dogs, they have only four toes per foot, which helps them run efficiently. Their powerful legs allow them to chase prey for kilometers at speeds reaching 60 km/h across the savanna." },
		{ "HotSpotFace", "Head: An African wild dog’s head is built for hunting, with strong jaws, sharp teeth, large ears, and excellent eyesight that help it find, chase, and catch prey." }
	};

	private void Awake()
	{
		targetImage = GetComponent<Image>();
		defaultColor = targetImage.color;
		SetColorAndAlpha(defaultColor, DefaultAlpha);

		if (textBoxDogFacts != null)
		{
			if (textBoxDogFactsText == null)
			{
				textBoxDogFactsText = textBoxDogFacts.GetComponentInChildren<TMP_Text>(true);
			}

			textBoxDogFacts.SetActive(false);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		SetColorAndAlpha(Color.yellow, HoverAlpha);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		SetColorAndAlpha(defaultColor, DefaultAlpha);

		if (textBoxDogFacts != null)
		{
			textBoxDogFacts.SetActive(false);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log($"Pointer clicked on {gameObject.name}");

		if (textBoxDogFacts != null)
		{
			SetDogFactsText();
			textBoxDogFacts.SetActive(true);
		}
		else
		{
			Debug.LogWarning("TextBoxDogFacts is not assigned on HotSpotScript.");
		}

		onClickPointerEvent?.Invoke();
	}

	private void SetDogFactsText()
	{
		if (textBoxDogFactsText == null)
		{
			Debug.LogWarning("TMP_Text component was not found on TextBoxDogFacts.");
			return;
		}

		string partName = gameObject.name;
		string factToShow = "Select a body part to see a fact.";

		if (DogFactsByPart.TryGetValue(partName, out string exactMatch))
		{
			factToShow = exactMatch;
		}
		else
		{
			foreach (KeyValuePair<string, string> factEntry in DogFactsByPart)
			{
				if (partName.IndexOf(factEntry.Key, System.StringComparison.OrdinalIgnoreCase) >= 0)
				{
					factToShow = factEntry.Value;
					break;
				}
			}
		}

		textBoxDogFactsText.text = factToShow;
	}

	private void SetColorAndAlpha(Color color, float alpha)
	{
		Color current = color;
		current.a = alpha;
		targetImage.color = current;
	}
}

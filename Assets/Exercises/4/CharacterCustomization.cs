
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] _clothes;

    public GameObject GetClothPrefab(Cloth cloth)
    {
        string clothId = cloth.Id;
        GameObject clothPrefab = null;

        switch (clothId)
        {
            case "Shirt":
                clothPrefab = _clothes[0];
                break;

            case "Bag":
                clothPrefab = _clothes[1];
                break;

            case "Shoes":
                clothPrefab = _clothes[2];
                break;

            case "Hair":
                clothPrefab = _clothes[3];
                break;

            case "Polemico":
                clothPrefab = _clothes[4];
                break;

            case "Hoodie":
                clothPrefab = _clothes[5];
                break;
        }

        return clothPrefab;
    }
}

public class Cloth
{
    public string Id;
}
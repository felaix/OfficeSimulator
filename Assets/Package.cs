using UnityEngine;

public class Package : MonoBehaviour
{
    public GameObject item;

    public void OnMouseDown()
    {
        RevealPackage();
    }

    public void RevealPackage()
    {
        if (item != null)
        {
            GameObject instance = Instantiate(item, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);

    }
}

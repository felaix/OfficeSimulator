using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    public GameObject gameObjectToActivate;
    public UpgradeData dataToUpgrade;

    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => UpgradeObject());
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    public void UpgradeObject()
    {
        if (dataToUpgrade != null)
        {
            dataToUpgrade.ApplyUpgrade();
        }

        if (gameObjectToActivate != null)
        {
            gameObjectToActivate.SetActive(true);
        }

        button.gameObject.SetActive(false);
    }
}

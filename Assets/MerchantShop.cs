using TMPro;
using UnityEngine;

public class MerchantShop : MonoBehaviour {
    [System.Serializable]
    public class Item {
        public string name;
        public int price;
        public bool purchased;
    }

    private Item[] items = {
        new Item { name = "Flashlight", price = 2, purchased = false },
        new Item { name = "MovementSpeed", price = 2, purchased = false }
    };

    void Start() {
        //ResetPurchases();
        LoadPurchases();
    }

    public Item[] GetItems() {
        return items;
    }

    public Item GetItem(string itemName) {
        foreach (Item item in items) {
            if (item.name == itemName) {
                return item;
            }
        }

        return null;
    }

    public void PurchaseItem(string itemName) {
        foreach (Item item in items) {
            if (item.name == itemName && PlayerScript.GetCoins() >= item.price) {
                if (!item.purchased) {
                    item.purchased = true;
                    PlayerScript.AddCoins(-item.price);
                    SavePurchases();
                }
                return;
            }
        }
    }

    private void SavePurchases() {
        for (int i = 0; i < items.Length; i++) {
            PlayerPrefs.SetInt("Purchased_" + items[i].name, items[i].purchased ? 1 : 0);
            Debug.Log("Saving " + items[i].name + " as " + (items[i].purchased ? "purchased" : "not purchased"));
        }
        PlayerPrefs.Save();
    }

    private void LoadPurchases() {
        for (int i = 0; i < items.Length; i++) {
            if (PlayerPrefs.HasKey("Purchased_" + items[i].name)) {
                items[i].purchased = PlayerPrefs.GetInt("Purchased_" + items[i].name) == 1;
                Debug.Log("Loaded " + items[i].name + " as " + (items[i].purchased ? "purchased" : "not purchased"));
            } else {
                Debug.Log(items[i].name + " not found in PlayerPrefs");
            }
        }
    }


    private void ResetPurchases() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}

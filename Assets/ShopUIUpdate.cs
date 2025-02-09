using TMPro;
using UnityEngine;
using static MerchantShop;

public class ShopUIUpdate : MonoBehaviour {
    public Transform shopUI;
    private MerchantShop merchantShop;

    void Start() {
        merchantShop = FindFirstObjectByType<MerchantShop>();
    }

    void Update() {
        if (merchantShop == null) return;

        Item[] items = merchantShop.GetItems();
        int amt = shopUI.childCount;

        for (int i = 0; i < amt; i++) {
            Transform row = shopUI.GetChild(i);
            Transform coinsBg = row.Find("coins_bg");
            
            if (coinsBg == null) continue;
            
            TMP_Text itemPriceText = coinsBg.Find("amount")?.GetComponent<TMP_Text>();

            if (itemPriceText == null || i >= items.Length) continue;

            Item item = items[i];
            int coins = PlayerScript.GetCoins();
            if (item.purchased) {
                itemPriceText.color = Color.clear;
            } else {
                itemPriceText.color = (coins >= item.price) ? Color.green : Color.red;
            }
        }
    }
}

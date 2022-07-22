using UnityEngine;
using UnityEngine.UI;

public class CoinPickuo : MonoBehaviour
{
    [SerializeField]
    private Text coinText;
    private int coins;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            OnCoinPickup(1);
            Destroy(collision.gameObject);
        }
    }

    private void OnCoinPickup(int amount)
    {
        coins += amount;
        coinText.text = coins.ToString();
    }
}

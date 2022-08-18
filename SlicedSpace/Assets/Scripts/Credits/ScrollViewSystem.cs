using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSystem : MonoBehaviour
{
    private ScrollRect _scrollRect;

    [SerializeField] public int scrollSpeedDivider = 100;

    void Start()
    {
        _scrollRect = GetComponent <ScrollRect>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput < 0f && _scrollRect.horizontalNormalizedPosition >= 0f) {
            _scrollRect.horizontalNormalizedPosition += horizontalInput / this.scrollSpeedDivider;
        } else if (horizontalInput > 0f && _scrollRect.horizontalNormalizedPosition <= 1f) {
            _scrollRect.horizontalNormalizedPosition += horizontalInput / this.scrollSpeedDivider;
        }

        if (verticalInput < 0f && _scrollRect.verticalNormalizedPosition >= 0f) {
            _scrollRect.verticalNormalizedPosition += verticalInput / this.scrollSpeedDivider;
        } else if (verticalInput > 0f && _scrollRect.verticalNormalizedPosition <= 1f) {
            _scrollRect.verticalNormalizedPosition += verticalInput / this.scrollSpeedDivider;
        }
    }
}


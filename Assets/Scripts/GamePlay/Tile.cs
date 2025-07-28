using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite On;
    [SerializeField] private Sprite Off;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Vector2Int GridPosition { get; private set; }
    [SerializeField] private bool _isOn;

    public bool IsOn => _isOn;

    public void Init(Vector2Int gridPosition)
    {
        GridPosition = gridPosition;
        SetState(false);
    }
    private void SetState(bool state)
    {
        _isOn = state;
        UpdateVisual();
    }
    private void OnMouseDown()
    {
        GameEventDispatcher.TileClicked(GridPosition);
    }

    public void Toggle()
    {
        SetState(!_isOn);
    }

    public void ResetState()
    {
        _isOn = false;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (_isOn)
        {
            //_spriteRenderer.sprite = On;
            _spriteRenderer.color = Color.yellow;
        }
        else
        {
            //_spriteRenderer.sprite = Off;
            _spriteRenderer.color = Color.gray;
        }
    }
}

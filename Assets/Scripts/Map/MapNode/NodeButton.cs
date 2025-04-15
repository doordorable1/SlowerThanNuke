using UnityEngine;
using UnityEngine.UI;

public class NodeButton : MonoBehaviour
{
    
    public NodeDataSO nodeData;
    public bool wasClicked = false;

    private Button _button;
    private Image _image;

    [SerializeField] Sprite _clearNode;

    void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if (wasClicked)
        {
            _image.sprite = _clearNode;
        }
    }

    public void SetInteractable(bool interactable)
    {
        _button.interactable = interactable;
    }
}


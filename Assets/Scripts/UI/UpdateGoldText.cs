using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGoldText : MonoBehaviour
{
    [SerializeField]
    private Text _goldText; // Reference to the UI Text component for displaying gold

    private PlayerController _playerController;

    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        UpdateGold(); // Initialize the gold text
    }
    private void Update()
    {
        UpdateGold();
    }
    public void UpdateGold()
    {
        if (_goldText != null && _playerController != null)
        {
            _goldText.text = "" + PlayerController._currentGold;
        }
    }
}

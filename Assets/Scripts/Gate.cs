using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private Vector2 _newPos;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private bool _opened = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.HasKeyToCastle = true;

            if(GameManager.instance.HasKeyToCastle == true)
            {
                UIManager.Instance.ShowHasKeyText();
                
                if(CrossPlatformInputManager.GetButtonDown("A_Button"))
                {
                    _opened = true;
                }
            }
            else
            {
                UIManager.Instance.ShowHasNoKeyText();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            UIManager.Instance.HideHasKeyText();
        }
    }

    void Update()
    {
        if (_opened == false)
            return;

        transform.position = Vector2.MoveTowards(transform.position, _newPos, _speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region REFERENCES

    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _followOffset;


    #endregion

    #region MONOBEHAVIOUR

    private void Start()
    {
        _player = FindObjectOfType<Player>().GetComponent<Transform>();
        _followOffset = transform.position - _player.position;
    }

    private void LateUpdate()
    {
        transform.position = _player.position + _followOffset;
    }


    #endregion

}
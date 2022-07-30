using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReaction : MonoBehaviour
{
    Material _reactionMaterial;
    Material _originalMaterial;

    [SerializeField] Color _reactionColor;

    [SerializeField] float _intencity;

    [SerializeField] float _reactionTime;
    float _time = 0;

    bool _isReaction = false;

    Renderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();

        _reactionMaterial = new Material(_renderer.material);
        _reactionMaterial.EnableKeyword("_EMISSION");

        _originalMaterial = new Material(_renderer.material);
        _originalMaterial.EnableKeyword("_SPECULAR_SETUP");
    }

    void Update()
    {
        if (!_isReaction) return;

        _time += Time.deltaTime;

        _reactionMaterial.color = _reactionColor;

        if (_time <= _reactionTime)
        {
            _reactionMaterial.SetColor("_EmissionColor", _reactionMaterial.color * Mathf.Pow(2, _intencity));
            _renderer.material = _reactionMaterial;
        }
        else
        {
            _originalMaterial.SetColor("_EmissionColor", _originalMaterial.color);
            _renderer.material = _originalMaterial;
            _time = 0;
            _isReaction = false;
        }
    }

    public void Play()
    {
        _isReaction = true;
    }
}

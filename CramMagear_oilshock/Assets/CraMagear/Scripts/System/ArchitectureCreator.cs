using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectureCreator : MonoBehaviour
{
    [SerializeField] private Transform _architectureGuide;
    [SerializeField] private Transform _architecturePrefab;

    [SerializeField] private LayerMask _collisionLayer;

    [SerializeField] private Transform _center;
    [SerializeField] private float _architectureRange;


    private Transform _guide;

    public bool _enable = false;

    private void Awake()
    {
        _guide = Instantiate(_architectureGuide);
        _guide.gameObject.SetActive(_enable);
    }

    public void ShowGuide()
    {
        if (_enable == false) return;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, Mathf.Infinity,_collisionLayer))
        {
            _guide.position = hit.point;
            
        }

        //”ÍˆÍ‚É‚æ‚éƒKƒCƒh‚Ì•\Ž¦ˆ—
        if((hit.point-_center.position).magnitude > _architectureRange)
        {
            _guide.gameObject.SetActive(false);
        }
        else
        {
            _guide.gameObject.SetActive(true);
        }
    }

    public bool Create()
    {
       if(_guide.gameObject.activeSelf == false) return false;
       Instantiate(_architecturePrefab, _guide.position, _guide.rotation);
        return true;
    }

    public void EnableToggle()
    {
        _enable = !_enable;
        _guide.gameObject.SetActive(_enable);
    }
}

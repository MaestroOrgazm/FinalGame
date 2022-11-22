using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Animator))]

public class BossTeleport : MonoBehaviour
{
    [SerializeField] private BossMove _bossMove;
    [SerializeField] private GameObject[] _points;

    private WaitForSeconds _animationDeley = new WaitForSeconds(0.5f);
    private Coroutine _teleport;
    private Animator _animator;
    private Boss _boss;

    private void OnEnable()
    {
        _bossMove.enabled = false;
        _boss = GetComponent<Boss>();
        _animator = GetComponent<Animator>();
        _teleport = StartCoroutine(Teleport());
    }

    private void OnDisable()
    {
        StopCoroutine(_teleport);
    }

    private IEnumerator Teleport()
    {
        yield return _animationDeley;
        _animator.SetTrigger("Teleport");
        yield return _animationDeley;

        while (Mathf.Abs(transform.position.y - _boss.Target.transform.position.y) > _bossMove.HeightDifference || Mathf.Abs(_boss.Target.transform.position.y - transform.position.y) > _bossMove.HeightDifference)
        {
            transform.position = _points[Random.Range(0, _points.Length)].transform.position;
            yield return null;
        }

        _animator.SetTrigger("Teleport2");
        yield return _animationDeley;
        _bossMove.enabled = true;
    }
}

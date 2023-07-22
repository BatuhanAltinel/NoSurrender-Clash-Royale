using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class Tower : MonoBehaviour ,IDamagable
{
    [Header("Tower Attributes")]
    
    [SerializeField] protected float _hitPoints;
    private protected float _maxHitPoints;
    private Vector3 _currentScale;
    private Vector3 _currentPosition;

    [SerializeField] protected UnitType _unitType;

    [SerializeField] protected Slider _healthSlider;

    
    [Header("Target Attributes")]

    [SerializeField] protected Unit _targetUnit;

    [SerializeField] protected bool _targetFounded;

    [SerializeField] protected Collider[] _targetUnitColliders;
    [SerializeField] protected List<Unit> _enemyUnits;
    
    [Header("Attack Attributes")]

    [SerializeField] protected float _range;

    [SerializeField] protected TowerArrow _towerArrow;
    [SerializeField] protected Transform _arrowThrowPoint;

    [SerializeField] protected float _hitSpeed;
    private protected float _hitCooldown;

    [SerializeField] private protected bool _canAttack;
    private protected bool _targetEliminated;

    [SerializeField] protected float _damage;



    private void OnEnable()
    {
        //EventManager.OnTowerAttack += AttackToEnemy;
    }

    private void OnDisable()
    {
        //EventManager.OnTowerAttack -= AttackToEnemy;
    }


    private void Awake() 
    {
        _currentPosition = transform.position;
        _currentScale = transform.localScale;
        _targetUnit = null;
    }


    private void Start()
    {
        _hitCooldown = _hitSpeed;
        _maxHitPoints = _hitPoints;
    }

    void Update()
    {
        AttackCoolDown();
        AttackToEnemy();
    }
    
    //protected void CheckEnemyUnitInSight()
    //{
    //    _targetUnitColliders =  Physics.OverlapSphere(transform.position, _range);

    //    if (_targetUnitColliders.Length > 0)
    //    {
    //        FindEnemyTargets();
    //    }

    //}


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    protected void FindEnemyTargets()
    {
        if (_targetFounded) return;

        foreach (var col in _targetUnitColliders)
        {

            if (col.gameObject.TryGetComponent(out Unit unit) && !_targetFounded)
            {

               

                if (unit._unitType != _unitType)
                {
                    Debug.Log("Unit bulundu" + col.name);
                    //SetTheTarget(unit);
                    _targetUnit = unit;

                    _targetFounded = true;
                    _targetEliminated = false;

                    //AttackToEnemy();

                    break;
                }

            }
        }

    }


    private void FindNearestTarget()
    {
        if (_targetFounded == true) return;

        float nearestDistance = float.MaxValue;

        foreach (Unit unit in _enemyUnits)
        {
            float tempDistance = Vector3.Distance(transform.position, unit.transform.position);

             if(tempDistance < nearestDistance)
             {
                nearestDistance = tempDistance;

                //SetTheTarget(unit);

                _targetFounded = true;
                _targetEliminated = false;
            }
        }
    }


    public void CheckTargetIsEliminated(Unit target)
    {
        if (target.GetUnitHitPoints() <= 0)
        {
            _targetEliminated = true;
            _targetFounded = false;
        }
        //else
        //    AttackToEnemy();
            
    }

    private void SetTheTarget(Unit target)
    {
        _targetUnit = target;
    }

    


    private void AttackToEnemy()
    {
        if(_canAttack && _targetFounded && !_targetEliminated)
        {

            CheckTargetIsEliminated(_targetUnit);

            GameObject arrow = SpawnManager.Instance.SpawnArrow(_arrowThrowPoint,_unitType);

            if(arrow != null && _targetUnit != null)
                arrow.GetComponent<TowerArrow>().AttackToTarget(_targetUnit.transform,_damage,this);
            
            _hitCooldown = 0;
        }
            
    }

    private void AttackCoolDown()
    {
        _hitCooldown += Time.deltaTime;

        if(_hitCooldown >= _hitSpeed)
        {
            _canAttack = true;
        }else
        {
            _canAttack = false;
            //_hitCooldown = 0;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        _hitPoints -= damageAmount;

        UpdateHealthSlider();
    }

    void UpdateHealthSlider()
    {
        _healthSlider.value = (_hitPoints / _maxHitPoints);
    }
}

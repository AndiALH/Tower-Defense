using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 1;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private int _windRes = 1;
    [SerializeField] private int _fireRes = 1;
    [SerializeField] private int _lightningRes = 1;
    [SerializeField] private SpriteRenderer _healthBar;
    [SerializeField] private SpriteRenderer _healthFill;

    private int _currentHealth;

    private ElementType _debuff;
    private float _debuffTime = 0f;

    private Color normalHealth;
    private Color debuffedHealth = new Color(44, 156, 0);

    public Vector3 TargetPosition { get; private set; }
    public int CurrentPathIndex { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        normalHealth = _healthFill.color;
    }

    // Update is called once per frame
    void Update()
    {
        //mengurangi waktu debuff jika debuff time lebih dari 0 (enemy sedang terkena debuff)
        if (_debuffTime > 0f)
        {
            _debuffTime -= Time.unscaledDeltaTime;
        }
        else
        {
            _debuffTime = 0;
            _healthFill.color = normalHealth;
        }
    }

    // Fungsi ini terpanggil sekali setiap kali menghidupkan game object yang memiliki script ini
    private void OnEnable()
    {
        _currentHealth = _maxHealth;
        _healthFill.size = _healthBar.size;
    }

    public void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, _moveSpeed * Time.deltaTime);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
        _healthBar.transform.parent = null;

        // Mengubah rotasi dari enemy
        Vector3 distance = TargetPosition - transform.position;
        if (Mathf.Abs(distance.y) > Mathf.Abs(distance.x))
        {
            // Menghadap atas
            if (distance.y > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            }
            // Menghadap bawah
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
            }
        }
        else
        {
            // Menghadap kanan (default)
            if (distance.x > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }

            // Menghadap kiri
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            }
        }
        _healthBar.transform.parent = transform;
    }

    public void ReduceEnemyHealth(int damage, ElementType element)
    {
        if (element == ElementType.Wind)
        {
            //Debug.Log("enemy got hit by wind attack");
            damage = ProcessWindAttack(damage);
        }
        if (element == ElementType.Fire)
        {
            //Debug.Log("enemy got hit by fire attack");
            damage = ProcessFireAttack(damage);
        }
        if (element == ElementType.Lightning)
        {
            //Debug.Log("enemy got hit by lightning attack");
            damage = ProcessLightningAttack(damage);
        }
        _currentHealth -= damage;
        AudioPlayer.Instance.PlaySFX("hit-enemy");

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            gameObject.SetActive(false);
            AudioPlayer.Instance.PlaySFX("enemy-die");
        }
    }

    private int ProcessWindAttack(int damage)
    {
        if (_debuffTime > 0f)
        {
            if (_debuff == ElementType.Fire)
            {
                damage *= 2;
                _debuffTime = 0f;
                _healthFill.color = normalHealth;
                Debug.Log("Fire Swirl reaction triggered");
            }
            else if (_debuff == ElementType.Lightning)
            {
                damage *= 2;
                _debuffTime = 0f;
                _healthFill.color = normalHealth;
                Debug.Log("Lightning Swirl reaction triggered");
            }
            else
            {
                _debuffTime = 7f;
                _healthFill.color = debuffedHealth;
                Debug.Log("Wind debuff extended");
            }
        }

        else
        {
            _debuffTime = 7f;
            _debuff = ElementType.Wind;
            _healthFill.color = debuffedHealth;
            Debug.Log("Wind debuff applied");
        }

        return (Mathf.Max(damage - _windRes, 0));
    }

    private int ProcessFireAttack(int damage)
    {
        if (_debuffTime > 0f)
        {
            if (_debuff == ElementType.Wind)
            {
                damage *= 2;
                _debuffTime = 0f;
                _healthFill.color = normalHealth;
                Debug.Log("Fire Swirl reaction triggered");
            }
            else if (_debuff == ElementType.Lightning)
            {
                damage *= 5;
                _debuffTime = 0f;
                _healthFill.color = normalHealth;
                Debug.Log("Overloaded reaction triggered");
            }
            else
            {
                _debuffTime = 10f;
                _healthFill.color = debuffedHealth;
                Debug.Log("Fire debuff extended");
            }
        }

        else
        {
            _debuffTime = 10f;
            _debuff = ElementType.Fire;
            _healthFill.color = debuffedHealth;
            Debug.Log("Fire debuff applied");
        }

        return (Mathf.Max(damage - _fireRes, 0));
    }

    private int ProcessLightningAttack(int damage)
    {
        if (_debuffTime > 0f)
        {
            if (_debuff == ElementType.Wind)
            {
                damage *= 2;
                _debuffTime = 0f;
                _healthFill.color = normalHealth;
                Debug.Log("Lightning Swirl reaction triggered");
            }
            else if (_debuff == ElementType.Fire)
            {
                damage *= 5;
                _debuffTime = 0f;
                _healthFill.color = normalHealth;
                Debug.Log("Overloaded reaction triggered");
            }
            else
            {
                _debuffTime = 10f;
                _healthFill.color = debuffedHealth;
                Debug.Log("Lightning debuff extended");
            }
        }

        else
        {
            _debuffTime = 10f;
            _debuff = ElementType.Lightning;
            _healthFill.color = debuffedHealth;
            Debug.Log("Lightning debuff applied");
        }

        return (Mathf.Max(damage - _lightningRes, 0));
    }

    // Menandai indeks terakhir pada path
    public void SetCurrentPathIndex(int currentIndex)
    {
        CurrentPathIndex = currentIndex;
    }
}

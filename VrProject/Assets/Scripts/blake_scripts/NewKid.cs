using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewKid : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _sandwhichRecoilDistance;

    private bool _isActive = false;
    private bool _isHit = false;

    private Vector3 _startPos;
    [SerializeField] private Transform _targetPos;
    [SerializeField] private Transform _tutorialPos;
    [SerializeField] private Sprite[] _kidForms;
    [SerializeField] private Slider _bar;
    private SpriteRenderer _renderer;


    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        TutorialManager.Instance.PauseGame.AddListener(GamePaused);
        TutorialManager.Instance.ResumeGame.AddListener(GameUnpaused);
        TutorialManager.Instance.EndTutorial.AddListener(StartMoving); 
    }
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _renderer.sprite = _kidForms[0];
        transform.position = _tutorialPos.position;

        float maxDistance = Vector3.Distance(_startPos, _targetPos.position);

        if (_bar != null)
        {
            _bar.maxValue = maxDistance;
            _bar.minValue = 0;
            _bar.value = maxDistance;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isActive && !_isHit)
        {
            Move();
        }
    }

    private void Update()
    {
        if (transform.position.z >= _targetPos.position.z)
        {
            SceneManager.LoadScene("MainMenu");
        }

        float dist = (_targetPos.position - _startPos).magnitude;
        float dist2 = (_targetPos.position - transform.position).magnitude;

        if (dist2 < dist / 2)
        {
            _renderer.sprite = _kidForms[0];
        }
        else if(dist2 >= dist *.75)
        {
            _renderer.sprite = _kidForms[2];
        }
        else if(dist2 >= dist/2)
        {
            _renderer.sprite = _kidForms[1];
        }

        _bar.value = Vector3.Distance(transform.position, _targetPos.position);

    }

    public void StartMoving()
    {
        _isActive = true;
        transform.position = _startPos;
    }
    public void Move()
    {
        transform.Translate((_targetPos.position - transform.position).normalized * _speed * Time.deltaTime);
    }
    public void GamePaused()
    {
        _isActive = false;
    }
    public void GameUnpaused()
    {
        _isActive = true;
    }


    IEnumerator MoveBack()
    {
        TutorialManager.Instance.KidHitWithSandwich.Invoke();
        float distTraveled = 0;
        while (distTraveled < _sandwhichRecoilDistance)
        {
            Vector3 origin = transform.position;
            transform.Translate((_startPos - transform.position).normalized * _speed * Time.deltaTime);
            distTraveled += Mathf.Abs((origin - transform.position).magnitude);
            yield return new WaitForFixedUpdate();
        }
        _isHit = false;
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sandwich")
        {
            _isHit = true;
            StartCoroutine(MoveBack());
        }
    }
}

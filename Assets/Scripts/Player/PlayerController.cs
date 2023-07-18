using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 100f;
    Rigidbody rb;
    Collider col;
    [SerializeField] float movement;
    [SerializeField] Transform pizzaBoxPivot;
    [SerializeField] int pizzasCollected = 1;
    [SerializeField] Rig characterRig;
    [SerializeField] Animator animator;
    [SerializeField] float animatorSpeedMultiplier = 1f;
    [SerializeField] float xPos = -2f;
    [SerializeField] float sideMoveSpeed = 1f;
    bool right = false;
    [SerializeField] GameObject pizzaBoxPrefab;
    [SerializeField] List<GameObject> pizzaBoxeArray = new List<GameObject>();

    Collider DelivaryHouseObj;
    bool inHouseRange = false;

    Vector2 startTouchPos;
    Vector2 currentTouchPos;
    public bool stopTouch = false;
    float requiredHoldDuration = .2f;
    float currentHoldDuration = 0.0f;

    [SerializeField] float sensitivity = 10f;

    float swipeRange = 50f;
    Vector2 touchDistance;

    public float xScrennPos;
    GameObject delivaryPizzaPos;


    /*
    [SerializeField] LineRenderer rightLineRenderer;
    [SerializeField] LineRenderer leftLineRenderer;
    Transform startPoint;
    Vector3 startPointOffset;
    Transform rightendPoint;
    Vector3 rightendPointOffset = new Vector3(.8f,0f,.2f);
    Transform leftendPoint;
    Vector3 leftendPointOffset;
    Vector3 controlPoint;*/
    int bezierCurveResolution = 10;

    public bool levelCompleted = false;
    public bool levelFailed = false;
    bool bonusPointsCompleted = false;
    public bool pizzasDelivered = false;
    bool move = false;
    AudioManager audioManager;

    [SerializeField] ParticleSystem upgradeParticleEff;
    [SerializeField] ParticleSystem degradeParticleEff;
    [SerializeField] ParticleSystem powerUpPartcleEff;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();


        if(characterRig != null)
            characterRig.weight = 1f;

        audioManager = AudioManager.instance;

        animator.SetFloat("speed", animatorSpeedMultiplier);

        pizzasCollected = 1;
        LevelManager.instance.currentPizzasCollected = pizzasCollected;
    }

    private void Update()
    {
        if (bonusPointsCompleted || levelFailed)
            return;


        #region PcControllers
        
        /*
        if (Input.GetAxisRaw("Horizontal") >= .1 || Input.GetAxisRaw("Horizontal") <= -.1f)
        {
            xScrennPos += Input.GetAxisRaw("Horizontal") * 2f;
        }
        else
        {
            xScrennPos = 0f;
        }

        if(Input.GetAxisRaw("Vertical") >= .1)
        {
            move = true;
            animator.SetBool("move", move);
        }
        else
        {
            move = false;
            animator.SetBool("move", move);
        }
        */
        #endregion

        #region MObileCOmtrollers

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
            move = true;
            animator.SetBool("move", move);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentTouchPos = Input.GetTouch(0).position;
            touchDistance = currentTouchPos - startTouchPos;
            xScrennPos = touchDistance.x;

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            xScrennPos = 0f;
            move = false;
            animator.SetBool("move", move);
        }

        #region Archieve
        /*
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            currentHoldDuration = 0.0f;
                            break;
                        case TouchPhase.Stationary:
                        case TouchPhase.Moved:
                            if (inHouseRange)
                            {
                                currentHoldDuration += Time.deltaTime;
                                if (currentHoldDuration >= requiredHoldDuration)
                                {

                                    //right Line Renderer
                                    rightLineRenderer.enabled = true;
                                    controlPoint = Vector3.Lerp(startPoint.position, rightendPoint.position, .5f) + new Vector3(-3f, 3f, 0f);

                                    Vector3[] _rightpositions = BezierCurvePositions(startPoint.position + startPointOffset, rightendPoint.position + rightendPointOffset, controlPoint);
                                    rightLineRenderer.SetPositions(_rightpositions);


                                    //left Line Renderer
                                    leftLineRenderer.enabled = true;
                                    controlPoint = Vector3.Lerp(startPoint.position, leftendPoint.position, .5f) + new Vector3(3f, 3f, 0f);

                                    Vector3[] _leftpositions = BezierCurvePositions(startPoint.position + startPointOffset, leftendPoint.position + leftendPointOffset, controlPoint);
                                    leftLineRenderer.SetPositions(_leftpositions);

                                }
                            }
                            break;
                        case TouchPhase.Ended:
                            if (inHouseRange)
                            {
                                rightLineRenderer.enabled = false;
                                leftLineRenderer.enabled = false;
                                if (touchDistance.x > 100f)
                                {
                                    delivaryPizzaPos = rightDelivaryPos;
                                    DeliverPizzas(4);
                                }
                                else if (touchDistance.x < -100f)
                                {
                                    delivaryPizzaPos = leftDelivaryPos;
                                    DeliverPizzas(5);
                                }
                            }
                            break;
                        case TouchPhase.Canceled:
                            currentHoldDuration = 0.0f;
                            if (inHouseRange)
                            {
                                rightLineRenderer.enabled = false;
                                leftLineRenderer.enabled = false;
                            }
                            break;
                    }
                }*/
        #endregion


        #endregion

        rb.position += new Vector3(xScrennPos * sensitivity * Time.deltaTime, 0f, 0f);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.4f, 3.4f), transform.position.y, transform.position.z); 
    }

    Vector3[] BezierCurvePositions(Vector3 startPos,Vector3 endPos,Vector3 controlPoint)
    {
        bezierCurveResolution = 10;
        Vector3[] _positions = new Vector3[bezierCurveResolution];

        for (int i = 0; i < bezierCurveResolution; i++)
        {
            float t = i / (float)(bezierCurveResolution-1);
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 _position = uuu * startPos + 3 * uu * t * controlPoint + 3 * u * tt * controlPoint + ttt * endPos;

            _positions[i] = _position;
        }

        return _positions;
    }
    void FixedUpdate()
    {
        if (bonusPointsCompleted || levelFailed) 
            return;


        if(move)
            rb.velocity = new Vector3(0f, rb.position.y, moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider col = other;

        if(col.CompareTag("PizzaBox"))
        {
            audioManager.Play("PizzaCollect");
            if (characterRig != null)
                characterRig.weight = 1f;
            SetBoxPos(col.gameObject);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject col = collision.gameObject;

        if (col.CompareTag("ObstucleBarrier"))
        {
            audioManager.Play("Hit");
            CinemachineShake.instance.CameraShake(10f, .2f);
            EnbaleBoxPhysics(pizzasCollected - 1);
            int _rand = Random.Range(0, 2);
            if(_rand == 0)
                col.GetComponent<Rigidbody>().AddForce(Vector3.right * Random.Range(8f,10f), ForceMode.Impulse);
            else
                col.GetComponent<Rigidbody>().AddForce(Vector3.left * Random.Range(8f, 10f), ForceMode.Impulse);
            col.GetComponent<Rigidbody>().AddForce(Vector3.up * 20f, ForceMode.Impulse);

            Destroy(col, 5f);
        }
    }




    public void AddBox(int _noOfBoxes)
    {
       
        for (int i = 0; i < _noOfBoxes; i++)
        {
            var _obj = Instantiate(pizzaBoxPrefab);
            SetBoxPos(_obj);
        }

        if (pizzasCollected >= 1)
        {
            if (characterRig != null)
                characterRig.weight = 1f;

            if (upgradeParticleEff != null)
                upgradeParticleEff.Play();
        }
    }

    public void RemoveBox(int _noOfBoxes)
    {
        if (pizzasCollected >= 1)
        {
            for (int i = 0; i < _noOfBoxes; i++)
            {
                if (pizzasCollected >= 1)
                {
                    Destroy(pizzaBoxeArray[pizzasCollected - 1]);
                    pizzaBoxeArray.RemoveAt(pizzasCollected - 1);
                    pizzasCollected--;
                }
            }

            CheckForOutOfPizzas();

            LevelManager.instance.currentPizzasCollected = pizzasCollected;


            if (!levelCompleted && degradeParticleEff != null)
                degradeParticleEff.Play();
        }

    }

    void SetBoxPos(GameObject _box)
    {
        _box.tag = "CollectedBox";
        _box.transform.SetParent(pizzaBoxPivot);
        _box.transform.localPosition = new Vector3(0f, pizzasCollected * .2f, 0f);
        _box.transform.rotation = Quaternion.Euler(Vector3.zero);
        _box.GetComponent<RotateAround>().enabled = false;
        pizzaBoxeArray.Add(_box.gameObject);


        pizzasCollected++;
        LevelManager.instance.currentPizzasCollected = pizzasCollected;
    }

    public void EnbaleBoxPhysics(int _indexFrom)
    {
        if (pizzasCollected <= 0)
            return;

        for (int i = pizzasCollected-1; i >= _indexFrom; i--)
        {
            GameObject _box = pizzaBoxeArray[i];

            _box.transform.parent = null;
            _box.GetComponent<Collider>().isTrigger = false;
            _box.GetComponent<Rigidbody>().isKinematic = false;
            //Destroy(_box, 3f);
            pizzaBoxeArray.RemoveAt(i);
            pizzasCollected--;
        }

        LevelManager.instance.currentPizzasCollected = pizzasCollected;

        CheckForOutOfPizzas();
    }


    public void PizzaDelivary(GameObject _delivaryPos, int _noOfPizzas)
    {
        if (pizzasCollected >= 1)
        {
            for (int i = 1; i <= _noOfPizzas; i++)
            {
                if (pizzasCollected >= 1)
                {
                    GameObject _box = pizzaBoxeArray[pizzaBoxeArray.Count - 1];
                    pizzaBoxeArray.RemoveAt(pizzaBoxeArray.Count - 1);
                    _box.transform.parent = null;
                    _box.GetComponent<PizzaBox>().moveTowardsPos(_delivaryPos);
                    pizzasCollected--;
                    LevelManager.instance.uiManager.AddCoin(2);
                }

            }

            audioManager.Play("MoreCashCollected");
            LevelManager.instance.currentPizzasCollected = pizzasCollected;

            CheckForOutOfPizzas();
        }
    }

    void CheckForOutOfPizzas()
    {
        if( pizzasCollected == 0)
        {
            LevelFaild();
        }

    }

    public void LevelFaild()
    {
        if(pizzasCollected == 0)
        {
            if (characterRig != null)
                characterRig.weight = 0f;
        }

        if (!levelCompleted)
        {
            animator.SetTrigger("levelFailed");
            levelFailed = true;
            LevelManager.instance.uiManager.LevelFailed();
        }
    }

    public void RemoveAllPizzas()
    {
        EnbaleBoxPhysics(0);
    }

    public void LevelCompleted()
    {
        levelCompleted = true;
        if(pizzasCollected == 0)
        {
            BonusPointCompleted();
            
        }
    }

    public void BonusPointCompleted()
    {
        bonusPointsCompleted = true;
        rb.velocity = Vector3.zero;
        int _rand = Random.Range(0, 2);
        animator.SetTrigger("levelCompleted0" + _rand);

        LevelManager.instance.uiManager.LevelComplete();
    }

    #region powerUps
    public void IncreaseSpeed(float _multiplier)
    {
        moveSpeed += _multiplier;
        animatorSpeedMultiplier += .1f;
        animator.SetFloat("speed", animatorSpeedMultiplier);
        if (powerUpPartcleEff != null)
            powerUpPartcleEff.Play();
    }
    #endregion
}

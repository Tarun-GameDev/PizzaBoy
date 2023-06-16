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
    [SerializeField] int pizzasCollected = 0;
    [SerializeField] Rig characterRig;
    [SerializeField] Animator animator;
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
    GameObject leftDelivaryPos;
    GameObject rightDelivaryPos;
    GameObject delivaryPizzaPos;


    [SerializeField] LineRenderer rightLineRenderer;
    [SerializeField] LineRenderer leftLineRenderer;
    Transform startPoint;
    Vector3 startPointOffset;
    Transform rightendPoint;
    Vector3 rightendPointOffset = new Vector3(.8f,0f,.2f);
    Transform leftendPoint;
    Vector3 leftendPointOffset;
    Vector3 controlPoint;
    int bezierCurveResolution = 10;

    public bool levelCompleted = false;
    bool bonusPointsCompleted = false;
    public bool pizzasDelivered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        rightLineRenderer.positionCount = bezierCurveResolution;
        leftLineRenderer.positionCount = bezierCurveResolution;
        rightLineRenderer.enabled = false;
        leftLineRenderer.enabled = false;

        if(characterRig != null)
            characterRig.weight = 0f;        

    }

    private void Update()
    {
        if (bonusPointsCompleted)
            return;

        if(!levelCompleted)
        {
            #region androidControllers
            /*
            movement = Input.GetAxis("Horizontal");

            if (movement > .1f && !right)
            {
                xPos = 2f;
                animator.Play("MoveSide-R");
                right = true;
            }
            else if (movement < -.1f && right)
            {
                xPos = -2f;
                animator.Play("MoveSide-L");
                right = false;
            }*/

            #endregion


            #region MObileCOmtrollers

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPos = Input.GetTouch(0).position;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                currentTouchPos = Input.GetTouch(0).position;
                touchDistance = currentTouchPos - startTouchPos;
                xScrennPos = touchDistance.x;

                /*
                if (!stopTouch)
                {
                    if (!right && touchDistance.x > swipeRange)
                    {
                        xPos = 2f;
                        animator.Play("MoveSide-R");
                        stopTouch = true;
                        right = true;
                    }
                    else if (right && touchDistance.x < -swipeRange)
                    {
                        xPos = -2f;
                        animator.Play("MoveSide-L");
                        stopTouch = true;
                        right = false;
                    }
                }*/

            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                stopTouch = false;
                xScrennPos = 0f;
            }


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

        }


        //rb.position = Vector3.Lerp(rb.position, new Vector3(xPos, rb.position.y, rb.position.z), Time.deltaTime * sideMoveSpeed);
        rb.position += new Vector3(xScrennPos * sensitivity * Time.deltaTime, 0f, 0f);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.6f, 3.6f), transform.position.y, transform.position.z);
        
       
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

    private void OnTriggerEnter(Collider other)
    {
        Collider col = other;

        if(col.CompareTag("PizzaBox"))
        {
            if (characterRig != null)
                characterRig.weight = 1f;
            SetBoxPos(col.gameObject);
        }


        if (col.CompareTag("HouseRange"))
        {
            delivaryPizzaPos = col.GetComponent<PizzaDelivary>().delivaryPos;
            DeliverPizzas(col.GetComponent<PizzaDelivary>().noOfPizzas);
            pizzasDelivered = true;
            /*
            startPoint = this.transform;
            rightendPoint = rightDelivaryPos.transform;
            leftendPoint = leftDelivaryPos.transform;
            */
            inHouseRange = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HouseRange"))
        {
            inHouseRange = false;
            DelivaryHouseObj = null;
            pizzasDelivered = false;
            /*
            rightLineRenderer.enabled = false;
            leftLineRenderer.enabled = false;*/
        }
    }


    void FixedUpdate()
    {
        if (bonusPointsCompleted)
            return;

        rb.velocity = new Vector3(0f, rb.position.y, moveSpeed * Time.fixedDeltaTime);
    }

    public void AddBox(int _noOfBoxes)
    {
        if(pizzasCollected == 0)
        {
            if (characterRig != null)
                characterRig.weight = 1f;
        }


        for (int i = 0; i < _noOfBoxes; i++)
        {
            var _obj = Instantiate(pizzaBoxPrefab);
            SetBoxPos(_obj);
        }
    }

    public void RemoveBox(int _noOfBoxes)
    {
        for (int i = 0; i < _noOfBoxes; i++)
        {
            if(pizzasCollected >= 1)
            {
                Destroy(pizzaBoxeArray[pizzasCollected-1]);
                pizzaBoxeArray.RemoveAt(pizzasCollected - 1);
                pizzasCollected--;
            }
        }

        if(pizzasCollected == 0)
        {
            if (characterRig != null)
                characterRig.weight = 0f;
        }
        
        LevelManager.instance.currentPizzasCollected = pizzasCollected;
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
        for (int i = pizzasCollected-1; i >= _indexFrom; i--)
        {
            GameObject _box = pizzaBoxeArray[i];
            
            _box.GetComponent<Collider>().isTrigger = false;
            _box.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(_box, 3f);
            pizzaBoxeArray.RemoveAt(i);
            pizzasCollected--;
        }

        LevelManager.instance.currentPizzasCollected = pizzasCollected;
    }

    public void DeliverPizzas(int _amount)
    {
        
        int _pizzas = pizzasCollected - _amount;

        for (int i = pizzasCollected - 1; i >= _pizzas; i--)
        {
            if (pizzasCollected >= 1)
            {
                GameObject _box = pizzaBoxeArray[i];
                _box.transform.parent = null;
                _box.GetComponent<PizzaBox>().moveTowardsPos(delivaryPizzaPos);
                pizzaBoxeArray.RemoveAt(i);
                pizzasCollected--;
            }
            else
            {
                Debug.Log("Out of Pizzas");
            }

        }

        if (pizzasCollected == 0)
        {
            if (characterRig != null)
                characterRig.weight = 0f;
        }

        LevelManager.instance.currentPizzasCollected = pizzasCollected;

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

        //play animation
        //stop move
    }
}

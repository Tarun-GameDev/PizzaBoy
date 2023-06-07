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
    [SerializeField] bool right = true;
    [SerializeField] GameObject pizzaBoxPrefab;
    [SerializeField] List<GameObject> pizzaBoxeArray = new List<GameObject>();
    [SerializeField] bool inHouseRange = false;



    public Vector2 startTouchPos;
    public Vector2 currentTouchPos;
    public Vector2 endTouchPos;
    public bool stopTouch = false;

    public float swipeRange;

    public float xScrennPos;
    [SerializeField] GameObject delivaryPizzaPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        if(characterRig != null)
            characterRig.weight = 0f;        

    }

    private void Update()
    {

        #region androidControllers
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
        }
        #endregion




        #region MObileCOmtrollers

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentTouchPos = Input.GetTouch(0).position;
            Vector2 Distance = currentTouchPos - startTouchPos;
            xScrennPos = Distance.x;
            if (!stopTouch)
            {
                if (!right &&  Distance.x > swipeRange)
                {
                    xPos = 2f;
                    animator.Play("MoveSide-R");
                    stopTouch = true;
                    right = true;
                }
                else if (right && Distance.x < -swipeRange)
                {
                    xPos = -2f;
                    animator.Play("MoveSide-L");
                    stopTouch = true;
                    right = false;
                }
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
        }
            #endregion


            rb.position = Vector3.Lerp(rb.position, new Vector3(xPos, rb.position.y, rb.position.z), Time.deltaTime * sideMoveSpeed);

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PizzaBox"))
        {
            if (characterRig != null)
                characterRig.weight = 1f;
            SetBoxPos(other.gameObject);
        }

        /*
        if (other.CompareTag("HouseRange"))
        {
            inHouseRange = true;
        }
        else
            inHouseRange = false;*/

    }


    private void OnTriggerStay(Collider other)
    {

        if(other.CompareTag("HouseRange"))
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("FFe");
                delivaryPizzaPos = other.GetComponent<PizzaDelivary>().leftPizzaDelivary;
                DeliverPizzas(1);
                
            }
        }
    }


    void FixedUpdate()
    {
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
        
        SceneManager.instance.currentPizzasCollected = pizzasCollected;
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
        SceneManager.instance.currentPizzasCollected = pizzasCollected;
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

        SceneManager.instance.currentPizzasCollected = pizzasCollected;
    }

    public void DeliverPizzas(int _amount)
    {
        for (int i = pizzasCollected - 1; i >= _amount; i--)
        {
            GameObject _box = pizzaBoxeArray[i];
            _box.GetComponent<PizzaBox>().moveTowardsPos(delivaryPizzaPos); 
            pizzaBoxeArray.RemoveAt(i);
            pizzasCollected--;
        }

        SceneManager.instance.currentPizzasCollected = pizzasCollected;
    }
}

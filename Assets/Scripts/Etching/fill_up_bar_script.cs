using UnityEngine;
using UnityEngine.UI;

public class fill_up_bar_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Renderer acidRenderer;
    private int endResult = -1;
    public Text announcer;
    public Text counter;
    public GameObject target;
    private bool barGrowing = false;
    private float timer = 0;
    public float growthCooldown = 0.15f;
    public Vector3 targetVector;
    public float etchTolerance;
    private Vector3 scaleVector = new Vector3 (8f,0,0);
    private Vector3 resetVector;// = new Vector3 (0,1,1);
    //Counts how many more times the player is allowed to dip the wafer
    public int dipsLeft;
    [SerializeField] private GameObject etchingTopLevel;
    public bool minigame_active = false;
    [SerializeField] private GameObject etchant;
    
    void Start()
    {
        resetVector = transform.localScale;
        //target vector is size of target
        targetVector = new Vector3 (280f, target.transform.localScale.y,target.transform.localScale.z);
        //bar width starts at 0
        // transform.localScale = new Vector3 (0,1,1);
        // announcer.text = "ETCH!";
        dipsLeft = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(minigame_active)
        {
            Active_Update();
        }
        
    }

    void Active_Update()
    {
        //Invert state if spacebar is pressed
        if(Input.GetKeyDown(KeyCode.Space) && dipsLeft > 0){
            barGrowing = !barGrowing;
            
            //dipsLeft is only decremented when the wafer is outside the acid
            if(!barGrowing){
                dipsLeft--;
                if(dipsLeft > 0) announcer.text = "Checking!";
            }
        }
        //State 1: Wafer has been lowered into the acid, etching process is happening
        if(barGrowing){
            // Debug.Log("Bar Growing");
            if(dipsLeft > 0) announcer.text = "Etching!";
            // acidRenderer.sortingLayerID = SortingLayer.NameToID("Acid high");
            gameObject.transform.SetAsFirstSibling();
            etchant.transform.SetAsLastSibling();
            if(timer <= growthCooldown){
                timer += Time.deltaTime;
            }else{
                transform.localScale += scaleVector;
                timer=0;
            }
        
            
        //State 2: Wafer is above acid, etching process is paused
        }else{
            if(dipsLeft > 0) announcer.text = "Checking!";
            // acidRenderer.sortingLayerID = SortingLayer.NameToID("Acid low");
            gameObject.transform.SetAsLastSibling();
            etchant.transform.SetAsFirstSibling();
            counter.text = dipsLeft.ToString();


            endResult = checkForWin(); 
            if(endResult == 1){
                dipsLeft = 0;
                announcer.text = "You won!";
                GameInputs.Instance.EnablePlayerActions();
                Destroy(etchingTopLevel, 0.5f);
            }else if(dipsLeft == 0){
                if(endResult == 0){
                    announcer.text = "You under etched!";
                    minigame_active = false;
                    Invoke("Reset", 1f);
                }else if(endResult == 2){
                    announcer.text = "You over etched!";
                    minigame_active = false;
                    Invoke("Reset", 1f);
                }
            }
        }  
        //Reset button for debugging
        // if(Input.GetKeyDown(KeyCode.R)){
        //     transform.localScale = resetVector;
        //     Debug.Log("R pressed");
        // }

        //0 = under, 1 = success, 2 = over
        int checkForWin(){
            //Game is won if target - tolerance < bar size < target + tolerance
            if((transform.localScale.x >= targetVector.x - etchTolerance/2.5) && (transform.localScale.x<= targetVector.x + etchTolerance)){
                return 1;
            }else if(transform.localScale.x < targetVector.x){
                return 0;
            }else{
                return 2;
            }
        }
    }

    public void begin()
    {
        minigame_active = true;
    }

    public void Reset()
    {
        timer = 0;
        endResult = -1; 
        transform.localScale = resetVector;
        // minigame_active = false;
        //target vector is size of target
        targetVector = new Vector3 (280f, target.transform.localScale.y,target.transform.localScale.z);
        //bar width starts at 0
        // transform.localScale = new Vector3 (0,1,1);
        // announcer.text = "ETCH!";
        barGrowing = false;
        dipsLeft = 3;
        counter.text = dipsLeft.ToString();
        minigame_active = true;
        // Debug.Log("etching reset");
    }
}

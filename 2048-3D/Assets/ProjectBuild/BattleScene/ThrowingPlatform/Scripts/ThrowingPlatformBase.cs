using System;
using System.Collections;
using UnityEngine;

public abstract class ThrowingPlatformBase : MonoBehaviour, IThrowingPlatform
{
    #region Serialized Fields
    [Header("Platform Settings")]
    [SerializeField, Min(0)] protected float widthZone;
    [SerializeField] protected Transform itemTarget;
    [SerializeField] private float impulseCollision = 10f;
    [SerializeField] private float respawnTimeItem;
    [SerializeField] private float speedItem = 2500f;

    [Header("Item Probabilities")]

    [SerializeField] protected int probably2;
    [SerializeField] protected int probably4;


    [Header("Object Pool Settings")]
    [SerializeField] protected ThrowableItemBase itemInstance;
    [SerializeField] protected Transform container;
    [SerializeField] protected int beginCountItems = 10;
    #endregion

    #region Protected Fields
    protected Camera cameraMain;
    //protected ObjectPool<ThrowableItemBase> ObjectPool { get; private set; }

    public ObjectPool<ThrowableItemBase> ObjectPool { get; protected set; }

    protected IInput InputTouch { get; set; }
    protected IInputCurrentSelected CurrentItemSelector { get; set; }
    protected IInputThrowigPlatform InputThrowigPlatform { get; set; }
    #endregion

    #region Public Properties
    public bool CanDragSelected { get; protected set; } = false;
    public ThrowableItemBase CurrentItem { get; protected set; }
    public bool CanDrag { get; set; } = true;


    #endregion

    #region Events
    public event Action OnTouch;
    public event Action OnThrow;
    public event Action<int> OnActivatedDeactivated;

    #endregion

    private void Awake()
    {
        Initialization();
    }

    private void Start()
    {
        SetNewItem();

        OnThrow += ThrowCurrentItem;

    }

    private void Update()
    {
        if (CanDrag)
        {
            InputThrowigPlatform?.Update();
        }
    }

    private void OnDrawGizmos/*Selected*/()
    {
        DrawIntervalWidthZone();
    }

    private void DrawIntervalWidthZone()
    {
        Gizmos.color = Color.red;

        Vector3 position = transform.position;

        Gizmos.DrawSphere(new Vector3(position.x + widthZone, position.y, position.z), 0.4f);
        Gizmos.DrawSphere(new Vector3(position.x - widthZone, position.y, position.z), 0.4f);
    }

    protected virtual void Initialization()
    {
        ObjectPool = new ObjectPool<ThrowableItemBase>(itemInstance, container, beginCountItems);
        cameraMain = Camera.main;

        InputTouch = new InputTouchDefault(cameraMain);
        CurrentItemSelector = new CurrentItemSelected(cameraMain);
        InputThrowigPlatform = new InputThrowigPlatformDefault(this);

    }

    protected void SetNewItem()
    {
        if (CurrentItem == null)
        {
            int cubeNumber = ProbabilityGenerator.GenerateWithProbability(probably4, 4, 2);

            ThrowableItemBase item = ObjectPool.Get(true);

            item.InitializationDataItem(new ThrowableItemData()
            {
                impulse = impulseCollision,
                number = cubeNumber
            }, OnActivatedDeactivated);
                
            AttachItemToTarget(item);

        }
    }

    protected void ThrowCurrentItem()
    {
        if (CurrentItem != null)
        {
            CurrentItem.Throw(new Vector3(0, speedItem, 0));

            ResetItemDetachAndResetItem();

            StartCoroutine(ReloadItemAfterThrow());
        }
    }

    protected IEnumerator ReloadItemAfterThrow()
    {
        yield return new WaitForSeconds(respawnTimeItem);

        SetNewItem();
    }


    protected void AttachItemToTarget(ThrowableItemBase item)
    {
        item.transform.parent = itemTarget.transform;

        item.transform.localPosition = Vector3.zero;


        CurrentItem = item;
    }

    protected void ResetItemDetachAndResetItem()
    {
        CurrentItem.transform.parent = null;

        itemTarget.localPosition = Vector3.zero;

        CurrentItem = null;
    }


    private Vector3 GetTouchPosition()
    {
        return InputTouch.GetPosition();
    }

    private Vector3 ClampPositionWithinZone(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, -widthZone, widthZone);
        position.y = 0;
        position.z = 0;
        return position;
    }


    private void ApplyPositionToTarget(Vector3 position)
    {
        itemTarget.localPosition = position;
    }

    public void BeginDrag()
    {
        if (CurrentItemSelector.IsCurrentItemSelected(CurrentItem))
        {
            CanDragSelected = true;
            OnTouch?.Invoke();
        }

    }

    public void Drag()
    {

        if (CanDragSelected)
        {
            Vector3 pos = GetTouchPosition();

            pos = ClampPositionWithinZone(pos);

            ApplyPositionToTarget(pos);
        }

    }

    public void EndDrag()
    {

        if (CanDragSelected)
        {
            CanDragSelected = false;
            OnThrow?.Invoke();
        }

    }
}
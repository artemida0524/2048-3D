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
    [SerializeField] private Vector3 direction;

    [Header("Item Probabilities")]

    [SerializeField] protected int probably2;
    [SerializeField] protected int probably4;

    [Header("Object Pool Settings")]
    [SerializeField] protected ThrowableItemBase itemInstance;
    [SerializeField] protected Transform container;
    [SerializeField] protected int beginCountItems = 10;
    public ObjectPool<ThrowableItemBase> ObjectPool { get; protected set; }
    #endregion

    #region Protected Fields
    protected Camera cameraMain;

    protected ItemHandlerInPlatform itemHandler;

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
    public event Action<int> OnDetectSameItem;

    #endregion

    private void Awake()
    {
        Initialization();
    }

    private void Start()
    {

        OnThrow += ThrowCurrentItem;

        StartCoroutine(CreateItemWithDelay());

    }

    private void Update()
    {
        if (CanDrag)
        {
            InputThrowigPlatform?.Update();
        }
    }

    private void OnDrawGizmos()
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

        InputTouch = new InputTouchDefault(cameraMain, itemTarget);
        CurrentItemSelector = new CurrentItemSelected(cameraMain);
        InputThrowigPlatform = new InputThrowigPlatformDefault(this);
        itemHandler = new ItemHandlerInPlatform(this);

    }

    protected void SetNewItem()
    {
        if (CurrentItem == null)
        {
            ThrowableItemBase item = itemHandler.CreateNewItem(probably4, impulseCollision, OnDetectSameItem);

            CurrentItem = itemHandler.AttachItemToTarget(item, itemTarget);
        }
    }

    protected void ThrowCurrentItem()
    {
        if (CurrentItem != null)
        {

            itemHandler.ThrowItem(CurrentItem, /*new Vector3(0, 0, speedItem)*/direction * speedItem);

            itemHandler.ResetItemDetachAndResetItem(CurrentItem, itemTarget);
            CurrentItem = null;

            StartCoroutine(CreateItemWithDelay());

        }
    }

    protected IEnumerator CreateItemWithDelay()
    {
        yield return new WaitForSeconds(respawnTimeItem);

        SetNewItem();
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
            Vector3 position = GetTouchPosition();

            position = ClampPositionWithinZone(position);

            ApplyPositionToTarget(position);
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
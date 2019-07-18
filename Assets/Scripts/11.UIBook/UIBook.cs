using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBook : MonoBehaviour
{
    private RectTransform _rect;
    /// <summary>
    /// 正在翻页的书页的左面
    /// </summary>
    private TheDraggingPage _leftSideOfPage;
    /// <summary>
    /// 正在翻页的书页的右面
    /// </summary>
    private TheDraggingPage _rightSideOfPage;
    /// <summary>
    /// 左侧静止页面
    /// </summary>
    private Page _leftPage;
    /// <summary>
    /// 右侧静止页面
    /// </summary>
    private Page _rightPage;

    private Sprite[] _bookSprites;

    private int _currentLeftId;
    private BookModel _model;
    private RectTransform _clippingMask;
    private DragPageBase _dragPage;
    private float _aniDuration = 0.5f;
    private bool _isDragging;

    public int CurrentLeftId
    {
        get
        {
            return _currentLeftId;
        }

        set
        {
            _currentLeftId = value;

            if (_currentLeftId < -1)
            {
                _currentLeftId = -1;
            }
            else if (_currentLeftId > _bookSprites.Length - 1)
            {
                _currentLeftId = _bookSprites.Length - 1;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas;

        InitComponent(out canvas);

        InitData(canvas);

        UpdateID(_isDragging);
    }

    private void InitComponent(out Canvas canvas)
    {
         _rect = GetComponent<RectTransform>();
        canvas = null;

        foreach (Canvas c in _rect.GetComponentsInParent<Canvas>())
        {
            if (c.isRootCanvas)
            {
                canvas = c;
                break;
            }
        }

        _clippingMask = _rect.Find("ClippingMask").GetComponent<RectTransform>();
        _rightPage = _rect.Find("RightPage").gameObject.AddComponent<Page>();
        _leftPage = _rect.Find("LeftPage").gameObject.AddComponent<Page>();
        _leftSideOfPage = _rect.Find("LeftSide").gameObject.AddComponent<TheDraggingPage>();
        _rightSideOfPage = _rect.Find("RightSide").gameObject.AddComponent<TheDraggingPage>();

        _rect.Find("RightDragButton").gameObject
            .AddComponent<DragButton>()
            .Init(OnMouseDragRightPage, OnUpdateDage, OnEndDragRightPage);

        _rect.Find("LeftDragButton").gameObject
           .AddComponent<DragButton>()
           .Init(OnMouseDragLeftPage, OnUpdateDage, OnEndDragLeftPage);

        _rightPage.Init(GetSprite);
        _leftPage.Init(GetSprite);
        _leftSideOfPage.Init(GetSprite);
        _rightSideOfPage.Init(GetSprite);
    }

    private Sprite GetSprite(int index)
    {
        if (index >= 0 && index < _bookSprites.Length)
        {
            return _bookSprites[index];
        }

        return null;
    }

    private void InitData(Canvas canvas)
    {
        _model = new BookModel();

        _bookSprites = Resources.LoadAll<Sprite>("Book");

        if (_bookSprites.Length > 0)
        {
            _rect.sizeDelta = new Vector2(_bookSprites[0].rect.width * 2, _bookSprites[0].rect.height);
        }

        CurrentLeftId = -1;
        _isDragging = false;

        float scaleFactor = 1;
        if (canvas != null)
            scaleFactor = canvas.scaleFactor;
        //计算屏幕上书页的显示尺寸
        float pageWidth = _rect.rect.width*scaleFactor/2;
        float pageHeight = _rect.rect.height * scaleFactor;

        Vector3 pos = _rect.position + Vector3.down*pageHeight/2;
        _model.BottomCenter = World2LoaclPos(pos);

        pos = _rect.position + Vector3.down*pageHeight/2 + Vector3.right*pageWidth;
        _model.RightCorner = World2LoaclPos(pos);

        pos = _rect.position + Vector3.up * pageHeight / 2;
        _model.TopCenter = World2LoaclPos(pos);

        pos = _rect.position + Vector3.down * pageHeight / 2 + Vector3.left * pageWidth;
        _model.LeftCorner = World2LoaclPos(pos);

        float width = _rect.rect.width/2;
        float height = _rect.rect.height;

        _model.PageWidth = width;
        _model.PageDiagonal = Mathf.Sqrt(Mathf.Pow(width, 2) + Mathf.Pow(height, 2));

        _clippingMask.sizeDelta = new Vector2(_model.PageDiagonal,_model.PageDiagonal + _model.PageWidth);
        _model.ClippingPivotY = _model.PageWidth/_clippingMask.sizeDelta.y;

        _leftSideOfPage.InitShadow(new Vector2(_model.PageDiagonal, _model.PageDiagonal));
        _rightSideOfPage.InitShadow(new Vector2(_model.PageDiagonal, _model.PageDiagonal));
    }

    public RectTransform GetClippingMask()
    {
        return _clippingMask;
    }

    private Vector3 World2LoaclPos(Vector3 world)
    {
        return _rect.InverseTransformPoint(world);
    }
    public Vector3 Local2WorldPos(Vector3 local)
    {
        return _rect.TransformPoint(local);
    }

    private void UpdateID(bool isDragging)
    {
        if (isDragging)
        {
            _leftPage.ID = CurrentLeftId;
            _leftSideOfPage.ID = CurrentLeftId + 1;
            _rightSideOfPage.ID = CurrentLeftId + 2;
            _rightPage.ID = CurrentLeftId + 3;
        }
        else
        {
            _leftPage.ID = CurrentLeftId;
            _rightPage.ID = CurrentLeftId + 1;
        }
    }

    private void OnMouseDragRightPage()
    {
        if (CurrentLeftId < _bookSprites.Length - 1)
        {
            _isDragging = true;
            _dragPage = new DragRightPage(this, _model, _leftSideOfPage, _rightSideOfPage, _rightPage.transform.position);
            _dragPage.BeginDragPage(World2LoaclPos(Input.mousePosition));
            UpdateID(_isDragging);
        }
    }

    private void OnMouseDragLeftPage()
    {
    
        if (CurrentLeftId > 0)
        {
            _dragPage = new DragLeftPage(this, _model, _rightSideOfPage, _leftSideOfPage, _leftPage.transform.position);
            _dragPage.BeginDragPage(World2LoaclPos(Input.mousePosition));
            _isDragging = true;
            CurrentLeftId -= 2;
            UpdateID(_isDragging);
        }
    }

    private void OnUpdateDage()
    {
        if(_isDragging)
            _dragPage.UpdateDrag();
    }

    private void OnEndDragRightPage()
    {
        if (_isDragging)
        {
            _isDragging = false;
            bool isLeft = JudgeCornerIsLeft();
            _dragPage.EndDragPage(() => AniEnd(isLeft));
        }
       
    }

    private void OnEndDragLeftPage()
    {
        if (_isDragging)
        {
            _isDragging = false;
            bool isLeft = JudgeCornerIsLeft();
            _dragPage.EndDragPage(() => AniEnd(isLeft));
        }
    }

    private void AniEnd(bool isLeft)
    {
        if (isLeft)
            CurrentLeftId += 2;
    }

    private bool JudgeCornerIsLeft()
    {
        return _model.CurrentPageCorner.x < _model.BottomCenter.x;
    }

    public Vector3 GetClickPos()
    {
        if (_isDragging)
        {
            return World2LoaclPos(Input.mousePosition);
        }
        else
        {
            return _model.ClickPoint;
        }
    }

    public Vector3 CulculateDraggingCorner(Vector3 click)
    {
        Vector3 corner = Vector3.zero;

        corner = LimitBotomCenter(click);
        
        return LimitTopCenter(corner);
    }

    private Vector3 LimitBotomCenter(Vector3 click)
    {
        Vector3 offset = click - _model.BottomCenter;
        float radians = Mathf.Atan2(offset.y, offset.x);

        Vector3 cornerLimit = new Vector3(
            _model.PageWidth * Mathf.Cos(radians)
            , _model.PageWidth * Mathf.Sin(radians))
            + _model.BottomCenter;

        float distance = Vector2.Distance(click, _model.BottomCenter);

        if (distance < _model.PageWidth)
        {
            return click;
        }
        else
        {
            return cornerLimit;
        }
    }

    private Vector3 LimitTopCenter(Vector3 corner)
    {
        Vector3 offset = corner - _model.TopCenter;
        float radians = Mathf.Atan2(offset.y, offset.x);

        Vector3 cornerLimit = new Vector3(
           _model.PageDiagonal * Mathf.Cos(radians)
           , _model.PageDiagonal * Mathf.Sin(radians))
           + _model.TopCenter;

        float distance = Vector2.Distance(corner, _model.TopCenter);

        if (distance > _model.PageDiagonal)
            return cornerLimit;

        return corner;
    }

    public float CulculateFoldAngle(Vector3 corner,Vector3 bookCorner,out Vector3 bottomCrossPoint)
    {
        Vector3 twoCornerCenter = (corner + bookCorner)/2;
        Vector3 offset = bookCorner - twoCornerCenter;
        float randians = Mathf.Atan2(offset.y, offset.x);
        float offsetX = twoCornerCenter.x - offset.y*Mathf.Tan(randians);
        offsetX = LimitOffsetX(offsetX, bookCorner, _model.BottomCenter);
        bottomCrossPoint = new Vector3(offsetX,_model.BottomCenter.y);
        
        offset = bottomCrossPoint - twoCornerCenter;
        return Mathf.Atan(offset.y/offset.x)*Mathf.Rad2Deg; //randians to degress
    }

    private float LimitOffsetX(float offsetX, Vector3 bookCorner,Vector3 bottomCenter)
    {
        if (offsetX > bottomCenter.x && bottomCenter.x > bookCorner.x)
            return bottomCenter.x;

        if(offsetX < bottomCenter.x && bottomCenter.x < bookCorner.x)
            return bottomCenter.x;

        return offsetX;
    }


    public void FlipAni(Vector3 target, Action onComplete)
    {
        StartCoroutine(PageAni(target, _aniDuration, () =>
        {
            if (onComplete != null)
                onComplete();
            ResetState();
        }));
    }

    private void ResetState()
    {
        UpdateID(_isDragging);
        _leftSideOfPage.SetActivevState(false);
        _rightSideOfPage.SetActivevState(false);
    }

    private IEnumerator PageAni(Vector3 target,float duration,Action onComplete)
    {
        Vector3 offset = (target - _model.ClickPoint) /duration;
        float symbol = (target - _model.ClickPoint).x;

        yield return new WaitUntil(() =>
        {
            _model.ClickPoint += offset*Time.deltaTime;
            _dragPage.UpdateDrag();

            if (symbol > 0)
            {
                return _model.ClickPoint.x >= target.x;
            }
            else
            {
                return _model.ClickPoint.x <= target.x;
            }
        });

        if (onComplete != null)
            onComplete();
    }
}

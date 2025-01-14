using System.ComponentModel;
using System.Runtime.CompilerServices;
using MouseControl.Controller;

namespace MouseControl;
public class Preferences : INotifyPropertyChanged
{
    private bool _enable = false;
    public bool isEnable
    {
        get => _enable;
        set
        {
            _enable = value;
            OnPropertyChanged();
        }
    }

    private bool _showCursor = true;
    public bool isShowCursor
    {
        get => _showCursor;
        set
        {
            _showCursor = value;
            OnPropertyChanged();
        }
    }

    private bool _boundCursor = true;
    public bool isBoundCursor
    {
        get => _boundCursor;
        set
        {
            _boundCursor = value;
            OnPropertyChanged();
        }
    }

    private bool _RLControl = true;
    public bool isRLControl
    {
        get => _RLControl;
        set
        {
            _RLControl = value;
            OnPropertyChanged();
        }
    }

    private float _sideRatio = 0.75f;
    public float SideRatio
    {
        get => _sideRatio;
        set
        {
            _sideRatio = value;
            OnPropertyChanged();
        }
    }

    private int _cursorScale = 1;
    public int CursorScale
    {
        get => _cursorScale;
        set
        {
            _cursorScale = value;
            OnPropertyChanged();
        }
    }

    private MouseBinding _mouseBinding = new MouseBinding();
    public MouseBinding MouseBinding
    {
        get => _mouseBinding;
        set
        {
            _mouseBinding = value;
            OnPropertyChanged();
        }
    }

    #region INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
    }
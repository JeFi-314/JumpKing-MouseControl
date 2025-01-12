using System.ComponentModel;
using System.Runtime.CompilerServices;
using JumpKing;
using Microsoft.Xna.Framework.Input;
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
            CursorManager.SetVisible(value && isShowCursor);
            CursorManager.SetClipCursor(value && isClipCursor);
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
            CursorManager.SetVisible(value);
            _showCursor = value;
            OnPropertyChanged();
        }
    }

    private bool _clipCursor = true;
    public bool isClipCursor
    {
        get => _clipCursor;
        set
        {
            CursorManager.SetClipCursor(value);
            _clipCursor = value;
            OnPropertyChanged();
        }
    }

    private bool _RLControl = true;
    public bool isRLControl
    {
        get => _RLControl;
        set
        {
            if (!value) CursorManager.SetCursor("Normal");
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

    #region INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
    }
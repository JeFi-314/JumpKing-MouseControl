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
            MouseIcon.SetVisible(value && isShowCursor);
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
            MouseIcon.SetVisible(value);
            _showCursor = value;
            OnPropertyChanged();
        }
    }

    private bool _RLControl = true;
    public bool isRLControl
    {
        get => _RLControl;
        set
        {
            if (!value) MouseIcon.SetCursor("Normal");
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
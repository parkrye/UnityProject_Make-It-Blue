using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseUI : MonoBehaviour
{
    private readonly Dictionary<string, RectTransform> _contents = new Dictionary<string, RectTransform>();
    private readonly Dictionary<string, UITemplate> _templates = new Dictionary<string, UITemplate>();
    private readonly Dictionary<string, PointerButton> _buttons = new Dictionary<string, PointerButton>();
    private readonly Dictionary<string, TMP_Text> _texts = new Dictionary<string, TMP_Text>();
    private readonly Dictionary<string, Slider> _sliders = new Dictionary<string, Slider>();
    private readonly Dictionary<string, Image> _images = new Dictionary<string, Image>();
    private readonly Dictionary<string, ToggleGroup> _toggleGroups = new Dictionary<string, ToggleGroup>();
    private readonly Dictionary<string, Toggle> _toggles = new Dictionary<string, Toggle>();
    private readonly Dictionary<string, TMP_Dropdown> _dropdowns = new Dictionary<string, TMP_Dropdown>();
    private readonly Dictionary<string, TMP_InputField> _inputFields = new Dictionary<string, TMP_InputField>();


    public UnityEvent OnCloseEvent = new UnityEvent();

    protected virtual void Awake()
    {
        BindingChildren();
        OnInit().Forget();
    }

    protected virtual void BindingChildren()
    {
        var childrenRect = GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < childrenRect.Length; i++)
        {
            var key = childrenRect[i].name;
            if (key.Contains("!") || key.Contains("(") || key.Contains(")"))
                continue;

            if (!_contents.ContainsKey(key))
            {
                _contents[key] = childrenRect[i];

                var tmp = childrenRect[i].GetComponent<UITemplate>();
                if (tmp)
                {
                    if (!_templates.ContainsKey(key))
                        _templates[key] = tmp;
                }

                var btn = childrenRect[i].GetComponent<PointerButton>();
                if (btn)
                {
                    if (!_buttons.ContainsKey(key))
                        _buttons[key] = btn;
                }

                var txt = childrenRect[i].GetComponent<TMP_Text>();
                if (txt)
                {
                    if (!_texts.ContainsKey(key))
                        _texts[key] = txt;
                }

                var sld = childrenRect[i].GetComponent<Slider>();
                if (sld)
                {
                    if (!_sliders.ContainsKey(key))
                        _sliders[key] = sld;
                }

                var img = childrenRect[i].GetComponent<Image>();
                if (img)
                {
                    if (!_images.ContainsKey(key))
                        _images[key] = img;
                }

                var tgg = childrenRect[i].GetComponent<ToggleGroup>();
                if (tgg)
                {
                    if (!_toggleGroups.ContainsKey(key))
                        _toggleGroups[key] = tgg;
                }

                var tgl = childrenRect[i].GetComponent<Toggle>();
                if (tgl)
                {
                    if (!_toggles.ContainsKey(key))
                        _toggles[key] = tgl;
                }

                var dd = childrenRect[i].GetComponent<TMP_Dropdown>();
                if (dd)
                {
                    if(!_dropdowns.ContainsKey(key))
                        _dropdowns[key] = dd;
                }

                var inf = childrenRect[i].GetComponent<TMP_InputField>();
                if (inf)
                {
                    if (!_inputFields.ContainsKey(key))
                        _inputFields[key] = inf;
                }
            }
        }
    }

    public virtual async UniTask OnInit()
    {
        await UniTask.NextFrame();
    }

    public virtual void OnOpen()
    {

    }

    public virtual void OnClose()
    {
        OnCloseEvent?.Invoke();
    }

    public bool GetContent(string name, out RectTransform result)
    {
        if (_contents.ContainsKey(name))
        {
            result = _contents[name];
            return true;
        }

        result = null;
        return false;
    }

    public bool GetTemplate(string name, out UITemplate result)
    {
        if (_templates.ContainsKey(name))
        {
            result = _templates[name];
            return true;
        }

        result = null;
        return false;
    }

    public bool GetText(string name, out TMP_Text result)
    {
        if (_texts.ContainsKey(name))
        {
            result = _texts[name];
            return true;
        }

        result = null;
        return false;
    }

    public bool GetButton(string name, out PointerButton result)
    {
        if (_buttons.ContainsKey(name))
        {
            result = _buttons[name];
            return true;
        }

        result = null;
        return false;
    }

    public bool GetSlider(string name, out Slider result)
    {
        if (_sliders.ContainsKey(name))
        {
            result = _sliders[name];
            return true;
        }

        result = null;
        return false;
    }

    public bool GetImage(string name, out Image result)
    {
        if (_images.ContainsKey(name))
        {
            result = _images[name];
            return true;
        }

        result = null;
        return false;
    }

    public bool GetToggleGroup(string name, out ToggleGroup result)
    {
        if (_toggleGroups.ContainsKey(name))
        {
            result = _toggleGroups[name];
            return true;
        }

        result = null;
        return false;
    }

    public bool GetToggle(string name, out Toggle result)
    {
        if (_toggles.ContainsKey(name))
        {
            result = _toggles[name];
            return true;
        }

        result = null;
        return false;
    }

    public bool GetDropDown(string name, out TMP_Dropdown result)
    {
        if (_dropdowns.ContainsKey(name))
        {
            result = _dropdowns[name];
            return true;
        }

        result = null;
        return false;
    }

    public bool GetInputField(string name, out TMP_InputField result)
    {
        if (_inputFields.ContainsKey(name))
        {
            result = _inputFields[name];
            return true;
        }

        result = null;
        return false;
    }
}
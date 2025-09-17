using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectScript : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public bool usingVertical = true;

    public Button[] buttons;

    private bool isLeftSelected = true;
    private bool inputReleased = true; // 入力がニュートラルに戻ったかどうか

    int currentBtn;
    int cannotSelectButton = -1;

    //void Start()
    //{
    //    SetStartButton(0);
    //}

    private void OnEnable()
    {
        SetStartButton(0);
    }

    public void SetStartButton(int buttonIndex)
    {
        EventSystem.current.SetSelectedGameObject(buttons[buttonIndex].gameObject);
        currentBtn = buttonIndex;
    }

    public void Init(int cannotSelect)
    {
        cannotSelectButton = cannotSelect;
    }

    void FixedUpdate()
    {
        float inputSelect = usingVertical ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("Horizontal");

        // 入力がニュートラルに戻ったらフラグを立てる
        if (inputSelect == 0)
        {
            inputReleased = true;
        }

        // 左右入力があり、かつ前回の入力からニュートラルに戻っていたら選択を切り替える
        if (inputSelect > 0.8f && inputReleased)
        {
            //ToggleSelection();
            inputReleased = false; // 入力を受け付けたのでフラグを下げる

            SelectButtonDown();
            //EventSystem.current.SetSelectedGameObject(buttons[currentBtn].gameObject);
        }
        else if (inputSelect < -0.8f && inputReleased)
        {
            //ToggleSelection();
            inputReleased = false; // 入力を受け付けたのでフラグを下げる

            SelectButtonUp();
            //EventSystem.current.SetSelectedGameObject(buttons[currentBtn].gameObject);
        }

        // Submitボタンで現在選択中のボタンを押す
        if (Input.GetButtonDown("Submit"))
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            if (selected != null)
            {
                Button btn = selected.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.Invoke();
                }
            }
        }
    }

    void ToggleSelection()
    {
        if (isLeftSelected)
        {
            EventSystem.current.SetSelectedGameObject(rightButton.gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(leftButton.gameObject);
        }
        isLeftSelected = !isLeftSelected;
    }

    void SelectButtonUp()
    {
        currentBtn += 1;

        if (currentBtn == cannotSelectButton)
            currentBtn += 1;

        if (currentBtn >= buttons.Length)
        {
            //if (btnInGroup == 2)
            //    currentBtn = 1;
            //else
            //    currentBtn = 0;

            currentBtn = 0;
        }

        if (currentBtn == cannotSelectButton)
            currentBtn += 1;

        EventSystem.current.SetSelectedGameObject(buttons[currentBtn].gameObject);
    }

    void SelectButtonDown()
    {
        currentBtn -= 1;

        if (currentBtn == cannotSelectButton)
            currentBtn -= 1;

        //if (btnInGroup == 2)
        //if (currentBtn < (btnInGroup == 2 ? 1 : 0))
        //{
        //    currentBtn = buttons.Length - 1;
        //}

        if (currentBtn < 0)
            currentBtn = buttons.Length - 1;

        if (currentBtn == cannotSelectButton)
            currentBtn -= 1;

        EventSystem.current.SetSelectedGameObject(buttons[currentBtn].gameObject);
    }
}


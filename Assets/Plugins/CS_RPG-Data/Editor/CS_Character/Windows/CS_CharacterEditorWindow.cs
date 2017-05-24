using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;



public class CS_CharacterEditorWindow : EditorWindow
{
    public static CS_CharacterEditorWindow curWindow;

    public CS_CharacterPropertyView propertyView;
    public CS_CharacterWorkView workView;
    public CS_CharacterGraph currentGraph;

    public float viewPercentage = 0.75f;

    public static void InitEditorWindow()
    {
        curWindow = GetWindow<CS_CharacterEditorWindow>();
        curWindow.titleContent.text = "Dialog Editor";

        CreateView();
    }

    void OnEnable()
    {
        Debug.Log("Enabled Windows");
    }

    void OnDestroy()
    {
        Debug.Log("Destroy Window");
    }

    void Update()
    {

    }

    void OnGUI()
    {
        if (propertyView == null || workView == null)
        {
            CreateView();
            return;
        }

        //Get and process Events
        Event e = Event.current;
        ProcessEvents(e);

        // Update Views
        workView.UpdateView(position, new Rect(0f, 0f, viewPercentage, 1f), e, currentGraph);
        propertyView.UpdateView(new Rect(position.width, position.y, position.width, position.height),
                                new Rect(viewPercentage, 0f, 1f - viewPercentage, 1f),
                                e, currentGraph);

        Repaint();
    }

    static void CreateView()
    {
        if (curWindow != null)
        {
            curWindow.propertyView = new CS_CharacterPropertyView();
            curWindow.workView = new CS_CharacterWorkView();
        }
        else
        {
            curWindow = GetWindow<CS_CharacterEditorWindow>();
        }
    }

    void ProcessEvents(Event e)
    {
        if (e.type == EventType.keyDown && e.keyCode == KeyCode.LeftArrow)
        {
            viewPercentage -= 0.01f;
        }
        if (e.type == EventType.keyDown && e.keyCode == KeyCode.RightArrow)
        {
            viewPercentage += 0.01f;
        }
    }
}

#endif

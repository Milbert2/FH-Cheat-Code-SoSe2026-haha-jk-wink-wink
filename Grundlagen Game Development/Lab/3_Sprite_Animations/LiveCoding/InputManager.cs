using SFML.Graphics;
using SFML.Window;

public class InputManager
{
    private static InputManager instance;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InputManager();

            return instance;
        }
    }

    private Dictionary<Keyboard.Key, bool> pressedKeys = new();
    private Dictionary<Keyboard.Key, bool> pressedKeysThisFrame = new();

    private InputManager() { }

    public void Initialize(RenderWindow window)
    {
        window.SetKeyRepeatEnabled(false);

        window.KeyPressed += OnKeyPressed;
        window.KeyReleased += OnKeyReleased;

        pressedKeys[Keyboard.Key.A] = false;
        pressedKeys[Keyboard.Key.W] = false;
        pressedKeys[Keyboard.Key.S] = false;
        pressedKeys[Keyboard.Key.D] = false;
        pressedKeys[Keyboard.Key.Space] = false;

        foreach (var key in pressedKeys.Keys)
            pressedKeysThisFrame[key] = false;
    }

    private void OnKeyReleased(object? sender, KeyEventArgs e)
    {
        if (pressedKeys.ContainsKey(e.Code))
        {
            pressedKeys[e.Code] = false;
        }
    }

    private void OnKeyPressed(object? sender, KeyEventArgs e)
    {
        if (pressedKeys.ContainsKey(e.Code))
        {
            pressedKeys[e.Code] = true;
            pressedKeysThisFrame[e.Code] = true;
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var key in pressedKeysThisFrame.Keys)
            pressedKeysThisFrame[key] = false;
    }

    public bool GetKeyDown(Keyboard.Key key)
    {
        return pressedKeys.ContainsKey(key) ? pressedKeys[key] : false;
    }

    public bool GetKeyPressed(Keyboard.Key key)
    {
        return pressedKeysThisFrame.ContainsKey(key) ? pressedKeysThisFrame[key] : false;
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Ship1.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Ship1: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Ship1()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Ship1"",
    ""maps"": [
        {
            ""name"": ""Ship"",
            ""id"": ""0227ffb6-ede4-489e-97b1-02b32d124785"",
            ""actions"": [
                {
                    ""name"": ""Thrust"",
                    ""type"": ""PassThrough"",
                    ""id"": ""da939334-0c9e-41ea-9713-5df1c20b6060"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Strafe"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ce31cca7-d6cc-442d-a286-40016f972988"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UpDown"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3ded040f-315c-4c2b-8eef-dd6c455c342c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""17d5b04e-2ade-4a6f-a1f5-0f12405dd440"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PitchYaw"",
                    ""type"": ""PassThrough"",
                    ""id"": ""206c594d-0d78-4f8b-8a30-45294a7687ae"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Boost"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2e6a375a-d8ec-4fc3-8c66-c101b024dde9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3279e956-ecca-43d0-94e2-4fb31d5c73b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press,Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootSpecial"",
                    ""type"": ""PassThrough"",
                    ""id"": ""911007e5-27f2-4cda-9319-6e8c113d3279"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootSpecial2"",
                    ""type"": ""PassThrough"",
                    ""id"": ""222787fe-d783-4b33-980a-0e208c686070"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Join"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9c1ae263-9d91-4a8b-9263-dbc6cab46886"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""ThrustKB"",
                    ""id"": ""18e5e420-34d7-4096-9ff9-6bc00100eba2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ef51645b-cb00-43cc-95e5-79fed3764f57"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""77d90213-f5f3-45ad-b681-2318a6c74821"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ThrustController"",
                    ""id"": ""8dbc82f7-e8a1-4ea6-97d6-b9d874f9f9bc"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""72b0c43a-e686-4674-aa21-cb98f02b040d"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7ff705da-5b3c-4572-97d5-175bf76daad8"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""StraveKB"",
                    ""id"": ""91d86f56-2783-4c9e-9aea-5fdd7a704539"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d6e68601-b502-441a-87e0-0de11ed15267"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c00609a3-bd10-4404-af36-45f1fe0106a2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""StraveController"",
                    ""id"": ""8baf7e13-c4d4-433d-acbb-6eece257e335"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c50ae706-dd84-4685-b652-b7c5fa2d8712"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0e7f32a9-a9a0-4359-bae4-3045820f0fdc"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""UpDownKB"",
                    ""id"": ""13f39424-bac3-4cb1-bd1d-de6c87c9463c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpDown"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0cbd68b3-0d95-400d-98ff-925e6d9e9115"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""UpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7409e068-c9c8-427d-a8e4-9dcf0ed3dc3e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""UpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""UpDownController"",
                    ""id"": ""782fb42d-29ba-4a7c-a9a5-0e5e1120e862"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpDown"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""405bb186-6733-4f2a-b92c-3d301120d03b"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""UpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c782b256-a02b-40c2-83c2-5bdc527da33b"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""UpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""RollKB"",
                    ""id"": ""7436cb7c-197c-4a8f-b971-3029a050a944"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""eef62708-f84f-477f-abfa-b4847bad7930"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""760ee917-9fdc-470a-b967-7715ae024255"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""RollController"",
                    ""id"": ""04624340-6fdd-4cbf-8531-6269c340727e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f4f50549-397e-4ad4-8250-7c0e98ade70b"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6b955888-a5c6-4770-b934-0d7d7dbdffe0"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0dbe333a-87a4-4c9b-ab26-d51ed5afb740"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""PitchYaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d192714c-b8a9-460b-90c2-249e1c2ebd9f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PitchYaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b488ad74-c826-4777-98ab-3fb644bb8a79"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98400686-39af-4937-b918-44b5e254dc99"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6568b061-df89-4972-bc2b-92231a5d860d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ShootSpecial"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""889cf6e1-710a-4ddd-9c29-9e72ea2ef1db"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ShootSpecial2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea81ad6f-9172-45f9-9690-92af2c0fe404"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""ShootSpecial2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4abf9cf8-a40a-4ffc-9ba8-1434a9a99084"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""ShootSpecial"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37f3c6fa-cc2b-4cac-bd33-f29d91268a7c"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03851cf0-334d-4bf2-9486-d958ea6e404e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e27b445-549a-4b1f-ac08-9d6cfeda06e1"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad488096-cb27-41e1-bd27-607bdc935028"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Ship
        m_Ship = asset.FindActionMap("Ship", throwIfNotFound: true);
        m_Ship_Thrust = m_Ship.FindAction("Thrust", throwIfNotFound: true);
        m_Ship_Strafe = m_Ship.FindAction("Strafe", throwIfNotFound: true);
        m_Ship_UpDown = m_Ship.FindAction("UpDown", throwIfNotFound: true);
        m_Ship_Roll = m_Ship.FindAction("Roll", throwIfNotFound: true);
        m_Ship_PitchYaw = m_Ship.FindAction("PitchYaw", throwIfNotFound: true);
        m_Ship_Boost = m_Ship.FindAction("Boost", throwIfNotFound: true);
        m_Ship_Shoot = m_Ship.FindAction("Shoot", throwIfNotFound: true);
        m_Ship_ShootSpecial = m_Ship.FindAction("ShootSpecial", throwIfNotFound: true);
        m_Ship_ShootSpecial2 = m_Ship.FindAction("ShootSpecial2", throwIfNotFound: true);
        m_Ship_Join = m_Ship.FindAction("Join", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Ship
    private readonly InputActionMap m_Ship;
    private List<IShipActions> m_ShipActionsCallbackInterfaces = new List<IShipActions>();
    private readonly InputAction m_Ship_Thrust;
    private readonly InputAction m_Ship_Strafe;
    private readonly InputAction m_Ship_UpDown;
    private readonly InputAction m_Ship_Roll;
    private readonly InputAction m_Ship_PitchYaw;
    private readonly InputAction m_Ship_Boost;
    private readonly InputAction m_Ship_Shoot;
    private readonly InputAction m_Ship_ShootSpecial;
    private readonly InputAction m_Ship_ShootSpecial2;
    private readonly InputAction m_Ship_Join;
    public struct ShipActions
    {
        private @Ship1 m_Wrapper;
        public ShipActions(@Ship1 wrapper) { m_Wrapper = wrapper; }
        public InputAction @Thrust => m_Wrapper.m_Ship_Thrust;
        public InputAction @Strafe => m_Wrapper.m_Ship_Strafe;
        public InputAction @UpDown => m_Wrapper.m_Ship_UpDown;
        public InputAction @Roll => m_Wrapper.m_Ship_Roll;
        public InputAction @PitchYaw => m_Wrapper.m_Ship_PitchYaw;
        public InputAction @Boost => m_Wrapper.m_Ship_Boost;
        public InputAction @Shoot => m_Wrapper.m_Ship_Shoot;
        public InputAction @ShootSpecial => m_Wrapper.m_Ship_ShootSpecial;
        public InputAction @ShootSpecial2 => m_Wrapper.m_Ship_ShootSpecial2;
        public InputAction @Join => m_Wrapper.m_Ship_Join;
        public InputActionMap Get() { return m_Wrapper.m_Ship; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShipActions set) { return set.Get(); }
        public void AddCallbacks(IShipActions instance)
        {
            if (instance == null || m_Wrapper.m_ShipActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ShipActionsCallbackInterfaces.Add(instance);
            @Thrust.started += instance.OnThrust;
            @Thrust.performed += instance.OnThrust;
            @Thrust.canceled += instance.OnThrust;
            @Strafe.started += instance.OnStrafe;
            @Strafe.performed += instance.OnStrafe;
            @Strafe.canceled += instance.OnStrafe;
            @UpDown.started += instance.OnUpDown;
            @UpDown.performed += instance.OnUpDown;
            @UpDown.canceled += instance.OnUpDown;
            @Roll.started += instance.OnRoll;
            @Roll.performed += instance.OnRoll;
            @Roll.canceled += instance.OnRoll;
            @PitchYaw.started += instance.OnPitchYaw;
            @PitchYaw.performed += instance.OnPitchYaw;
            @PitchYaw.canceled += instance.OnPitchYaw;
            @Boost.started += instance.OnBoost;
            @Boost.performed += instance.OnBoost;
            @Boost.canceled += instance.OnBoost;
            @Shoot.started += instance.OnShoot;
            @Shoot.performed += instance.OnShoot;
            @Shoot.canceled += instance.OnShoot;
            @ShootSpecial.started += instance.OnShootSpecial;
            @ShootSpecial.performed += instance.OnShootSpecial;
            @ShootSpecial.canceled += instance.OnShootSpecial;
            @ShootSpecial2.started += instance.OnShootSpecial2;
            @ShootSpecial2.performed += instance.OnShootSpecial2;
            @ShootSpecial2.canceled += instance.OnShootSpecial2;
            @Join.started += instance.OnJoin;
            @Join.performed += instance.OnJoin;
            @Join.canceled += instance.OnJoin;
        }

        private void UnregisterCallbacks(IShipActions instance)
        {
            @Thrust.started -= instance.OnThrust;
            @Thrust.performed -= instance.OnThrust;
            @Thrust.canceled -= instance.OnThrust;
            @Strafe.started -= instance.OnStrafe;
            @Strafe.performed -= instance.OnStrafe;
            @Strafe.canceled -= instance.OnStrafe;
            @UpDown.started -= instance.OnUpDown;
            @UpDown.performed -= instance.OnUpDown;
            @UpDown.canceled -= instance.OnUpDown;
            @Roll.started -= instance.OnRoll;
            @Roll.performed -= instance.OnRoll;
            @Roll.canceled -= instance.OnRoll;
            @PitchYaw.started -= instance.OnPitchYaw;
            @PitchYaw.performed -= instance.OnPitchYaw;
            @PitchYaw.canceled -= instance.OnPitchYaw;
            @Boost.started -= instance.OnBoost;
            @Boost.performed -= instance.OnBoost;
            @Boost.canceled -= instance.OnBoost;
            @Shoot.started -= instance.OnShoot;
            @Shoot.performed -= instance.OnShoot;
            @Shoot.canceled -= instance.OnShoot;
            @ShootSpecial.started -= instance.OnShootSpecial;
            @ShootSpecial.performed -= instance.OnShootSpecial;
            @ShootSpecial.canceled -= instance.OnShootSpecial;
            @ShootSpecial2.started -= instance.OnShootSpecial2;
            @ShootSpecial2.performed -= instance.OnShootSpecial2;
            @ShootSpecial2.canceled -= instance.OnShootSpecial2;
            @Join.started -= instance.OnJoin;
            @Join.performed -= instance.OnJoin;
            @Join.canceled -= instance.OnJoin;
        }

        public void RemoveCallbacks(IShipActions instance)
        {
            if (m_Wrapper.m_ShipActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IShipActions instance)
        {
            foreach (var item in m_Wrapper.m_ShipActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ShipActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ShipActions @Ship => new ShipActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IShipActions
    {
        void OnThrust(InputAction.CallbackContext context);
        void OnStrafe(InputAction.CallbackContext context);
        void OnUpDown(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnPitchYaw(InputAction.CallbackContext context);
        void OnBoost(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnShootSpecial(InputAction.CallbackContext context);
        void OnShootSpecial2(InputAction.CallbackContext context);
        void OnJoin(InputAction.CallbackContext context);
    }
}

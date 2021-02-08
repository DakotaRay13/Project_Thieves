// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerScripts/PlayerInputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputControls"",
    ""maps"": [
        {
            ""name"": ""Platforming"",
            ""id"": ""4b7f5120-18b8-45a9-8988-abc4724480f5"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""ce850528-5674-461f-bd33-81456d871af2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5ab8199a-102a-4de0-b04e-bb64cef0d584"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LightAttack"",
                    ""type"": ""Button"",
                    ""id"": ""22010ddc-f903-44c8-89b3-621d5827e6d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HeavyAttack"",
                    ""type"": ""Button"",
                    ""id"": ""ccda6e42-0385-4eab-b2d9-d43fa12ac7bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpecialAttack"",
                    ""type"": ""Button"",
                    ""id"": ""41bfe6d6-ecef-414f-a498-179c0c334851"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""9a1a1892-d9a7-471e-b5ba-0ee87aea7622"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4969875d-ab92-441b-b200-466033dfb4ca"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""edcb127c-bad8-4195-aa81-6b01d24bb9be"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller_Pad"",
                    ""id"": ""7c02f906-13c5-456f-a441-be813e912131"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""2d00604f-2cf0-4385-9ef1-2830c6a74104"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""9849110c-718e-42fb-a030-fb3f902a9f95"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller_Joystick"",
                    ""id"": ""9e92e899-3457-449f-bfd8-152598f76084"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3372a2bb-1c37-44ab-b42f-3b94cd86967f"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ad81d745-1a5a-4c0e-b08e-199dad550429"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d4c6c2d8-b0cc-4977-bf7f-745b406227a1"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6bfb2f4-388e-4f76-88ad-c464291ccb39"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6fe7c4f-e5c6-4c64-93a0-597b1badbfcf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aba8e4b6-fb31-4fa9-b923-91b9ad7c5dfd"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f86170ca-8654-412b-a331-7f4e06c52936"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff3f1c16-fd33-4a77-85cb-d9b45d8dcd02"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""450fc95e-e6b4-457a-9574-5e355ad60506"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bdb9068-e0ef-47e1-a559-d68aacdbda35"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Platforming
        m_Platforming = asset.FindActionMap("Platforming", throwIfNotFound: true);
        m_Platforming_Move = m_Platforming.FindAction("Move", throwIfNotFound: true);
        m_Platforming_Jump = m_Platforming.FindAction("Jump", throwIfNotFound: true);
        m_Platforming_LightAttack = m_Platforming.FindAction("LightAttack", throwIfNotFound: true);
        m_Platforming_HeavyAttack = m_Platforming.FindAction("HeavyAttack", throwIfNotFound: true);
        m_Platforming_SpecialAttack = m_Platforming.FindAction("SpecialAttack", throwIfNotFound: true);
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

    // Platforming
    private readonly InputActionMap m_Platforming;
    private IPlatformingActions m_PlatformingActionsCallbackInterface;
    private readonly InputAction m_Platforming_Move;
    private readonly InputAction m_Platforming_Jump;
    private readonly InputAction m_Platforming_LightAttack;
    private readonly InputAction m_Platforming_HeavyAttack;
    private readonly InputAction m_Platforming_SpecialAttack;
    public struct PlatformingActions
    {
        private @PlayerInputControls m_Wrapper;
        public PlatformingActions(@PlayerInputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Platforming_Move;
        public InputAction @Jump => m_Wrapper.m_Platforming_Jump;
        public InputAction @LightAttack => m_Wrapper.m_Platforming_LightAttack;
        public InputAction @HeavyAttack => m_Wrapper.m_Platforming_HeavyAttack;
        public InputAction @SpecialAttack => m_Wrapper.m_Platforming_SpecialAttack;
        public InputActionMap Get() { return m_Wrapper.m_Platforming; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlatformingActions set) { return set.Get(); }
        public void SetCallbacks(IPlatformingActions instance)
        {
            if (m_Wrapper.m_PlatformingActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump;
                @LightAttack.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnLightAttack;
                @LightAttack.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnLightAttack;
                @LightAttack.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnLightAttack;
                @HeavyAttack.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnHeavyAttack;
                @SpecialAttack.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnSpecialAttack;
            }
            m_Wrapper.m_PlatformingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @LightAttack.started += instance.OnLightAttack;
                @LightAttack.performed += instance.OnLightAttack;
                @LightAttack.canceled += instance.OnLightAttack;
                @HeavyAttack.started += instance.OnHeavyAttack;
                @HeavyAttack.performed += instance.OnHeavyAttack;
                @HeavyAttack.canceled += instance.OnHeavyAttack;
                @SpecialAttack.started += instance.OnSpecialAttack;
                @SpecialAttack.performed += instance.OnSpecialAttack;
                @SpecialAttack.canceled += instance.OnSpecialAttack;
            }
        }
    }
    public PlatformingActions @Platforming => new PlatformingActions(this);
    public interface IPlatformingActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLightAttack(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnSpecialAttack(InputAction.CallbackContext context);
    }
}

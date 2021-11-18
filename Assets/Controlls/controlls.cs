// GENERATED AUTOMATICALLY FROM 'Assets/Controlls/controlls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controlls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controlls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""controlls"",
    ""maps"": [
        {
            ""name"": ""bullet_hell"",
            ""id"": ""f0f737fe-bb61-4e68-a002-2165ff12b565"",
            ""actions"": [
                {
                    ""name"": ""move_rigth"",
                    ""type"": ""Button"",
                    ""id"": ""bf91b95c-d35e-41fb-ba7b-fea2274f20a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""move_left"",
                    ""type"": ""Button"",
                    ""id"": ""d3695ff6-bb93-4396-89d7-566f19a688fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""move_up"",
                    ""type"": ""Button"",
                    ""id"": ""7669a425-2159-41cf-8e20-4f2179b25804"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""move_down"",
                    ""type"": ""Button"",
                    ""id"": ""05c8a075-2460-4b8e-a3cf-105675779aa6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""pause_menu"",
                    ""type"": ""Button"",
                    ""id"": ""82f0b198-e0a7-4278-8de2-262b4a9d0639"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""shoot"",
                    ""type"": ""Button"",
                    ""id"": ""0b8c52fa-a532-48cc-add5-d8fcf69b0865"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""charge"",
                    ""type"": ""Button"",
                    ""id"": ""7502b748-3d25-49d0-80e5-e54c60aa5ce0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""16afa7b9-32b8-46f0-9bbb-b8290e7611fa"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_rigth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3665340-239e-4f2a-a6bb-f5090e035dec"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_rigth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9dcefa7d-ae93-4f47-af23-722c8842ba72"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e84f1ccc-534d-422d-9d71-583321b4aa1f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6eb4bb6-d44d-4af7-982e-446ffa64739a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75bcca6f-cb44-4c75-8b0c-69c626407f9c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91a4d8ad-ce8a-4bf8-ae46-eb3ced8da93e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c31b4e3d-2843-49c5-b59f-0dc05c10640e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move_down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfdc6b99-6e85-4345-a466-847da6985468"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""pause_menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c7ea018-fa91-40ff-a075-2ff90d8355de"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9b42444-200d-413e-8c62-d8d2374f5cac"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""charge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // bullet_hell
        m_bullet_hell = asset.FindActionMap("bullet_hell", throwIfNotFound: true);
        m_bullet_hell_move_rigth = m_bullet_hell.FindAction("move_rigth", throwIfNotFound: true);
        m_bullet_hell_move_left = m_bullet_hell.FindAction("move_left", throwIfNotFound: true);
        m_bullet_hell_move_up = m_bullet_hell.FindAction("move_up", throwIfNotFound: true);
        m_bullet_hell_move_down = m_bullet_hell.FindAction("move_down", throwIfNotFound: true);
        m_bullet_hell_pause_menu = m_bullet_hell.FindAction("pause_menu", throwIfNotFound: true);
        m_bullet_hell_shoot = m_bullet_hell.FindAction("shoot", throwIfNotFound: true);
        m_bullet_hell_charge = m_bullet_hell.FindAction("charge", throwIfNotFound: true);
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

    // bullet_hell
    private readonly InputActionMap m_bullet_hell;
    private IBullet_hellActions m_Bullet_hellActionsCallbackInterface;
    private readonly InputAction m_bullet_hell_move_rigth;
    private readonly InputAction m_bullet_hell_move_left;
    private readonly InputAction m_bullet_hell_move_up;
    private readonly InputAction m_bullet_hell_move_down;
    private readonly InputAction m_bullet_hell_pause_menu;
    private readonly InputAction m_bullet_hell_shoot;
    private readonly InputAction m_bullet_hell_charge;
    public struct Bullet_hellActions
    {
        private @Controlls m_Wrapper;
        public Bullet_hellActions(@Controlls wrapper) { m_Wrapper = wrapper; }
        public InputAction @move_rigth => m_Wrapper.m_bullet_hell_move_rigth;
        public InputAction @move_left => m_Wrapper.m_bullet_hell_move_left;
        public InputAction @move_up => m_Wrapper.m_bullet_hell_move_up;
        public InputAction @move_down => m_Wrapper.m_bullet_hell_move_down;
        public InputAction @pause_menu => m_Wrapper.m_bullet_hell_pause_menu;
        public InputAction @shoot => m_Wrapper.m_bullet_hell_shoot;
        public InputAction @charge => m_Wrapper.m_bullet_hell_charge;
        public InputActionMap Get() { return m_Wrapper.m_bullet_hell; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Bullet_hellActions set) { return set.Get(); }
        public void SetCallbacks(IBullet_hellActions instance)
        {
            if (m_Wrapper.m_Bullet_hellActionsCallbackInterface != null)
            {
                @move_rigth.started -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_rigth;
                @move_rigth.performed -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_rigth;
                @move_rigth.canceled -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_rigth;
                @move_left.started -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_left;
                @move_left.performed -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_left;
                @move_left.canceled -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_left;
                @move_up.started -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_up;
                @move_up.performed -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_up;
                @move_up.canceled -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_up;
                @move_down.started -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_down;
                @move_down.performed -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_down;
                @move_down.canceled -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnMove_down;
                @pause_menu.started -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnPause_menu;
                @pause_menu.performed -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnPause_menu;
                @pause_menu.canceled -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnPause_menu;
                @shoot.started -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnShoot;
                @shoot.performed -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnShoot;
                @shoot.canceled -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnShoot;
                @charge.started -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnCharge;
                @charge.performed -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnCharge;
                @charge.canceled -= m_Wrapper.m_Bullet_hellActionsCallbackInterface.OnCharge;
            }
            m_Wrapper.m_Bullet_hellActionsCallbackInterface = instance;
            if (instance != null)
            {
                @move_rigth.started += instance.OnMove_rigth;
                @move_rigth.performed += instance.OnMove_rigth;
                @move_rigth.canceled += instance.OnMove_rigth;
                @move_left.started += instance.OnMove_left;
                @move_left.performed += instance.OnMove_left;
                @move_left.canceled += instance.OnMove_left;
                @move_up.started += instance.OnMove_up;
                @move_up.performed += instance.OnMove_up;
                @move_up.canceled += instance.OnMove_up;
                @move_down.started += instance.OnMove_down;
                @move_down.performed += instance.OnMove_down;
                @move_down.canceled += instance.OnMove_down;
                @pause_menu.started += instance.OnPause_menu;
                @pause_menu.performed += instance.OnPause_menu;
                @pause_menu.canceled += instance.OnPause_menu;
                @shoot.started += instance.OnShoot;
                @shoot.performed += instance.OnShoot;
                @shoot.canceled += instance.OnShoot;
                @charge.started += instance.OnCharge;
                @charge.performed += instance.OnCharge;
                @charge.canceled += instance.OnCharge;
            }
        }
    }
    public Bullet_hellActions @bullet_hell => new Bullet_hellActions(this);
    public interface IBullet_hellActions
    {
        void OnMove_rigth(InputAction.CallbackContext context);
        void OnMove_left(InputAction.CallbackContext context);
        void OnMove_up(InputAction.CallbackContext context);
        void OnMove_down(InputAction.CallbackContext context);
        void OnPause_menu(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnCharge(InputAction.CallbackContext context);
    }
}

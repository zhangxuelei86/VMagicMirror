﻿using UniRx;
using UnityEngine;
using Zenject;

namespace Baku.VMagicMirror
{
    [RequireComponent(typeof(ParticleStore))]
    public class ParticleControlReceiver : MonoBehaviour
    {
        private const int InvalidTypingEffectIndex = ParticleStore.InvalidTypingEffectIndex;

        [Inject] private ReceivedMessageHandler _handler = null;

        private ParticleStore _particleStore = null;

        private int _selectedIndex = -1;
        private bool _keyboardIsVisible = true;
        private bool _midiVisible = false;

        void Start()
        {
            _particleStore = GetComponent<ParticleStore>();

            _handler.Commands.Subscribe(message =>
            {
                switch (message.Command)
                {
                    case MessageCommandNames.SetKeyboardTypingEffectType:
                        SetParticleType(message.ToInt());
                        break;
                    case MessageCommandNames.HidVisibility:
                        SetKeyboardVisibility(message.ToBoolean());
                        break;
                    case MessageCommandNames.MidiControllerVisibility:
                        SetMidiVisibility(message.ToBoolean());
                        break;
                    default:
                        break;
                }
            });
        }

        private void SetParticleType(int typeIndex)
        {
            _selectedIndex = typeIndex;
            UpdateParticleIndex();
        }

        private void SetKeyboardVisibility(bool visible)
        {
            _keyboardIsVisible = visible;
            UpdateParticleIndex();
        }

        private void SetMidiVisibility(bool visible)
        {
            _midiVisible = visible;
            UpdateParticleIndex();
        }

        private void UpdateParticleIndex()
        {
            _particleStore.SetParticleIndex(
                _keyboardIsVisible ? _selectedIndex : InvalidTypingEffectIndex,
                _midiVisible ? _selectedIndex : InvalidTypingEffectIndex
                );
        }

    }
}

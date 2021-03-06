﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] private List<DemoAction> actions = new List<DemoAction>();
    [SerializeField] private List<Lairinus.UI.ActionSlotUI> actionSlots = new List<Lairinus.UI.ActionSlotUI>();
    [SerializeField] private List<Button> actionButtons = new List<Button>();

    private void Update()
    {
        UpdateActionInput();
        UpdateActionSlots();
    }

    private void Awake()
    {
        InitializeActionSlots();
        OnClick_StartActions();
    }

    private void InitializeActionSlots()
    {
        for (var a = 0; a < actionSlots.Count; a++)
        {
            if (a < actions.Count && actions[a] != null)
            {
                actionSlots[a].SetActionIcon(actions[a].icon);
                actionSlots[a].SetAction(actions[a]);
            }
        }
    }

    private void TryStartAction(Lairinus.UI.ActionSlotUI actionSlot, DemoAction action)
    {
        if (actionSlot != null)
        {
            if (!actionSlot.isEnabled)
                return;
        }

        if (action != null)
            action.UseAction();
    }

    private void UpdateActionInput()
    {
        for (var a = 0; a < actions.Count; a++)
        {
            if (actions[a] != null)
            {
                if (Input.GetKeyDown(actions[a].keycode))
                {
                    if (actionSlots[a].isEnabled)
                        actions[a].UseAction();
                    break;
                }
            }
        }
    }

    private void UpdateActionSlots()
    {
        for (var a = 0; a < actions.Count; a++)
        {
            DemoAction action = actions[a];
            if (a < actionSlots.Count)
            {
                if (actionSlots[a] != null)
                {
                    actionSlots[a].UpdateActionSlot(true);
                }
            }
        }
    }

    #region Click Events

    /// <summary>
    /// Inspector access - sets the duration of the Action
    /// </summary>
    /// <param name="durationInSeconds"></param>
    public void OnClick_SetDuration(int durationInSeconds)
    {
        foreach (DemoAction a in actions)
        {
            if (a != null)
            {
                a.ResetAction();
                a.SetDurationAndCooldown(durationInSeconds, a.totalCooldownTime);
            }
        }

        OnClick_StartActions();
    }

    /// <summary>
    /// Inspector access - sets the cooldown of the action
    /// </summary>
    /// <param name="cooldownInSeconds"></param>
    public void OnClick_SetCooldown(int cooldownInSeconds)
    {
        foreach (DemoAction a in actions)
        {
            if (a != null)
            {
                a.ResetAction();
                a.SetDurationAndCooldown(a.totalDurationTime, cooldownInSeconds);
            }
        }

        OnClick_StartActions();
    }

    /// <summary>
    /// Inspector access - resets an action's cooldown and duration
    /// </summary>
    public void OnClick_ResetActions()
    {
        foreach (DemoAction a in actions)
        {
            if (a != null)
            {
                a.ResetAction();
            }
        }

        foreach (Lairinus.UI.ActionSlotUI asu in actionSlots)
        {
            if (asu != null)
            {
                asu.UpdateActionSlot(true);
            }
        }
    }

    /// <summary>
    /// Inspector access - starts all actions
    /// </summary>
    public void OnClick_StartActions()
    {
        for (var a = 0; a < actions.Count; a++)
        {
            Lairinus.UI.ActionSlotUI actionSlot = actionSlots[a];
            DemoAction action = actions[a];
            TryStartAction(actionSlot, action);
        }
    }

    /// <summary>
    /// Inspector access - disables all actions
    /// </summary>
    public void OnClick_EnableAllActionSlots(bool enable)
    {
        foreach (Lairinus.UI.ActionSlotUI a in actionSlots)
        {
            if (a != null)
            {
                a.EnableActionSlot(enable);
            }
        }
    }

    #endregion Click Events
}
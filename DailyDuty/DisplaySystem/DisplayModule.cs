﻿using System;
using DailyDuty.ConfigurationSystem;
using DailyDuty.DisplaySystem.DisplayTabs;
using Dalamud.Interface;
using Dalamud.Interface.Components;
using ImGuiNET;

namespace DailyDuty.DisplaySystem
{
    internal abstract class DisplayModule : IDisposable
    {
        public string CategoryString = "CategoryString Not Set";

        protected abstract GenericSettings GenericSettings { get; }


        protected virtual void DrawContents()
        {

            ImGui.Checkbox($"Enabled##{CategoryString}", ref GenericSettings.Enabled);
            ImGui.Spacing();

            if (GenericSettings.Enabled)
            {
                ImGui.Indent(15 * ImGuiHelpers.GlobalScale);

                DisplayData();

                ImGui.Spacing();

                if (SettingsTab.EditModeEnabled)
                {
                    ImGui.Indent(15 * ImGuiHelpers.GlobalScale);

                    EditModeOptions();
                    ImGui.Spacing();

                    ImGui.Indent(-15 * ImGuiHelpers.GlobalScale);
                }

                NotificationOptions();
                ImGui.Spacing();

                DisplayOptions();

                ImGui.Indent(-15 * ImGuiHelpers.GlobalScale);
            }

            ImGui.Spacing();
        }

        protected abstract void DisplayData();
        protected abstract void DisplayOptions();
        protected abstract void EditModeOptions();
        protected abstract void NotificationOptions();

        public virtual void Draw()
        {
            if (ImGui.CollapsingHeader(CategoryString))
            {
                ImGui.Spacing();

                DrawContents();
            }
        }

        public abstract void Dispose();

        protected void OnLoginReminderCheckbox(GenericSettings settings)
        {
            ImGui.Checkbox($"Login Reminder##{CategoryString}", ref settings.LoginReminder);
            ImGuiComponents.HelpMarker("Display this module's status in chat on login if this module is incomplete.");
        }

        protected void OnTerritoryChangeCheckbox(GenericSettings settings)
        {
            ImGui.Checkbox($"Zone Change Reminder##{CategoryString}", ref settings.TerritoryChangeReminder);
            ImGuiComponents.HelpMarker("Display this module's status in chat on any non-duty instance change if this module is incomplete.");
        }

        protected void EditNumberField(string label, ref int refValue)
        {
            ImGui.Text(label + ":");

            ImGui.SameLine();

            ImGui.PushItemWidth(30 * ImGuiHelpers.GlobalScale);
            ImGui.InputInt($"##{label}{CategoryString}", ref refValue, 0, 0);
            ImGui.PopItemWidth();
        }

        protected void NotificationField(string label, ref bool refValue, string helpText = "")
        {
            ImGui.Checkbox($"{label}##{CategoryString}", ref refValue);

            if (helpText != string.Empty)
            {
                ImGuiComponents.HelpMarker(helpText);
            }
        }

        protected void NumericDisplay(string label, int value)
        {
            ImGui.Text(label + ":");
            ImGui.SameLine();
            ImGui.Text($"{value}");
        }

        // HH:MM:SS
        protected void TimeSpanDisplay(string label, TimeSpan span)
        {
            ImGui.Text(label + ":");
            ImGui.SameLine();
            
            if (span == TimeSpan.Zero)
            {
                ImGui.TextColored(new(0, 255, 0, 255), $" {span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}");
            }
            else
            {
                ImGui.Text($" {span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}");
            }
        }

        protected void DaysTimeSpanDisplay(string label, TimeSpan delta)
        {
            ImGui.Text(label + ":");
            ImGui.SameLine();

            var daysDisplay = delta.Days switch
            {
                1 => $"{delta.Days} day, ",
                > 1 => $"{delta.Days} days, ",
                _ => ""
            };

            if (delta == TimeSpan.Zero)
            {
                ImGui.TextColored(new(0, 255, 0, 255), $"{delta.Hours:00}:{delta.Minutes:00}:{delta.Seconds:00}");
            }
            else
            {
                ImGui.Text($"{daysDisplay}{delta.Hours:00}:{delta.Minutes:00}:{delta.Seconds:00}");
            }
        }

        protected void Text(string text)
        {
            ImGui.Text(text);
            ImGui.Spacing();
        }
    }
}

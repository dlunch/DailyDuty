﻿using System.Linq;
using DailyDuty.ConfigurationSystem;
using Dalamud.Interface;
using Dalamud.Interface.Components;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;

namespace DailyDuty.DisplaySystem.DisplayModules
{
    internal class CustomDeliveries : DisplayModule
    {
        private static Weekly.CustomDeliveriesSettings Settings => Service.Configuration.CharacterSettingsMap[Service.Configuration.CurrentCharacter].CustomDeliveriesSettings;
        protected override GenericSettings GenericSettings => Settings;

        public CustomDeliveries()
        {
            CategoryString = "Custom Delivery";
        }

        protected override void DisplayData()
        {
            NumericDisplay("Remaining Allowances", Settings.AllowancesRemaining);
        }

        protected override void DisplayOptions()
        {
        }

        protected override void EditModeOptions()
        {
            Text("Manually Set Allowances Remaining");

            EditNumberField("Allowances",ref Settings.AllowancesRemaining);
        }

        protected override void NotificationOptions()
        {
            OnLoginReminderCheckbox(Settings);
            OnTerritoryChangeCheckbox(Settings);
        }
        
        public override void Dispose()
        {
        }
    }
}

﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("Admin Key", "birthdates", "1.2.3")]
    [Description("Get admin from just a key!")]
    public class AdminKey : RustPlugin
    {

        private void Init()
        {
            LoadConfig();
            if (_config.keys == null)
            {
                PrintError("Keys aren't setup correctly, please correct them or reset the config!");
            }
            else if (_config.keys.Count < 1)
            {
                PrintWarning("There are no admin keys!");
            }
            cmd.AddChatCommand("adminkey", this, AdminKeyCommand);
        }

        private ConfigFile _config;

        void RemoveAdmin(BasePlayer player)
        {
            player.SetPlayerFlag(BasePlayer.PlayerFlags.ThirdPersonViewmode, false);
            player.SendConsoleCommand("global.noclip 0");
            player.SendConsoleCommand("global.god false");
            player.SetPlayerFlag(BasePlayer.PlayerFlags.IsAdmin, false);
            player.Connection.authLevel = 1;
            ServerUsers.Set(player.userID, ServerUsers.UserGroup.None, player.displayName, "Removed Admin");
            ServerUsers.Save();
        }

        void AddAdmin(BasePlayer player)
        {
            // Begin edits by Death
            player.SetPlayerFlag(BasePlayer.PlayerFlags.IsAdmin, true); // Forces player flag isAdmin to true without having to relog
            player.Connection.authLevel = 2; // Forces player's auth level without having to relog. 1 = admin 2 = owner
            ServerUsers.Set(player.userID, ServerUsers.UserGroup.Owner, player.displayName, "Owner from admin key"); //edit by birthdates to set group in cfg
            ServerUsers.Save(); // Direct way of saving users CFG without running a console command.
        }

        public void AdminKeyCommand(BasePlayer player, string command, string[] args)
        {

            if (args.Length < 1)
            {
                SendReply(player, lang.GetMessage("InvalidKey", this, player.UserIDString));
            }
            else
            {
                if (!_config.keys.Contains(args[0]))
                {
                    SendReply(player, lang.GetMessage("InvalidKey", this, player.UserIDString));
                }
                else
                {
                    if (player.IsAdmin)
                    {
                        RemoveAdmin(player);
                        SendReply(player, lang.GetMessage("AdminRemoved", this, player.UserIDString));
                        return;
                    }
                    AddAdmin(player);
                    SendReply(player, lang.GetMessage("AdminGiven", this, player.UserIDString));
                }

            }
        }

        protected override void LoadDefaultMessages()
        {
            lang.RegisterMessages(new Dictionary<string, string>
            {
                {"InvalidKey", "Invalid key!"},
                {"AdminGiven", "Success! You are now in the admin group!"},
                {"AlreadyAdmin", "You are already an admin"},
                {"AdminRemoved", "Success! You have been removed from the admin group!"}
            }, this);
        }

        public class ConfigFile
        {
            [JsonProperty(PropertyName = "Admin keys")]
            public List<string> keys;

            public static ConfigFile DefaultConfig()
            {
                return new ConfigFile()
                {
                    keys = new List<string>()
                    {
                        "abc123",
                        "123abc"
                    }
                };
            }

        }

        protected override void LoadConfig()
        {
            base.LoadConfig();

            _config = Config.ReadObject<ConfigFile>();
            if (_config == null)
            {
                LoadDefaultConfig();
            }
        }

        protected override void LoadDefaultConfig()
        {
            _config = ConfigFile.DefaultConfig();
            PrintWarning("Default configuration has been loaded.");
        }

        protected override void SaveConfig()
        {
            Config.WriteObject(_config);
        }

    }
}
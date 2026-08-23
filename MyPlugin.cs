using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cs2Plugin
{
    public class AutoMessageChatConfig : BasePluginConfig
    {
        [JsonPropertyName("Prefix")]
        public string Prefix { get; set; } = "{LightBlue}● {White}[{LightBlue}FG :: INFO{White}]{Default}";

        [JsonPropertyName("MessageIntervalSeconds")]
        public float MessageIntervalSeconds { get; set; } = 120.0f;

        [JsonPropertyName("Messages")]
        public List<string> Messages { get; set; } = new List<string>
        {
            "Zapraszamy do naszego sklepu pod komendą {Green}!sklep{Default}.",
            "Trwa {Red}rekrutacja na admina{Default}! Złóż podanie na naszym forum.",
            "Pamiętaj o zachowaniu {LightBlue}kultury{Default} na serwerze."
        };
    }

    public class AutoMessageChat : BasePlugin, IPluginConfig<AutoMessageChatConfig>
    {
        public override string ModuleName => "AutoMessageChat";
        public override string ModuleVersion => "1.0.1";
        public override string ModuleAuthor => "tomson";
        public override string ModuleDescription => "Broadcasts configurable messages to the server chat.";

        public AutoMessageChatConfig Config { get; set; } = new AutoMessageChatConfig();
        
        private int _currentMessageIndex = 0;

        public void OnConfigParsed(AutoMessageChatConfig config)
        {
            Config = config;
        }

        public override void Load(bool hotReload)
        {
            // Rejestracja powtarzającego się timera
            AddTimer(Config.MessageIntervalSeconds, BroadcastNextMessage, TimerFlags.REPEAT);
        }

        private void BroadcastNextMessage()
        {
            if (Config.Messages == null || Config.Messages.Count == 0) return;

            // Pobranie obecnej wiadomości
            string message = Config.Messages[_currentMessageIndex];
            
            // Zastosowanie kolorów w prefiksie i wiadomości
            string formattedPrefix = FormatColors(Config.Prefix);
            string formattedMessage = FormatColors(message);

            // Wysyłanie do wszystkich graczy 
            Server.PrintToChatAll($" {formattedPrefix} {formattedMessage}");

            // Inkrementacja i zapętlenie licznika
            _currentMessageIndex++;
            if (_currentMessageIndex >= Config.Messages.Count)
            {
                _currentMessageIndex = 0;
            }
        }

        // Funkcja zamienia tekstowe tagi (np. {Green}, {Red}) na właściwe znaki kodujące kolory w silniku CS2.
        // Lista poniżej zawiera wszystkie zdefiniowane w CounterStrike kolory dostępne do użytku.
        private string FormatColors(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            text = text.Replace("{Default}", ChatColors.Default.ToString());
            text = text.Replace("{White}", ChatColors.White.ToString());
            text = text.Replace("{DarkRed}", ChatColors.DarkRed.ToString());
            text = text.Replace("{Green}", ChatColors.Green.ToString());
            text = text.Replace("{LightYellow}", ChatColors.LightYellow.ToString());
            text = text.Replace("{LightBlue}", ChatColors.LightBlue.ToString());
            text = text.Replace("{Olive}", ChatColors.Olive.ToString());
            text = text.Replace("{Lime}", ChatColors.Lime.ToString());
            text = text.Replace("{Red}", ChatColors.Red.ToString());
            text = text.Replace("{LightPurple}", ChatColors.LightPurple.ToString());
            text = text.Replace("{Purple}", ChatColors.Purple.ToString());
            text = text.Replace("{Grey}", ChatColors.Grey.ToString());
            text = text.Replace("{Yellow}", ChatColors.Yellow.ToString());
            text = text.Replace("{Gold}", ChatColors.Gold.ToString());
            text = text.Replace("{Silver}", ChatColors.Silver.ToString());
            text = text.Replace("{Blue}", ChatColors.Blue.ToString());
            text = text.Replace("{DarkBlue}", ChatColors.DarkBlue.ToString());
            text = text.Replace("{BlueGrey}", ChatColors.BlueGrey.ToString());
            text = text.Replace("{Magenta}", ChatColors.Magenta.ToString());
            text = text.Replace("{LightRed}", ChatColors.LightRed.ToString());

            return text;
        }
    }
}

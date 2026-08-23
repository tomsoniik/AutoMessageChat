/*
 * ==============================================================================
 * Plugin Name: AutoMessageChat
 * Author: tomson
 * Version: 1.0.1
 * 
 * Opis:
 * Uniwersalny plugin do serwerów Counter-Strike 2 napisany w C# (CounterStrikeSharp).
 * 
 * Główne funkcje:
 * - Automatyczne wysyłanie cyklicznych wiadomości/reklam na czat serwera co X sekund.
 * - Konfigurowalny prefiks z obsługą wszystkich kolorów dostępnych w silniku CS2.
 * - Wysyłanie publicznej informacji, gdy gracz wchodzi lub wychodzi z serwera.
 * - Wysyłanie specjalnej, opóźnionej wiadomości powitalnej (tylko do nowo dołączonego gracza).
 * - Wygodna komenda (!testad / css_testad) do sprawdzania wyglądu wiadomości z configu.
 * 
 * Konfiguracja tworzy się automatycznie w pliku: 
 * addons/counterstrikesharp/configs/plugins/AutoMessageChat/AutoMessageChat.json
 * ==============================================================================
 */

using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Commands;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cs2Plugin
{
    public class AutoMessageChatConfig : BasePluginConfig
    {
        [JsonPropertyName("Prefix")]
        public string Prefix { get; set; } = "{LightBlue}● {White}[{LightBlue}FG :: INFO{White}]{Default}";

        [JsonPropertyName("WelcomeMessage")]
        public string WelcomeMessage { get; set; } = "{Green}Dołącz na Nasze Sociale wpisując {LightRed}!sociale{Default}";

        [JsonPropertyName("PlayerJoinMessage")]
        public string PlayerJoinMessage { get; set; } = "{LightBlue}{PLAYER} {White}właśnie dołączył na serwer!";

        [JsonPropertyName("PlayerDisconnectMessage")]
        public string PlayerDisconnectMessage { get; set; } = "{LightBlue}{PLAYER} {White}opuścił serwer.";

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

            // Komenda do szybkiego przetestowania działania na serwerze (użycie na czacie: !testad lub w konsoli css_testad)
            AddCommand("css_testad", "Test wiadomości z configu", (player, info) =>
            {
                BroadcastNextMessage();
            });

            // Wykrywanie wejścia gracza na serwer
            RegisterEventHandler<EventPlayerConnectFull>(OnPlayerConnectFull);

            // Wykrywanie wyjścia gracza z serwera
            RegisterEventHandler<EventPlayerDisconnect>(OnPlayerDisconnect);
        }

        private HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
        {
            var player = @event.Userid;

            // Sprawdzamy czy to prawdziwy gracz (nie bot ani HLTV)
            if (player == null || !player.IsValid || player.IsBot || player.IsHLTV)
                return HookResult.Continue;

            // Wysyłamy informację do wszystkich graczy, że ktoś dołączył
            if (!string.IsNullOrEmpty(Config.PlayerJoinMessage))
            {
                string formattedPrefix = FormatColors(Config.Prefix);
                string joinMsg = Config.PlayerJoinMessage.Replace("{PLAYER}", player.PlayerName);
                string formattedJoinMessage = FormatColors(joinMsg);
                Server.PrintToChatAll($" {formattedPrefix} {formattedJoinMessage}");
            }

            // Odczekujemy 5 sekund, żeby gracz zdążył załadować mapę i widział czat (wiadomość prywatna)
            AddTimer(5.0f, () => 
            {
                if (player != null && player.IsValid && !string.IsNullOrEmpty(Config.WelcomeMessage))
                {
                    string formattedPrefix = FormatColors(Config.Prefix);
                    string formattedMessage = FormatColors(Config.WelcomeMessage);
                    
                    player.PrintToChat($" {formattedPrefix} {formattedMessage}");
                }
            });

            return HookResult.Continue;
        }

        private HookResult OnPlayerDisconnect(EventPlayerDisconnect @event, GameEventInfo info)
        {
            var player = @event.Userid;

            if (player == null || !player.IsValid || player.IsBot || player.IsHLTV)
                return HookResult.Continue;

            if (!string.IsNullOrEmpty(Config.PlayerDisconnectMessage))
            {
                string formattedPrefix = FormatColors(Config.Prefix);
                // Używamy zmiennej @event.Name dostarczonej przez silnik (lub ewentualnie player.PlayerName)
                string playerName = @event.Name ?? player.PlayerName ?? "Nieznany Gracz";
                string leaveMsg = Config.PlayerDisconnectMessage.Replace("{PLAYER}", playerName);
                string formattedLeaveMessage = FormatColors(leaveMsg);
                
                Server.PrintToChatAll($" {formattedPrefix} {formattedLeaveMessage}");
            }

            return HookResult.Continue;
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

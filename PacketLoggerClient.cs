using System.Diagnostics;
using System.Text;

namespace AreaServerLevelEditor;

internal sealed record PlayerProfile(int SocketId, string Nickname, int Level, string ClassName,
    uint Experience, uint ExperienceRequired, uint Gold, string Model)
{
    public override string ToString() => $"{Nickname}  (Lv.{Level})";
}

internal sealed class PacketLoggerClient : IDisposable
{
    private readonly Action<string, int, string> onChat;
    private readonly Action<string> onStatus;
    private readonly Action<PlayerProfile> onPlayer;
    private readonly Action<int> onPlayerLeft;
    private Process? process;

    public PacketLoggerClient(Action<string, int, string> onChat, Action<string> onStatus,
        Action<PlayerProfile> onPlayer, Action<int> onPlayerLeft)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        this.onChat = onChat;
        this.onStatus = onStatus;
        this.onPlayer = onPlayer;
        this.onPlayerLeft = onPlayerLeft;
    }

    public Task StartAsync()
    {
        string? executable = FindLogger();
        if (executable is null)
        {
            onStatus("AreaServerDataEditor.exe was not found");
            return Task.CompletedTask;
        }

        try
        {
            process = new Process
            {
                StartInfo = new ProcessStartInfo(executable)
                {
                    WorkingDirectory = Path.GetDirectoryName(executable)!,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                },
                EnableRaisingEvents = true
            };
            process.OutputDataReceived += (_, e) => { if (e.Data is not null) ParseLine(e.Data); };
            process.ErrorDataReceived += (_, e) => { if (!string.IsNullOrWhiteSpace(e.Data)) onStatus(e.Data); };
            process.Exited += (_, _) => onStatus("Packet logger stopped");
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            onStatus("Packet logger ready");
        }
        catch (Exception ex)
        {
            onStatus("Logger could not be started: " + ex.Message);
        }
        return Task.CompletedTask;
    }

    private void ParseLine(string line)
    {
        if (line.StartsWith("PLAYER_LEFT\t", StringComparison.Ordinal) &&
            int.TryParse(line.AsSpan("PLAYER_LEFT\t".Length), out int leftSocket))
        {
            onPlayerLeft(leftSocket);
            return;
        }
        if (line.StartsWith("PLAYER_EVENT_HEX\t", StringComparison.Ordinal))
        {
            string[] player = line.Split('\t');
            if (player.Length != 9 || !int.TryParse(player[1], out int socketId) ||
                !int.TryParse(player[2], out int playerLevel)) return;
            string profileNickname = DecodeShiftJisHex(player[3]);
            _ = int.TryParse(player[4], out int classId);
            _ = uint.TryParse(player[5], out uint experience);
            _ = uint.TryParse(player[6], out uint experienceRequired);
            _ = uint.TryParse(player[7], out uint gold);
            string model = DecodeShiftJisHex(player[8]);
            string[] classes = { "Twin Blade", "Blademaster", "Heavy Blade", "Heavy Axe", "Long Arm", "Wavemaster" };
            string className = classId >= 0 && classId < classes.Length ? classes[classId] : "Unknown";
            onPlayer(new PlayerProfile(socketId, profileNickname, playerLevel, className,
                experience, experienceRequired, gold, model));
            return;
        }

        if (line.StartsWith("CHAT_EVENT_HEX\t", StringComparison.Ordinal))
        {
            string[] encodedFields = line.Split('\t', 5);
            if (encodedFields.Length != 5) return;
            int encodedLevel = int.TryParse(encodedFields[2], out int parsedLevel) ? parsedLevel : -1;
            string decodedNickname = DecodeShiftJisHex(encodedFields[3]);
            string decodedMessage = DecodeShiftJisHex(encodedFields[4]);
            if (string.IsNullOrWhiteSpace(decodedMessage)) return;
            if (string.IsNullOrEmpty(decodedNickname)) decodedNickname = $"Player #{encodedFields[1]}";
            onChat(decodedNickname, encodedLevel, decodedMessage);
            return;
        }

        if (!line.StartsWith("CHAT_EVENT\t", StringComparison.Ordinal)) return;
        string[] fields = line.Split('\t', 5);
        if (fields.Length != 5) return;
        int level = -1;
        _ = int.TryParse(fields[2], out level);
        string nickname = fields[3] == "?" ? $"Player #{fields[1]}" : fields[3];
        if (string.IsNullOrWhiteSpace(fields[4])) return;
        onChat(nickname, level, fields[4]);
    }

    private static string DecodeShiftJisHex(string hex)
    {
        if (hex.Length == 0 || (hex.Length & 1) != 0) return string.Empty;
        try
        {
            byte[] bytes = Convert.FromHexString(hex);
            return Encoding.GetEncoding(932).GetString(bytes).TrimEnd('\0');
        }
        catch
        {
            return string.Empty;
        }
    }

    private static string? FindLogger()
    {
        string[] candidates =
        {
            Path.Combine(AppContext.BaseDirectory, "AreaServerDataEditor.exe"),
            @"C:\Users\PC\source\repos\AreaServerDataEditor\Debug\AreaServerDataEditor.exe"
        };
        return candidates.FirstOrDefault(File.Exists);
    }

    public void Dispose()
    {
        if (process is null) return;
        try { if (!process.HasExited) process.Kill(true); } catch { }
        process.Dispose();
    }
}

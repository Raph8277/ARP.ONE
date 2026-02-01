using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ARP.ONE.BlazorApp.Services
{
    public sealed class ThemeRepository
    {
        private readonly string _dbPath;

        public ThemeRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task EnsureCreatedAsync()
        {
            var dir = Path.GetDirectoryName(_dbPath);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }

            await using var conn = new SqliteConnection($"Data Source={_dbPath}");
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText =
                """
                CREATE TABLE IF NOT EXISTS themes (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL UNIQUE,
                    json TEXT NOT NULL
                );
                """;

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<IReadOnlyList<string>> GetNamesAsync()
        {
            var results = new List<string>();
            await using var conn = new SqliteConnection($"Data Source={_dbPath}");
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT name FROM themes ORDER BY name;";

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                results.Add(reader.GetString(0));
            }

            return results;
        }

        public async Task<ThemeRecord?> GetByNameAsync(string name)
        {
            await using var conn = new SqliteConnection($"Data Source={_dbPath}");
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, json FROM themes WHERE name = $name LIMIT 1;";
            cmd.Parameters.AddWithValue("$name", name);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new ThemeRecord(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            }

            return null;
        }

        public async Task UpsertAsync(string name, string json)
        {
            await using var conn = new SqliteConnection($"Data Source={_dbPath}");
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText =
                """
                INSERT INTO themes (name, json)
                VALUES ($name, $json)
                ON CONFLICT(name) DO UPDATE SET json = excluded.json;
                """;
            cmd.Parameters.AddWithValue("$name", name);
            cmd.Parameters.AddWithValue("$json", json);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> DeleteAsync(string name)
        {
            await using var conn = new SqliteConnection($"Data Source={_dbPath}");
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM themes WHERE name = $name;";
            cmd.Parameters.AddWithValue("$name", name);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }
    }

    public sealed record ThemeRecord(int Id, string Name, string Json);
}

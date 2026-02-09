using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data.Seeders;

public static class RoomSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasData(
            new Room
            {
                Id = 1,
                RoomCode = "A3",
                RoomName = "Ruang Kuliah A 301",
                Capacity = 60,
                Location = "Gedung D4 Lantai 3",
                RoomStatus = "Available",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Room
            {
                Id = 2,
                RoomCode = "B2",
                RoomName = "Ruang Kuliah B 202",
                Capacity = 45,
                Location = "Gedung D4 Lantai 2",
                RoomStatus = "Available",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Room
            {
                Id = 3,
                RoomCode = "Aula-Mini",
                RoomName = "Mini Theater",
                Capacity = 100,
                Location = "Gedung Pascasarjana Lantai 6",
                RoomStatus = "Available",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Room
            {
                Id = 4,
                RoomCode = "LAB-C3",
                RoomName = "Lab Komputer Jaringan C 307",
                Capacity = 30,
                Location = "Gedung D4 Lantai 1",
                RoomStatus = "Available",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Room
            {
                Id = 5,
                RoomCode = "LAB-C2",
                RoomName = "Lab APD C 205",
                Capacity = 30,
                Location = "Gedung D4 Lantai 2",
                RoomStatus = "Maintenance",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Room
            {
                Id = 6,
                RoomCode = "Aula-Utama",
                RoomName = "AUDITORIUM",
                Capacity = 500,
                Location = "Gedung Pascasarjana Lantai 6",
                RoomStatus = "Available",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Room
            {
                Id = 7,
                RoomCode = "SAW",
                RoomName = "Ruang Kelas SAW-06.07",
                Capacity = 15,
                Location = "Gedung SAW Lantai 6",
                RoomStatus = "Available",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Room
            {
                Id = 8,
                RoomCode = "PS",
                RoomName = "Ruang Kelas PS-05.12",
                Capacity = 50,
                Location = "Gedung Pascasarjana Lantai 5",
                RoomStatus = "Available",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

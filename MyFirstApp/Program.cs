using Microsoft.Data.SqlClient;
using System;

string connectionString = @"Server=PC-NGHIEMTRAN1\SQLEXPRESS; Database=ProjectDB; Trusted_Connection=True; TrustServerCertificate=True";

// Mở kho dữ liệu
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connected Successfully!");
        while (true)
        {
            Console.WriteLine("\nThem tay vot moi? (Y/N): ");
            string? add = Console.ReadLine();
            if (add != null && add.ToUpper() == "Y")
            {
                PlayerAdd(connection);
            }
            else
            {
                Console.WriteLine("\nChot danh sach. Dang tai du lieu tu kho...");
                break;
            }
        }
        PlayerList(connection);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to connect! {ex.Message}");
    }
}



// 1. CREATE (Thêm tay vợt)
static void PlayerAdd(SqlConnection connection)
{
    Console.WriteLine("\n--- THEM TAY VOT MOI ---");
    Console.WriteLine("\nNhap ten tay vot: ");
    string newName = Console.ReadLine();

    Console.WriteLine("\nNhap ten hang vot: ");
    string newBrand = Console.ReadLine();

    Console.WriteLine("\nTong so tran thang: ");
    int newWins = int.Parse(Console.ReadLine());

    string insertQuery = @"INSERT INTO BadmintonPlayers (PlayerName, RacketBrand, MatchesWon) VALUES (@Name, @Brand, @Wins)";
    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
    {
        insertCommand.Parameters.AddWithValue("@Name", newName);
        insertCommand.Parameters.AddWithValue("@Brand", newBrand);
        insertCommand.Parameters.AddWithValue("@Wins", newWins);

        int rowAffected = insertCommand.ExecuteNonQuery();
        Console.WriteLine($"Da them thanh cong {rowAffected} tay vot vao trong danh sach!!!");
    }

}



// 2. READ (Danh sách tay vợt)
static void PlayerList(SqlConnection connection)
{
    string sqlQuery = "SELECT PlayerName, RacketBrand, MatchesWon FROM BadmintonPlayers";
    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            Console.WriteLine("\n--- DANH SACH TAY VOT ---");
            while (reader.Read())
            {
                string name = reader.GetString(0);
                string brand = reader.GetString(1);
                int wins = reader.GetInt32(2);

                Console.WriteLine($"Tay vot {name} dung hang {brand} da chien thang {wins} tran!!!");
            }
        }
    }
}
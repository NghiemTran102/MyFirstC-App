using Microsoft.Data.SqlClient;

string connectionString = @"Server=PC-NGHIEMTRAN1\SQLEXPRESS;Database=ProjectDB;Trusted_Connection=True;TrustServerCertificate=True;";

using (SqlConnection connection = new SqlConnection(connectionString)) {
    try
    {
        connection.Open();
        Console.WriteLine("Connected Successfully!");
        
        Console.WriteLine("\n--- THEM TAY VOT MOI ---");
        Console.WriteLine("\nTen tay vot: ");
        string newName = Console.ReadLine();
        Console.WriteLine("\nDung vot hang: ");
        string newBrand = Console.ReadLine();
        Console.WriteLine("\nSo tran thang: ");
        int newWins = int.Parse(Console.ReadLine());

        string insertQuery = "INSERT INTO BadmintonPlayers (PlayerName, RacketBrand, MatchesWon) VALUES (@Name, @Brand, @Wins)";
        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
        {
            insertCommand.Parameters.AddWithValue("@Name", newName);
            insertCommand.Parameters.AddWithValue("@Brand", newBrand);
            insertCommand.Parameters.AddWithValue("@Wins", newWins);

            int rowAffected = insertCommand.ExecuteNonQuery();
            Console.WriteLine($"Da them thanh cong {rowAffected} tay vot vao Database!");
        }

        string sqlQuery = "SELECT PlayerName, RacketBrand, MatchesWon FROM BadmintonPlayers";

        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("\n--- Danh Sach Tay Vot ---");
                while(reader.Read())
                {
                    string name = reader.GetString(0);
                    string brand = reader.GetString(1);
                    int wins = reader.GetInt32(2);

                    Console.WriteLine($"Tay vot {name} dung vot hang {brand} da chien thang {wins} lan!");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Failed to connect! " + ex.Message);
    }
}


//Console.WriteLine("\n--- THEM TAY VOT MOI ---");

//// 1. Nhập thông tin từ bàn phím Console
//Console.Write("Nhap ten tay vot: ");
//string newName = Console.ReadLine();

//Console.Write("Nhap hang vot (VD: Yonex, Victor...): ");
//string newBrand = Console.ReadLine();

//Console.Write("So tran thang: ");
//int newWins = int.Parse(Console.ReadLine()); // Chuyển chữ gõ từ bàn phím thành số nguyên

//// 2. Chuẩn bị câu lệnh SQL (Dùng @ThamSo để chống Hacker bơm mã độc SQL Injection)
//string insertQuery = "INSERT INTO BadmintonPlayers (PlayerName, RacketBrand, MatchesWon) VALUES (@Name, @Brand, @Wins)";

//using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
//{
//    // 3. Lắp giá trị thật vừa gõ ở trên vào các tham số @
//    insertCommand.Parameters.AddWithValue("@Name", newName);
//    insertCommand.Parameters.AddWithValue("@Brand", newBrand);
//    insertCommand.Parameters.AddWithValue("@Wins", newWins);

//    // 4. Ra lệnh thực thi việc cất vào kho!
//    int rowsAffected = insertCommand.ExecuteNonQuery();
//    Console.WriteLine($"\n=> Da them thanh cong {rowsAffected} tay vot vao Database!");
//}
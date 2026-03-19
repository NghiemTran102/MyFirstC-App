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
            Console.Write("\n--- DANH SACH CHUC NANG ---");
            Console.Write("\n1. Them tay vot moi vao danh sach");
            Console.Write("\n2. Cap nhat thong tin tay vot");
            Console.Write("\n3. Danh sach tay vot");
            Console.Write("\n4. Xoa tay vot");
            Console.Write("\n5. Thoat ung dung");
            Console.WriteLine("\nXin chao! Vui long chon chuc nang de tiep tuc (1/2/3/4/5): ");
            string input = Console.ReadLine();
            int option;
            if (int.TryParse(input, out option))
            {
                switch (option)
                {
                    case 1:
                        PlayerAdd(connection);
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
                                break;
                            }
                        }
                        break;

                    case 2:
                        PlayerUpdate(connection);
                        break;

                    case 3:
                        PlayerList(connection);
                        break;

                    case 4:
                        PlayerDelete(connection);
                        break;

                    case 5:
                        Console.WriteLine("Cam on va hen gap lai!");
                        return;


                }
            }
        }
        //while (true)
        //{
        //    Console.WriteLine("\nThem tay vot moi? (Y/N): ");
        //    string? add = Console.ReadLine();
        //    if (add != null && add.ToUpper() == "Y")
        //    {
        //        PlayerAdd(connection);
        //    }
        //    else
        //    {
        //        Console.WriteLine("\nChot danh sach. Dang tai du lieu tu kho...");
        //        break;
        //    }
        //}
        //PlayerList(connection);
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

// 3. UPDATE (Thay đổi thông tin của tay vợt)
static void PlayerUpdate(SqlConnection connection)
{
    Console.WriteLine("\n--- CAP NHAT SO TRAN THANG---");
    Console.WriteLine("\nNhap ten tay vot ban muon thay doi: ");
    string? targetName = Console.ReadLine();

    Console.WriteLine("\nNhap so tran thang moi: ");
    int updateWins = int.Parse(Console.ReadLine());

    string updateQuery = "UPDATE BadmintonPlayers SET MatchesWon = @Wins WHERE PlayerName = @Name";
    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
    {
        updateCommand.Parameters.AddWithValue("@Name", targetName);
        updateCommand.Parameters.AddWithValue("@Wins", updateWins);
        int rowAffected = updateCommand.ExecuteNonQuery();

        if(rowAffected > 0)
        {
            Console.WriteLine($"Thanh cong! Da cap nhat so tran thang cua {targetName} thanh {updateWins} tran!");
        }
        else
        {
            Console.WriteLine($"That bai! Khong tim thay tay vot nao ten {targetName} trong database!");
        }
    }
}

// 4. DELETE (Xóa thông tin tay vợt)
static void PlayerDelete(SqlConnection connection)
{
    Console.WriteLine("\n--- XOA TAY VOT KHOI DANH SACH ---");
    Console.WriteLine("Nhap ten tay vot ban muon xoa: ");
    string? targetName = Console.ReadLine();

    string deleteQuery = "DELETE FROM BadmintonPlayers WHERE PlayerName = @Name";
    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
    {
        deleteCommand.Parameters.AddWithValue("@Name", targetName);

        int rowAffected = deleteCommand.ExecuteNonQuery();

        if (rowAffected > 0)
        {
            Console.WriteLine($"\nThanh cong! Da xoa {targetName} khoi danh sach!");
        } 
        else
        {
            Console.WriteLine("Khong tim thay ten de xoa!");
        }
    }
}
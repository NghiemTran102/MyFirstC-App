using Microsoft.Data.SqlClient;

string connectionString = @"Server=PC-NGHIEMTRAN1\SQLEXPRESS;Database=ProjectDB;Trusted_Connection=True;TrustServerCertificate=True;";

using (SqlConnection connection = new SqlConnection(connectionString)) {
    try
    {
        connection.Open();
        Console.WriteLine("Connected Successfully!");
        string sqlQuery = "SELECT PlayerName, RacketBrand, MatchesWon FROM BadmintonPlayers WHERE MatchesWon > 15";

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


//// 1. Viết câu lệnh SQL (Y hệt như lúc bạn gõ bên SSMS)
//string sqlQuery = "SELECT PlayerName, RacketBrand, MatchesWon FROM BadmintonPlayers";

//// 2. Tạo "xe tải" mang câu lệnh này chạy trên "đường ống" connection
//using (SqlCommand command = new SqlCommand(sqlQuery, connection))
//{
//    // 3. Thực thi lệnh và nhờ "người bốc vác" đọc kết quả
//    using (SqlDataReader reader = command.ExecuteReader())
//    {
//        Console.WriteLine("\n--- DANH SACH TAY VOT ---");

//        // 4. Vòng lặp: Cứ mỗi khi còn dòng dữ liệu, thì đọc tiếp
//        while (reader.Read())
//        {
//            // Lấy dữ liệu theo thứ tự cột đã SELECT ở trên (đếm từ số 0)
//            string name = reader.GetString(0);  // Cột 0: PlayerName
//            string racket = reader.GetString(1); // Cột 1: RacketBrand
//            int wins = reader.GetInt32(2);       // Cột 2: MatchesWon (số nguyên)

//            // In ra màn hình bằng cú pháp chuỗi $ siêu xịn
//            Console.WriteLine($"- Tay vot {name} dung vot {racket}, da thang {wins} tran.");
//        }
//    }
//}

//string playerName = "NghiemTran";
//string racketBrand = "Lining";
//int matchesWon = 18;
//double powerScale = 9.5;
//bool isPro = true;

//Console.WriteLine($"Tay vot: {playerName} \n");
//Console.WriteLine($"So tran thang: {matchesWon} \n");
//Console.WriteLine($"Dung vot hang: {racketBrand} \n");
//Console.WriteLine($"Test Git lan 2 \n");
using System;
using System.Data;
using System.Data.SqlClient;


namespace PraktikumADOConsole
{
    internal class Program
    {
        // Connection String untuk menghubungkan ke database SQL Server
        static string connectionString = "Data Source=PREDATOR579;" +
                                         "Initial Catalog=ManajemenObat;Integrated Security=True";
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("=== Aplikasi Manajemen Obat ===");
                Console.WriteLine("1. Tambah Data Obat");
                Console.WriteLine("2. Tampilkan Data Obat");
                Console.WriteLine("3. Hapus Data Obat");
                Console.WriteLine("4. Tambah Data Suplier");
                Console.WriteLine("5. Tampilkan Data Suplier");
                Console.WriteLine("6. Hapus Data Suplier");
                Console.WriteLine("7. Tambah Data Transaksi");
                Console.WriteLine("8. Tampilkan Data Transaksi");
                Console.WriteLine("9. Hapus Data Transaksi");
                Console.WriteLine("10. Tambah Data Apoteker");
                Console.WriteLine("11. Tampilkan Data Apoteker");
                Console.WriteLine("12. Hapus Data Apoteker");
                Console.WriteLine("13. Keluar");
                Console.Write("Pilih menu (1-4): ");

                string pilihan = Console.ReadLine();
                switch (pilihan)
                {
                    case "1":
                        InsertData();
                        break;
                    case "2":
                        RefreshData();
                        break;
                    case "3":
                        DeleteData();
                        break;
                    case "13":
                        Console.WriteLine("Terima kasih telah menggunakan aplikasi ini.");
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid, coba lagi!");
                        break;
                }

                Console.WriteLine("\nTekan ENTER untuk kembali ke menu utama...");
                Console.ReadLine();
            }
        }

        static void InsertData()
        {
            Console.Write("Masukkan ID Obat: ");
            string id_obat = Console.ReadLine();

            Console.Write("Masukkan Nama Obat: ");
            string nama = Console.ReadLine();

            Console.Write("Masukkan Kategori Obat: ");
            string kategori = Console.ReadLine();

            Console.Write("Masukkan Tanggal Kadaluwarsa: ");
            string tgl_kadaluwarsa = Console.ReadLine();

            string query = "INSERT INTO obat (id_obat, nama, kategori, tgl_kadaluwarsa) " +
                           "VALUES (@id_obat, @nama, @kategori, @tgl_kadaluwarsa)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_obat", id_obat);
                cmd.Parameters.AddWithValue("@nama", nama);
                cmd.Parameters.AddWithValue("@kategori", kategori);
                cmd.Parameters.AddWithValue("@tgl_kadaluwarsa", tgl_kadaluwarsa);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Data berhasil ditambahkan.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Terjadi kesalahan: " + ex.Message);
                }
            }
        }

        static void RefreshData()
        {
            string query = "SELECT * FROM Obat";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine("Data Obat:");
                    Console.WriteLine("---------------------------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine($"id_obat: {reader["id_obat"]}");
                        Console.WriteLine($"nama: {reader["nama"]}");
                        Console.WriteLine($"kategori: {reader["kategori"]}");
                        Console.WriteLine($"tgl_kadaluwarsa: {reader["tgl_kadaluwarsa"]}");
                        Console.WriteLine("---------------------------------------------------------");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Terjadi kesalahan: " + ex.Message);
                }
            }
        }

        static void DeleteData()
        {
            Console.Write("Masukkan ID Obat mahasiswa yang akan dihapus: ");
            string id_obat = Console.ReadLine();

            string query = "DELETE FROM obat WHERE id_obat = @id_obat";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_obat", id_obat);

                try
                {
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        Console.WriteLine("Data berhasil dihapus.");
                    else
                        Console.WriteLine("Data tidak ditemukan.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Terjadi kesalahan: " + ex.Message);
                }
            }
        }
    }
}
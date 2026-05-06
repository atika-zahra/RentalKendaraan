List<Kendaraan> data_kendaraan = new List<Kendaraan>()
{
    new Kendaraan("Vario", 125000, "N 1234 A"),
    new Kendaraan("NMAX", 300000, "N 5678 B"),
    new Mobil("Avanza", 500000, "N 6789 C"),
    new Mobil("Toyota", 520000, "N 1234 D"),
    new MiniBus("Elf", 100000, "N 5678 E"),
    new MiniBus("HiAce", 445000, "N 6789 F")
};
//list itu dinamis, kalau array dia sudah di set dari awal

while (true)
{
    Console.WriteLine("--- Rental Kendaraan ---");
    Console.WriteLine("\n Daftar Kendaraan");

    foreach (var dk in data_kendaraan)
    {
        dk.tampilkanInfo();
    }

    Console.WriteLine("\nPilih menu: ");
    Console.WriteLine("1. Sewa\n2. Kembali\n3. Keluar");
    Console.WriteLine("Pilihan Anda: ");
    string pilihan = Console.ReadLine();

    if (pilihan == "1")
    {
        Console.WriteLine(" Input nama kendaraan: ");
        string nama_kendaraan = Console.ReadLine();
        var cariKendaraan = data_kendaraan.FirstOrDefault(ck => string.Equals(ck.NamaKendaraan, nama_kendaraan, StringComparison.OrdinalIgnoreCase));

        if (cariKendaraan == null)
        {
            Console.WriteLine("\nKendaraan tidak ditemukan");
        }
        else if (cariKendaraan.isAvailable)
        {
            Console.WriteLine("\nInput jumlah hari sewa: ");
            int hari = int.Parse(Console.ReadLine());

            double total_sewa = cariKendaraan.HitungTotal(hari);

            cariKendaraan.UbahStatus();

            Console.WriteLine($"Total pembayaran sewa: Rp {total_sewa}");
        }
    }

    else if (pilihan == "2")
    {
        Console.WriteLine("\n Input nama kendaraan: ");
        string nama_kendaraan = Console.ReadLine();
        var cariKendaraan = data_kendaraan.FirstOrDefault(ck => string.Equals(ck.NamaKendaraan, nama_kendaraan, StringComparison.OrdinalIgnoreCase));

        if (cariKendaraan == null)
        {
            Console.WriteLine("\nKendaraan tidak ditemukan");
        }
        else if (!cariKendaraan.isAvailable)
        {
            cariKendaraan.UbahStatus();

            Console.WriteLine("\nKendaraan berhasil dikembalikan!");
        }
        else
        {
            Console.WriteLine("\n Proses pengembalian tidak bisa dilakukan");
        }
    }
    else if (pilihan == "3")
    {
        Console.WriteLine("Tekan ENTER untuk menutup aplikasi...");
        Console.ReadLine();
        break;
    }
    else
    {
        Console.WriteLine("\nTidak valid");
    }
}
class Kendaraan
{
    //ini field
    protected string _namaKendaraan;
    protected double _hargaSewaPerHari;
    protected string _nomorPolisi;
    protected bool _isAvailable;
    //constructor itu set nilai awal dari field 

    //ini constructor, ini dipanggil saat membuat object (untuk set nilai awal)
    public Kendaraan(string namaKendaraan, double hargaSewaPerHari, string nomorPolisi)
    {
        _namaKendaraan = namaKendaraan;
        _hargaSewaPerHari = hargaSewaPerHari;
        _nomorPolisi = nomorPolisi;
        _isAvailable = true;
    }
    // ini enkapsulasi (property)
    public string NamaKendaraan
    {
        get { return _namaKendaraan; }
        set { _namaKendaraan = value;}
    }

    public double HargaSewaPerHari
    {
        get { return _hargaSewaPerHari;}
        set
        {
            if (value > 0)
            {
                _hargaSewaPerHari = value;
            }

            else
            {
                Console.WriteLine("Harga sewa harus lebih dari nol");
            }
        }
    }

    public string NomorPolisi
    {
        get { return _nomorPolisi; }
    }

    public bool isAvailable
    {
        get { return _isAvailable; }
    }

    //method
    public void tampilkanInfo()
    {
        Console.WriteLine($"Nama Kendaraan: {_namaKendaraan}");
        Console.WriteLine($"Harga Sewa Per Hari: {_hargaSewaPerHari}");
        Console.WriteLine($"Nomor Polisi: {_nomorPolisi}");
        Console.WriteLine($"Ketersediaan: {(_isAvailable ? "Tersedia" : "Tidak Tersedia" )}");
        
    }

    public void UbahStatus()
    {
        _isAvailable = !_isAvailable;
    }

    public virtual double HitungTotal(int hari)
    {
        return _hargaSewaPerHari * hari;
    }
}

class Mobil : Kendaraan
{
    private double _biayaAsuransi;
    public Mobil (string namaKendaraan, double hargaSewaPerHari, string nomorPolisi) : base(namaKendaraan, hargaSewaPerHari, nomorPolisi)
    {
        _biayaAsuransi = 50000;
    }

    public override double HitungTotal(int hari)
    {
       return base.HitungTotal(hari) + _biayaAsuransi;
    }
}

class MiniBus : Kendaraan
{
    private double _biayaSopir;
    public MiniBus(string namaKendaraan, double hargaSewaPerHari, string nomorPolisi) : base(namaKendaraan, hargaSewaPerHari, nomorPolisi)
    {
        _biayaSopir = 100000;
    }

    public override double HitungTotal(int hari)
    {
        _biayaSopir = _biayaSopir * hari;
        return base.HitungTotal(hari) + _biayaSopir;
    }
}


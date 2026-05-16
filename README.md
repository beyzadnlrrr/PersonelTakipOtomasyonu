#  Teknoloji Odaklı Personel Takip Sistemi

Bu proje, C# Windows Forms ve MS SQL Server kullanılarak geliştirilmiş, modern bir işletmenin personel ve teknoloji departman yönetimini dijitalleştiren bir otomasyon sistemidir (Vize Yazılım Projesi).

##  Sistem Özellikleri (CRUD Mimarisi)
- **Personel Listeleme:** Veritabanındaki tüm personelleri anlık olarak DataGridView üzerinde listeler.
- **Teknoloji Odaklı Ekleme:** Personelleri Ad, Soyad, TC No, Maaş ve SQL'den dinamik çekilen Teknoloji Departmanları (Siber Güvenlik, Yazılım Geliştirme vb.) ile kaydeder.
- **Gelişmiş Güncelleme:** Kullanıcı deneyimini (UX) artırmak adına ID kutusu kaldırılmış; tablodan seçilen personelin bilgileri arka planda otomatik yakalanarak güncellenmektedir.
- **Güvenli Silme:** Seçilen kayıtlar kullanıcıdan onay alınarak (DialogResult) güvenli bir şekilde silinir.

##  Teknik Gereksinimler
- **Geliştirme Ortamı:** Visual Studio 2022
- **Yazılım Dili:** C# (.NET Framework)
- **Veritabanı:** MS SQL Server (LocalDB)
- **Veri Bağlantısı:** ADO.NET (System.Data.SqlClient)

## 📸 Ekran Görüntüsü ve Kullanım
*Sistemin arayüz tasarımı ve çalışan hali aşağıda yer almaktadır:*

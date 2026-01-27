Customer Hub 

1. Hedefler

Proje tamamlandığında şunlar hedeflenmektedir:

C& .NET=> Syntax, Dependency Injection, Configuration ve Logging
ASP.NET Core MVC=> Model, View, Controller, Validation ve Routing
Entity Framework Core=> Entity Framework Code-first yaklaşımı, Migration'lar, SQLite
Architecture=> Katmanlı mimari, Separation of Concerns
DDD Temelleri=> Entity, Value Object, Aggregate ve Domain kuralları

 

2. Proje Kapsamı

#Kapsam İçinde

 Kavram  Açıklama 
      
 Customer  Temel Entity - Bireysel veya Kurumsal tipte 
 Customer Status  Müşteri durumları: Active, Suspended, Closed 
 Customer Contact  İletişim bilgisi 
 Customer Address  Lokasyon bilgisi 

#Kapsam DIŞINDA

- Authentication/Authorization (Kimlik doğrulama/Yetkilendirme)
- Ürünler, abonelikler, faturalandırma gibi ileri seviye konular
- Dış entegrasyonlar

 

3. Gereksinimler

#3.1 Fonksiyonel Gereksinimler

##Müşteri Yönetimi

 ID  Gereksinim  Öncelik 
  
 *  Müşteri oluşturma (Bireysel/Kurumsal)  Olmazsa Olmaz 
 *  Müşteri detaylarını görüntüleme  Olmazsa Olmaz 
 *  Müşterileri listeleme  Olmazsa Olmaz 
 *  İsme göre müşteri arama  Olmazsa Olmaz 
 *  Müşteri düzenleme (Edit)  Olmazsa Olmaz 
 *  Müşteri kapatma (Soft delete)  Olmazsa Olmaz 
 *  Müşteriyi askıya alma/aktifleştirme (Suspend/Activate)  Olsa İyi Olur 
 *  Statü/tipe göre filtreleme  Olsa Güzel Olur 

##Müşteri Alanları

 Alan  Zorunlu mu?  Notlar 
  -
 CustomerType  Evet  Individual veya Business 
 TCKN/VKN  Evet  Benzersiz olmalı 
 Name/CompanyName  Evet  Tipe göre değişir 
 Email  Koşullu  Email veya Phone'dan en az biri olmalı 
 Phone  Koşullu  Email veya Phone'dan en az biri olmalı 
 Status  Evet  Varsayılan: Active 

##Business Rules (İş Kuralları)

Uygulamanın zorunlu kılması gereken kurallar şunlardır:

 Kural  Açıklama 
      
 *  TCKN/VKN benzersiz olmalıdır.  
 *  TCKN/VKN sadece rakam olmalı
 *  İsim zorunludur. 
 *  Email veya Phone alanlarından en az biri girilmelidir. 
 *  Kapanmış müşteriler düzenlenemez. 
 *  Statü geçişleri: Active ↔ Suspended → Closed 

##İletişim Yönetimi

 ID  Gereksinim  Öncelik 
  
 *  Müşteriye contact ekleme  Olsa İyi Olur 
 *  Contact düzenleme/silme  Olsa İyi Olur 
 *  Müşteri başına birden fazla contact  Olsa Güzel Olur 

##Adres

 ID  Gereksinim  Öncelik 
  
 *  Müşteri adresi ekleme/güncelleme  Olsa Güzel Olur 

#3.2 Non-Functional Gereksinimler

 Kategori  Gereksinim 
 
 Architecture  Temiz katman ayrımı, Controller'larda domain logic olmamalı 
 Persistence  EF Core ile SQLite, code-first migration'lar 
 Validation  Kullanıcı dostu hata mesajlarıyla Server-side validation , fluent validation kullanımı
 Logging  Kritik işlemler için built-in ILogger kullanımı 

 

4. MVP (Minimum Viable Product)

Projenin başarılı sayılması için tamamlaman gereken asgari kapsam budur.

#MVP Özellikleri

 Özellik  Kabul Kriterleri (Acceptance Criteria) 
   
 Müşteri CRUD  Müşteri oluşturma, görüntüleme, listeleme, düzenleme, kapatma 
 Arama  Müşteri listesini isme göre filtreleme 
 Unique ID  Mükerrer TCKN/VKN girildiğinde hata gösterilmeli 
 Validation  Zorunlu alanlarda hata mesajları gösterilmeli 
 Statü Değişimi  Suspend, Activate, Close butonları çalışmalı 
 Düzenleme Kısıtlaması  Silinmiş statüsündeki müşteriler editlenememeli 

#Yapılacak Ekranlar

 Ekran  Ana Öğeler 
 
 Müşteri Listesi  Arama kutusu, sonuç tablosu (İsim, Tip, Statü, ID), aksiyon linkleri 
 Müşteri Oluştur  Tip seçici, dinamik form, validation hataları 
 Müşteri Detayları  Özet, statü badge'i, aksiyon butonları, contact bölümü 
 Müşteri Düzenle  Önceden doldurulmuş form, validation, kapalıysa engelleme 


6. Veri Modeli

#Entity'ler

##Customers

 Alan  Tip  Notlar 
 
 Id  Guid  PK (Primary Key) 
 Type  Enum  Individual, Business 
 TCKNOrVKN  String  Unique 
 Name  String  Required 
 Status  Enum  Active, Suspended, Closed 
 AddressCity  String  Nullable 
 AddressLine  String  Nullable 
 CreatedAt  DateTime  Auto 
 UpdatedAt  DateTime  Auto 

##CustomerContacts

 Alan  Tip  Notlar 
 
 Id  Guid  PK 
 CustomerId  Guid  FK (Foreign Key) 
 Name  String  Required 
 Email  String  Nullable 
 Phone  String  Nullable 
 Role  Enum  Primary, Billing, Technical, Other 

 

7. Definition of Done (Bitti Tanımı)

#Bir Özellik Şu Durumda Tamamlanmıştır:

- Projenin hata almadan çalışması
- Kodun doğru katmanda yer alması
- Domain kuralları uygulanması


8. Kullanılacak Teknolojiler

 Framework  .NET 8/10 
 Web  ASP.NET Core MVC 
 ORM  Entity Framework Core 
 Database  SQLite 
 Testing (Opsiyonel)  xUnit 

 

9. Faydalı Kaynaklar

Microsoft CDocumentation https://learn.microsoft.com/en-us/dotnet/csharp 
ASP.NET Core MVC Tutorial https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app 
EF Core Getting Started https://learn.microsoft.com/en-us/ef/core/get-started 
DDD Fundamentals https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns 
Fluent validation https://docs.fluentvalidation.net/en/latest/

